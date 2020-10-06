using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.General;

namespace Team_Rocket_Game.Model.Entities
{
    public abstract class Component : Subject
    {
        //Radius from position to determine collision
        protected double hitBoxRadius;
        public double HitBoxRadius
        {
            get { return this.hitBoxRadius; }
            set { this.hitBoxRadius = value; }
        }

        protected Texture2D sprite;
        public virtual Texture2D Sprite
        {
            get { return this.sprite; }
            set
            {
                this.sprite = value;
                this.hitBoxRadius = sprite.Width / 2;
            }
        }
        protected Texture2D sprite2;
        public virtual Texture2D Sprite2
        {
            get { return this.sprite2; }
            set
            {
                this.sprite2 = value;
                this.hitBoxRadius = sprite2.Width / 2;
            }
        }
        protected Texture2D spriteInvincible;
        public virtual Texture2D SpriteInvincible
        {
            get { return this.spriteInvincible; }
            set
            {
                this.spriteInvincible = value;
                this.hitBoxRadius = spriteInvincible.Width / 2;
            }
        }

        protected bool isDead;
        public bool IsDead
        {
            get { return this.isDead; }
            set { this.isDead = value; }
        }

        protected Vector2 position;
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        protected Vector2 positionForInv;
        public Vector2 PositionForInv
        {
            get { return this.positionForInv; }
            set { this.positionForInv = value; }
        }

        protected double velocity;
        public double Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }

        protected double acceleration = 1;
        public double Acceleration
        {
            get { return this.acceleration; }
            set { this.acceleration = value; }
        }

        //constructor
        public Component()
        {
            
        }

        // Methods
        // public draw function
        public virtual void Draw(SpriteBatch spriteBatch) { }

        // Update the game
        public virtual void Update(GameTime gameTime, CollisionManager cb) { }

        // checks if the game object is in the game window
        public bool isInWindow()
        {
            return this.IsInWindowWidth() && this.IsInWindowHeight();
        }

        public bool IsInWindowWidth()
        {
            if (sprite == null) return false;
            float adjustedWidth = position.X + sprite.Width;
            if (position.X < 0 || (adjustedWidth > GameConfig.Width))
            {
                return false;
            }
            return true;
        }

        public bool IsInWindowHeight()
        {
            if (sprite == null) return false;
            float adjustedHeight = position.Y + sprite.Height;
            if (position.Y < 0 || (adjustedHeight > GameConfig.Height))
            {
                return false;
            }
            return true;
        }

        public virtual Vector2 GetCenterCoordinates()
        {
            return new Vector2(this.Position.X + (float)this.Sprite.Width / 2, this.Position.Y + this.Sprite.Height / 2);
        }

        public bool BoundsContains(Component obj)
        {
            Vector2 centerCoordinates = obj.GetCenterCoordinates();

            return BoundsContains(centerCoordinates)
                || BoundsContains(new Vector2(centerCoordinates.X + obj.Sprite.Width / 2, centerCoordinates.Y))
                || BoundsContains(new Vector2(centerCoordinates.X - obj.Sprite.Width / 2, centerCoordinates.Y))
                || BoundsContains(new Vector2(centerCoordinates.X, centerCoordinates.Y + obj.Sprite.Height / 2))
                || BoundsContains(new Vector2(centerCoordinates.X, centerCoordinates.Y - obj.Sprite.Height / 2));
        }
        // checks if pos is in the sprite texture bounds
        public bool BoundsContains(Vector2 coords)
        {
            if (
                (coords.X >= GetCenterCoordinates().X - sprite.Width / 2 && coords.X <= GetCenterCoordinates().X + sprite.Width / 2)
                &&
                (coords.Y >= GetCenterCoordinates().Y - sprite.Height / 2 && coords.Y <= GetCenterCoordinates().Y + sprite.Height / 2)
                )
            {
                return true;
            }
            return false;
        }
    }
}
