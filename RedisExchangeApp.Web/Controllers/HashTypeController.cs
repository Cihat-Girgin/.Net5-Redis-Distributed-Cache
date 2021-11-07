using Microsoft.AspNetCore.Mvc;
using RedisExchangeApp.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeApp.Web.Controllers
{
    public class HashTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        private string _listKey = "HashNames";

        public HashTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(4);
        }
        public IActionResult Index()
        {
            if (_db.KeyExists(_listKey))
            {
                Dictionary<string,string> nameList = new();
                _db.HashGetAll(_listKey).ToList().ForEach(name =>
                {
                    nameList.Add(name.Name.ToString(),name.Value.ToString());
                });
                return View(nameList);
            }
            return View(new Dictionary<string,string>());
        }
        [HttpPost]
        public IActionResult Add(string key,string value)
        {
            _db.KeyExpire(_listKey, DateTime.Now.AddMinutes(1));
            _db.HashSet(_listKey, key, value);
            return RedirectToAction(nameof(Index));
        }
    }
}
