using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Model.Entities;
using System.IO;
using Newtonsoft.Json;
using Team_Rocket_Game.Model.Entities.Bullets;
using Team_Rocket_Game.General.Movement;
using Team_Rocket_Game.General.Movement.MovementFactories;

namespace Team_Rocket_Game.Model.Factories
{
    public abstract class BulletAbstractFactory
    {
        protected string json;
        protected StreamReader input;
        protected AbstractMovementFactory factory;

        public abstract Bullet Create(Vector2 origin);

        //Generic JSON interpreter for bullets
        protected virtual void InitJSON(string basename)
        {
            string dirname = "../../../../JSON/Bullets/";
            this.input = new StreamReader(dirname + basename);
            this.json = this.input.ReadToEnd();
        }
    }

    public abstract class EnemyBulletAbstractFactory : BulletAbstractFactory
    {
        //Need a list for json deserialization
        protected List<EnemyBullet> bullets = new List<EnemyBullet>();

        public override Bullet Create(Vector2 origin)
        {
            //Clone a new bullet from the original bullet
            Bullet bullet = bullets[0].Clone();

            //Set movement
            this.factory = MovementFactory.MakeFactory(bullet.MovementPattern);
            Movement movement = this.factory.Create(origin, new Vector2(0, -1), (float)bullet.Velocity);
            bullet.Movement = movement;

            //Set position of bullet
            bullet.Position = origin;

            return bullet;
        }


        //Create our OG bullet using JSON data
        protected override void InitJSON(string basename)
        {
            //Reads the json file of a given name
            base.InitJSON(basename);

            //Sets the list of bullets to the deserialized bullet
            this.bullets = JsonConvert.DeserializeObject<List<EnemyBullet>>(this.json);
        }
    }
}
