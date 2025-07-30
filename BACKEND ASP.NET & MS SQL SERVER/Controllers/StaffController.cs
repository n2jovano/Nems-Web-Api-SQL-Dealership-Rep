using AutoMapper;
using DealershipApp.Dto;
using DealershipApp.Interfaces;
using DealershipApp.Models;
using DealershipApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DealershipApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IDealershipRepository _dealershipRepository;
        private readonly IMapper _mapper;

        public StaffController(IStaffRepository staffRepository, IDealershipRepository dealershipRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _dealershipRepository = dealershipRepository;
            _mapper = mapper;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Staff>))]
        public IActionResult GetAllStaff()
        {
            //var staff = _staffRepository.GetAllStaff();
            var staff = _mapper.Map<List<StaffDto>>(_staffRepository.GetAllStaff());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(staff);
        }



        [HttpGet("{staff}/id")]
        [ProducesResponseType(200, Type = typeof(Staff))]
        public IActionResult GetStaffById(int staff)
        {
            //var my_staff = _staffRepository.GetStaffById(staff);
            var my_staff = _mapper.Map<StaffDto>(_staffRepository.GetStaffById(staff));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(my_staff);
        }



        [HttpGet("{staff}/nameById")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GetStaffNameById(int staff)
        {
            if (!_staffRepository.StaffExists(staff))
            {
                return NotFound();
            }
            var my_staff = _staffRepository.GetStaffNameById(staff);
            //var vehicles = _mapper.Map<List<VehicleDto>>(_vehicleRepository.GetVehicles());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(my_staff);
        }



        [HttpGet("{staff}/staffByProfession")]//--
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GetStaffByProfession(string staff)
        {
            //var my_staff = _staffRepository.GetStaffByProfession(staff);
            var my_staff = _mapper.Map<List<StaffDto>>(_staffRepository.GetStaffByProfession(staff));
            if (my_staff.IsNullOrEmpty())  //if null or empty, old method
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(my_staff);
        }



        //create
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStaff([FromQuery] int dealershipId, [FromBody] StaffDto staffCreate)  //query strings, aka how data is inputted ie /&stringid=2 in url
        {
            if (staffCreate == null)
            {
                return BadRequest(ModelState);
            }

            var staffdataFirstName = _staffRepository.GetAllStaff().Where(v => v.FirstName.Trim().ToUpper() == staffCreate.FirstName.TrimEnd().ToUpper()).FirstOrDefault();
            var staffdataLastName = _staffRepository.GetAllStaff().Where(v => v.LastName.Trim().ToUpper() == staffCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();

            if ((staffdataFirstName != null) && (staffdataLastName != null))
            {
                ModelState.AddModelError("", "Staff already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staffMap = _mapper.Map<Staff>(staffCreate);

            //line below required as fk is mandatory, dont forget to add customer repository above, also argument above fromquery
            staffMap.Dealership = _dealershipRepository.GetDealership(dealershipId);

            if (!_staffRepository.CreateStaff(staffMap))
            {
                ModelState.AddModelError("", "theres an issue with something when trying to save");
                return StatusCode(500, ModelState);
            }

            return Ok("created");
        }
    }
}
