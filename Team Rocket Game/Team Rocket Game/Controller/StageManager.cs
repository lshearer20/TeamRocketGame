using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Entities;

namespace Team_Rocket_Game.Controller
{
    public class StageManager
    {
        private WaveManager _waveManager;
        private GameData _gameData;

        List<StageConfig> stages;

        //Contains the index of the current phase
        private int currentStageIndex;
        public int CurrentStageIndex
        {
            get { return currentStageIndex; }
            set
            {
                this.currentStageIndex = value;
            }
        }

        //Index of the current wave in the current stage
        private int currentWave;

        // Fields
        private double _timer;

        public StageManager(GameData gameData, WaveManager waveManager)
        {
            this._waveManager = waveManager;
            this._gameData = gameData;

            //Load json data based on what difficulty level we're using
            //initStages();
        }

        public void initStages()
        {
            Difficulty currentDifficulty = _gameData.Difficulty;
            string baseName = "";

            //Load json data based on what difficulty level we're using
            switch (currentDifficulty)
            {
                case Difficulty.Easy:
                    baseName = "Easy.json";
                    InitStages(baseName);
                    break;
                case Difficulty.Medium:
                    baseName = "Medium.json";
                    InitStages(baseName);
                    break;
                case Difficulty.Hard:
                    baseName = "Hard.json";
                    InitStages(baseName);
                    break;
                case Difficulty.Insane:
                    baseName = "Insane.json";
                    InitStages(baseName);
                    break;
                default:
                    baseName = "Medium.json";
                    InitStages(baseName);
                    break;
            }
        }

        //Read in stage information from JSON
        private void InitStages(string basename)
        {
            StreamReader input = _gameData.GetStagesStreamReader(basename);
            string json = input.ReadToEnd();

            //Grab JSON data
            //String is stage name, WaveConfig has a list of a list of enemy data
            //Wave Config has to be an object otherwise JSON gets mad
            Dictionary<string, WaveConfig> jsonStages = JsonConvert.DeserializeObject<Dictionary<string, WaveConfig>>(json);

            //Initialize list of stages
            this.stages = new List<StageConfig>();

            //For each stage in the list we just deserialized, configure
            foreach (var stage in jsonStages.Keys)
            {
                //Configure stage and add to list
                this.stages.Add(ConfigureStage(jsonStages, stage));
            }
        }

        //Set up stage and configure each wave in the stage
        //using the wave manager
        public StageConfig ConfigureStage(Dictionary<string, WaveConfig> jsonStages, string stage)
        {
            //Reset game
            //Despawn all enemies/bullets from last stage

            //Grab the waves in the stage, which is a list of enemy configurations
            WaveConfig waveConfigs = jsonStages[stage];
            List<Wave> waves = new List<Wave>();

            //For each set of enemies in a wave
            for(int i = 0; i < waveConfigs.waves.Count; ++i)
            {
                //Tell wave manager to configure the wave
                //Pass it the enemy info
                waves.Add(_waveManager.ConfigureWave(waveConfigs, i));
            }

            //Return stage configuration
            return new StageConfig(waves);
        }

        //Checks if all waves in the stage are complete
        public bool isStageComplete()
        {
            //While there are still waves in the stage
            //Checks if current wave is complete
            return _waveManager.isWaveComplete();
        }

        // Check if all stages are done
        public bool isAllStagesComplete()
        {
            if (currentStageIndex >= stages.Count)
            {
                return true;
            }
            return false;
        }

        //Checks if all stages are complete
        public bool isGameOver()
        {
            // check if we're on the last stage or not
            // game can't be over if we're not on the last
            // stage right?
            if (currentStageIndex < stages.Count)
            {
                return false;
            }

            // ask wave manager if wave is complete
            // if not, return false
            if (!_waveManager.isWaveComplete())
            {
                return false;
            }

            //// check that all enenmies in the list are dead
            //for (int i = 0; i < _gameData.CurrentEnemies.Count; i++)
            //{
            //    if (!_gameData.CurrentEnemies[i].IsDead)
            //    {
            //        return false;
            //    }
            //}

            // retrun true to mark game over
            return true;
        }

        // Update
        public void Update(GameTime gameTime)
        {
            // if GAMEOVER
            if (isGameOver())
            {
                return;
            }

            _timer += gameTime.ElapsedGameTime.TotalSeconds;

            // if the wave is complete or if it is the first wave then load the next config 
            if (this._waveManager.isWaveComplete() || (this.currentStageIndex == 0 && this.currentWave == 0))
            {
                if (currentStageIndex < stages.Count)
                {
                    if (currentWave < stages[currentStageIndex].Waves.Count)
                    {
                        this._waveManager.setWave(stages[CurrentStageIndex].Waves[currentWave++].EnemyConfig);
                    }
                }
            }




            //// if index is out of bounds set it to the last wave
            //if (currentStageIndex >= this.stages.Count)
            //{
            //    currentStageIndex = this.stages.Count - 1;
            //}

            //// 
            //if (this._waveManager.isWaveComplete() || (this.currentStageIndex == 0 && this.currentWave == 0))
            //{
            //    this._waveManager.setWave(stages[CurrentStageIndex].Waves[currentWave++].EnemyConfig);
            //}
            
            // if last wave
            if (currentStageIndex < this.stages.Count && this.stages[currentStageIndex].Waves.Count <= currentWave)
            {
                currentStageIndex++;
                currentWave = 0;
            }



            this._waveManager.Update(gameTime);
        }
    }
}
