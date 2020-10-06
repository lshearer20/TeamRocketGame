using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.General.Movement
{
    public abstract class AbstractMovementFactory
    {
        public abstract Movement Create(Vector2 origin, Vector2 direction, float speed);
    }
}
