using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore3_demo.Repositories
{
    public class DepartmentRepositories : IDepartmentRepositories {
        private readonly List<Department> _departments = new List<Department> ();
        public DepartmentRepositories () {
            _departments.AddRange (
                new Department[] {
                    new Department {
                        Id = 1,
                            Name = "HR",
                            EmployeeCount = 16,
                            Location = "BEIJING"
                    }, new Department {
                        Id = 2,
                            Name = "R&D",
                            EmployeeCount = 52,
                            Location = "Shanghai"
                    }, new Department {
                        Id = 3,
                            Name = "Sales",
                            EmployeeCount = 200,
                            Location = "China"
                    }
                }
            );
        }

        public Task<Department> Add (Department department) {
            department.Id = _departments.Max (c => c.Id) + 1;
            _departments.Add (department);
            return Task.FromResult(department);
        }

        public Task<IEnumerable<Department>> GetAll () {
            return Task.Run (() => _departments.AsEnumerable ());
        }

        public Task<Department> GetById (int id) {
            return Task.Run (() => _departments.FirstOrDefault (x => x.Id == id));
        }

        // public Task<CompanySummary> GetCompanySummary () {
        //     return Task.Run (() => {
        //         return new CompanySummary {
        //         EmployeeCount = _departments.Sum (x => x.EmployeeCount),
        //         AverageDepartmentEmployeeCount = (int) _departments.Average (x => x.EmployeeCount)
        //         };
        //     });
        // }
    }
}
