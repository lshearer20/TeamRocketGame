using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.Model.Factories.EnemyFactories;

namespace Team_Rocket_Game.Controller.Commands
{
    public class SpawnEnemyA : Spawn
    {
        // Fields
        private EnemyAFactory _reciever;
        private GameData _myGameData;
        private EnemyConfig _myEnemyConfig;
        public EnemyConfig MyEnemyConfig
        {
            get { return this._myEnemyConfig; }
            set { this._myEnemyConfig = value; }
        }
        private BulletManager bulletManager;

        // Constructor
        public SpawnEnemyA(BulletManager bulletManager, GameData gameData)
        {
            this.bulletManager = bulletManager;
            this._reciever = gameData.EnemyAFact;
            this._myGameData = gameData;
            this._myEnemyConfig = null;
        }

        // execute
        public override void execute()
        {
            //Load sets position of enemy
            Enemy enemy = this._reciever.Create(bulletManager, _myEnemyConfig, _myGameData, new Vector2(Game1.GetTexture("enemyA").Width / 2, Game1.GetTexture("enemyA").Height));

            this._myGameData.AddEnemy(enemy);
        }
    }
}