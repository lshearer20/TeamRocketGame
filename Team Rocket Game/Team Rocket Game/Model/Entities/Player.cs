using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.Controller.Commands;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Model.Factories;
using Team_Rocket_Game.Model.Factories.BulletFactories;

namespace Team_Rocket_Game.Model.Entities
{
    public class Player : Sprite
    {
        protected static Player instance;

        private bool invincible;
        public bool Invincible
        {
            get { return invincible; }
            set { this.invincible = value; }
        }

        private bool invincibleReset;
        public bool InvincibleReset
        {
            get { return invincibleReset; }
            set { this.invincibleReset = value; }
        }

        private double invincibleResetTime;
        public double InvincibleResetTime
        {
            get { return invincibleResetTime; }
            set { this.invincibleResetTime = value; }
        }

        private bool cheatingOn;
        public bool CheatingOn
        {
            get { return cheatingOn; }
            set { this.cheatingOn = value; }
        }

        private Texture2D cheat;

        //Singleton
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = init();
                    instance.cheatingOn = false;
                    instance.invincible = false;
                    instance.invincibleReset = false;
                }
                return instance;
            }
        }

        //Needs to be static
        private static Player init()
        {
            StreamReader reader = GameData.GetPlayerStreamReader();
            string json = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<Player>(json);
        }

        public static void Reset()
        {
            instance = null;
        }

        public Player() : base()
        {
            this.Sprite = Game1.GetTexture("playerShip");
            this.Sprite2 = Game1.GetTexture("slowMotionSpaceship");
            this.SpriteInvincible = Game1.GetTexture("invincible_ship");
            this.cheat = Game1.GetTexture("CheatMode");
            this.position = new Vector2
            (
                (GameConfig.Width / 2 - (float)this.hitBoxRadius),
                (GameConfig.Height - this.Sprite.Height)
            );
        }

        public void Move(float newX, float newY)
        {
            float oldX = this.position.X, oldY = this.position.Y;
            //Move Player
            this.position.X = newX;
            this.position.Y = newY;
            //If newest user position updates moves the user out of the window 
            if (!this.isInWindow())
            {
                if (!this.IsInWindowWidth())
                {
                    this.position.X = oldX;
                }
                if (!this.IsInWindowHeight())
                {
                    this.position.Y = oldY;
                }
            }
        }

        public override void Update(GameTime gameTime, CollisionManager collider)
        {
            collider.UdateObjectPositionWithFunction(this, () => { base.Update(gameTime, collider); return true; });
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Check if dead
            if (this.isDead)
            {
                if (this.Health == 0)
                {
                    instance = null;
                }
                return;
            }

            //decide which sprite to draw. normal or slow motion sprite.
            base.Draw(spriteBatch);
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                spriteBatch.Draw(this.Sprite2, position, Color.White);
            else if (this.invincibleReset==true || this.invincible == true)
            {
                spriteBatch.Draw(this.SpriteInvincible, position, Color.White);
            }
            else
                spriteBatch.Draw(this.Sprite, position, Color.White);

            if(this.CheatingOn)
            {
                spriteBatch.Draw(this.cheat, new Vector2(0, 0), Color.White);
            }
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

        public override double TakeDamage(double d)
        {
            if (invincible || invincibleReset)
            {
                return 0;
            }

            return base.TakeDamage(d);
        }
    }
}