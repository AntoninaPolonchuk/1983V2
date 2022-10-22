using Dapper;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace _1983.Models
{
    public class PageData
    {
        public GameLoadInfo GameDataUser { get; set; }
        public string GameDataUserJson { get; set; }
        public GameLoadInfo GameDataFromDb { get; set; }
        public List<GameLoadInfoDb> GameDataFromDbTest { get; set; }

        private IDbConnection Connect;

        public List<House> houses = new List<House>();


        public PageData(IDbConnection dbConnection, Guid hash)
        {
            GameDataFromDb = new GameLoadInfo();
            Connect = dbConnection;
            ReadTable(hash);
            GameDataUserJson = JsonConvert.SerializeObject(GameDataFromDb);
        }


        public void ReadTable(Guid hash)
        {

            using (IDbConnection database = Connect)
            {
                GameDataFromDbTest = database.Query<GameLoadInfoDb>("SELECT * FROM House where Hash = " + "'" + hash + "'").ToList();

                var q = GameDataFromDbTest[0].HouseList;
                var res = JsonConvert.DeserializeObject<House[]>(q);
                for (int i = 0; i < res.Length; i++)
                {
                    houses.Add(res[i]);
                }

                GameDataFromDb.Hash = GameDataFromDbTest[0].Hash;
                GameDataFromDb.MoneyRest = GameDataFromDbTest[0].MoneyRest;
                GameDataFromDb.RentPercent = GameDataFromDbTest[0].RentPercent;
                GameDataFromDb.TaxPercent = GameDataFromDbTest[0].TaxPercent;
                GameDataFromDb.LevelUpCost = GameDataFromDbTest[0].LevelUpCost;
                GameDataFromDb.Text1 = GameDataFromDbTest[0].Text1;
                GameDataFromDb.Text2 = GameDataFromDbTest[0].Text2;
                GameDataFromDb.Text3 = GameDataFromDbTest[0].Text3;
                GameDataFromDb.HouseList = houses;


                //string path = "C:\\Users\\Taras Ponomarov\\Desktop\\qwe.txt";
                //FileStream file = File.Open((path), FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //StreamWriter writer = new StreamWriter(file);

                //writer.Write(q);
                //writer.Close();

            }
        }


    }
}
