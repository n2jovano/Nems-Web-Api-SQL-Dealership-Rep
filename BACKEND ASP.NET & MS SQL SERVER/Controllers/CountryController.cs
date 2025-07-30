using AutoMapper;
using DealershipApp.Dto;
using DealershipApp.Interfaces;
using DealershipApp.Models;
using DealershipApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DealershipApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))] 
        public IActionResult GetAllCountries()
        {
            //var country = _countryRepository.GetAllCountries();
            var country = _mapper.Map<List<CountryDto>>(_countryRepository.GetAllCountries());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(country);
        }



        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryById(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }
            var country = _countryRepository.GetCountryById(countryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(country);
        }


        [HttpGet("{countryName}/getCountryByName")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GetCountryByName(string countryName)
        {
            //var country = _countryRepository.GetCountryByName(countryName);
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByName(countryName));

            if (country == null) //if null
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(country);
        }


        //https://localhost:7040/api/Country
        ////create
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)  //query strings, aka how data is inputted ie /&stringid=2 in url
        {
            if (countryCreate == null)
            {
                return BadRequest(ModelState);
            }


            //checks if it exists
            var customerData = _countryRepository.GetAllCountries().Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (customerData != null)
            {
                ModelState.AddModelError("", "Country already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(countryCreate);

            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "theres an issue with something when trying to save");
                return StatusCode(500, ModelState);
            }

            return Ok("created");
        }


        //need to add id in both fields
        [HttpPut("{countryId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto countryUpdated)
        {
            if (countryUpdated == null)
            {
                return BadRequest(ModelState);
            }

            if (countryId != countryUpdated.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var countryMapper = _mapper.Map<Country>(countryUpdated);

            if (!_countryRepository.UpdateCountry(countryMapper))
            {
                ModelState.AddModelError("", "something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        ////v2.0 updated from above without countryid parameter which doesnt do anything
        //[HttpPut]
        ////[HttpPut("{countryId}/put")]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult UpdateCountry([FromBody]CountryDto countryUpdated)
        //{
        //    if (countryUpdated == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    //if (countryId != countryUpdated.Id)
        //    //{
        //    //    return BadRequest(ModelState);
        //    //}

        //    //updated
        //    if (!_countryRepository.CountryExists(countryUpdated.Id))
        //    {
        //        return NotFound();
        //    }


        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();   
        //    }

        //    var countryMapper = _mapper.Map<Country>(countryUpdated);

        //    if(!_countryRepository.UpdateCountry(countryMapper))
        //    {
        //        ModelState.AddModelError("", "there was an issue");
        //        return StatusCode(500, ModelState);
        //    }
        //    //return NoContent();
        //    return Ok();
        //}

        [HttpDelete("{countryId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            var countryToDelete = _countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_countryRepository.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "issue while attempting to delete custome");
            }
            return NoContent();
        }
    }
}
