using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;

namespace _1983.Models
{
    public class PageData
    {
        public List<TableHouse> HouseLoadInfo { get; set; }
        private IDbConnection dbConnection;

        public PageData(IDbConnection dbConnection)
        {
            HouseLoadInfo = new List<TableHouse>();
            this.dbConnection = dbConnection;
        }

        public void ReadTable()
        {
            using (IDbConnection database = dbConnection)
            {
                HouseLoadInfo = database.Query<TableHouse>("SELECT * FROM House").ToList();
            }
        }

    }
}
