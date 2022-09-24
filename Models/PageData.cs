using Dapper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;

namespace _1983.Models
{
    public class PageData
    {
        public GameLoadInfo GameDataUser { get; set; }
        public string GameDataUserJson { get; set; }

        public PageData()
        {
            GameDataUser = new GameLoadInfo();
            GameDataUserJson = JsonConvert.SerializeObject(GameDataUser);
        }




        // Предача информации с базы данных в лист

        //    public List<TableHouse> HouseLoadInfo { get; set; }
        //    private IDbConnection dbConnection;

        //    public PageData(IDbConnection dbConnection)
        //    {
        //        HouseLoadInfo = new List<TableHouse>();
        //        this.dbConnection = dbConnection;
        //    }

        //    public void ReadTable()
        //    {
        //        using (IDbConnection database = dbConnection)
        //        {
        //            HouseLoadInfo = database.Query<TableHouse>("SELECT * FROM House").ToList();
        //        }
        //    }

    }
}
