using Microsoft.AspNetCore.Mvc;
using RedisExchangeApp.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeApp.Web.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        private string _listKey = "HashNames";


        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(2);
        }
        public IActionResult Index()
        {
            if (_db.KeyExists(_listKey))
            {
                HashSet<string> namesList = new();
                _db.SetMembers(_listKey).ToList().ForEach(name =>
                {
                    namesList.Add(name.ToString());
                });
                return View(namesList);
            }
            return View(new HashSet<string>());
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            if (!_db.KeyExists(_listKey))
            {
                _db.KeyExpire(_listKey, DateTime.Now.AddMinutes(5));
            }

            _db.SetAdd(_listKey, name);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
