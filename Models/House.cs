namespace _1983.Models
{
    public class House
    {
        public string Position { get; set; } // расположение
        public int Level { get; set; } // уровень
        public int DaysForLevelDown { get; set; } // остаток дней до понижения уровня домика
        public double Visability { get; set; }


        public House(string position)
        {
            Position = position;
            DaysForLevelDown = 0;
            Visability = 1;

            if (position == "Yl_1" || position == "Yl_2" || position == "Yl_3")
            {
                Level = 1;
            }
            else
            {
                Level = 0;
            }
        }
    }
}

