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
    }
}
