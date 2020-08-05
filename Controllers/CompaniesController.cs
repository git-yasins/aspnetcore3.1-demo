using System;
using System.ComponentModel;
using System.Threading.Tasks;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore3_demo.Controllers {
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

        /// <summary>
        /// 设置Options 谓词
        /// 获取服务器支持的HTTP请求方法
        /// 用来检查服务器的性能。例如：AJAX进行跨域请求时的预检，
        /// 需要向另外一个域名的资源发送一个HTTP OPTIONS请求头，用以判断实际发送的请求是否安全。
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetComaniesOptions () {
            Response.Headers.Add ("Allow", "GET,POST,OPTIONS");
            return Ok ();
        }
        /// <summary>
        /// 根据公司ID删除公司信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns>statusCode 204</returns>
        [HttpDelete ("{companyId}")]
        public async Task<IActionResult> DeleteCompany (Guid companyId) {
            var companyEntity = await companyRepository.GetCompanyAsync (companyId);

            if (companyEntity == null) {
                return NotFound ();
            }

            //查询employee到dbContext进行追踪,以便根据父表删除子表数据
            await companyRepository.GetEmployeesAsync (companyId, null, null);

            companyRepository.DeleteCompany (companyEntity);
            await companyRepository.SaveAsync ();

            return NoContent ();
        }
    }
}
