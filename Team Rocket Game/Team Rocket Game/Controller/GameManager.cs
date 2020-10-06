using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller.Collision;
using Team_Rocket_Game.Controller.Commands;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.View;
using Team_Rocket_Game.View.Screens;


namespace Team_Rocket_Game.Controller
{
    // Our "Controller" class of our MVC architecture
    public class GameManager
    {
        #region Fields
        //Initialize managers
        private InputManager inputManager;
        public InputManager InputManager
        {
            get { return inputManager; }
        }

        private ScreenManager screenManager;

        public ScreenManager ScreenManager
        {
            get { return screenManager; }
        }

        private double timePaused;
        private GameTime timeElapsed;

        private GameState currentGameState;
        public GameState CurrentGameState
        {
            get { return currentGameState; }
            set
            {
                if(currentGameState != value)
                {
                    this.currentGameState = value;
                    
                    switch(this.currentGameState)
                    {
                        case GameState.MENU:
                            screenManager.SetScreen(ScreenView.MENU);
                            MenuOption = MenuOption.PLAY_GAME;
                            break;
                        case GameState.PLAY:
                            screenManager.SetScreen(ScreenView.INGAME);
                            break;
                        case GameState.PAUSEDMENU:
                            screenManager.SetScreen(ScreenView.PAUSED_MENU);
                            break;
                        case GameState.GAMEOVER:
                            if (this.gameOver == GameOver.LOSE)
                            {
                                screenManager.SetScreen(ScreenView.GAMELOSE);
                                GameOverMenuOption = MenuOption.MENU;
                                //MenuOption = MenuOption.MENU;
                            }
                            else
                            {
                                screenManager.SetScreen(ScreenView.GAMEWIN);
                                GameOverMenuOption = MenuOption.MENU;
                            }
                            break;
                        case GameState.PAUSE:
                            this.timePaused = this.timeElapsed.TotalGameTime.TotalSeconds;
                            break;
                        case GameState.RESET:
                            //this.stageManager.CurrentStage = 0;
                            Player.Reset();
                            //this.stageManager.ConfigureNextStage(gameData);
                            this.CurrentGameState = GameState.MENU;
                            break;
                        default: break;
                    }
                }
            }
        }

        private MenuOption menuOption;
        public MenuOption MenuOption
        {
            get { return menuOption; }
            set { menuOption = value; }
        }

        private MenuOption settingsMenuOption;
        public MenuOption SettingsMenuOption
        {
            get { return settingsMenuOption; }
            set { settingsMenuOption = value; }
        }

        private DifficultyMenuOption difficultySettingsMenuOption;
        public DifficultyMenuOption DifficultySettingsMenuOption
        {
            get { return difficultySettingsMenuOption; }
            set { difficultySettingsMenuOption = value; }
        }

        private MenuOption gameOverMenuOption;
        public MenuOption GameOverMenuOption
        {
            get { return gameOverMenuOption; }
            set { gameOverMenuOption = value; }
        }

        private GameOver gameOver;
        public GameOver GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        //Snapshot of last game model
        private GameData gameData;

        //Snapshot of last game view
        private GameScreen gameScreen;

        private KeyboardState previousKeyboardState;
#endregion

        // Subtask mangers
        // manager to handle the enemies
        private EnemyManager enemyManager;
        public EnemyManager EnemyManager
        {
            get { return this.enemyManager; }
        }

        // manager for handling the bullets
        private BulletManager bulletManager;
        public BulletManager BulletManager
        {
            get { return this.bulletManager; }
        }

        // manager for handling the waves
        private WaveManager waveManager;
        public WaveManager WaveManager
        {
            get { return waveManager; }
        }

        // manager for handling stages
        private StageManager stageManager;
        public StageManager StageManager
        {
            get { return this.StageManager; }
        }

        // Manager to handle all things collision related
        private CollisionManager collisionManager;
        public CollisionManager CollisionManager
        {
            get { return this.collisionManager; }
        }

        //Constructor
        public GameManager(GameData gameData, GameScreen gameScreen)
        {
            GameConfig.MoveKeys = MoveKeys.WASD;

            this.gameData = gameData;
            this.gameScreen = gameScreen;
            this.previousKeyboardState = Keyboard.GetState();

            //Initialize and set screen to start
            screenManager = new ScreenManager(gameData, this);
            screenManager.SetScreen(ScreenView.MENU);

            //Set current game state and initalize menu and managers
            this.currentGameState = GameState.MENU; //initial screen
            this.menuOption = MenuOption.PLAY_GAME; // initial menu option
            this.settingsMenuOption = MenuOption.DIFFICULTY; // initial settings menu option

            this.difficultySettingsMenuOption = DifficultyMenuOption.EASY;
            this.GameOverMenuOption = MenuOption.MENU;

            this.bulletManager = new BulletManager(gameData);
            this.inputManager = new InputManager(bulletManager); //give bullet manager to input manager to handle player shooting
        }

        public void InitManagers()
        {
            //Initialize managers
            this.collisionManager = gameData.CollisionManager;
            this.enemyManager = new EnemyManager(bulletManager, gameData); //give bullet manager to enemy manager to handle enemy shooting
            this.waveManager = new WaveManager(enemyManager);
            this.stageManager = new StageManager(gameData, waveManager);
        }

        public void ResetGame()
        {
            gameData.deadBullets.Clear();
            gameData.playerBullets.Clear();
            gameData.enemyBullets.Clear();
            gameData.CurrentEnemies.Clear();
            this.enemyManager = null;
            this.waveManager = null;
            this.stageManager = null;
        }

        //Update
        public void Update(GameTime gameTime)
        {
            this.timeElapsed = gameTime;
            // Gets the state of the keyboard and checks the combos as follows.
            KeyboardState keyboardState = Keyboard.GetState();
            switch (this.CurrentGameState)
            {
                case GameState.PAUSE:
                    if (timePaused + 3 < timeElapsed.TotalGameTime.TotalSeconds)
                    {
                        this.CurrentGameState = GameState.PLAY;
                    }
                    break;
                case GameState.PLAY:
                    GameState newGameState1;
                    this.inputManager.HandlePlayerInput(keyboardState);
                    //this.collisionDetection(gameTime);
                    this.inputManager.InGamePauseMenu(this, keyboardState, previousKeyboardState, menuOption, out newGameState1);
                    this.CurrentGameState = newGameState1;
                    this.gameData.Update(gameTime);//xxx
                    this.stageManager.Update(gameTime);
                    if (Player.Instance.IsDead) //For real dead - all lives gone
                    {
                        //Change screen to game over
                        this.gameOver = GameOver.LOSE;
                        this.CurrentGameState = GameState.GAMEOVER;
                    }
                    else if (this.stageManager.isGameOver()) //Player won 
                    {
                        this.gameOver = GameOver.WIN;
                        this.CurrentGameState = GameState.GAMEOVER;
                    }
                    break;
                case GameState.MENU:
                    GameState newGameState = GameState.MENU;
                    if (screenManager.Current == ScreenView.SETTINGS) //In the settings menu
                    {
                        this.settingsMenuOption = inputManager.SettingsMenuMove(this, keyboardState, previousKeyboardState, settingsMenuOption, out newGameState);
                    }
                    else if(screenManager.Current == ScreenView.DIFFICULTY_SETTINGS) //In the difficulty settings menu
                    {
                        this.difficultySettingsMenuOption = inputManager.DifficultySettingsMenuMove(this, keyboardState, previousKeyboardState, difficultySettingsMenuOption, out newGameState);
                        switch(difficultySettingsMenuOption)
                        {
                            case DifficultyMenuOption.EASY:
                                gameData.Difficulty = Difficulty.Easy;
                                break;
                            case DifficultyMenuOption.MEDIUM:
                                gameData.Difficulty = Difficulty.Medium;
                                break;
                            case DifficultyMenuOption.HARD:
                                gameData.Difficulty = Difficulty.Hard;
                                break;
                            case DifficultyMenuOption.INSANE:
                                gameData.Difficulty = Difficulty.Insane;
                                break;
                        }
                    }
                    else if(screenManager.Current == ScreenView.MENU) //In the main menu
                    {
                        this.menuOption = inputManager.MenuMove(this, keyboardState, previousKeyboardState, menuOption, out newGameState);
                        if (currentGameState != GameState.PLAY && newGameState == GameState.PLAY)
                        {
                            ResetGame();
                            InitManagers();
                            this.stageManager.initStages();
                        }
                    }
                    this.CurrentGameState = newGameState;
                    break;
                case GameState.PAUSEDMENU:
                    GameState newGameState3;
                    if (screenManager.Current == ScreenView.SETTINGS)
                    {
                        inputManager.PausedSettingsMenuMove(this, keyboardState, previousKeyboardState);
                        newGameState3 = GameState.PAUSEDMENU;
                    }
                    else //PAUSED_MENU
                    {
                        this.menuOption = inputManager.PausedMenuMove(this, keyboardState, previousKeyboardState, menuOption, out newGameState3);
                        if (currentGameState != GameState.PLAY && newGameState3 == GameState.PLAY)
                        {
                            //this.stageManager.ConfigureNextStage(gameData);
                        }
                    }
                    this.CurrentGameState = newGameState3;
                    break;
                case GameState.GAMEOVER: //Win or Lose
                    GameState newGameState4;
                    this.gameOverMenuOption = inputManager.GameOverMenuMove(this, keyboardState, previousKeyboardState, gameOverMenuOption, out newGameState4);
                    this.CurrentGameState = newGameState4;
                    break;
                case GameState.EXIT:
                    System.Environment.Exit(0);
                    break;
            }
            previousKeyboardState = keyboardState;
        }

    }
}
