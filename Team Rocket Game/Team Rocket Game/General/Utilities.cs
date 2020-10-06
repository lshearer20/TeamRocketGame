using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Model.Entities;

namespace Team_Rocket_Game.General
{
    //A static utilities class
    public static class Utilities
    {
        public static readonly Random RANDOM = new Random();

        public static int randInt(int min, int max)
        {
            return RANDOM.Next(min, max);
        }

        public static double randDouble(double min, double max)
        {
            return RANDOM.NextDouble() * (max - min) + min;
        }
    }
}
