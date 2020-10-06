﻿using Microsoft.Xna.Framework;
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
    public class ShootSpecialPlayerBullet : Shoot
    {
        // Fields
        // Reciever makes bullets
        private BulletAbstractFactory myReciever;
        private GameData gameData;

        // Constructor
        public ShootSpecialPlayerBullet(GameData gameData)
        {
            this.myReciever = gameData.PlayerSpecBulletFact;
            this.gameData = gameData;
        }

        // execute
        public override void execute()
        {
            Bullet newBullet = this.myReciever.Create(new Vector2(Player.Instance.GetCenterCoordinates().X - Game1.GetTexture("playerbulletspecial").Width / 2, Player.Instance.GetCenterCoordinates().Y - Game1.GetTexture("playerbulletspecial").Height));

            gameData.AddPlayerBullet((PlayerBullet)newBullet);

            //Sound effect
            newBullet.PlaySoundEffect();
        }
    }
}
