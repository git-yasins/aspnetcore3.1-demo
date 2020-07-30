using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore3_demo.Controllers {
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany (Guid companyId, [FromQuery (Name = "gender")] string genderDisplay, string search) {
            if (!await companyRepository.CompanyExistsAsync (companyId)) {
                return NotFound ();
            }
            var employees = await companyRepository.GetEmployeesAsync (companyId, genderDisplay, search);
            var employeesDto = mapper.Map<IEnumerable<EmployeeDto>> (employees);
            return Ok (employeesDto);
        }

        [HttpGet ("{employeeId}")]
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
    }
}
