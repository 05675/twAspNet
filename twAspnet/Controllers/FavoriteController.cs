using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using twAspnet.Models;

namespace twAspnet.Controllers
{
    //[Authorize]
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
            var uId = User.Claims.FirstOrDefault(_ => _.Type == "UserId")?.Value;
            List<Favorite> favorite = context.Favorite.Where(f => f.RegisterUserId == uId).ToList();
            ViewData["uId"] = uId;
            return View(favorite);
        }
    }
}