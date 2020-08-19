using System.Collections.Generic;
using aspnetcore3_demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore3_demo.Controllers {
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase {
        [HttpGet (Name = nameof (GetRoot))]
        public IActionResult GetRoot () {
            var links = new List<LinkDto> ();
            links.Add (new LinkDto (Url.Link (nameof (GetRoot), new { }), "self", "GET"));
            links.Add (new LinkDto (Url.Link (nameof (CompaniesController.GetCompanies), new { }), "companies", "GET"));
            links.Add (new LinkDto (Url.Link (nameof (CompaniesController.CreateCompany), new { }), "companies", "POST"));
            return Ok (links);
        }
    }
}
