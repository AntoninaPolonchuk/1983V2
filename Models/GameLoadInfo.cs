using System;
using System.Collections.Generic;

namespace _1983.Models
{
    public class GameLoadInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Hash { get; set; }

        public int MoneyRest { get; set; } // остаток денег
        public int RentPercent { get; set; } // процент аренды
        public int TaxPercent { get; set; } // процент налогов

        public List<House> HouseList { get; set; }

        public GameLoadInfo()
        {
            Id = 0;
            Name = "Й";
            Hash = Guid.NewGuid();
            MoneyRest = 100000000;
            RentPercent = 100;
            TaxPercent = 100;

            HouseList = new List<House>();

            
            HouseList.Add(new House("Yl1"));
            HouseList.Add(new House("Yl2"));
            HouseList.Add(new House("Yl3"));
            HouseList.Add(new House("Yl4"));
            HouseList.Add(new House("Yl5"));
            HouseList.Add(new House("Yl6"));
            HouseList.Add(new House("Yl7"));
            HouseList.Add(new House("Yl8"));
            HouseList.Add(new House("Yl9"));
            HouseList.Add(new House("Yl10"));
            HouseList.Add(new House("Yl11"));

        }
    }
}
