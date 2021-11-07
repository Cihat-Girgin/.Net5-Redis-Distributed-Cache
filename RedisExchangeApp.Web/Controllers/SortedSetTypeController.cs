using Microsoft.AspNetCore.Mvc;
using RedisExchangeApp.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeApp.Web.Controllers
{
    public class SortedSetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        private string _listKey = "SortedSetNames";

        public SortedSetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(3);
        }
        public IActionResult Index()
        {
            if (_db.KeyExists(_listKey))
            {
                SortedSet<string> nameList = new();
                _db.SortedSetScan(_listKey).ToList().ForEach(name =>
                {
                    nameList.Add(name.ToString());
                });
                return View(nameList);
            }
            return View(new SortedSet<string>());
        }
        public IActionResult Add(string name,int score)
        {
            _db.KeyExpire(_listKey, DateTime.Now.AddMinutes(1));
            _db.SortedSetAdd(_listKey, name, score);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
