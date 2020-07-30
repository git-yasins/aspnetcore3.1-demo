using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Parameters;
using aspnetcore3_demo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore3_demo.Controllers {
    /*
    ApiController 特性功能
    可以启动自动HTTP400响应
    要求使用属性路由(Attribute Routing)
    推断参数的绑定源
    Multipart/form-data请求推断
    错误状态代码的问题详细信息
    */
    [ApiController]
    [Route ("api/companies")]
    //[Route ("api/[controller]")]
    public class CompaniesController : ControllerBase {
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper mapper;

        public CompaniesController (ICompanyRepository companyRepository, IMapper mapper) {
            this.companyRepository = companyRepository??throw new ArgumentNullException (nameof (companyRepository));
            this.mapper = mapper??throw new ArgumentNullException (nameof (mapper));
        }

        //[HttpGet("api/Compaines")]   api/companies?CompanyName=Microsoft&Search=y
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompaines ([FromQuery] CompanyDtoParameters parameters) {
            var compaines = await companyRepository.GetCompaniesAsync (parameters);
            var companyDtos = mapper.Map<IEnumerable<CompanyDto>> (compaines);
            return Ok (companyDtos);
        }

        [HttpHead] //只返回头,不返回响应体
        [HttpGet ("{companyId}")]
        public async Task<ActionResult<CompanyDto>> GetCompany (Guid companyId) {
            var exists = await companyRepository.CompanyExistsAsync (companyId);
            if (!exists) {
                return NotFound ();
            }
            var company = await companyRepository.GetCompanyAsync (companyId);
            if (company == null) {
                return NotFound ();
            }
            return Ok (mapper.Map<CompanyDto> (company));
        }
    }
}