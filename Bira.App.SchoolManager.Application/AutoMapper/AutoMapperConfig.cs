using AutoMapper;
using Bira.App.SchoolManager.Domain.DTOs.Request;
using Bira.App.SchoolManager.Domain.DTOs.Response;
using Bira.App.SchoolManager.Domain.Entities;

namespace Bira.App.SchoolManager.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<School, SchoolDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<School, SchoolResponse>().ReverseMap();
            CreateMap<Student, StudentResponse>().ReverseMap();
            CreateMap<Address, AddressResponse>().ReverseMap();
        }
    }
}