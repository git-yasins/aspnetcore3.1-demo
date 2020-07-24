using System.Threading.Tasks;
using aspnetcore3_demo.Models;
using aspnetcore3_demo.Services;
using Microsoft.AspNetCore.Mvc;
namespace aspnetcore3._1_demo.Controllers
{
    //[Route("Department/[Action]")]
    public class DepartmentController : Controller {
        private readonly IDepartmentService _departmentService;

        public DepartmentController (IDepartmentService departmentService) {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index () {
            ViewBag.Title = "部门列表";
            var departments = await _departmentService.GetAll ();
            return View (departments);
        }

        public IActionResult Add () {
            ViewBag.Title = "添加部门";
            return View (new Department ());
        }

        [HttpPost]
        public async Task<IActionResult> Add (Department model) {
            if (ModelState.IsValid) {
                await _departmentService.Add (model);
            }
            return RedirectToAction (nameof (Index));
        }
    }
}
