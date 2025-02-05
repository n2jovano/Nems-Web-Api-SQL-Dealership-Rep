using AutoMapper;
using DealershipApp.Dto;
using DealershipApp.Interfaces;
using DealershipApp.Models;
using DealershipApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace DealershipApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleDealershipController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDealershipRepository _dealershipRepository;
        //private readonly IMapper _mapper;

        public VehicleDealershipController(IVehicleRepository vehicleRepository, IDealershipRepository dealershipRepository)
        {
            _vehicleRepository = vehicleRepository;
            _dealershipRepository = dealershipRepository;
            //_mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vehicle>))]
        public IActionResult GetAllCountries()
        {
            //var vehicleAll = _vehicleRepository.GetVehicles();
            //var dealerAll = _dealershipRepository.GetDealerships();
            //var country = _mapper.Map<List<CountryDto>>(_countryRepository.GetAllCountries());

            //var myvar = from s in _vehicleRepository.GetVehicles()
            //            join d in _dealershipRepository.GetDealerships()
            //            on s.Id equals d.Id
            //            select s;

            //var entryPoint = _dealershipRepository.GetDealerships().Join(
            //    _vehicleRepository.GetVehicles(), 
            //    d => d.Id, v => v.Id, (d, v) => new
            //    {
            //        UniqueId = v.Id,
            //        Tid = d.Id,
            //        name = v.Name,
            //        colour = v.Colour,
            //        dealership = d.Name
            //    }).ToList();

            //inner join (shows only 3 cars for 3 available dealerships)
            var vehicleDealership_table = _vehicleRepository.GetVehicles().Join(
                _dealershipRepository.GetDealerships(),
                v => v.Id, d => d.Id, (v, d) => new
                {
                    UniqueId = v.StoreId,
                    //Tid = d.Id,
                    name = v.Name,
                    colour = v.Colour,
                    dealership = d.Name
                }).ToList();

            //outer join shows all vehicles with respect to the dealership
            var vehicleDealership_table2 = from v in _vehicleRepository.GetVehicles()
                              join d in _dealershipRepository.GetDealerships()
                              on v.StoreId equals d.Id into v_d
                              from d in v_d.DefaultIfEmpty()
                                  //where d.Name == null
                                  //select v;
                              select new { 
                                  vehicle = v.Name, 
                                  colour = v.Colour,
                                  dealership = d.Name };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(vehicleDealership_table2);
        }
    }
}


