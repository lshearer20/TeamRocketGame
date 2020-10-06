using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.General.Movement;
using Microsoft.Xna.Framework.Audio;

namespace Team_Rocket_Game.Model.Entities
{
    public abstract class Bullet : Component
    {
        private SoundEffect sound;
        public SoundEffect Sound
        {
            get { return this.sound; }
            set { this.sound = value; }
        }

        protected Movement movement;
        public Movement Movement
        {
            get { return this.movement; }
            set { this.movement = value; }
        }

        protected MovementPattern movementPattern;
        public MovementPattern MovementPattern
        {
            get { return this.movementPattern; }
            set { this.movementPattern = value; }
        }

        protected string texture;
        public string Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public override Texture2D Sprite
        {
            get
            {
                if (this.sprite == null)
                {
                    sprite = Game1.GetTexture(this.texture);
                    this.hitBoxRadius = sprite.Width / 2;
                }
                return this.sprite;
            }
            set
            {
                this.sprite = value;
                this.hitBoxRadius = sprite.Width / 2;
            }
        }

        protected int damage;
        public int Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        public Bullet(Movement movement, string texture)
        {
            this.movement = movement;
            this.texture = texture;
            this.Sprite = Game1.GetTexture(this.texture);
        }

        public void PlaySoundEffect()
        {
            sound.CreateInstance().Play();
        }

        public override void Update(GameTime gameTime, CollisionManager cb)
        {
            if (this.IsDead)
            {
                return;
            }

            //If the projectile has gone off screen then it is destroyed
            if (!this.isInWindow())
            {
                IsDead = true;
                return;
            }

            cb.UdateObjectPositionWithFunction(this, () => { position = movement.NextPoint(); return true; });
        }

        public bool HasBeenFired()
        {
            return this.movement.Moved();
        }

        //Use for factories
        public abstract Bullet Clone();
    }
}