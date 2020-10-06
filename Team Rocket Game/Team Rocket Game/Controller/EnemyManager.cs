using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.Controller.Commands;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.Model.Factories;
using Team_Rocket_Game.Model.Factories.EnemyFactories;

// Spawns enemies and contains enemy textures
namespace Team_Rocket_Game.Controller
{
    public class EnemyManager
    {
        // Fields
        private GameData _gameData;
        public List<Enemy> currentEnemies;
        public List<Enemy> deadEnemies;
        public bool Add { get; set; }
        public float SpawnTimer { get; set; }

        // Command Fields?
        private SpawnEnemyA spawnA;
        private SpawnEnemyB spawnB;
        private SpawnMidBoss spawnMid;
        private SpawnFinalBoss spawnFinal;

        //Constructor
        public EnemyManager(BulletManager bulletManager, GameData gameData)
        {
            this._gameData = gameData;

            //Initialize commands
            spawnA = new SpawnEnemyA(bulletManager, _gameData);
            spawnB = new SpawnEnemyB(bulletManager, _gameData);
            spawnMid = new SpawnMidBoss(bulletManager, _gameData);
            spawnFinal = new SpawnFinalBoss(bulletManager, _gameData);
        }

        //Called for every wave
        public void Spawn(EnemyConfig enemy)
        {
                switch (enemy.enemyType)
                {
                    case EnemyType.EnemyA:
                        spawnA.MyEnemyConfig = enemy;
                        spawnA.execute();
                        break;
                    case EnemyType.EnemyB:
                        spawnB.MyEnemyConfig = enemy;
                        spawnB.execute();
                        break;
                    case EnemyType.MidBoss:
                        spawnMid.MyEnemyConfig = enemy;
                        spawnMid.execute();
                        break;
                    case EnemyType.FinalBoss:
                        spawnFinal.MyEnemyConfig = enemy;
                        spawnFinal.execute();
                        break;
                }
        }

        //Event for when enemies are added or removed
        public void UpdateEnemies(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Complete

            //for (int i = this.currentEnemies.Count - 1; i >= 0; i--)
            //{
            //    if (this.currentEnemies[i].IsDead)
            //    {
            //        this.deadEnemies.Add(this.currentEnemies[i]);
            //        this.currentEnemies.RemoveAt(i);
            //    }
            //}

            if ((sender as Enemy).IsDead)
            {
                this.currentEnemies.Remove(sender as Enemy);
            }
        }

        // check if all enenmies are dead
        public bool areAllEnemiesDead()
        {
            foreach (Enemy e in _gameData.CurrentEnemies)
            {
                if (!e.IsDead)
                {
                    return false;
                }
            }
            return true;
        }
    }
}