using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Dapper;


namespace _1983.Models
{

    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid Hash { get; set; }

        public User(string Login, string Password, IDbConnection connection)
        {
            this.Login = Login;
            this.Password = Password;
            Hash = Guid.NewGuid();

            GameLoadInfo GameDataUser = new GameLoadInfo();
            GameDataUser.GameLoadData();
            string list = JsonConvert.SerializeObject(GameDataUser.HouseList);
            GameDataUser.Hash = Hash;

            using (IDbConnection database = connection)
            {
                database.Execute("INSERT INTO UserInfo (Login, Password, Hash) VALUES ('" + Login + "' , '" + Password + "' , '" + Hash + "' )");

                database.Execute("INSERT INTO House (Hash, MoneyRest, RentPercent, TaxPercent, LevelUpCost, Text1, Text2, Text3) " +
                        "VALUES (@Hash, @MoneyRest, @RentPercent, @TaxPercent, @LevelUpCost, @Text1, @Text2, @Text3)", GameDataUser);
                database.Execute("UPDATE House SET HouseList = '" + list + "' where Hash = '" + Hash + "'");
            }

        }

    }

    

}
