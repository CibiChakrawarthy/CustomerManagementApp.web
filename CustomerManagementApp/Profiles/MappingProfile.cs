
using AutoMapper;
using CustomerManagementApp.DTOs;
using CustomerManagementApp.Models;

namespace CustomerManagementApp.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }

    }
}
