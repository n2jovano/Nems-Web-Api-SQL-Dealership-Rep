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
    public class CustomerServiceVehicleController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IVehicleRepository _vehicleRepository;
        //private readonly IMapper _mapper;

        public CustomerServiceVehicleController(ICustomerRepository customerRepository, IServiceRepository serviceRepository, IVehicleRepository vehicleRepository)
        {
            _customerRepository = customerRepository;
            _serviceRepository = serviceRepository;
            _vehicleRepository = vehicleRepository;
            //_mapper = mapper;
        }



        [HttpGet]//needs work
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GetAllCountries()
        {

            //var three_tables = (from s in _serviceRepository.GetAllServices()
            //                  join c in _customerRepository.GetCustomers() on s.VehicleId equals c.Id 
            //                  join v in _vehicleRepository.GetVehicles() on c.Id equals v.Id

            //                  //into s_c
            //                  //from c in s_c.DefaultIfEmpty()


            //                      where c.Id == v.Id

            //                  select new
            //                  {
            //                      serviceId = s.Id,
            //                      serviceType = s.TypeofService,
            //                      description = s.Description,
            //                      repairDate = s.RepairDate,
            //                      vehicle = s.Vehicle,
            //                      firstName = c.FirstName,
            //                      vehicle2 = v.Name,
            //                      //lastName = c.LastName,
            //                      //phone = c.Phone,
            //                  }).Take(2);

            //var three_tables = (from s in _serviceRepository.GetAllServices()
            //                    join c in _customerRepository.GetCustomers() on s.VehicleId equals c.Id into s_c_jointable
            //                    from columns in s_c_jointable.DefaultIfEmpty()
            //                    join v in _vehicleRepository.GetVehicles() on new { s.Id}
            //                    equals
            //                    new { Idy = v.Id} into s_v_jointable
            //                    from sectable in s_v_jointable.DefaultIfEmpty()

            //                    select new
            //                    {
            //                        sIdy = s.Idy,
            //                        serviceId = s.Id,
            //                        serviceType = s.TypeofService,
            //                        description = s.Description,
            //                        repairDate = s.RepairDate,
            //                        vehicle = s.Vehicle,
            //                        firstName = s_c_jointable.FirstName,
            //                        vehicle2 = s_v_jointable.Name,
            //                        lastName = s_c_jointable.LastName,
            //                        phone = s_c_jointable.Phone,
            //                    }).ToList();


            var three_tables = (from s in _serviceRepository.GetAllServices()                              
                                join v in _vehicleRepository.GetVehicles() on s.VehicleId  equals v.Id
                                join c in _customerRepository.GetCustomers() on v.Id equals c.Id



                                select new
                                {
                                    serviceId = s.Id,
                                    serviceType = s.TypeofService,
                                    description = s.Description,
                                    repairDate = s.RepairDate,
                                    //vehicle = s.Vehicle,    <-- this value will cause an error sdince its nonexistant in value in sql
                                    vehicleName = v.Name,
                                    colour = v.Colour,
                                    Year = v.VehicleYear,
                                    firstName = c.FirstName,
                                    lastName = c.LastName,
                                    phone = c.Phone,
                                }).ToList();



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(three_tables);
        }
    }
}
