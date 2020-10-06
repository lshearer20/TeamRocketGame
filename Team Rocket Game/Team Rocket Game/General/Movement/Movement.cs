using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.General.Movement
{
    //Used for movement patterns for enemies, enemy bullets, and player bullets
    public abstract class Movement
    {
        public virtual void Reset(Nullable<Vector2> origin = null)
        {

        }

        public abstract Vector2 NextPoint();

        protected Boolean deflected = false;
        public Boolean Deflected
        {
            get { return this.deflected; }
            set { this.deflected = value; }
        }

        public Vector2 offset = new Vector2(0, 0);

        public abstract Vector2 GetVelocityVector();
        public void SetVelocityVector(Vector2 vector)
        {
            this.offset = vector;

            // new velocity vector breaks path, deflected bullet
            this.deflected = true;
        }

        public abstract bool Moved();

        public virtual Vector2 CurrentPosition()
        {
            return new Vector2(0, 0);
        }
    }
}
