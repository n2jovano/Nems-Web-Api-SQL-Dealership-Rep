//using Microsoft.AspNetCore.Components;  //remove to remove route error
using AutoMapper;
using DealershipApp.Dto;
using DealershipApp.Interfaces;
using DealershipApp.Models;
using DealershipApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DealershipApp.Controllers  //(c) api controllers and (d) & (e) in dto & program!  (f) in mappingProfiles!
{
    [Route("api/[controller]")]  //attributes, allows class to be a controller
    [ApiController]
    public class DealershipController : Controller
    {
        private readonly IDealershipRepository _dealershipRepository;
        private readonly ICountryRepository _countryRepository;  //foreign key addition
        //(h.1)declare imapper, maps valid fields & removes null fields
        private readonly IMapper _mapper;

        //(h.2) add imapper  31:30 stop
        public DealershipController(IDealershipRepository dealershipRepository, ICountryRepository countryRepository, IMapper mapper)  //2nd argument created for imapper
        {
            _dealershipRepository = dealershipRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dealership>))]
        public IActionResult GetDealerships()             //returning a list of all items
        {
            //var dealerships = _dealershipRepository.GetDealerships();   //original
            var dealerships = _mapper.Map<List<DealershipDto>>(_dealershipRepository.GetDealerships());  //imapper implemented

            //verifies if values are able to successfully bind the model & whether any rules were broken
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(dealerships);
        }



        [HttpGet("{dealerId}")]
        [ProducesResponseType(200, Type = typeof(Dealership))]
        [ProducesResponseType(400)]
        public IActionResult GetDealership(int dealerId)           //returning value based on id argument
        {
            if (!_dealershipRepository.DealershipExists(dealerId))
            {
                return NotFound();
            }

            //var dealership = _dealershipRepository.GetDealership(dealerId);  //original
            var dealership = _mapper.Map<DealershipDto>(_dealershipRepository.GetDealership(dealerId));  //imapper implemented

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dealership);
        }



        [HttpGet("{dealerId}/vehiclesByDealerId")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dealership>))]
        [ProducesResponseType(400)]
        public IActionResult GetVehiclesByDealership(int dealerId)           //returning value based on id argument
        {
            if (!_dealershipRepository.DealershipExists(dealerId))
            {
                return NotFound();
            }

            //var dealership = _dealershipRepository.GetDealership(dealerId);  //original
            var dealership = _mapper.Map<List<VehicleDto>>(_dealershipRepository.GetVehiclesByDealership(dealerId));  //imapper implemented

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dealership);
        }



        //create
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDealership([FromQuery] int countryId, [FromBody] DealershipDto dealershipCreate)  //query strings, aka how data is inputted ie /&stringid=2 in url
        {
            if (dealershipCreate == null)
            {
                return BadRequest(ModelState);
            }

            var dealershipData = _dealershipRepository.GetDealerships().Where(v => v.Name.Trim().ToUpper() == dealershipCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (dealershipData != null)
            {
                ModelState.AddModelError("", "Dealership already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dealershipMap = _mapper.Map<Dealership>(dealershipCreate);

            //line below required as fk is mandatory, dont forget to add customer repository above, also argument above fromquery
            dealershipMap.Country = _countryRepository.GetCountry(countryId);

            if (!_dealershipRepository.CreateDealership(dealershipMap))
            {
                ModelState.AddModelError("", "theres an issue with something when trying to save");
                return StatusCode(500, ModelState);
            }

            return Ok("created");
        }



        //create#2
        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreateDealership_2([FromBody] DealershipDto dealershipCreate, [FromQuery] int vehicleId, [FromQuery] int countryId)  //query strings, aka how data is inputted ie /&stringid=2 in url
        //{
        //    if (dealershipCreate == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var dealershipData = _dealershipRepository.GetDealerships().Where(v => v.Name.Trim().ToUpper() == dealershipCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

        //    if (dealershipData != null)
        //    {
        //        ModelState.AddModelError("", "Dealership already exists!");
        //        return StatusCode(422, ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    //new
        //    var dealershipMap = _mapper.Map<Dealership>(dealershipCreate);

        //    //line below required as fk is mandatory, dont forget to add customer repository above, also argument above fromquery
        //    dealershipMap.Country = _countryRepository.GetCountry(countryId);

        //    if (!_dealershipRepository.CreateDealership_2(dealershipMap, vehicleId))
        //    {
        //        ModelState.AddModelError("", "theres an issue with something when trying to save");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok("created");
        //}

    }
}


//vehicle rating at 19:57