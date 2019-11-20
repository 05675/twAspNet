﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTweet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using twAspnet.Models;

namespace twAspnet.Controllers
{
    public class EnviromentController : Controller
    {       
        private void TweetSearch()
        {

            var option = new DbContextOptionsBuilder<TwaspDbContext>();
            var connectionString = "Twasp.db";
            option.UseSqlite(connectionString);
            using var context = new TwaspDbContext(option.Options);
            var enviroment = context.Enviroment.Single();

            string keyword = "電子工作";
            var tokens = Tokens.Create(enviroment.Akey, enviroment.ASecretKey, enviroment.AToken, enviroment.ATokenSecret);
            var result = tokens.Search.Tweets(count => 100, q => keyword);
            //countは読み込み数。指定しなければDefoultの数値が入る。
            foreach (var value in result)
            {
                string scrName = value.User.ScreenName; //@User_ID
                string name = value.User.Name; //ユーザー名
                string text = value.Text; //Tweet
                textBoxStatus.AppendText("@" + scrName.ToString() + " / " + name + System.Environment.NewLine + text + System.Environment.NewLine);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}