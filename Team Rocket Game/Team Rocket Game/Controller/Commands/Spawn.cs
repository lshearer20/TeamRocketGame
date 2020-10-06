using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Model.Entities;

namespace Team_Rocket_Game.Controller.Commands
{
    public abstract class Spawn : Command
    {
        // execute
        public abstract void execute();

        //Function to allow for random spawn
        public Vector2 GetRandomSpawnPosition(string texture, double spawnInterval)
        {
            double spawn = Math.Abs(spawnInterval * spawnInterval);
            Random rand = new Random(Math.Abs((int)spawn));
            Vector2 ret = new Vector2(rand.Next(0, GameConfig.Width - Game1.GetTexture(texture).Width), -Game1.GetTexture(texture).Height);
            return ret;
        }
    }
}
