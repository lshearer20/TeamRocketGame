using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.Controller.Commands;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.Model.Entities.Bullets;

namespace Team_Rocket_Game.Controller
{
    public class BulletManager
    {
        private GameData gameData;

        //Commands
        private ShootPlayerBullet shootPlayerBullet;
        private ShootSpecialPlayerBullet shootSpecialPlayerBullet;
        private ShootEnemyABullet shootEnemyABullet;
        private ShootEnemyBBullet shootEnemyBBullet;
        private ShootMidBossBullet shootMidBossBullet;
        private ShootFinalBossBullet shootFinalBossBullet;

        public BulletManager(GameData gameData)
        {
            this.gameData = gameData;

            //Set commands
            shootPlayerBullet = new ShootPlayerBullet(gameData);
            shootSpecialPlayerBullet = new ShootSpecialPlayerBullet(gameData);
            shootEnemyABullet = new ShootEnemyABullet(gameData);
            shootEnemyBBullet = new ShootEnemyBBullet(gameData);
            shootMidBossBullet = new ShootMidBossBullet(gameData);
            shootFinalBossBullet = new ShootFinalBossBullet(gameData);
        }

        //Called when player presses space bar to shoot
        public void PlayerShoot()
        {
            //Check if player can shoot
            if(!Player.Instance.Shoot())
            {
                return;
            }
            
            //Check if cheat mode is active
            if(Player.Instance.CheatingOn == true)
            {
                shootSpecialPlayerBullet.execute();
            }
            else
            {
                shootPlayerBullet.execute();
            }
        }

        //Called by enemies when they can shoot
        public void EnemyShoot(EnemyType enemy)
        {
            switch(enemy)
            {
                case EnemyType.EnemyA:
                    shootEnemyABullet.execute();
                    break;
                case EnemyType.EnemyB:
                    shootEnemyBBullet.execute();
                    break;
                case EnemyType.MidBoss:
                    shootMidBossBullet.execute();
                    break;
                case EnemyType.FinalBoss:
                    shootFinalBossBullet.execute();
                    break;
            }
        }

        //Event for when bullets are removed
        public void UpdateDeadBullets(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Complete
            foreach (Bullet bullet in this.gameData.enemyBullets)
            {
                if (bullet.IsDead)
                {
                    gameData.deadBullets.Add(bullet);
                }
            }

            foreach (Bullet bullet in gameData.playerBullets)
            {
                if (bullet.IsDead)
                {
                    gameData.deadBullets.Add(bullet);
                }
            }

            foreach (Bullet bullet in gameData.deadBullets)
            {
                if (bullet is PlayerBullet)
                {
                    gameData.playerBullets.Remove(bullet as PlayerBullet);
                }
                else if (bullet is EnemyBullet)
                {
                    gameData.enemyBullets.Remove(bullet as EnemyBullet);
                }
            }
        }

        public void Update(GameTime gameTime, CollisionManager cb)
        {

        }
    }
}
