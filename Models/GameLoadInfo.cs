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

            for (int i = 1; i < 21; i++)
            {
                HouseList.Add(new House("Yl" + i));
            }
        }
    }
}
