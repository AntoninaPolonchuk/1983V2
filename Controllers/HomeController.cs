﻿using _1983.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace _1983.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;      
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        public IDbConnection dbConnection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyHouse")); // соединение с базой
            }
        }

        public IActionResult Login(bool State, string Text)
        {
            RegisrtationInfo registration = new RegisrtationInfo(State, Text);

            string path = "C:\\Users\\Taras Ponomarov\\Desktop\\qwe.txt";
            FileStream file = System.IO.File.Open((path), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(registration.RegInfo);
            writer.Close();

            return View(registration);

        }
            
        public IActionResult Game()
        {

            if (Request.Cookies.ContainsKey("Hash") == true)
            {
                PageData pageData1 = new PageData(dbConnection, new Guid(Request.Cookies["Hash"])); // передаем содинение и хеш на страницу
                return View(pageData1);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult CheckLogin(IFormCollection collect)
        {
            string login = collect["name"];
            string password = collect["password"];
            

            using (IDbConnection database = dbConnection)
            {
                var hashinfo = database.Query("select hash from UserInfo where Login = '" + login + "' AND Password = '" + password + "'").FirstOrDefault();

                if (hashinfo != null)
                {
                    Guid hash = hashinfo.hash;
                    Response.Cookies.Append("Hash", hash.ToString());
                    return RedirectToAction("Game");
                }
                else
                {
                    return RedirectToAction("Login", new {State = false, Text = "неверно введен логин, или пароль"});
                }       
            }
        }


        public IActionResult Register()
        {    
            return RedirectToAction("Login", new {State = true});
        }


        public IActionResult RegisterLogin(IFormCollection collect)
        {
            User newUser = new User(collect["name"], collect["password"], dbConnection);
            Response.Cookies.Append("Hash", newUser.Hash.ToString());

            return RedirectToAction("Game");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}



//Запрос в базу на соответствие логин - пароль
//Response.Cookies.Append("123", хеш)

//string path = "C:\\Users\\Taras Ponomarov\\Desktop\\qwe.txt";
//FileStream file = System.IO.File.Open((path), FileMode.OpenOrCreate, FileAccess.ReadWrite);
//StreamWriter writer = new StreamWriter(file);
//writer.Write(login);
// writer.Close();


//using (IDbConnection database = dbConnection)
//{
//    database.QuerySingle("SELECT Hash FROM House WHERE Name = '" + login + "'");
//}


//return RedirectToAction("Game");
//return Json(hash);

