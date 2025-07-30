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
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public IActionResult GetCustomers()
        {
            //var customers = _customerRepository.GetCustomers();
            var customers = _mapper.Map<List<CustomerDto>>(_customerRepository.GetCustomers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(customers);
        }




        [HttpGet("{customerId}")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        public IActionResult GetCustomer(int customerId)
        {
            if (!_customerRepository.CustomerExists(customerId))
                return NotFound();

            var country = _mapper.Map<CustomerDto>(_customerRepository.GetCustomer(customerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }



        ////create
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCustomer([FromBody] CustomerDto customerCreate)  //query strings, aka how data is inputted ie /&stringid=2 in url
        {
            if (customerCreate == null)
            {
                return BadRequest(ModelState);
            }

            var customerFirstNameData = _customerRepository.GetCustomers().Where(c => c.FirstName.Trim().ToUpper() == customerCreate.FirstName.TrimEnd().ToUpper()).FirstOrDefault();
            var customerLastNameData = _customerRepository.GetCustomers().Where(c => c.LastName.Trim().ToUpper() == customerCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();
            //var customerIdData = _customerRepository.GetCustomers().Where(c => c.Id == customerCreate.Id);


            if ((customerFirstNameData != null) && (customerLastNameData != null) /*&& (customerLastNameData != null)*/)
            {
                ModelState.AddModelError("", "Customer already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerMap = _mapper.Map<Customer>(customerCreate);

            if (!_customerRepository.CreateCustomer(customerMap))
            {
                ModelState.AddModelError("", "theres an issue with something when trying to save");
                return StatusCode(500, ModelState);
            }

            return Ok("created");
        }

        //https : //localhost:7040/api/Customer/4
        //requires id field to be filled aswell
        [HttpPut("{customerId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCustomer(int customerId, [FromBody] CustomerDto customerUpdated)
        {
            if (customerUpdated == null)
            {
                return BadRequest(ModelState);
            }

            if (customerId != customerUpdated.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_customerRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerMapper = _mapper.Map<Customer>(customerUpdated);

            if (!_customerRepository.UpdateCustomer(customerMapper))
            {
                ModelState.AddModelError("", "something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{customerId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCustomer(int customerId)
        {
            if (!_customerRepository.CustomerExists(customerId))
                {
                    return NotFound();
                }

            var customerToDelete = _customerRepository.GetCustomer(customerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_customerRepository.DeleteCustomer(customerToDelete))
            {
                ModelState.AddModelError("", "issue while attempting to delete customer");
            }
            return NoContent();
        }
    }
}