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
    public class CustomerServiceController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceRepository _serviceRepository;
        //private readonly IMapper _mapper;

        public CustomerServiceController(ICustomerRepository customerRepository, IServiceRepository serviceRepository)
        {
            _customerRepository = customerRepository;
            _serviceRepository = serviceRepository;
            //_mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public IActionResult GetAllCountries()
        {

            //outer join shows serivice on right only displays 3 customers but not all their repairs, wrong
            var entryPoint2 = from c in _customerRepository.GetCustomers()
                              join s in _serviceRepository.GetAllServices()
                              on c.Id equals s.Id into c_s
                              from s in c_s.DefaultIfEmpty()
                                  //where d.Name == null
                                  //select v;
                              select new
                              {
                                  firstName = c.FirstName,
                                  lastName = c.LastName,
                                  phone = c.Phone,
                                  serviceType = s.TypeofService,
                                  description = s.Description,
                                  repairDate = s.RepairDate,
                              };

            //correct way reversed
            var entryPoint3 = from s in _serviceRepository.GetAllServices()
                              join c in _customerRepository.GetCustomers()
                              on s.VehicleId equals c.Id into s_c
                              from c in s_c.DefaultIfEmpty()
                                  //where d.Name == null
                                  //select v;
                              select new
                              {
                                  serviceId = s.Id,
                                  serviceType = s.TypeofService,
                                  description = s.Description,
                                  repairDate = s.RepairDate,
                                  vehicle = s.Vehicle,
                                  firstName = c.FirstName,
                                  //lastName = c.LastName,
                                  //phone = c.Phone,
                              };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(entryPoint3);
        }
    }
}
