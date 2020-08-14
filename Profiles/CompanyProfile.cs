using System.Reflection.Metadata;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Models;
using AutoMapper;

namespace aspnetcore3_demo.Profiles {
    public class CompanyProfile : Profile {
        public CompanyProfile () {
            CreateMap<Company, CompanyDto> ()
                .ForMember (desc => desc.CompanyName, opt => opt.MapFrom (sc => sc.Name));
            CreateMap<CompanyAddDto, Company> ();
            //返回资源 输出:从Company映射到CompanyFullDto
            CreateMap<Company, CompanyFullDto> ();
            //创建资源 输入:从CompanyAddWithBankruptTimeDto映射到实体Company
            CreateMap<CompanyAddWithBankruptTimeDto, Company> ();
        }
    }
}
