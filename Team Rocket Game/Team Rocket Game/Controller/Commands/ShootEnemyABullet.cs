using Microsoft.Xna.Framework;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.Model.Entities.Bullets;
using Team_Rocket_Game.Model.Factories;

namespace Team_Rocket_Game.Controller.Commands
{
    public class ShootEnemyABullet : Shoot
    {
        // Fields
        // Reciever makes bullets
        private BulletAbstractFactory myReciever;
        // GameData is where the bullet is being stored
        private GameData gameData;

        // Constructor
        public ShootEnemyABullet(GameData myGameData)
        {
            this.myReciever = myGameData.EnemyABulletFact;
            this.gameData = myGameData;
        }
        // execute
        public override void execute()
        {
            //Need coordinates of shooter
            Bullet newBullet = this.myReciever.Create(new Vector2(Game1.GetTexture("enemyAbullet").Width / 2, Game1.GetTexture("enemyAbullet").Height));

            //Add bullet to list of enemy bullets
            gameData.AddEnemyBullet((EnemyBullet)newBullet);
            
            //Sound effect
            newBullet.PlaySoundEffect();
        }
    }
}
