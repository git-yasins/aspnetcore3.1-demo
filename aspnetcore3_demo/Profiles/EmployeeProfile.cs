using System;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Models;
using AutoMapper;

namespace aspnetcore3_demo.Profiles {
    public class EmployeeProfile : Profile {
        public EmployeeProfile () {
            CreateMap<Employee, EmployeeDto> ()
                .ForMember (desc => desc.Name, opt => opt.MapFrom (src => $"{src.FirstName} {src.LastName}"))
                .ForMember (desc => desc.GenderDisplay, opt => opt.MapFrom (src => src.Gender))
                .ForMember (desc => desc.Age, opt => opt.MapFrom (src => DateTime.Now.Year - src.DateOfBirth.Year));

            CreateMap<EmployeeAddDto, Employee> ();
            CreateMap<EmployeeUpdateDto, Employee> ();
            CreateMap<Employee, EmployeeUpdateDto> ();
        }
    }
}
