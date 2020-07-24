using System.Threading.Tasks;
using aspnetcore3_demo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspnetcore3_demo.Pages.Employee {
    public class AddEmployeeModel : PageModel {
        private readonly IDepartmentService _departmentService;

        private readonly IEmployeeService _employeeService;
        [BindProperty]
        public aspnetcore3_demo.Models.Employee Employee { get; set; }

        public AddEmployeeModel (IEmployeeService employeeService) {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> OnPostAsync (int departmentId) {
            Employee.DepartmentId = departmentId;

            if (!ModelState.IsValid) {
                return Page ();
            }

            await _employeeService.Add (Employee);
            return RedirectToPage ("EmployeeList",new {departmentId});
        }
    }
}
