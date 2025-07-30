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
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICustomerRepository _customerRepository; //foreign key addition
        private readonly IMapper _mapper;


        public VehicleController(IVehicleRepository vehicleRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vehicle>))]
        public IActionResult GetVehicles()
        {
            //var vehicles = _vehicleRepository.GetVehicles();
            var vehicles = _mapper.Map<List<VehicleDto>>(_vehicleRepository.GetVehicles());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(vehicles);
        }



        [HttpGet("{vehicleId}")]
        [ProducesResponseType(200, Type = typeof(Vehicle))]
        [ProducesResponseType(400)]
        public IActionResult GetVehicle(int vehicleId)
        {
            if (!_vehicleRepository.VehicleExists(vehicleId))
            {
                return NotFound();
            }

            //var vehicle = _vehicleRepository.GetVehicle(vehicleId);  //original
            var vehicle = _mapper.Map<VehicleDto>(_vehicleRepository.GetVehicle(vehicleId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(vehicle);
        }



        [HttpGet("/avgYear")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetVehicleAvgYear()
        {
            //if (!_vehicleRepository.VehicleExists(vehicleId))
            //{
            //    return NotFound();
            //}

            var vehicle = _vehicleRepository.GetVehicleAvgYear();  //original, imapper not needed


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(vehicle);
        }



        [HttpGet("{vehicleId}/getColourById")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetVehicles(int vehicleId)
        {
            if (!_vehicleRepository.VehicleExists(vehicleId))
            {
                return NotFound();
            }

            var vehicle = _vehicleRepository.GetVehicleColourById(vehicleId);  //imapper not needed


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(vehicle);
        }



        [HttpGet("{colour}/getAllVehiclesByColour")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetAllVehiclesByColour(string colour)
        {
            //var vehicle = _vehicleRepository.GetAllVehiclesByColour(colour);
            var vehicle = _mapper.Map<List<VehicleDto>>(_vehicleRepository.GetAllVehiclesByColour(colour));  //imapper implemented


            if (vehicle.IsNullOrEmpty())  //if null or empty, old method
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(vehicle);
        }


        [HttpGet("{vehicleId}/isAutomatic")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public IActionResult GetVehicleIsAutomatic(int vehicleId)
        {
            if (!_vehicleRepository.VehicleExists(vehicleId))
            {
                return NotFound();
            }

            var vehicle = _vehicleRepository.GetVehicleIsAutomatic(vehicleId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(vehicle);
        }



        [HttpGet("{vehicleId}/getYear")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetVehicleYear(int vehicleId)
        {
            if (!_vehicleRepository.VehicleExists(vehicleId))
            {
                return NotFound();
            }

            var vehicle = _vehicleRepository.GetVehicleYear(vehicleId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(vehicle);
        }



        [HttpGet("{vehicleId}/getOrderDateById")]
        [ProducesResponseType(200, Type = typeof(DateTime))]
        [ProducesResponseType(400)]
        public IActionResult GetVehicleOrderDate(int vehicleId)
        {
            if (!_vehicleRepository.VehicleExists(vehicleId))
            {
                return NotFound();
            }

            var vehicle = _vehicleRepository.GetVehicleOrderDate(vehicleId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(vehicle);
        }



        //create  temp removed
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateVehicle([FromQuery] int customerId, [FromBody] VehicleDto vehicleCreate)  //query strings, aka how data is inputted ie /&stringid=2 in url
        {
            if (vehicleCreate == null)
            {
                return BadRequest(ModelState);
            }

            var vehicledata = _vehicleRepository.GetVehicles().Where(v => v.Name.Trim().ToUpper() == vehicleCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (vehicledata != null)
            {
                ModelState.AddModelError("", "Vehicle already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleMap = _mapper.Map<Vehicle>(vehicleCreate);

            //line below required as fk is mandatory, dont forget to add customer repository above, also argument above fromquery
            vehicleMap.Customer = _customerRepository.GetCustomer(customerId);

            if (!_vehicleRepository.CreateVehicle(vehicleMap))
            {
                ModelState.AddModelError("", "theres an issue with something when trying to save");
                return StatusCode(500, ModelState);
            }

            return Ok("created");
        }






        //[HttpPut("{vehicleId")]
        //[ProducesResponseType(404)]
        ////[ProducesResponseType(200)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult UpdateVehicle(int vehicleId, [FromBody] VehicleDto vehicleUpdated)
        //{
        //    if (vehicleUpdated == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (vehicleId != vehicleUpdated.Id)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    //updated
        //    if (!_vehicleRepository.VehicleExists(vehicleId))
        //    {
        //        return NotFound();
        //    }


        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var vehicleMapper = _mapper.Map<Vehicle>(vehicleUpdated);

        //    if (!_vehicleRepository.UpdateVehicle(vehicleMapper))
        //    {
        //        ModelState.AddModelError("", "there was an issue finding vehicle");
        //        return StatusCode(500, ModelState);
        //    }
        //    //return NoContent();
        //    return Ok();
        //}

        //https : //localhost:7040/api/Vehicle/26
        //requires id field to be filled aswell
        [HttpPut("{vehicleId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateVehicle(int vehicleId, [FromBody] VehicleDto vehicleUpdated)
        {
            if (vehicleUpdated == null)
            {
                return BadRequest(ModelState);
            }

            if (vehicleId != vehicleUpdated.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_vehicleRepository.VehicleExists(vehicleId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicleMapper = _mapper.Map<Vehicle>(vehicleUpdated);

            if (!_vehicleRepository.UpdateVehicle(vehicleMapper))
            {
                ModelState.AddModelError("", "something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{vehicleId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteVehicle(int vehicleId)
        {
            if (!_vehicleRepository.VehicleExists(vehicleId))
            {
                return NotFound();
            }

            var vehicleToDelete = _vehicleRepository.GetVehicle(vehicleId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_vehicleRepository.DeleteVehicle(vehicleToDelete))
            {
                ModelState.AddModelError("", "issue while attempting to delete vehicle");
            }
            return NoContent();
        }
    }
}
