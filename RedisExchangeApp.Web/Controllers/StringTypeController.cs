using Microsoft.AspNetCore.Mvc;
using RedisExchangeApp.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeApp.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;

        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {
            _db.StringSet("Name", "Example User");
            return View();
        }
        public IActionResult Show()
        {
            var value = _db.StringGet("Name");
            if (value.HasValue)
            {
                ViewBag.Value = value.ToString();
            }
            return View();
        }
    }
}
