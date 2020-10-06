using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller;
using Team_Rocket_Game.General.Movement;
using Team_Rocket_Game.General.Movement.MovementFactories;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.Model.Entities.Enemies;

namespace Team_Rocket_Game.Model.Factories.EnemyFactories
{
    public class EnemyBFactory : EnemyAbstractFactory
    {
        public override Enemy Create(BulletManager bulletManager, EnemyConfig enemy, GameData gameData, Vector2 origin)
        {
            this.bulletManager = bulletManager;

            EnemyB newEnemy = new EnemyB(bulletManager, gameData);

            //Load - sets position of enemy as well
            Load(newEnemy, enemy, origin);

            return newEnemy;
        }
    }
}
