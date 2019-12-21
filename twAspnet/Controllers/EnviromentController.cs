using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private readonly TwaspDbContext context;

        public EnviromentController(TwaspDbContext context)
        {
            this.context = context;
        }
        public NameValueCollection Form { get; }

        private IActionResult Search(string keyword)
        {
            var enviroment = context.Enviroment.Single();
            ViewData["searchKey"] = keyword;

            //検索実行
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
            return View("~/Views/Enviroment/Index.cshtml");
        }
        public IActionResult Index(IFormCollection formCollection)
        {
            string keyword = formCollection["search"];
            return Search(keyword);
        }
        public IActionResult RegisterFavorite(IFormCollection formCollection)
        {
            var searchTweet = formCollection["searchTweet"];
            var searchScreenName = formCollection["searchScreenName"];
            var searchName = formCollection["searchName"];
            var searchId = formCollection["searchId"];
            var searchCreatedAt = formCollection["searchCreatedAt"];
           
            var favorite = new Favorite
            {
                Tweet = searchTweet,
                ScreenName = searchScreenName,
                Name = searchName,
                UrlId = searchId,
                CreatedAt = searchCreatedAt,
                Favoritedate = DateTime.Now,
            };

            //DBへInsert
            context.Favorite.Add(favorite);
            //Commit
            context.SaveChanges();

            string keyword = formCollection["searchKey"];

            return Search(keyword);
        }
    }
}