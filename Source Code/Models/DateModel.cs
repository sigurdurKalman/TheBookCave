using System;

namespace TheBookCave.Models
{
    public class DateModel
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public DateModel()
        {
            Year = 1;
            Month = 1;
            Day = 1;
        }

        public DateModel(int y, int m, int d)
        {
            Year = y;
            Month = m;
            Day = d;
        }

        public override string ToString()
        {
            return Day + "/" + Month + "/" + Year;
        }

        public static bool operator <(DateModel left, DateModel right)
        {
            int leftTotal = 10000 * left.Year + 100 * left.Month + left.Day;
            int rightTotal = 10000 * right.Year + 100 * right.Month + right.Day;
            return leftTotal < rightTotal;
        }

        public static bool operator >(DateModel left, DateModel right)
        {
            int leftTotal = 10000 * left.Year + 100 * left.Month + left.Day;
            int rightTotal = 10000 * right.Year + 100 * right.Month + right.Day;
            return leftTotal > rightTotal;
        }
    }
}