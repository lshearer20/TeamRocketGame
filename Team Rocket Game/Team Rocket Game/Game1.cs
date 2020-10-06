using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Team_Rocket_Game.Controller;
using Team_Rocket_Game.General;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.View;
using Team_Rocket_Game.View.Screens;

namespace Team_Rocket_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static Game1 Instance;

        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SoundEffect> songs = new Dictionary<string, SoundEffect>();//because they are wav files it only recognizes them as soundeffects
        private static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Set up subsystems of architecture
        private GameScreen gameScreen;
        private GameManager gameManager;
        private GameData gameData;

        List<Background> backgrounds;

        public Game1()
        {
            Instance= this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameConfig.Height = graphics.GraphicsDevice.Viewport.Height;
            GameConfig.Width = graphics.GraphicsDevice.Viewport.Width;
            Initialize();

            //Initialize subsystems
            gameData = new GameData(GetTexture("playerShip"));
            gameScreen = new GameScreen(gameData);
            gameManager = new GameManager(gameData, gameScreen);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            backgrounds = new List<Background>();

            //Player
            LoadTexture("playerShip");
            LoadTexture("slowMotionSpaceship");
            LoadTexture("health");
            LoadTexture("invincible_ship");
            
            //Enemies
            LoadTexture("midboss");
            LoadTexture("finalboss");
            LoadTexture("enemyB");
            LoadTexture("enemyA");
            
            //Bullets
            LoadTexture("enemyAbullet");
            LoadTexture("enemyBbullet");
            LoadTexture("midbossbullet");
            LoadTexture("playerbullet");
            LoadTexture("playerbulletspecial");

            //In-Game Backgrounds
            LoadTexture("Nebula");
            LoadTexture("Nebula2");
            LoadTexture("Nebula3");
            LoadTexture("Stars Small_1");

            //Build backgrounds
            LoadBackground();

            //Screens
            LoadTexture("menu_background");
            LoadTexture("Paused_background");
            LoadTexture("Settings_background");
            LoadTexture("Difficulty_Settings_background");
            LoadTexture("win_background");
            LoadTexture("lose_background");

            //Fonts
            LoadTexture("CheatMode");
            LoadTexture("Play_game");
            LoadTexture("Settings");
            LoadTexture("Difficulty");
            LoadTexture("Easy");
            LoadTexture("Medium");
            LoadTexture("Hard");
            LoadTexture("Insane");
            LoadTexture("Return_to_Main_Menu");
            LoadTexture("Exit");
            LoadTexture("Exit_Game");
            LoadTexture("menu_arrow");
            LoadTexture("Continue");
            LoadTexture("Arrow_keys");
            LoadTexture("Wasd_keys");
            LoadTexture("health");

            //Powerups

            //Sound Effects
            LoadSound("enemy_shot");
            LoadSound("player_shot");
            LoadSound("MenuSelection");
            LoadSound("EnterSelection");

            //Music
            LoadMusic("menu");
            LoadMusic("battle");
        }

        private void LoadBackground()
        {
            //Add backgrounds
            backgrounds.Add(new Background(GetTexture("Nebula"), new Vector2(300, 300), 0.6f));
            backgrounds.Add(new Background(GetTexture("Nebula2"), new Vector2(300, 300), 0.8f));
            backgrounds.Add(new Background(GetTexture("Nebula3"), new Vector2(300, 300), 1.1f));
            backgrounds.Add(new Background(GetTexture("Stars Small_1"), new Vector2(100, 100), 1.1f));
        }

        private void LoadMusic(string name)
        {
            songs[name] = Content.Load<SoundEffect>(name);
        }

        private void LoadSound(string name)
        {
            soundEffects[name] = Content.Load<SoundEffect>(name);
        }

        private void LoadTexture(string name)
        {
            textures[name] = Content.Load<Texture2D>(name);
        }

        //Function to grab textures based on name
        public static Texture2D GetTexture(string name)
        {
            return textures[name];
        }

        public static SoundEffect GetSound(string name)
        {
            return soundEffects[name];
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            //Update backgrounds
            foreach (Background bg in backgrounds)
                bg.Update(gameTime, new Vector2(0, -1), GraphicsDevice.Viewport);

            gameManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

            GraphicsDevice.Clear(Color.Black);

            //Grab current screen so we can see if we need to draw backgrounds
            //We could not get the backgrounds to draw from the GameScreen class so we did it here in a check
            ScreenView current = gameManager.ScreenManager.Current;

            //Check if we're in game. If so, draw game backgrounds
            if(current == ScreenView.INGAME)
            {
                foreach (Background background in backgrounds)
                {
                    background.Draw(spriteBatch);
                }

                gameManager.ScreenManager.Draw(graphics, spriteBatch);
            }
            else //Do not draw game backgrounds
            {
                gameManager.ScreenManager.Draw(graphics, spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}