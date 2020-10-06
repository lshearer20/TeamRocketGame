using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Entities;

namespace Team_Rocket_Game.Controller
{
    //Used for the JSON deserializer, we need to establish what our JSON files will look like to determine the configurations
    //Documentation for JSON setup
    public class EnemyConfig
    {
        public EnemyType enemyType;
        public MovementPattern movementPattern;
        public int health;
        public float acceleration;
        public double velocity;
        public int randomSpawn;
        public int fireRate;
        public Position direction;
        //public Stopwatch timeAlive;
        public BulletType bulletType; //default to enemy's basic bullet type unless otherwise specified
        public int count;
        public float spawnInterval;

        public EnemyConfig()
        {
            //Empty
        }

        public EnemyConfig(EnemyConfig copy)
        {
            this.enemyType = copy.enemyType;
            this.movementPattern = copy.movementPattern;
            this.health = copy.health;
            this.acceleration = copy.acceleration;
            this.velocity = copy.velocity;
            this.randomSpawn = copy.randomSpawn;
            this.fireRate = copy.fireRate;
            this.direction = new Position(copy.direction);
            this.bulletType = copy.bulletType;
            this.count = copy.count;
            this.spawnInterval = copy.spawnInterval;
        }
    }

    public class Position
    {
        public double X;
        public double Y;

        //For random starting position
        public Boolean random;

        public Position()
        {
            //Empty
        }

        public Position(Position copy)
        {
            X = copy.X;
            Y = copy.Y;
            random = copy.random;
        }
    }

    public class Wave
    {
        private List<EnemyConfig> enemyConfig;
        public List<EnemyConfig> EnemyConfig
        {
            get { return this.enemyConfig; }
        }

        // JSON attributes
        //private float wave_length;

        public Wave(List<EnemyConfig> enemyConfigs)
        {
            this.enemyConfig = enemyConfigs;
        }
    }

    public class WaveConfig
    {
        public List<List<EnemyConfig>> waves;
    }

    public class StageConfig
    {
        //List of waves
        private List<Wave> waves;
        public List<Wave> Waves
        {
            get { return waves; }
        }

        public StageConfig (List<Wave> waves)
        {
            this.waves = waves;
        }
    }
}
