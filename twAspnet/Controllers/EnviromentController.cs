using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTweet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using twAspnet.Models;

namespace twAspnet.Controllers
{
    public class EnviromentController : Controller
    {
        public IActionResult Index(IFormCollection formCollection)
        {
                string keyword = formCollection["search"];

                //DBからkeyを取得
                var option = new DbContextOptionsBuilder<TwaspDbContext>();
                var connectionString = "Twasp.db";
                option.UseSqlite(connectionString);
                using var context = new TwaspDbContext(option.Options);
                var enviroment = context.Enviroment.Single();

                //検索
                if (!string.IsNullOrEmpty(keyword))
                {
                    ViewData["searched"] = true;
                    var tokens = Tokens.Create(enviroment.Akey, enviroment.ASecretKey, enviroment.AToken, enviroment.ATokenSecret);
                    var result = tokens.Search.Tweets(count => 10, q => keyword);
                    ViewData["result"] = result;
                }
                else
                {
                    //検索をしていない
                    ViewData["searched"] = false;
                }
                return View();
        }
        public IActionResult ShowSearchResults()
        {
            return View();
        }
    }
}