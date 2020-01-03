using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using twAspnet.Models;

namespace twAspnet.Controllers
{
    public class FavoriteController : Controller
    {
        //コンテキスト
        private readonly TwaspDbContext context;

        //コンストラクタ
        public FavoriteController(TwaspDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(string screenName)
        {
            //select * from Favorite;と同じ意味。かつList化
            List<Favorite> favorite = context.Favorite.ToList();

            var uName = User.Claims.FirstOrDefault(_ => _.Type == "ScreenName").Value;
            
            ViewData["uName"] = uName;

            return View(favorite);
        }
    }
}