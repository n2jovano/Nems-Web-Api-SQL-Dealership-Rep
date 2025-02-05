using AutoMapper;
using DealershipApp.Dto;
using DealershipApp.Models;

namespace DealershipApp.AutoMapper
{
    public class MappingProfiles : Profile  //(f) automapper implementation
    {
        public MappingProfiles()
        {
            CreateMap<Dealership, DealershipDto>().ReverseMap();
            CreateMap<Vehicle, VehicleDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Staff, StaffDto>().ReverseMap();
            CreateMap<Service, ServiceDto>().ReverseMap();
            //OR
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
