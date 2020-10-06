using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.Controller.Commands;
using Team_Rocket_Game.General.Movement;
using Team_Rocket_Game.Model.Factories;
using Team_Rocket_Game.Controller;

namespace Team_Rocket_Game.Model.Entities
{
    public class Enemy : Sprite
    {
        protected static int enemySpawn = 5;

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

        private BulletManager bulletManager;

        private EnemyType enemyType;
        public EnemyType EnemyType
        {
            get { return enemyType; }
            set { enemyType = value; }
        }

        //Constructor
        public Enemy(BulletManager bulletManager) : base()
        {
            this.bulletManager = bulletManager;
        }

        //Function to allow for random spawn
        public Vector2 GetRandomSpawnPosition(double spawnInterval)
        {
            Vector2 ret = new Vector2(-1000, -1000);

            //Generates spawn point until sprite is in the game window
            while (Math.Abs(ret.X) > GameConfig.Width || Math.Abs(ret.Y) > (GameConfig.Height / 5))
            {
                Random rand = new Random();
                ret = new Vector2(rand.Next(0, GameConfig.Width - this.sprite.Width), rand.Next(0, GameConfig.Height - this.sprite.Height));
            }

            return ret;
        }

        public bool Shoot()
        {
            //Check if they have met the fireRate req. to shoot
            if (!this.CanShoot())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void Update(GameTime gameTime, CollisionManager cb)
        {
            cb.UdateObjectPositionWithFunction(this, () =>
            {
                if (!this.isDead)
                {
                    if (this.BoundsContains(Player.Instance) || Player.Instance.BoundsContains(this))
                    {
                        Player.Instance.TakeDamage(1);
                        this.TakeDamage(1);
                    }

                    //Now handle movement. First check if they have movement, then oscillation, then all other cases
                    if(this.movementPattern == MovementPattern.None)
                    {
                            //Shoot
                            if (this.Shoot())
                            {
                                bulletManager.EnemyShoot(this.enemyType);
                            }
                    }
                    else if (this.movementPattern == MovementPattern.Oscillate)
                    {
                        OscillateMovement move = new OscillateMovement(movement.CurrentPosition(), (float)this.velocity);

                        //First check if the object has hit the window. Flip direction if it did
                        if (position.X > GameConfig.Height)
                        {
                            //Move enemy to next point
                            position = move.NextOscillatedPoint(true); //pass it true so it switches directions
                            //Shoot
                            if (this.Shoot())
                            {
                                bulletManager.EnemyShoot(this.enemyType);
                            }
                        }
                        else
                        {
                            //Move enemy to next point
                            position = move.NextOscillatedPoint(false);
                            //Shoot
                            if (this.Shoot())
                            {
                                bulletManager.EnemyShoot(this.enemyType);
                            }
                        }
                    }
                    else
                    {
                        //Move Enemy to new position in its set path
                        position = movement.NextPoint();
                        // enemy went off screen without dying, so destroy it
                        if (position.Y > GameConfig.Height)
                        {
                            this.IsDead = true;
                            movement.Reset();
                        }
                        else
                        {
                            //Shoot
                            if (this.Shoot())
                            {
                                bulletManager.EnemyShoot(this.enemyType);
                            }
                        }
                    }
                }
                base.Update(gameTime, cb);
                return true;
            });
        }
    }
}
