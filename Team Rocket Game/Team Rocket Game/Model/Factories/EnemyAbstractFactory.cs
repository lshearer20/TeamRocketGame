using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller;
using Team_Rocket_Game.General;
using Team_Rocket_Game.General.Movement;
using Team_Rocket_Game.General.Movement.MovementFactories;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.Model.Factories.EnemyFactories;

namespace Team_Rocket_Game.Model.Factories
{
    public static class EnemyFactoryofFactories
    {
        public static Enemy Create(BulletManager bulletManager, EnemyConfig enemy, GameData gameData, Vector2 origin)
        {
            //Make a factory for the corresponding type of enemy
            EnemyAbstractFactory factory = MakeFactory(enemy);

            if (factory != null)
            {
                return factory.Create(bulletManager, enemy, gameData, origin);
            }
            return null;
        }

        //Our enemy factory machine
        public static EnemyAbstractFactory MakeFactory(EnemyConfig enemy)
        {
            //Make our abstract factory object to return
            EnemyAbstractFactory factory = null;

            //Find out what type of enemy we're dealing with and return the appropriate factory
            switch (enemy.enemyType)
            {
                case EnemyType.EnemyA:
                    factory = new EnemyAFactory();
                    break;
                case EnemyType.EnemyB:
                    factory = new EnemyBFactory();
                    break;
                case EnemyType.MidBoss:
                    factory = new MidBossFactory();
                    break;
                case EnemyType.FinalBoss:
                    factory = new FinalBossFactory();
                    break;
            }

            return factory;
        }
    }

    public abstract class EnemyAbstractFactory
    {
        protected AbstractMovementFactory factory;
        protected BulletManager bulletManager;

        public abstract Enemy Create(BulletManager bulltManager, EnemyConfig enemy, GameData gameData, Vector2 origin);

        //Load enemy based on JSON using a configuration
        protected void Load(Enemy enemy, EnemyConfig enemyConfiguration, Vector2 origin)
        {
            //Set enemy starting position
            //Enemy is randomly spawned
            if (enemyConfiguration.randomSpawn == 1)
            {
                enemy.Position = enemy.GetRandomSpawnPosition(enemyConfiguration.spawnInterval);
            }
            else
            {
                enemy.Position = new Vector2((float)origin.X, (float)origin.Y);
            }

            //Load enemy attributes
            enemy.MaxHealth = enemyConfiguration.health;
            enemy.FireRate = enemyConfiguration.fireRate;
            enemy.Velocity = enemyConfiguration.velocity;
            enemy.Health = enemyConfiguration.health;
            enemy.MovementPattern = enemyConfiguration.movementPattern;

            //Set movement
            if (enemy.MovementPattern != MovementPattern.None)
            {
                this.factory = MovementFactory.MakeFactory(enemy.MovementPattern);
                Movement movement = this.factory.Create(enemy.Position, new Vector2((float)enemyConfiguration.direction.X, (float)enemyConfiguration.direction.Y), (float)enemy.Velocity);
                enemy.Movement = movement;
            }
        }
    }
}
