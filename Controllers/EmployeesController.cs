using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Parameters;
using aspnetcore3_demo.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        /// <param name="parameters">查询条件</param>
        /// <returns></returns>
        [HttpGet(Name=nameof(GetEmployeesForCompany))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany (Guid companyId, [FromQuery] EmployeeDtoParameters parameters) {
            if (!await companyRepository.CompanyExistsAsync (companyId)) {
                return NotFound ();
            }
            var employees = await companyRepository.GetEmployeesAsync (companyId, parameters);
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
        [HttpPost (Name = nameof (CreateEmployeeForCompany))]
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

        /// <summary>
        /// 根据公司ID和员工ID整体更新员工信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="employeeId">员工ID</param>
        /// <param name="employee">更新的员工信息</param>
        /// /// <returns></returns>
        [HttpPut ("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeForCompany (Guid companyId, Guid employeeId, EmployeeUpdateDto employee) {
            if (!await companyRepository.CompanyExistsAsync (companyId)) {
                return NotFound ();
            }

            var employeeEntity = await companyRepository.GetEmployeeAsync (companyId, employeeId);
            if (employeeEntity == null) {
                //如果传进来的对象为空,则新增对象
                var employeeToAddEntity = mapper.Map<Employee> (employee);
                employeeToAddEntity.Id = employeeId;
                companyRepository.AddEmployee (companyId, employeeToAddEntity);
                await companyRepository.SaveAsync ();

                var dtoEntity = mapper.Map<EmployeeDto> (employeeToAddEntity);
                return CreatedAtRoute (nameof (GetEmployeeForCompany), new { companyId, employeeId = dtoEntity.Id }, dtoEntity);
                //   return NotFound ();
            }

            mapper.Map (employee, employeeEntity);

            companyRepository.UpdateEmployee (employeeEntity);
            await companyRepository.SaveAsync ();
            //status 204
            return NoContent ();
        }

        #region patch json传参注释
        /*

                     headers Content-Type:application/json-patch+json
                     http://localhost:5000/api/companies/d3da0df3-6097-40cc-9682-df4650bb34f5/employees/4f4ea5a8-5d05-41f1-bbcb-67cf14f09472
                     [
                       {
                         "op":"replace",
                         "path":"/employeeNo",
                         "value":"545454554"
                       },
                       {
            	          "op":"add",
            	          "path":"/dateOfBirth",
                          "value":"2020-2-2"
                        },
                        {
                          "op":"copy",
                          "from":"/employeeNo",
                          "path":"/lastName"
                        },
                        {
                          "op":"remove",
            	          "path":"/dateOfBirth",
                        }
                     ]

                    //新增|更新
                     [
                        {
            	            "op":"add",
            	            "path":"/employeeNo",
                            "value":"7777"
                        },
                        {
            	            "op":"add",
            	            "path":"/firstName",
                             "value":"5555"
                        },
                        {
            	            "op":"add",
            	            "path":"/lastName",
                            "value":"4444"
                        },
                        {
            	            "op":"add",
            	            "path":"/gender",
                            "value":2
                        },
                    ]
                    */
        #endregion

        /// <summary>
        /// 局部更新
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="employeeId">员工ID</param>
        /// <param name="jsonPatchDocument">Patch 局部更新JSON文档对象</param>
        /// <returns></returns>
        [HttpPatch ("{employeeId}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany (Guid companyId, Guid employeeId, JsonPatchDocument<EmployeeUpdateDto> jsonPatchDocument) {
            if (!await companyRepository.CompanyExistsAsync (companyId)) {
                return NotFound ();
            }

            var employeeEntity = await companyRepository.GetEmployeeAsync (companyId, employeeId);
            if (employeeEntity == null) {
                //如果为空则新增
                var employeeDto = new EmployeeUpdateDto ();
                //将传入的jsonPatchDocument数据序列化到employeeDto对象
                jsonPatchDocument.ApplyTo (employeeDto, ModelState);

                if (!TryValidateModel (employeeDto)) {
                    return ValidationProblem (ModelState);
                }
                //将EmployeeUpdateDto映射成Employee供新增数据库
                var employeeToAdd = mapper.Map<Employee> (employeeDto);
                employeeToAdd.Id = employeeId;

                companyRepository.AddEmployee (companyId, employeeToAdd);
                await companyRepository.SaveAsync ();
                //将Employee映射成EmployeeDto供查询
                var dtoToReturn = mapper.Map<EmployeeDto> (employeeToAdd);
                return CreatedAtRoute (nameof (GetEmployeeForCompany), new { companyId, employeeId = dtoToReturn.Id }, dtoToReturn);
                //return NotFound ();
            }

            var dtoToPatch = mapper.Map<EmployeeUpdateDto> (employeeEntity);
            jsonPatchDocument.ApplyTo (dtoToPatch, ModelState); //第二个参数校验Patch JSON传入的字段是否正确

            //验证jsonPatchDocument
            if (!TryValidateModel (dtoToPatch)) {
                return ValidationProblem (ModelState);
            }

            mapper.Map (dtoToPatch, employeeEntity);

            companyRepository.UpdateEmployee (employeeEntity);
            await companyRepository.SaveAsync ();
            //返回204
            return NoContent ();
        }
        /// <summary>
        /// 根据公司ID和员工ID删除一条记录
        /// /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="employeeId">员工ID</param>
        /// <returns>status 204</returns>
        [HttpDelete ("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompany (Guid companyId, Guid employeeId) {
            if (!await companyRepository.CompanyExistsAsync (companyId)) {
                return NotFound ();
            }

            var employeeEntity = await companyRepository.GetEmployeeAsync (companyId, employeeId);
            if (employeeEntity == null) {
                return NotFound ();
            }

            companyRepository.DeleteEmployee (employeeEntity);
            await companyRepository.SaveAsync ();

            return NoContent ();
        }

        /// <summary>
        /// 重写验证Model 加入控制器
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        public override ActionResult ValidationProblem (Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelStateDictionary) {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>> ();
            return (ActionResult) options.Value.InvalidModelStateResponseFactory (ControllerContext);
        }
    }
}
