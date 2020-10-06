using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Team_Rocket_Game.General.Movement;

namespace Team_Rocket_Game.General.Movement.MovementFactories
{
    //Only case of a straight movement pattern - direction is only change
    public class StraightPattern : AbstractMovementFactory
    {
        public override Movement Create(Vector2 origin, Vector2 direction, float velocity)
        {
            return new SingleMovement(origin, direction, velocity);
        }
    }
}
