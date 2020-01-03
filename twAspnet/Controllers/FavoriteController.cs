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
        public IActionResult Index()
        {
            //select * from Favorite;と同じ意味。かつList化
            List<Favorite> favorite = context.Favorite.ToList();

            var uName = User.Claims.FirstOrDefault(_uName => _uName.ValueType == "ScreenName")?.Value;
            ViewData["searched"] = true;
            ViewData["uName"] = uName;
            return View(favorite);
        }
    }
}