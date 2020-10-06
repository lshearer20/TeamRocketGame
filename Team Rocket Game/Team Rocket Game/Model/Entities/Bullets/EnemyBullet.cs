using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.General.Movement;

namespace Team_Rocket_Game.Model.Entities.Bullets
{
    public class EnemyBullet : Bullet
    {
        public EnemyBullet(Movement movement, string texture) : base(movement, texture)
        {
            this.movement = movement;
            this.Sprite = Game1.GetTexture(this.texture);
            this.Sound = Game1.GetSound("enemy_shot");
        }

        public override Bullet Clone()
        {
            EnemyBullet copy = new EnemyBullet(this.movement, this.texture);
            copy.Texture = this.texture;
            copy.Velocity = this.velocity;
            copy.Position = this.position;
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
            cb.HandleEnemyBulletCollisions(this);
        }
    }
}
