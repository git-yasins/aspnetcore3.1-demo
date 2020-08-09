using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Helpers;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Parameters;
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
        private readonly IPropertyMappingService propertyMappingService;
        private readonly IPropertyCheckService propertyCheckService;

        public CompaniesController (ICompanyRepository companyRepository, IMapper mapper, IPropertyMappingService propertyMappingService, IPropertyCheckService propertyCheckService) {
            this.companyRepository = companyRepository??throw new ArgumentNullException (nameof (companyRepository));
            this.mapper = mapper??throw new ArgumentNullException (nameof (mapper));
            this.propertyMappingService = propertyMappingService??throw new ArgumentNullException (nameof (propertyMappingService));
            this.propertyCheckService = propertyCheckService??throw new ArgumentNullException (nameof (propertyCheckService));
        }

        /// <summary>
        /// /// 根据公司ID查询一条记录
        /// </summary>
        /// <param name="companyId">公司GUID</param>
        /// <returns>返回CompanyDto</returns>
        [HttpHead] //只返回头,不返回响应体
        [HttpGet ("{companyId}", Name = nameof (GetCompany))]
        /// <summary>
        /// 根据公司ID获取公司信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="fields">数据塑形字段</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCompany (Guid companyId, [FromQuery] string fields) {
            var exists = await companyRepository.CompanyExistsAsync (companyId);
            if (!exists) {
                return NotFound ();
            }
            if (!propertyCheckService.TypeHasProperties<CompanyDto> (fields)) {
                return BadRequest ();
            }
            var company = await companyRepository.GetCompanyAsync (companyId);
            if (company == null) {
                return NotFound ();
            }

            var links = CreateLinksForCompany (companyId, fields);
            var linkedDict = mapper.Map<CompanyDto> (company).ShapeData (fields) as IDictionary<string, object>;
            linkedDict.Add ("links", links);

            return Ok (linkedDict);
        }

        /// <summary>
        ///  分页,查询和搜索公司信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet (Name = nameof (GetCompanies))]
        public async Task<IActionResult> GetCompanies ([FromQuery] CompanyDtoParameters parameters) {

            if (!propertyMappingService.ValidationMappingExistesFor<CompanyDto, Company> (parameters.OrderBy)) {
                return BadRequest ();
            }
            if (!propertyCheckService.TypeHasProperties<CompanyDto> (parameters.Fields)) {
                return BadRequest ();
            }
            var companies = await companyRepository.GetCompaniesAsync (parameters);

            var previousLink = companies.HasPrevious?CreateCompaniesResourceUri (parameters, ResourceUriType.PriviousPage) : null;
            var nextLink = companies.HasNext?CreateCompaniesResourceUri (parameters, ResourceUriType.NextPage) : null;
            //分页信息匿名类
            var paginationMetadata = new {
                totalCount = companies.TotalCount,
                PagedSize = companies.PageSize,
                currentPage = companies.CurrentPage,
                totalPages = companies.TotalPages,
                previousLink,
                nextLink
            };

            //添加分页链接Headers
            Response.Headers.Add ("X-Pagination", JsonSerializer.Serialize (paginationMetadata,
                new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping } //防止URI 其他字符转义
            ));

            var companyDtos = mapper.Map<IEnumerable<CompanyDto>> (companies);

            //create
            var links = CreateLinksForCompany (parameters, companies.HasPrevious, companies.HasNext);
            var shapedData = companyDtos.ShapeData (parameters.Fields);
            //遍历为每集合的每条数据创建LINK
            var shapedCompaniesWithLinks = shapedData.Select (c => {
                var companyDict = c as IDictionary<string, object>;
                var companyLinks = CreateLinksForCompany ((Guid) companyDict["Id"], null);
                companyDict.Add ("links", companyLinks);
                return companyDict;
            });
            //links {value:[xxx,xxx],links}
            var linkedCollectionResource = new {
                value = shapedCompaniesWithLinks,
                links
            };
            return Ok (linkedCollectionResource);
        }

        /// <summary>
        /// 创建一条公司记录
        /// </summary>
        /// <param name="company">公司信息对象</param>
        /// <returns></returns>
        [HttpPost (Name = nameof (CreateCompany))]
        public async Task<ActionResult<CompanyDto>> CreateCompany ([FromBody] CompanyAddDto company) {
            var entity = mapper.Map<Company> (company);
            companyRepository.AddCompany (entity);
            await companyRepository.SaveAsync ();
            var ResultDto = mapper.Map<CompanyDto> (entity);

            var links = CreateLinksForCompany (ResultDto.Id, null);
            var linkedDict = ResultDto.ShapeData (null) as IDictionary<string, object>;
            linkedDict.Add ("links", links);

            return CreatedAtRoute (nameof (GetCompany), new { companyId = linkedDict["Id"] }, linkedDict);
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
        [HttpDelete ("{companyId}", Name = nameof (DeleteCompany))]
        public async Task<IActionResult> DeleteCompany (Guid companyId) {
            var companyEntity = await companyRepository.GetCompanyAsync (companyId);

            if (companyEntity == null) {
                return NotFound ();
            }

            //查询employee到dbContext进行追踪,以便根据父表删除子表数据
            await companyRepository.GetEmployeesAsync (companyId, null);

            companyRepository.DeleteCompany (companyEntity);
            await companyRepository.SaveAsync ();

            return NoContent ();
        }

        /// <summary>
        ///  创建分页链接URI
        /// </summary>
        /// <param name="parameters">公司信息分页查询对象</param>
        /// <param name="type">上一页|下一页 分页枚举</param>
        /// <returns>公司数据分页Uri链接</returns>
        private string CreateCompaniesResourceUri (CompanyDtoParameters parameters, ResourceUriType type) {
            switch (type) {
                case ResourceUriType.PriviousPage:
                    return CreateLink (parameters, type);
                case ResourceUriType.NextPage:
                    return CreateLink (parameters, type);
                default:
                    return CreateLink (parameters, ResourceUriType.Default);
            }
        }

        /// <summary>
        /// 生成分页链接
        /// </summary>
        /// <param name="parameters">公司信息查询参数</param>
        /// <param name="type">分页枚举 为NextPage+1 PriviousPage-1 否则默认</param>
        /// <returns>分页链接</returns>
        private string CreateLink (CompanyDtoParameters parameters, ResourceUriType type) {
            return Url.Link (nameof (GetCompanies), new {
                pageNumber =
                    (type == ResourceUriType.PriviousPage) ?
                    (parameters.PageNumber - 1) : (type == ResourceUriType.NextPage) ?
                    (parameters.PageNumber + 1) : parameters.PageNumber,
                    fields = parameters.Fields,
                    orderBy = parameters.OrderBy,
                    pageSize = parameters.PageSize,
                    companyName = parameters.CompanyName,
                    search = parameters.Search
            });
        }

        /// <summary>
        /// 生成单个资源 HATEOAS 链接
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="fields">指定CompanyDto返回的字段</param>
        /// <returns></returns>
        private IEnumerable<LinkDto> CreateLinksForCompany (Guid companyId, string fields) {

            var links = new List<LinkDto> ();
            links.Add (new LinkDto (
                Url.Link (nameof (GetCompany), string.IsNullOrWhiteSpace (fields) ? new { companyId, fields } : new { companyId, fields }),
                "self",
                "GET"));
            links.Add (new LinkDto (
                Url.Link (nameof (GetCompany), new { companyId }),
                "delete_company",
                "DELETE"));
            links.Add (new LinkDto (
                Url.Link (nameof (EmployeesController.CreateEmployeeForCompany), new { companyId }),
                "create_employee_for_company",
                "POST"));
            links.Add (new LinkDto (
                Url.Link (nameof (EmployeesController.GetEmployeesForCompany), new { companyId }),
                "get_employees_for_company",
                "GET"
            ));
            return links;
        }
        /// <summary>
        /// 生成多个资源 HATEOAS 链接
        ///  </summary>
        /// <param name="companyDtoParameters"></param>
        /// <param name="hasPrevious">是否存在上一页</param>
        /// <param name="hasNext">是否存在下一页</param>
        /// <returns></returns>
        private IEnumerable<LinkDto> CreateLinksForCompany (CompanyDtoParameters companyDtoParameters, bool hasPrevious, bool hasNext) {
            var links = new List<LinkDto> ();
            links.Add (new LinkDto (CreateLink (companyDtoParameters, ResourceUriType.Default), "self", "GET"));
            if (hasPrevious)
                links.Add (new LinkDto (CreateLink (companyDtoParameters, ResourceUriType.PriviousPage), "previous_page", "GET"));
            if (hasNext)
                links.Add (new LinkDto (CreateLink (companyDtoParameters, ResourceUriType.NextPage), "next_page", "GET"));
            return links;
        }
    }
}
