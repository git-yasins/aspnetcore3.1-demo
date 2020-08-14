using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using aspnetcore3_demo.Data;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Helpers;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Parameters;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore3_demo.Services {
    public class CompanyRepository : ICompanyRepository {
        private readonly RoutineDBContext context;
        private readonly IPropertyMappingService propertyMappingService;

        public CompanyRepository (RoutineDBContext context, IPropertyMappingService propertyMappingService) {
            this.context = context ??
                throw new ArgumentNullException (nameof (context));
            this.propertyMappingService = propertyMappingService??
            throw new ArgumentNullException (nameof (propertyMappingService));;
        }
        public void AddCompany (Company company) {
            if (company == null) {
                throw new ArgumentNullException (nameof (company));
            }
            company.Id = Guid.NewGuid ();
            if (company.Employees != null) {
                foreach (var employee in company.Employees) {
                    employee.Id = Guid.NewGuid ();
                }
            }
            context.Companys.Add (company);
        }

        public void AddEmployee (Guid companyId, Employee employee) {
            if (employee == null) {
                throw new ArgumentNullException (nameof (employee));
            }
            if (companyId == Guid.Empty) {
                throw new ArgumentNullException (nameof (companyId));
            }

            employee.CompanyId = companyId;
            context.Employees.Add (employee);
        }

        public async Task<bool> CompanyExistsAsync (Guid companyId) {
            if (companyId == Guid.Empty) {
                throw new ArgumentNullException (nameof (companyId));
            }
            return await context.Companys.AnyAsync (x => x.Id == companyId);
        }

        public void DeleteCompany (Company company) {
            if (company == null) {
                throw new ArgumentNullException (nameof (company));
            }
            context.Companys.Remove (company);
        }

        public void DeleteEmployee (Employee employee) {
            if (employee == null) {
                throw new ArgumentNullException (nameof (employee));
            }
            context.Employees.Remove (employee);
        }

        public async Task<PagedList<Company>> GetCompaniesAsync (CompanyDtoParameters parameters) {
            if (parameters == null) {
                throw new ArgumentNullException (nameof (parameters));
            }
            // if (string.IsNullOrEmpty (parameters.CompanyName) && string.IsNullOrWhiteSpace (parameters.Search)) {
            //     return await context.Companys.ToListAsync ();
            // }
            var queryExpression = context.Companys as IQueryable<Company>;
            if (!string.IsNullOrWhiteSpace (parameters.CompanyName)) {
                parameters.CompanyName = parameters.CompanyName.Trim ();
                queryExpression = queryExpression.Where (x => x.Name == parameters.CompanyName);
            }
            if (!string.IsNullOrWhiteSpace (parameters.Search)) {
                parameters.Search = parameters.Search.Trim ();
                queryExpression = queryExpression
                    .Where (x => x.Name.Contains (parameters.Search) || x.Introduction.Contains (parameters.Search));
            }

            //排序
            var mappingDictionary = propertyMappingService.GetPropertyMapping<CompanyDto, Company> ();
            queryExpression = queryExpression.ApplySort (parameters.OrderBy, mappingDictionary);

            //返回分页的结果
            return await PagedList<Company>.Create (queryExpression, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync (IEnumerable<Guid> companyIds) {
            if (companyIds == null) {
                throw new ArgumentNullException (nameof (companyIds));
            }
            return await context.Companys.Where (x => companyIds.Contains (x.Id))
                .OrderBy (X => X.Name).ToListAsync ();
        }

        public async Task<Company> GetCompanyAsync (Guid companyId) {
            if (companyId == Guid.Empty) {
                throw new ArgumentNullException (nameof (companyId));
            }
            return await context.Companys.FirstOrDefaultAsync (x => x.Id == companyId);
        }

        public async Task<Employee> GetEmployeeAsync (Guid companyId, Guid employeeId) {
            if (companyId == Guid.Empty) {
                throw new ArgumentNullException (nameof (companyId));
            }
            if (employeeId == Guid.Empty) {
                throw new ArgumentNullException (nameof (employeeId));
            }
            return await context.Employees
                .Where (x => x.CompanyId == companyId && x.Id == employeeId)
                .FirstOrDefaultAsync ();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync (Guid companyId, EmployeeDtoParameters parameters) {
            if (companyId == Guid.Empty) {
                throw new ArgumentNullException (nameof (companyId));
            }
            // if (string.IsNullOrWhiteSpace (parameters.Gender) && string.IsNullOrWhiteSpace (parameters.Q)) {
            //     return await context.Employees.Where (x => x.CompanyId == companyId)
            //         .OrderBy (x => x.EmployeeNo).ToListAsync ();
            // }

            var items = context.Employees.Where (x => x.CompanyId == companyId);

            //性别查询
            if (!string.IsNullOrWhiteSpace (parameters.Gender)) {
                var genderStr = parameters.Gender.Trim ();
                var gender = Enum.Parse<Gender> (genderStr);
                items = items.Where (x => x.Gender == gender);
            }

            //搜索匹配
            if (!string.IsNullOrWhiteSpace (parameters.Q)) {
                parameters.Q = parameters.Q.Trim ().ToLower ();
                items = items.Where (x =>
                    x.EmployeeNo.ToLower ().Contains (parameters.Q) ||
                    x.FirstName.ToLower ().Contains (parameters.Q) ||
                    x.LastName.ToLower ().Contains (parameters.Q));
            };

            //排序
            // if (!string.IsNullOrWhiteSpace (parameters.OrderBy)) {
            //     if (parameters.OrderBy.ToLowerInvariant () == "name") {
            //         items = items.OrderBy (x => x.FirstName).ThenBy (x => x.LastName);
            //     }
            // }

            //将DTO属性映射到数据库实体MODEL
            var mappingDictionary = propertyMappingService.GetPropertyMapping<EmployeeDto, Employee> ();
            //排序
            items = items.ApplySort (parameters.OrderBy, mappingDictionary);

            return await items.ToListAsync ();
        }

        public async Task<bool> SaveAsync () {
            return await context.SaveChangesAsync () >= 0;
        }

        public void UpdateCompany (Company company) {

        }

        public void UpdateEmployee (Employee employee) {

        }
    }
}
