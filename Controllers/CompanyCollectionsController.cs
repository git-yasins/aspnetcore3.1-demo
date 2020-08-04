using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Helpers;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore3_demo.Controllers {
    /// <summary>
    /// 公司集合操作
    /// </summary>
    [ApiController]
    [Route ("api/companycollections")]
    public class CompanyCollectionsController : ControllerBase {
        private readonly IMapper mapper;
        private readonly ICompanyRepository companyRepository;

        public CompanyCollectionsController (IMapper mapper, ICompanyRepository companyRepository) {
            this.mapper = mapper??throw new ArgumentNullException (nameof (mapper));
            this.companyRepository = companyRepository??throw new ArgumentNullException (nameof (companyRepository));;
        }
        /// <summary>
        /// 通过Company ID集合获取公司信息
        /// /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet ("({ids})", Name = nameof (GetCompanyCollection))]
        public async Task<IActionResult> GetCompanyCollection ([FromRoute][ModelBinder (BinderType = typeof (ArrayModelBinder))] IEnumerable<Guid> ids) {
            if (ids == null) {
                return BadRequest ();
            }
            var entities = await companyRepository.GetCompaniesAsync (ids);
            //if (entities.Count () != ids.Count ()) {
                //return NotFound ();
            //}
            var dtoToReturn = mapper.Map<IEnumerable<CompanyDto>> (entities);
            return Ok (dtoToReturn);
        }

        /// <summary>
        /// 批量创建公司信息
        /// </summary>
        /// <param name="companyCollections">公司集合对象</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection (IEnumerable<CompanyAddDto> companyCollections) {
            var companyEntities = mapper.Map<IEnumerable<Company>> (companyCollections);
            foreach (var company in companyEntities) {
                companyRepository.AddCompany (company);
            }
            await companyRepository.SaveAsync ();

            var dtos = mapper.Map<IEnumerable<CompanyDto>> (companyEntities);
            var idsString = string.Join (",", dtos.Select (x => x.Id));

            //返回201状态 调用GetCompanyCollection Action 传递需要的参数信息
            return CreatedAtRoute (nameof (GetCompanyCollection), new { ids = idsString }, dtos);
        }
    }
}
