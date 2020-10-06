using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.Model.Factories;
using Team_Rocket_Game.General;

namespace Team_Rocket_Game.Model.Entities
{
    public abstract class Sprite : Component
    {
        protected double gameTimeCheck, timecheck, playertimecheck;
        #region Fields
        protected Stopwatch timeAlive;
        public Stopwatch TimeAlive
        {
            get { return this.timeAlive; }
            set { this.timeAlive = value; }
        }

        protected Stopwatch timeInvincible;
        public Stopwatch TimeInvincible
        {
            get { return this.timeInvincible; }
            set { this.timeInvincible = value; }
        }


        protected double? lastFiredTime;
        public Nullable<double> LastFiredTime
        {
            get { return this.lastFiredTime; }
            set { this.lastFiredTime = value; }
        }

        protected double fireRate;
        public double FireRate
        {
            get { return this.fireRate; }
            set { this.fireRate = value; }
        }

        //This contains the amount of health a character has left
        protected double health;
        public double Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        //This contains the initial health that the character spawned with
        protected double maxHealth;
        public double MaxHealth
        {
            get { return this.maxHealth; }
            set { this.maxHealth = value; }
        }

        protected BulletAbstractFactory BulletFactory;
        protected List<Bullet> bullets;

        public List<Bullet> Bullets
        {
            get { return this.bullets; }
            set { this.bullets = value; }
        }

        #endregion

        // Constructor
        public Sprite() : base()
        {
            this.health = 5;
            this.timeAlive = new Stopwatch();
            this.timeAlive.Start();
            this.bullets = new List<Bullet>();
        }

        #region Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, position, Color.White);
        }

        // Update the status of the sprite
        public override void Update(GameTime gameTime, CollisionManager cb) // list<sprite> will be for determining if player is hit by bullet or enemy
        {
            if (this is Player)
            {
                if (Player.Instance.InvincibleReset == true && this.timeInvincible.ElapsedMilliseconds > 2000)
                {
                    Player.Instance.InvincibleReset = false;
                }
            }
        }

        /* Returns the remaining amount after it is delt to the character */
        public virtual double TakeDamage(double amount)
        {
            double overflow = 0;
            this.health -= amount;
            if (this.health <= 0)
            {
                overflow = Math.Abs(this.health);
                this.isDead = true;
            }
            if(this is Player)
            {
                Player.Instance.InvincibleReset = true;
                this.timeInvincible = new Stopwatch();
                this.timeInvincible.Start();
                this.position = new Vector2
            (
                (GameConfig.Width / 2 - (float)this.hitBoxRadius),
                (GameConfig.Height - this.Sprite.Height)
            );
            }
            return overflow;
        }

        protected bool CanShoot()
        {
            //sprite has fired a bullet
            if (this.lastFiredTime.HasValue)
            {
                //if the sprite has to wait longer until they can shoot again
                if (this.timeAlive.ElapsedMilliseconds < this.lastFiredTime + this.fireRate)
                {
                    return false;
                }
            }
            //sets last bullet shot time to current time
            this.lastFiredTime = this.timeAlive.ElapsedMilliseconds;
            return true;
        }

        #endregion
    }
}