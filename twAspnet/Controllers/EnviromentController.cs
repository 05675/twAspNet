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

            var option = new DbContextOptionsBuilder<TwaspDbContext>();
            var connectionString = "Twasp.db";
            option.UseSqlite(connectionString);
            using var context = new TwaspDbContext(option.Options);
            var enviroment = context.Enviroment.Single();

            if (!string.IsNullOrEmpty(keyword))
            {
                var tokens = Tokens.Create(enviroment.Akey, enviroment.ASecretKey, enviroment.AToken, enviroment.ATokenSecret);
                var result = tokens.Search.Tweets(count => 100, q => keyword);
                //countは読み込み数。指定しなければDefoultの数値が入る。

                ViewData["result"] = result;

                foreach (var value in result)
                {
                    string scrName = value.User.ScreenName; //@User_ID
                    string name = value.User.Name;          //ユーザー名
                    string text = value.Text;               //Tweet
                    
                    // textBoxStatus.AppendText("@" + scrName.ToString() + " / " + name + System.Environment.NewLine + text + System.Environment.NewLine);
                }
            }
            return View();
        }

        //public IActionResult Search(IFormCollection formCollection)
        //{
        //    string words = formCollection["words"];

        //    var searchResults = new SearchResults(
        //      Tokens.Create(
        //        "",
        //        "",
        //        "",
        //        "")
        //        .Search.Tweets(q: words, lang: "ja", result_type: "recent", count: 100)
        //        .ToList()
        //        .Select(result => new SearchResult(result.Text))
        //        .ToList());

        //    //TempData.Put("searchResults", searchResults);
        //    return RedirectToAction("ShowSearchResults", "Home");
        //}



        public IActionResult ShowSearchResults()
        {
            //ViewData["searchResult"] = TempData.Get<SearchResults>("searchResults");
            return View();
        }
    }
}