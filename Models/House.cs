namespace _1983.Models
{
    public class House
    {
        public string Position { get; set; } // расположение
        public int Level { get; set; } // уровень
        public int DaysForLevekDown { get; set; } // остаток дней до понижения уровня домика


        public House(string position)
        {
            Position = position;
            Level = 0;
            DaysForLevekDown = 10;
        }
    }
}

