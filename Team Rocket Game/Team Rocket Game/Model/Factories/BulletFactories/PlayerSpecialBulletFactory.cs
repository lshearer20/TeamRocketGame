using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.General.Movement;
using Team_Rocket_Game.General.Movement.MovementFactories;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.Model.Entities.Bullets;

namespace Team_Rocket_Game.Model.Factories.BulletFactories
{
    public class PlayerSpecialBulletFactory : BulletAbstractFactory
    {
        //Need a list for json deserialization
        protected List<PlayerBullet> bullets = new List<PlayerBullet>();

        //Our constructor that reads the player bullet data from a json file
        public PlayerSpecialBulletFactory()
        {
            //Read in player bullet stats
            this.InitJSON("PlayerBulletSpecial.json");
            this.json = this.input.ReadToEnd();
        }

        //Create our OG bullet using JSON data
        protected override void InitJSON(string basename)
        {
            //Reads the json file of a given name
            base.InitJSON(basename);

            //Sets the list of bullets to the deserialized bullet
            this.bullets = JsonConvert.DeserializeObject<List<PlayerBullet>>(this.json);
        }

        //Create our PlayerBullet
        public override Bullet Create(Vector2 origin)
        {
            //Clone new player bullet from our OG bullet
            Bullet bullet = bullets[0].Clone();

            //Set movement
            this.factory = MovementFactory.MakeFactory(bullet.MovementPattern);
            Movement movement = this.factory.Create(origin, new Vector2(0, -1), (float)bullet.Velocity);
            bullet.Movement = movement;

            //Set position of bullet
            bullet.Position = origin;

            return bullet;
        }
    }
}
