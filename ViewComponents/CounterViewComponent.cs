using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace aspnetcore3_demo.ViewComponents {
    public class CounterViewComponent : ViewComponent {
        private readonly IDatabase _db;
        public CounterViewComponent (IConnectionMultiplexer redis) {
            _db = redis.GetDatabase ();
        }

        public async Task<IViewComponentResult> InvokeAsync () {
            var controller = RouteData.Values["controller"] as string;
            var action = RouteData.Values["action"] as string;

            if (!string.IsNullOrWhiteSpace (controller) &&
                !string.IsNullOrWhiteSpace (action)) {
                var pageId = $"{controller}-{action}";
                await _db.StringIncrementAsync (pageId);

                return View ("Default", pageId + ":" + await _db.StringGetAsync (pageId));
            }

            throw new System.Exception ("Cannot get pageId");
        }
    }
}
