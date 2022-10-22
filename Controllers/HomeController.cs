using _1983.Models;
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
       //bool autorisation = false; //проверка авторизации пользователя, будет брать инфо по хэшу

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


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
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
                return View(Login());
            }
            //else // если пользователь не авторизован, домики по умолчанию
            //{
            //    PageData pageData2 = new PageData(dbConnection);
            //    Response.Cookies.Append("Hash", pageData2.GameDataUser.Hash.ToString());
            //    return View(pageData2);

            //}
        }

        public IActionResult Menu()
        {
            return View();
        }



        //Сохранение
        public IActionResult GameSave(IFormCollection dataSave)
        {
            GameLoadInfo gameData = JsonConvert.DeserializeObject<GameLoadInfo>(dataSave["datainfo"]); //десериализация
            Guid guid = new Guid(Request.Cookies["Hash"]);

            string list = JsonConvert.SerializeObject(gameData.HouseList); // КОСТЫЛЬ МОЕЙ МЕЧТЫ
            GameLoadInfoDb json = new GameLoadInfoDb();

            json.HouseList = list;
            json.Hash = gameData.Hash;
            json.TaxPercent = gameData.TaxPercent;
            json.MoneyRest = gameData.MoneyRest;
            json.RentPercent = gameData.RentPercent;
            json.LevelUpCost = gameData.LevelUpCost;
            json.Text1 = gameData.Text1;
            json.Text2 = gameData.Text2;
            json.Text3 = gameData.Text3;



            //string path = "C:\\Users\\Taras Ponomarov\\Desktop\\qwe.txt";
            //FileStream file = System.IO.File.Open((path), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //StreamWriter writer = new StreamWriter(file);
            //writer.Write(list);
            //writer.Close();




            using (IDbConnection database = dbConnection)
            {
                database.Execute("UPDATE House SET " +
                    "MoneyRest=@MoneyRest, " +
                    "RentPercent = @RentPercent, " +
                    "TaxPercent = @TaxPercent, " +
                    "LevelUpCost = @LevelUpCost, " +
                    "Text1 = @Text1, " +
                    "Text2 = @Text2, " +
                    "Text3 = @Text3, " +
                    "HouseList= @HouseList " +
                    "where Hash = '" + guid + "'", json);
            }



            //}

            return Json(DateTime.Now.ToString("g") + " : " + dataSave["datainfo"]);
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
                    return RedirectToAction("Login");
                }       
            }
        }

        public IActionResult RegisterLogin(IFormCollection collect)
        {

            User newUser = new User(collect["name"], collect["password"]);

            Response.Cookies.Append("Hash", newUser.Hash.ToString());

            GameLoadInfo GameDataUser = new GameLoadInfo();
            GameDataUser.GameLoadData();
            string list = JsonConvert.SerializeObject(GameDataUser.HouseList);
            GameDataUser.Hash = newUser.Hash;

            using (IDbConnection database = dbConnection)
            {
                database.Execute("INSERT INTO UserInfo (Login, Password, Hash) VALUES (@Login, @Password, @Hash)", newUser);
               
                database.Execute("INSERT INTO House (Hash, MoneyRest, RentPercent, TaxPercent, LevelUpCost, Text1, Text2, Text3) " +
                        "VALUES (@Hash, @MoneyRest, @RentPercent, @TaxPercent, @LevelUpCost, @Text1, @Text2, @Text3)", GameDataUser);
                database.Execute("UPDATE House SET HouseList = '" + list + "' where Hash = '" + newUser.Hash + "'");
            }

                return RedirectToAction("Game");
        }




        //public IActionResult RegisterLogin(string log)
        //{
        //    //string login = log;
        //    //Response.Cookies.Append("123", Guid.NewGuid().ToString());
        //    //Insert

        //    return RedirectToAction("Game");
        //}






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







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
