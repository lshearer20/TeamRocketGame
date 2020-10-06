using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.Controller.Commands;
using Team_Rocket_Game.General.Movement;

namespace Team_Rocket_Game.Model.Entities.Enemies
{
    public class MidBoss : Enemy
    {
        public MidBoss(BulletManager bulletManager, GameData gameData) : base(bulletManager)
        {
            this.BulletFactory = new EnemyBulletFactory();
            this.sprite = Game1.GetTexture("midboss");
        }

        public override void Update(GameTime gameTime, CollisionManager cb)
        {
            base.Update(gameTime, cb);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (!isDead)
            {
                spriteBatch.Draw(this.Sprite, position, Color.White);
            }
        }
    }
}
