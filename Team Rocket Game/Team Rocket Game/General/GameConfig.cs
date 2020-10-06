using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.General
{
    //Static class for game configuration variables
    //Useful for letting the user change things in settings
    public static class GameConfig
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static MoveKeys MoveKeys { get; set; }
    }
}