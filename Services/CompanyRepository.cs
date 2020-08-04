using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using aspnetcore3_demo.Data;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Parameters;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore3_demo.Services {
    public class CompanyRepository : ICompanyRepository {
        private readonly RoutineDBContext context;
        public CompanyRepository (RoutineDBContext context) {
            this.context = context ??
                throw new ArgumentNullException (nameof (context));
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

        public async Task<IEnumerable<Company>> GetCompaniesAsync (CompanyDtoParameters parameters) {
            if (parameters == null) {
                throw new ArgumentNullException (nameof (parameters));
            }
            if (string.IsNullOrEmpty (parameters.CompanyName) && string.IsNullOrWhiteSpace (parameters.Search)) {
                return await context.Companys.ToListAsync ();
            }
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
            return await queryExpression.ToListAsync ();
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

        public async Task<IEnumerable<Employee>> GetEmployeesAsync (Guid companyId, string genderDisplay, string search) {
            if (companyId == Guid.Empty) {
                throw new ArgumentNullException (nameof (companyId));
            }
            if (string.IsNullOrWhiteSpace (genderDisplay) && string.IsNullOrWhiteSpace (search)) {
                return await context.Employees.Where (x => x.CompanyId == companyId)
                    .OrderBy (x => x.EmployeeNo).ToListAsync ();
            }

            var items = context.Employees.Where (x => x.CompanyId == companyId);

            if (!string.IsNullOrWhiteSpace (genderDisplay)) {
                var genderStr = genderDisplay.Trim ();
                var gender = Enum.Parse<Gender> (genderStr);
                items = items.Where (x => x.Gender == gender);
            }

            if (!string.IsNullOrWhiteSpace (search)) {
                search = search.Trim ();
                items = items.Where (x => x.EmployeeNo.Contains (search) || x.FirstName.Contains (search) || x.LastName.Contains (search));
            };

            return await items.OrderBy (x => x.EmployeeNo).ToListAsync ();
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
