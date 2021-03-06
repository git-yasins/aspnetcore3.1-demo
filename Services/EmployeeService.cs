using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore3_demo.Models;

namespace aspnetcore3_demo.Services {
    public class EmployeeService : IEmployeeService {
        private readonly List<Employee> _employees = new List<Employee> ();
        public EmployeeService () {
            _employees.AddRange (new Employee[] {
                new Employee {
                    Id = 1,
                        DepartmentId = 1,
                        FirstName = "Nick",
                        LastName = "Carter",
                        Gender = Gender.男
                },
                new Employee {
                    Id = 2,
                        DepartmentId = 1,
                        FirstName = "Michael",
                        LastName = "Jackson",
                        Gender = Gender.男
                }, new Employee {
                    Id = 3,
                        DepartmentId = 2,
                        FirstName = "Avril",
                        LastName = "Lavigne",
                        Gender = Gender.女
                }, new Employee {
                    Id = 4,
                        DepartmentId = 3,
                        FirstName = "Katy",
                        LastName = "Winslet",
                        Gender = Gender.女
                }
            });
        }
        public Task Add (Employee employee) {
            employee.Id = _employees.Max (c => c.Id) + 1;
            _employees.Add (employee);
            return Task.CompletedTask;
        }

        public Task<Employee> Fire (int id) {
            return Task.Run (() => {
                var empoyee = _employees.FirstOrDefault (c => c.Id == id);
                if (empoyee != null) {
                    empoyee.Fired = true;
                    return empoyee;
                }
                return null;
            });
        }

        public Task<IEnumerable<Employee>> GetByDepartmentId (int departmentId) {
            return Task.Run (() =>
                _employees.Where (c => c.DepartmentId == departmentId)
            );
        }
    }
}