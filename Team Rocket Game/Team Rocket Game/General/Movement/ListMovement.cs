using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Team_Rocket_Game.General.Movement
{
    //Allows for movement that is not a straight line
    public class ListMovement : Movement
    {
        private List<Vector2> points;
        public List<Vector2> Points
        {
            get { return points; }
            set { this.points = value; }
        }

        //To track where in the list we are
        private int listIndex;

        //Constructor
        public ListMovement(List<Vector2> points)
        {
            this.points = points;
        }

        public override Vector2 GetVelocityVector()
        {
            Vector2 velocityVector;

            if(listIndex > 0)
            {
                velocityVector = (points[listIndex] - points[listIndex - 1]);
            }
            else
            {
                velocityVector = points[listIndex] - points[points.Count - 1];
            }

            return velocityVector;
        }

        public override bool Moved()
        {
            //Will be zero if we have not moved through the list
            return this.listIndex == 0;
        }

        public override Vector2 NextPoint()
        {
            //Collision
            if(!(this.offset.X == 0 && this.offset.Y == 0))
            {
                return this.offset;
            }

            Vector2 nextPoint = new Vector2();

            //Grab next point in the list
            if (listIndex + 1 < points.Count)
            {
                nextPoint = points[listIndex++];
            }

            //Check if we've finished the list. If so, reset
            if(listIndex >= points.Count)
            {
                listIndex = 0;
            }

            return nextPoint;
        }
    }
}
