using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.Controller.Commands;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.Model.Entities.Bullets;
using Team_Rocket_Game.Model.Factories;
using Team_Rocket_Game.Model.Factories.BulletFactories;
using Team_Rocket_Game.Model.Factories.EnemyFactories;

namespace Team_Rocket_Game.Model
{
    // Our "Model" class of our MVC architecture
    // AKA GameObjectManager
    public class GameData : Observer
    {
        // Fields
        private CollisionManager collisionManager;
        public CollisionManager CollisionManager
        {
            get { return collisionManager; }
            set { collisionManager = value; }
        }

        //Difficulty setting
        private Difficulty difficulty;
        public Difficulty Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }

        // Enemy Factory Fields
        private EnemyAFactory enemyAFact;
        private EnemyBFactory enemyBFact;
        private MidBossFactory midBossFact;
        private FinalBossFactory finalBossFact;

        //Bullet factory fields
        private PlayerBulletFactory playerBulletFact;
        private PlayerSpecialBulletFactory playerSpecBulletFact;
        private EnemyABulletFactory enemyABulletFact;
        private EnemyBBulletFactory enemyBBulletFact;
        private MidBossBulletFactory midBossBulletFact;
        private FinalBossBulletFactory finalBossBulletFact;

        //Bullet lists
        public List<PlayerBullet> playerBullets;
        public List<EnemyBullet> enemyBullets;
        public List<Bullet> deadBullets;

        //List of current enemies
        private ObservableCollection<Enemy> currentEnemies;
        public ObservableCollection<Enemy> CurrentEnemies
        {
            get { return currentEnemies; }
        }

        public bool NoEnemies
        {
            get { return currentEnemies.Count == 0; }
        }

        public static StreamReader GetPlayerStreamReader()
        {
            return new StreamReader("../../../../JSON/player.json");
        }

        public StreamReader GetStagesStreamReader(string basename)
        {
            return new StreamReader("../../../../JSON/Stages/" + basename);
        }

        // Factory getters
        public EnemyAFactory EnemyAFact
        {
            get { return this.enemyAFact; }
        }

        public EnemyBFactory EnemyBFact
        {
            get { return this.enemyBFact; }
        }

        public MidBossFactory MidBossFact
        {
            get { return this.midBossFact; }
        }

        public FinalBossFactory FinalBossFact
        {
            get { return this.finalBossFact; }
        }

        public PlayerBulletFactory PlayerBulletFact
        {
            get { return this.playerBulletFact; }
        }

        public PlayerSpecialBulletFactory PlayerSpecBulletFact
        {
            get { return this.playerSpecBulletFact; }
        }

        public EnemyABulletFactory EnemyABulletFact
        {
            get { return this.enemyABulletFact; }
        }

        public EnemyBBulletFactory EnemyBBulletFact
        {
            get { return this.enemyBBulletFact; }
        }

        public MidBossBulletFactory MidBossBulletFact
        {
            get { return this.midBossBulletFact; }
        }

        public FinalBossBulletFactory FinalBossBulletFact
        {
            get { return this.finalBossBulletFact; }
        }

        //Constructor
        public GameData(Texture2D playerTexture)
        {
            this.collisionManager = new CollisionManager(
                    GameConfig.Height,
                    GameConfig.Width,
                    Game1.GetTexture("playerShip").Width
                );
            this.Initialize();
            Player.Instance.Position = new Vector2
            (
                (GameConfig.Width / 2 - playerTexture.Width / 2),
                (GameConfig.Height - playerTexture.Height)
            );
        }

        //Initialize game data
        private void Initialize()
        {
            //Enemies
            this.currentEnemies = new ObservableCollection<Enemy>();
            //When enemy is added or removed from collection "updateEnimies" is automatically called
            this.currentEnemies.CollectionChanged += UpdateEnemies;

            //Initialize lists
            this.playerBullets = new List<PlayerBullet>();
            this.enemyBullets = new List<EnemyBullet>();
            this.deadBullets = new List<Bullet>();

            //Initialize enemy factories
            this.enemyAFact = new EnemyAFactory();
            this.enemyBFact = new EnemyBFactory();
            this.midBossFact = new MidBossFactory();
            this.finalBossFact = new FinalBossFactory();

            //Initialize bullet factories
            this.playerBulletFact = new PlayerBulletFactory();
            this.playerSpecBulletFact = new PlayerSpecialBulletFactory();
            this.enemyABulletFact = new EnemyABulletFactory();
            this.enemyBBulletFact = new EnemyBBulletFactory();
            this.midBossBulletFact = new MidBossBulletFactory();
            this.finalBossBulletFact = new FinalBossBulletFactory();

            //Set difficulty to medium to start
            difficulty = Difficulty.Medium;
        }

        // add an enemy
        public void AddEnemy(Enemy newEnemy)
        {
            newEnemy.attach(this);
            this.currentEnemies.Add(newEnemy);
        }

        public void AddEnemyBullet(EnemyBullet bullet)
        {
            bullet.attach(this);
            this.enemyBullets.Add(bullet);
        }

        public void AddPlayerBullet(PlayerBullet bullet)
        {
            bullet.attach(this);
            this.playerBullets.Add(bullet);
        }

        public void UpdateEnemies(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        public void UpdateEnemyBullets(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        public void UpdatePlayerBullets(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        //Update all game elements
        public void Update(GameTime gameTime)
        {
            Player.Instance.Update(gameTime, collisionManager);

            //Update all current enemies and bullets
            foreach (Enemy enemy in currentEnemies)
            {
                enemy.Update(gameTime, collisionManager);
            }

            foreach (Bullet bullet in playerBullets)
            {
                bullet.Update(gameTime, collisionManager);
            }
            foreach (Bullet bullet in enemyBullets)
            {
                bullet.Update(gameTime, collisionManager);
            }
        }

        //Clear stage for next one
        public void ResetPlayerDeath()
        {
            Player.Instance.Bullets.Clear();
            this.ResetPlayerPosition();
            this.Initialize();
        }

        //Reset player's position, in case they die
        private void ResetPlayerPosition()
        {
            Player.Instance.Position = new Vector2
            (
                (GameConfig.Width / 2 - (float)Player.Instance.HitBoxRadius),
                (GameConfig.Height - Player.Instance.Sprite.Height)
            );
        }

        // obsUpdae is called from a subject that 
        // GameData is watching.
        void Observer.obsUpdate(Subject mySubject)
        {
            // TODO: check if the subject (if a subclass of Enemy or Bullet)
            //       is dead. If so remove from list. Otherwise do ?
            if (mySubject is Enemy)
            {
                if ((mySubject as Enemy).IsDead)
                {
                    this.currentEnemies.Remove(mySubject as Enemy);
                }
            }
            else if (mySubject is Bullet)
            {
                if ((mySubject as Bullet).IsDead)
                {
                    // TODO: add code to handle if is bullet and is dead.
                    //       currently we don't have a bullet list.
                }
            }
        }
    }
}
