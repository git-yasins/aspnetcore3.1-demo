using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore3_demo.Controllers {
    /// <summary>
    /// 员工api
    /// </summary>
    [ApiController]
    [Route ("api/companies/{companyId}/employees")]
    public class EmployeesController : ControllerBase {
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper mapper;

        public EmployeesController (ICompanyRepository companyRepository, IMapper mapper) {
            this.companyRepository = companyRepository ??
                throw new ArgumentNullException (nameof (companyRepository));
            this.mapper = mapper ??
                throw new ArgumentNullException (nameof (mapper));
        }
        /// <summary>
        /// 根据公司ID获取公司下的员工信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="genderDisplay">性别</param>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany (Guid companyId, [FromQuery (Name = "gender")] string genderDisplay, string search) {
            if (!await companyRepository.CompanyExistsAsync (companyId)) {
                return NotFound ();
            }
            var employees = await companyRepository.GetEmployeesAsync (companyId, genderDisplay, search);
            var employeesDto = mapper.Map<IEnumerable<EmployeeDto>> (employees);
            return Ok (employeesDto);
        }
        /// <summary>
        /// 根据公司ID+员工 ID获取具体员工信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="employeeId">员工ID</param>
        /// <returns></returns>
        [HttpGet ("{employeeId}", Name = nameof (GetEmployeeForCompany))]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeForCompany (Guid companyId, Guid employeeId) {
            if (!await companyRepository.CompanyExistsAsync (companyId)) {
                return NotFound ();
            }

            var employee = await companyRepository.GetEmployeeAsync (companyId, employeeId);
            if (employee == null) {
                return NotFound ();
            }
            var employeeDto = mapper.Map<EmployeeDto> (employee);
            return Ok (employeeDto);
        }
        /// <summary>
        /// 根据公司ID创建属于该公司下的员工信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="employee">员工对象</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany (Guid companyId, EmployeeAddDto employee) {
            if (!await companyRepository.CompanyExistsAsync (companyId)) {
                return NotFound ();
            }

            var entity = mapper.Map<Employee> (employee);

            companyRepository.AddEmployee (companyId, entity);
            await companyRepository.SaveAsync ();

            var dtoEntity = mapper.Map<EmployeeDto> (entity);
            return CreatedAtRoute (nameof (GetEmployeeForCompany), new { companyId, employeeId = dtoEntity.Id }, dtoEntity);
        }
    }
}
