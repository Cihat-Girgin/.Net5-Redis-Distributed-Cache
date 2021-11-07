using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeApp.Web.Controllers
{
    public class SortedSetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
