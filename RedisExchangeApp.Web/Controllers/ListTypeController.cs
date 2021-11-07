using Microsoft.AspNetCore.Mvc;
using RedisExchangeApp.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeApp.Web.Controllers
{
    public class ListTypeController : Controller
    {

        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        private string _listKey = "Names";

        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(1);
        }
        public IActionResult Index()
        {
            if (_db.KeyExists(_listKey))
            {
                List<string> namesList = new();
                _db.ListRange(_listKey).ToList().ForEach(name =>
                {
                    namesList.Add(name.ToString());
                });
                return View(namesList);
            }

            return View(new List<string>());
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            _db.ListRightPush(_listKey, name);
            return RedirectToAction(nameof(Index));
        }
        
    }

}
