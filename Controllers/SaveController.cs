using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;
using _1983.Models;
using System;
using Newtonsoft.Json;
using Dapper;

namespace _1983.Controllers
{
    public class SaveController : Controller
    {
        private readonly IConfiguration _config;

        public SaveController(IConfiguration configuration)
        {    
            _config = configuration;
        }

        public IDbConnection dbConnection
        {
            get { return new SqlConnection(_config.GetConnectionString("MyHouse")); } 
        }

        //Сохранение
        public IActionResult GameSave(IFormCollection dataSave)
        {
            GameLoadInfo gameData = JsonConvert.DeserializeObject<GameLoadInfo>(dataSave["datainfo"]); 
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

            return Json(DateTime.Now.ToString("g") + " : " + dataSave["datainfo"]);
        }
    }
}
