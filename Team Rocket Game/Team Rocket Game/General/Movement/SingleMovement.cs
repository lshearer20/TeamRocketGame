using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.General.Movement
{
    //For the case where the object is moving in a straight line
    public class SingleMovement : Movement
    {
        private Vector2 originalPosition;
        private float velocity;
        private Vector2 currentPosition;
        
        //Constructor
        public SingleMovement(Vector2 origin, Vector2 direction, float velocity)
        {
            this.velocity = velocity;
            this.originalPosition = origin;
            this.currentPosition = origin;
            this.offset = direction;
        }

        public override Vector2 CurrentPosition()
        {
            return this.currentPosition;
        }

        public override Vector2 GetVelocityVector()
        {
            return (this.offset) * velocity;
        }

        public override void Reset(Vector2? origin = null)
        {
            //If no origin, just set position to original position
            //Else, object has moved
            if (origin == null)
            {
                currentPosition = originalPosition;
            }
            else
            {
                currentPosition = originalPosition;
                originalPosition = origin.Value;
            }
        }

        //Set next point
        public override Vector2 NextPoint()
        {
            //Need to check if object goes out of bounds first
            currentPosition += (this.offset) * velocity;
            return currentPosition;
        }

        //Check if this has moved
        public override bool Moved()
        {
            if(this.currentPosition != this.originalPosition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}