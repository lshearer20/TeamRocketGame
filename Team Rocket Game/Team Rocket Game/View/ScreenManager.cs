using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.View.Screens;

namespace Team_Rocket_Game.View
{
    //Contains generic draw function
    public interface Screen
    {
        void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch);
    }

    public class ScreenManager
    {
        private ScreenView current;
        
        //Getter for current screen
        public ScreenView Current
        {
            get { return current; }
        }

        //Dict of screens
        private Dictionary<ScreenView, Screen> screens;

        //Constructor
        public ScreenManager(GameData gameData, GameManager gameManager)
        {
            //Initialize the dict of screens and add each enumerated ScreenView
            screens = new Dictionary<ScreenView, Screen>();
            AddScreen(ScreenView.MENU, new MenuScreen(gameManager));
            AddScreen(ScreenView.SETTINGS, new SettingsScreen(gameManager));
            AddScreen(ScreenView.DIFFICULTY_SETTINGS, new DifficultySettingsScreen(gameManager));
            AddScreen(ScreenView.INGAME, new GameScreen(gameData));
            AddScreen(ScreenView.GAMEWIN, new WinScreen(gameManager));
            AddScreen(ScreenView.GAMELOSE, new LoseScreen(gameManager));
            AddScreen(ScreenView.PAUSED_MENU, new PausedMenuScreen(gameManager));
        }

        //Add a screen to the dictionary
        public void AddScreen(ScreenView type, Screen screen)
        {
            screens[type] = screen;
        }

        //Set current screen
        public void SetScreen(ScreenView view)
        {
            //Stop old music
            HandleMusicOff(current, view);

            //Switch view
            current = view;

            //Start new music
            HandleMusicOn(current);
        }

        public void HandleMusicOff(ScreenView oldView, ScreenView newView)
        {
            //Check what the old screen was
            switch (oldView)
            {
                case ScreenView.MENU:
                    MenuScreen menuScreen = (MenuScreen)screens[oldView];
                    if(newView == ScreenView.SETTINGS)
                    {
                        //Do nothing, let music play
                    }
                    else
                    {
                        menuScreen.StopMusic();
                    }
                    break;
                case ScreenView.PAUSED_MENU:
                    //Do nothing, no music plays during pause menu
                    break;
                case ScreenView.GAMELOSE:
                    //Going to menu, do nothing
                    break;
                case ScreenView.GAMEWIN:
                    //Going to menu, do nothing
                    break;
                case ScreenView.INGAME:
                    GameScreen gameScreen = (GameScreen)screens[oldView];
                    if(newView != ScreenView.PAUSED_MENU)
                    {
                        gameScreen.StopMusic();
                    }
                    else //next view is the pause menu, don't restart music
                    {
                        gameScreen.PauseMusic();
                    }
                    break;
                case ScreenView.SETTINGS:
                    //Do nothing, only case is to go from settings to menu
                    break;
            }
        }

        public void HandleMusicOn(ScreenView view)
        {
            //Check what the new screen is
            switch (view)
            {
                case ScreenView.MENU:
                    MenuScreen menuScreen = (MenuScreen)screens[view];
                    menuScreen.StartMusic();
                    break;
                case ScreenView.PAUSED_MENU:
                    //Do nothing, no music during pause menu
                    break;
                case ScreenView.GAMELOSE:
                    //In game music is stopped already
                    break;
                case ScreenView.GAMEWIN:
                    //In game music is stopped already
                    break;
                case ScreenView.INGAME:
                    GameScreen gameScreen = (GameScreen)screens[view];
                    gameScreen.StartMusic();
                    break;
                case ScreenView.SETTINGS:
                    //Do nothing, only case is to go from settings to menu
                    break;
            }
        }

        //Draw screen
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            //Grab current screen
            Screen screen = screens[current];

            //Draw it if it's not null
            if (screen != null)
            {
                screen.Draw(graphics, spriteBatch);
            }
        }
    }
}