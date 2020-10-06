using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Team_Rocket_Game.General.Movement
{
    public class OscillateMovement : Movement
    {
        private float velocity;
        private int direction; //-1 for left, 1 for right
        private SingleMovement move;

        public OscillateMovement(Vector2 origin, float velocity)
        {
            this.velocity = velocity;

            //Set direction to go right first
            this.direction = 1;

            move = new SingleMovement(origin, new Vector2(1, 0), velocity);
        }

        //Called if the sprite has hit the window and switches directions if so
        public void SwitchDirections(Vector2 origin)
        {
            if(direction == 1)
            {
                direction = -1;
                move = new SingleMovement(origin, new Vector2(-1, 0), this.velocity);

            }
            else if (direction == -1)
            {
                direction = 1;
                move = new SingleMovement(origin, new Vector2(1, 0), this.velocity);
            }
        }

        public override Vector2 GetVelocityVector()
        {
            return this.move.GetVelocityVector();
        }

        public override bool Moved()
        {
            return this.move.Moved();
        }

        public Vector2 NextOscillatedPoint(bool switchDirections)
        {
            //Checks if direction should be switched
            if (switchDirections)
            {
                SwitchDirections(this.CurrentPosition());
            }

            return this.NextPoint();
        }

        public override Vector2 NextPoint()
        {
            //Grabs next point based on direction
            return this.move.NextPoint();
        }
    }
}