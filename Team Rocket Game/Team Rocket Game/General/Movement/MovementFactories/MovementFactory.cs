using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.General.Movement.MovementFactories
{
    public static class MovementFactory
    {
        //Takes a movement type, origin, direction, and speed to make the corresponding movement
        public static Movement CreateMovement(MovementPattern pattern, Vector2 origin, Vector2 direction, float velocity)
        {
            AbstractMovementFactory factory = MakeFactory(pattern);
            
            if(factory != null)
            {
                return factory.Create(origin, direction, velocity);
            }
            else
            {
                return null;
            }
        }

        //Makes the corresponding movement factory and returns it
        public static AbstractMovementFactory MakeFactory(MovementPattern pattern)
        {
            AbstractMovementFactory factory = null;

            switch(pattern)
            {
                case MovementPattern.Straight:
                    factory = new StraightPattern();
                    break;
                case MovementPattern.Oscillate:
                    factory = new OscillatePattern();
                    break;
            }

            return factory;
        }
    }
}
