using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace _1983.Models
{
    public class GameLoadInfo
    {
        public Guid Hash { get; set; }
        public int MoneyRest { get; set; } 
        public int RentPercent { get; set; } 
        public int TaxPercent { get; set; } 
        public int LevelUpCost { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public List<House> HouseList { get; set; }
        public List<Random> RandomList { get; set; }



        public void GameLoadData()
        {
            Hash = Guid.NewGuid();
            MoneyRest = 10000000;
            RentPercent = 100;
            TaxPercent = 100;
            LevelUpCost = 100;

            HouseList = new List<House>();

            for (int i = 1; i < 20; i++)
            {
                HouseList.Add(new House("Yl_" + i));
            }
        }
    }
}
