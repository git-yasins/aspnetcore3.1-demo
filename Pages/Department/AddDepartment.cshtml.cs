using System.Threading.Tasks;
using aspnetcore3_demo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspnetcore3_demo.Pages.Department {
    public class AddDepartmentModel : PageModel {
        private readonly IDepartmentService _departmentService;

        [BindProperty]
        public aspnetcore3_demo.Models.Department Department { get; set; }

        public AddDepartmentModel (IDepartmentService departmentService) {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> OnPostAsync () {
            if (!ModelState.IsValid) {
                return Page ();
            }
            await _departmentService.Add(Department);
            return RedirectToPage("/Index");
        }
    }
}
