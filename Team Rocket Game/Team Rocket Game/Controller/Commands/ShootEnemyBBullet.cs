using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.Model.Entities.Bullets;
using Team_Rocket_Game.Model.Factories;

namespace Team_Rocket_Game.Controller.Commands
{
    public class ShootEnemyBBullet : Shoot
    {
        // Fields
        // Reciever makes bullets
        private BulletAbstractFactory myReciever;
        // GameData is where the bullet is being stored
        private GameData gameData;

        // Constructor
        public ShootEnemyBBullet(GameData myGameData)
        {
            this.myReciever = myGameData.EnemyBBulletFact;
            this.gameData = myGameData;
        }
        // execute
        public override void execute()
        {
            //Need coordinates of shooter
            Bullet newBullet = this.myReciever.Create(new Vector2(Game1.GetTexture("enemyBbullet").Width / 2, Game1.GetTexture("enemyBbullet").Height));

            //Add bullet to list of enemy bullets
            gameData.AddEnemyBullet((EnemyBullet)newBullet);

            //Sound effect
            newBullet.PlaySoundEffect();
        }
    }
}
