using System.Threading.Tasks;
using aspnetcore3_demo.Services;
using Microsoft.AspNetCore.Mvc;
namespace aspnetcore3_demo.ViewComponents {
    public class CompanySummaryViewComponent : ViewComponent {
        private readonly IDepartmentService _departmentService;

        public CompanySummaryViewComponent (IDepartmentService departmentService) {
            _departmentService = departmentService;
        }
        public async Task<IViewComponentResult> InvokeAsync (string title) {
            ViewBag.title=title;
            var summary = await _departmentService.GetCompanySummary ();
            return View (summary);
        }
    }
}
