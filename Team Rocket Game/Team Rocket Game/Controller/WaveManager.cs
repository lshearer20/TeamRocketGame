using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller.Commands;
using Team_Rocket_Game.Model;

namespace Team_Rocket_Game.Controller
{
    public class WaveManager
    {
        //Enemy Manager - passed in constructor
        private EnemyManager _enemyManager;

        // Other Fields
        private double _timer;
        public double Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }

        private int _enemy_index;
        public int Enemy_Index
        {
            get { return _enemy_index; }
            set { _enemy_index = value;}
        }
        private int _enemy_count;
        private double _spawn_interval;
        private List<EnemyConfig> _cur_wave;
        

        //Constructor
        public WaveManager(EnemyManager enemyManager)
        {
            this._enemyManager = enemyManager;
            this._cur_wave = new List<EnemyConfig>();
        }

        public Wave ConfigureWave(WaveConfig waveConfigs, int index)
        {
            //Grab list of enemy configs at index
            List<EnemyConfig> waveConfig = waveConfigs.waves[index];
            List<EnemyConfig> wave = new List<EnemyConfig>();

            //For each enemy in the wave
            for(int i = 0; i < waveConfig.Count; ++i)
            {
                //Save enemy config to send to enemy manager
                EnemyConfig enemy = waveConfig[i];

                ////Send to enemy info to the enemy manager to spawn
                //_enemyManager.Spawn(enemy);

                //Add enemy config to list of enemies in wave
                wave.Add(enemy);
            }

            //Return new wave config
            return new Wave(wave);
        }

        // set wave
        public void setWave(List<EnemyConfig> nextWave)
        {
            this._cur_wave = nextWave;
            this._enemy_count = nextWave[0].count;
            this._enemy_index = 0;
            this._spawn_interval = nextWave[0].spawnInterval;
        }

        //Checks if wave timer is up (if there is one) and/or if all enemies are dead
        public bool isWaveComplete()
        {
            //check if all enemies have been called
            if (_enemy_index < _enemy_count)
            {
                return false;
            }

            // if there are enemies still alive, then return false
            if(!_enemyManager.areAllEnemiesDead())
            {
                return false;
            }

            // all checks passed, return true
            return true;
        }

        // UPdate function. calls spawn if ready
        public void Update(GameTime gameTime)
        {
            _timer += gameTime.ElapsedGameTime.TotalSeconds;

            // if we've spawned all enemies then do nothing
            if (_enemy_count <= _enemy_index)
            {
                return;
            }

            // If timer is over the spawn interval set to 0
            if(_timer > _spawn_interval)
            {
                _timer = 0f;
                // call spawn on the enemy
                _enemyManager.Spawn(_cur_wave[0]);
                _enemy_index++;
            }
        }
    }
}