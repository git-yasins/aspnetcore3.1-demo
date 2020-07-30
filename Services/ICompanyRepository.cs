using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using aspnetcore3_demo.Entities;

namespace aspnetcore3_demo.Services {
    public interface ICompanyRepository {
        Task<IEnumerable<Company>> GetCompaniesAsync ();
        Task<Company> GetCompanyAsync (Guid companyId);
        Task<IEnumerable<Company>> GetCompaniesAsync (IEnumerable<Guid> CompanyIds);
        void AddCompany (Company company);
        void UpdateCompany (Company company);
        void DeleteCompany (Company company);
        Task<bool> CompanyExistsAsync (Guid companyId);
        Task<IEnumerable<Employee>> GetEmployeesAsync (Guid companyId,string genderDisplay,string search);
        Task<Employee> GetEmployeeAsync (Guid companyId, Guid employeeId);
        void AddEmployee (Guid companyId, Employee employee);
        void UpdateEmployee (Employee employee);
        void DeleteEmployee(Employee employee);
        Task<bool> SaveAsync ();
    }
}
