using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.General.Movement;
using Team_Rocket_Game.Controller.Collision;

namespace Team_Rocket_Game.Model.Entities.Bullets
{
    public class PlayerBullet : Bullet
    {
        public PlayerBullet(Movement movement, string texture) : base(movement, texture)
        {
            this.movement = movement;
            this.Sprite = Game1.GetTexture(this.texture);
            this.Sound = Game1.GetSound("player_shot");
        }

        public override Bullet Clone()
        {
            PlayerBullet copy = new PlayerBullet(this.movement, this.texture);
            copy.Texture = this.texture;
            copy.Velocity = this.velocity;
            copy.position = this.position;
            copy.Movement = this.movement;
            copy.Acceleration = this.acceleration;
            copy.Damage = this.Damage;
            copy.MovementPattern = this.movementPattern;
            copy.Sprite = this.sprite;
            copy.Sound = this.Sound;
            return copy;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, position, Color.White);
        }

        public override void Update(GameTime gameTime, CollisionManager cb)
        {
            base.Update(gameTime, cb);
            cb.HandlePlayerBulletCollisions(this);
        }
    }
}
