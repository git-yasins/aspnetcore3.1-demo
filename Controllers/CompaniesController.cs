using System;
using System.Threading.Tasks;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore3_demo.Controllers
{
    /// <summary>
    /// 公司API
    /// </summary>
    /// <returns></returns>
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

        [HttpHead] //只返回头,不返回响应体
        [HttpGet ("{companyId}", Name = nameof (GetCompany))]
        /// <summary>
        /// 根据公司ID获取公司信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
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
        /// <summary>
        /// 创建一条公司记录
        /// </summary>
        /// <param name="company">公司信息对象</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany ([FromBody] CompanyAddDto company) {
            var entity = mapper.Map<Company> (company);
            companyRepository.AddCompany (entity);
            await companyRepository.SaveAsync ();
            var ResultDto = mapper.Map<CompanyDto> (entity);
            return CreatedAtRoute (nameof (GetCompany), new { companyId = ResultDto.Id }, ResultDto);
        }
    }
}
