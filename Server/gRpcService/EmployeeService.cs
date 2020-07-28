using System.Security.Cryptography;
using System.Threading.Tasks;
using aspnetcore3_demo;
using aspnetcore3_demo.Repositories;
namespace aspnetcore3_demo.gRpcService {
    public class EmployeeService : Employees.EmployeesBase {
        private readonly IEmployeeRepositories employeeRepositories;

        public EmployeeService (IEmployeeRepositories employeeRepositories) {
            this.employeeRepositories = employeeRepositories;
        }

        public override async Task<GetByDepartmentIdResponse> GetByDepartmentId (GetByDepartmentIdRequest request, Grpc.Core.ServerCallContext context) {
            var result = new GetByDepartmentIdResponse ();
            var employees = await employeeRepositories.GetByDepartmentId (request.DepartmentId);
            result.Employees.AddRange (employees);
            return result;
        }

        public override async Task<AddEmployeeResponse> AddEmployee (AddEmployeeRequest request, Grpc.Core.ServerCallContext context) {
            var result = new AddEmployeeResponse ();
            var employee = await employeeRepositories.Add (request.Employee);
            result.Employee = employee;
            return result;
        }

        public override async Task<FireEmployeeResponse> FireEmployee (FireEmployeeRequest request, Grpc.Core.ServerCallContext context) {
            var result = new FireEmployeeResponse ();
            var employee = await employeeRepositories.Fire (request.Id);
            result.Employee = employee;
            return result;
        }
    }
}
