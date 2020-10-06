using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.General.Movement.MovementFactories
{
    //So bosses can go back and forth across the screen
    public class OscillatePattern : AbstractMovementFactory
    {
        public override Movement Create(Vector2 origin, Vector2 direction, float velocity)
        {
            return new OscillateMovement(origin, velocity);
        }
    }
}
