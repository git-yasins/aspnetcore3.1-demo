using System.Threading.Tasks;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore3_demo.Controllers {
    public class EmployeeController : Controller {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        public EmployeeController (IEmployeeService employeeService, IDepartmentService departmentService) {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index (int departmentId) {
            var department = await _departmentService.GetById (departmentId);
            ViewBag.Title = $"员工 of {department.Name}";
            ViewBag.DepartmentId = departmentId;

            var employees = await _employeeService.GetByDepartmentId (departmentId);
            return View (employees);
        }

        public IActionResult Add (int departmentId) {
            ViewBag.Title = "新增员工";
            return View (new Employee {
                DepartmentId = departmentId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add (Employee model) {
            if (ModelState.IsValid) {
                await _employeeService.Add (model);
            }
            return RedirectToAction (nameof (Index), new { departmentId = model.DepartmentId });
        }

        public async Task<IActionResult> Fire (int employeeId) {

            var employee = await _employeeService.Fire (employeeId);
            return RedirectToAction (nameof (Index), new { departmentId = employee.DepartmentId });
        }
    }
}
