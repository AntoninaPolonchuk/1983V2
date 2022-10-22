using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1983.Models
{
    public class GameLoadInfoDb
    {
        public Guid Hash { get; set; }
        public int MoneyRest { get; set; } 
        public int RentPercent { get; set; } 
        public int TaxPercent { get; set; } 
        public int LevelUpCost { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string HouseList { get; set; }

    }

}
