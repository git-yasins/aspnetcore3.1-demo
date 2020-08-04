using System.Reflection.Metadata;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Models;
using AutoMapper;

namespace aspnetcore3_demo.Profiles {
    public class CompanyProfile : Profile {
        public CompanyProfile () {
            CreateMap<Company, CompanyDto> ()
                .ForMember (desc => desc.CompanyName, opt => opt.MapFrom (sc => sc.Name));
            CreateMap<CompanyAddDto,Company> ();
        }
    }
}
