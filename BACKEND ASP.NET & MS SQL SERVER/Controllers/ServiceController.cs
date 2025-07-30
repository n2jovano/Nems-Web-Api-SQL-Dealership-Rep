//using Microsoft.AspNetCore.Components;
using AutoMapper;
using DealershipApp.Dto;
using DealershipApp.Interfaces;
using DealershipApp.Models;
using DealershipApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;

namespace DealershipApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IVehicleRepository _vehicleRepository; //foreign key addition
        private readonly IMapper _mapper;

        public ServiceController(IServiceRepository serviceRepository, IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Service>))]
        public IActionResult GetVehicles()
        {
            //var service = _serviceRepository.GetAllServices();
            var service = _mapper.Map<List<ServiceDto>>(_serviceRepository.GetAllServices());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(service);
        }



        [HttpGet("{serviceId}/GetDescriptionById")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetAllVehiclesByColour(int serviceId)
        {
            var service = _serviceRepository.GetDescriptionById(serviceId);
            //var vehicle = _mapper.Map<List<VehicleDto>>(_vehicleRepository.GetAllVehiclesByColour(colour));  //imapper implemented


            if (service.IsNullOrEmpty())  //if null or empty, old method
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(service);
        }



        [HttpGet("{serviceId}/GetRepairDateById")]
        [ProducesResponseType(200, Type = typeof(DateTime))]
        [ProducesResponseType(400)]
        public IActionResult GetVehicleOrderDate(int serviceId)
        {
            if (!_serviceRepository.ServiceExists(serviceId))
            {
                return NotFound();
            }

            var service = _serviceRepository.RepairDateById(serviceId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(service);
        }



        //create
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateService([FromQuery] int vehicleId, [FromBody] ServiceDto serviceCreate)  //query strings, aka how data is inputted ie /&stringid=2 in url
        {
            if (serviceCreate == null)
            {
                return BadRequest(ModelState);
            }

            //no identical ids
            //var serviceData = _serviceRepository.GetAllServices().Where(v => v.Id == serviceCreate.Id);

            //if (serviceData != null)
            //{
            //    ModelState.AddModelError("", "Service already exists!");
            //    return StatusCode(422, ModelState);
            //}

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceMap = _mapper.Map<Service>(serviceCreate);

            //line below required as fk is mandatory, dont forget to add customer repository above, also argument above fromquery
            serviceMap.Vehicle = _vehicleRepository.GetVehicle(vehicleId);

            if (!_serviceRepository.CreateService(serviceMap))
            {
                ModelState.AddModelError("", "theres an issue with something when trying to save");
                return StatusCode(500, ModelState);
            }

            return Ok("created");
        }
    }
}
