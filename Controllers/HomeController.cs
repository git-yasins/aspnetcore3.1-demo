using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore3_demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace aspnetcore3_demo.Controllers {

    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        //Redis服务
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public HomeController (ILogger<HomeController> logger, IConnectionMultiplexer connectionMultiplexer) {
            _logger = logger;
            this._redis = connectionMultiplexer;
            _database = _redis.GetDatabase ();
        }

        public IActionResult Index () {
            _database.StringSet ("fullName", "Michale Jackson");
            var name = _database.StringGet ("fullName");
            return View ("Index",name);
        }

        public IActionResult Privacy () {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
