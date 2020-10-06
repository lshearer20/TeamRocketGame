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
    public class ShootPlayerBullet : Shoot
    {
        // Fields
        // Reciever makes bullets
        private BulletAbstractFactory myReciever;
        private GameData gameData;

        // Constructor
        public ShootPlayerBullet(GameData gameData)
        {
            this.gameData = gameData;
            this.myReciever = gameData.PlayerBulletFact;
        }

        // execute
        public override void execute()
        {
            Vector2 coordinates = new Vector2(Player.Instance.GetCenterCoordinates().X -Game1.GetTexture("playerbullet").Width / 2, Player.Instance.GetCenterCoordinates().Y - Game1.GetTexture("playerbullet").Height);

            Bullet newBullet = this.myReciever.Create(coordinates);
            
            gameData.AddPlayerBullet((PlayerBullet)newBullet);

            //Sound effect
            newBullet.PlaySoundEffect();
        }
    }
}
