using System.Threading.Tasks;
using aspnetcore3_demo;
using aspnetcore3_demo.Repositories;

namespace aspnetcore3_demo.gRpcService {
    public class DepartmentService : Departments.DepartmentsBase {
        private readonly IDepartmentRepositories departmentRepositories;

        public DepartmentService(IDepartmentRepositories departmentRepositories)
{
            this.departmentRepositories = departmentRepositories;
        }

        public override async Task<GetAllDepartmentsResponse> GetAll (GetAllDepartmentsRequest request, Grpc.Core.ServerCallContext context) {
            var result = new GetAllDepartmentsResponse();
            var departments = await departmentRepositories.GetAll();
            result.Departments.AddRange(departments);
            return result;
        }

        public override async Task<AddDepartmentResponse> Add (AddDepartmentRequest request, Grpc.Core.ServerCallContext context) {
            var department = await departmentRepositories.Add(request.Department);
            var result =  new AddDepartmentResponse{Department = department};
            return result;
        }
    }
}
