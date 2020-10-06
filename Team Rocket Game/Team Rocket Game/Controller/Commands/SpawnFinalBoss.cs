using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Factories.EnemyFactories;

namespace Team_Rocket_Game.Controller.Commands
{
    public class SpawnFinalBoss : Spawn
    {
        // Fields
        private FinalBossFactory _reciever;
        private GameData _myGameData;
        private EnemyConfig _myEnemyConfig;
        public EnemyConfig MyEnemyConfig
        {
            get { return this._myEnemyConfig; }
            set { this._myEnemyConfig = value; }
        }
        private BulletManager bulletManager;

        // Constructor
        public SpawnFinalBoss(BulletManager bulletManager, GameData gameData)
        {
            this.bulletManager = bulletManager;
            this._reciever = gameData.FinalBossFact;
            this._myGameData = gameData;
            this._myEnemyConfig = null;
        }

        // execute
        public override void execute()
        {
            Vector2 position = new Vector2
            (
                ((GameConfig.Width / 2) - (Game1.GetTexture("finalboss").Width / 2)),
                (0 + (Game1.GetTexture("finalboss").Height / 3))
            );

            this._myGameData.AddEnemy(this._reciever.Create(this.bulletManager, this._myEnemyConfig, this._myGameData, position));
        }
    }

}
