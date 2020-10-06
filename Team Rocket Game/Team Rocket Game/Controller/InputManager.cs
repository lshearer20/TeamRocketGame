using Microsoft.Xna.Framework.Input;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.General;
using Team_Rocket_Game.View;
using Team_Rocket_Game.Model.Entities;
using Microsoft.Xna.Framework;
using Team_Rocket_Game.View.Screens;
using Microsoft.Xna.Framework.Audio;

namespace Team_Rocket_Game.Controller
{
    public class InputManager
    {
        private SoundEffect cursor;
        private SoundEffect enter;
        private double playerVelocity;
        private BulletManager bulletManager;
        Keys leftKey = Keys.Space, rightKey = Keys.Space, upKey = Keys.Space, downKey = Keys.Space;

        public InputManager(BulletManager bulletManager)
        {
            cursor = Game1.GetSound("MenuSelection");
            enter = Game1.GetSound("EnterSelection");

            //Set up bullet manager for player shooting
            this.bulletManager = bulletManager;

            //Set up movement configuration
            this.setArrowKeys();

            //Grab player's base velocity
            this.playerVelocity = Player.Instance.Velocity;
        }

        public void UpdateGameSpeed(KeyboardState state)
        {
            double oldVelocity = this.playerVelocity;
            if (state.IsKeyDown(Keys.Q))
            {
                //Player.Instance.Velocity = Math.Abs(velocity / 2);
                Player.Instance.Velocity = oldVelocity * 0.5;
            }
            else
            {
                //Player.Instance.Velocity = velocity;
                Player.Instance.Velocity = this.playerVelocity;
            }
        }
        
        public void UpdateCheatMode(KeyboardState state)
        {
            //Grab old values
            double fireRate = Player.Instance.FireRate;
            double velocity = Player.Instance.Velocity;

            if (state.IsKeyDown(Keys.LeftShift))
            {
                Player.Instance.CheatingOn = true;
                Player.Instance.Invincible = true;
                Player.Instance.Velocity = (velocity * 2);
                Player.Instance.FireRate = (fireRate * 2);
            }
            else
            {
                Player.Instance.CheatingOn = false;
                Player.Instance.Invincible = false;
                Player.Instance.Velocity = velocity;
                Player.Instance.FireRate = fireRate;
            }
        }

        public void InGamePauseMenu(GameManager controller, KeyboardState state, KeyboardState previousState, MenuOption menuStateIn, out GameState newGameState)
        {

            if (state.IsKeyDown(Keys.P))
            {
                newGameState = GameState.PAUSEDMENU;
            }
            else
                newGameState = GameState.PLAY;
        }

        public void HandlePlayerInput(KeyboardState keyboardState)
        {
            //First check if the player has toggled the slow speed
            this.UpdateGameSpeed(keyboardState);
            //Then check if cheat mode is activated
            this.UpdateCheatMode(keyboardState);
            //Then check if player is shooting
            this.Shoot(keyboardState);
            //this.InGamePauseMenu(keyboardState);
            float newX;
            float newY;
            if (this.CheckPlayerMovementStatus(keyboardState, out newX, out newY))
            {
                Player.Instance.Move(newX, newY);
            }
        }

        //Shoot
        public void Shoot(KeyboardState state)
        {
            // Spacebar fires user projectile.
            if (state.IsKeyDown(Keys.Space))
            {
                this.bulletManager.PlayerShoot();
            }
        }

        public void setArrowKeys()
        {
            leftKey = Keys.Left;
            rightKey = Keys.Right;
            upKey = Keys.Up;
            downKey = Keys.Down;
        }
       
        public void setWASDKeys()
        {
            leftKey = Keys.A;
            rightKey = Keys.D;
            upKey = Keys.W;
            downKey = Keys.S;
        }

        //Check if player's position changed
        public bool CheckPlayerMovementStatus(KeyboardState state, out float newX, out float newY)
        {
            float originalX = Player.Instance.Position.X;
            float originalY = Player.Instance.Position.Y;
            newX = originalX;
            newY = originalY;

            //variables to hold calculations preventing redundant calculations
            float movementSpeed = (float)(Player.Instance.Velocity * Player.Instance.Acceleration);
            //down right
            if (state.IsKeyDown(rightKey) && state.IsKeyDown(downKey) && !(state.IsKeyDown(leftKey)) && !(state.IsKeyDown(upKey)))
            {
                newX = originalX + movementSpeed * (float)Math.Cos(Math.PI / 4);
                newY = originalY + movementSpeed * (float)Math.Sin(Math.PI / 4);
            }
            //up right
            if (state.IsKeyDown(rightKey) && state.IsKeyDown(upKey) && !(state.IsKeyDown(leftKey)) && !(state.IsKeyDown(downKey)))
            {
                newX = originalX + movementSpeed * (float)Math.Cos(Math.PI / 4);
                newY = originalY - movementSpeed * (float)Math.Sin(Math.PI / 4);
            }
            //right
            if (state.IsKeyDown(rightKey) && !(state.IsKeyDown(upKey)) && !(state.IsKeyDown(downKey)) && !(state.IsKeyDown(leftKey)))
            {
                newX = originalX + movementSpeed;
            }
            //left down
            if (state.IsKeyDown(leftKey) && state.IsKeyDown(downKey) && !(state.IsKeyDown(upKey)) && !(state.IsKeyDown(rightKey)))
            {
                newX = originalX - movementSpeed * (float)Math.Cos(Math.PI / 4);
                newY = originalY + movementSpeed * (float)Math.Sin(Math.PI / 4);
            }
            //left up
            if (state.IsKeyDown(leftKey) && state.IsKeyDown(upKey) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(rightKey)))
            {
                newX = originalX - movementSpeed * (float)Math.Cos(Math.PI / 4);
                newY = originalY - movementSpeed * (float)Math.Sin(Math.PI / 4);
            }
            //left
            if (state.IsKeyDown(leftKey) && !(state.IsKeyDown(upKey)) && !(state.IsKeyDown(downKey)) && !(state.IsKeyDown(rightKey)))
            {
                newX = originalX - movementSpeed;
            }
            //up
            if (state.IsKeyDown(upKey) && !(state.IsKeyDown(leftKey)) && !(state.IsKeyDown(rightKey)) && !(state.IsKeyDown(downKey)))
            {
                newY = originalY - movementSpeed;
            }
            //down
            if (state.IsKeyDown(downKey) && !(state.IsKeyDown(leftKey)) && !(state.IsKeyDown(rightKey)) && !(state.IsKeyDown(upKey)))
            {
                newY = originalY + movementSpeed;
            }
            //returns true if the player was moved, false otherwise
            return (newX != originalX || newY != originalY) ? true : false;
        }

        public void PlayCursorSoundEffect()
        {
            cursor.CreateInstance().Play();
        }

        public void PlayEnterSoundEffect()
        {
            enter.CreateInstance().Play();
        }

        public MenuOption MenuMove(GameManager controller, KeyboardState state, KeyboardState previousState, MenuOption menuStateIn, out GameState newGameState)
        {

            if (state.IsKeyDown(downKey) && previousState.IsKeyUp(downKey))
            {
                PlayCursorSoundEffect();
                newGameState = GameState.MENU;
                switch (menuStateIn)
                {
                    case MenuOption.PLAY_GAME:
                        return MenuOption.SETTINGS;
                    case MenuOption.SETTINGS:
                        return MenuOption.EXIT;
                    case MenuOption.EXIT:
                        return MenuOption.EXIT;
                }
            }
            else if (state.IsKeyDown(upKey) && previousState.IsKeyUp(upKey))
            {
                PlayCursorSoundEffect();
                newGameState = GameState.MENU;
                switch (menuStateIn)
                {
                    case MenuOption.PLAY_GAME:
                        return MenuOption.PLAY_GAME;
                    case MenuOption.SETTINGS:
                        return MenuOption.PLAY_GAME;
                    case MenuOption.EXIT:
                        return MenuOption.SETTINGS;
                }
            }
            else if (state.IsKeyDown(Keys.Enter) && previousState.IsKeyUp(Keys.Enter))
            {
                PlayEnterSoundEffect();
                switch (menuStateIn)
                {
                    case MenuOption.PLAY_GAME:
                        newGameState = GameState.PLAY;
                        return MenuOption.PLAY_GAME;
                    case MenuOption.SETTINGS:
                        newGameState = GameState.MENU;
                        controller.ScreenManager.SetScreen(ScreenView.SETTINGS);
                        return MenuOption.SETTINGS;
                    case MenuOption.EXIT:
                        newGameState = GameState.EXIT;
                        return MenuOption.EXIT;
                }
            }
            newGameState = GameState.MENU;
            return menuStateIn;
        }

        public MenuOption SettingsMenuMove(GameManager controller, KeyboardState state, KeyboardState previousState, MenuOption menuStateIn, out GameState newGameState)
        {
            if (state.IsKeyDown(upKey) && previousState.IsKeyUp(upKey))
            {
                PlayCursorSoundEffect();
                newGameState = GameState.MENU;
                switch (menuStateIn)
                {
                    case MenuOption.DIFFICULTY:
                        return MenuOption.DIFFICULTY;
                    case MenuOption.ARROWKEYS:
                        return MenuOption.DIFFICULTY;
                    case MenuOption.WASDKEYS:
                        return MenuOption.ARROWKEYS;
                    case MenuOption.MENU:
                        return MenuOption.WASDKEYS;
                }
            }
            else if (state.IsKeyDown(downKey) && previousState.IsKeyUp(downKey))
            {
                PlayCursorSoundEffect();
                newGameState = GameState.MENU;
                switch (menuStateIn)
                {
                    case MenuOption.DIFFICULTY:
                        return MenuOption.ARROWKEYS;
                    case MenuOption.ARROWKEYS:
                        return MenuOption.WASDKEYS;
                    case MenuOption.WASDKEYS:
                        return MenuOption.MENU;
                    case MenuOption.MENU:
                        return MenuOption.MENU;
                }
            }
            else if (state.IsKeyDown(Keys.Enter) && previousState.IsKeyUp(Keys.Enter))
            {
                PlayEnterSoundEffect();
                newGameState = GameState.MENU;

                //Switch to difficulty menu if selected
                if(menuStateIn == MenuOption.DIFFICULTY)
                {
                    controller.ScreenManager.SetScreen(ScreenView.DIFFICULTY_SETTINGS);
                }
                else
                {
                    controller.ScreenManager.SetScreen(ScreenView.MENU);
                }

                //Apply movement changes
                if(menuStateIn == MenuOption.ARROWKEYS)
                {
                    GameConfig.MoveKeys = MoveKeys.ARROW;
                    this.setArrowKeys();
                }
                else if (menuStateIn == MenuOption.WASDKEYS)
                {
                    GameConfig.MoveKeys = MoveKeys.WASD;
                    this.setWASDKeys();
                }
            }
            newGameState = GameState.MENU;
            return menuStateIn;
        }

        public void PausedSettingsMenuMove(GameManager controller, KeyboardState state, KeyboardState previousState)
        {
            if (state.IsKeyDown(upKey))
            {
                PlayCursorSoundEffect();
                GameConfig.MoveKeys = MoveKeys.ARROW;
            }
            else if (state.IsKeyDown(downKey))
            {
                PlayCursorSoundEffect();
                GameConfig.MoveKeys = MoveKeys.WASD;
            }
            else if (state.IsKeyDown(Keys.Enter) && previousState.IsKeyUp(Keys.Enter))
            {
                PlayEnterSoundEffect();
                controller.ScreenManager.SetScreen(ScreenView.PAUSED_MENU);
            }
        }

        public MenuOption GameOverMenuMove(GameManager controller, KeyboardState state, KeyboardState previousState, MenuOption menuStateIn, out GameState newGameState)
        {
            newGameState = GameState.GAMEOVER;
            if (state.IsKeyDown(upKey) && previousState.IsKeyUp(upKey))
            {
                PlayCursorSoundEffect();
                newGameState = GameState.GAMEOVER;
                switch (menuStateIn)
                {
                    case MenuOption.MENU:
                        return MenuOption.MENU;
                    case MenuOption.EXIT:
                        return MenuOption.MENU;
                }
            }
            else if (state.IsKeyDown(downKey) && previousState.IsKeyUp(downKey))
            {
                PlayCursorSoundEffect();
                newGameState = GameState.GAMEOVER;
                switch (menuStateIn)
                {
                    case MenuOption.MENU:
                        return MenuOption.EXIT;
                    case MenuOption.EXIT:
                        return MenuOption.EXIT;
                }
            }
            else if (state.IsKeyDown(Keys.Enter) && previousState.IsKeyUp(Keys.Enter))
            {
                PlayEnterSoundEffect();
                switch (menuStateIn)
                {
                    case MenuOption.MENU:
                        newGameState = GameState.RESET;
                        //newGameState = GameState.MENU;
                        return MenuOption.MENU;
                    case MenuOption.EXIT:
                        newGameState = GameState.EXIT;
                        return MenuOption.EXIT;
                }
            }
            return menuStateIn;
        }

        public DifficultyMenuOption DifficultySettingsMenuMove(GameManager controller, KeyboardState state, KeyboardState previousState, DifficultyMenuOption menuStateIn, out GameState newGameState)
        {
            if (state.IsKeyDown(upKey) && previousState.IsKeyUp(upKey))
            {
                PlayCursorSoundEffect();
                newGameState = GameState.MENU;
                switch (menuStateIn)
                {
                    case DifficultyMenuOption.EASY:
                        return DifficultyMenuOption.EASY;
                    case DifficultyMenuOption.MEDIUM:
                        return DifficultyMenuOption.EASY;
                    case DifficultyMenuOption.HARD:
                        return DifficultyMenuOption.MEDIUM;
                    case DifficultyMenuOption.INSANE:
                        return DifficultyMenuOption.HARD;
                    case DifficultyMenuOption.MENU:
                        return DifficultyMenuOption.INSANE;
                }
            }
            else if (state.IsKeyDown(downKey) && previousState.IsKeyUp(downKey))
            {
                PlayCursorSoundEffect();
                newGameState = GameState.MENU;
                switch (menuStateIn)
                {
                    case DifficultyMenuOption.EASY:
                        return DifficultyMenuOption.MEDIUM;
                    case DifficultyMenuOption.MEDIUM:
                        return DifficultyMenuOption.HARD;
                    case DifficultyMenuOption.HARD:
                        return DifficultyMenuOption.INSANE;
                    case DifficultyMenuOption.INSANE:
                        return DifficultyMenuOption.MENU;
                    case DifficultyMenuOption.MENU:
                        return DifficultyMenuOption.MENU;
                }
            }
            else if (state.IsKeyDown(Keys.Enter) && previousState.IsKeyUp(Keys.Enter))
            {
                PlayEnterSoundEffect();
                newGameState = GameState.MENU;
                controller.ScreenManager.SetScreen(ScreenView.MENU);
            }
            newGameState = GameState.MENU;
            return menuStateIn;
        }

        public MenuOption PausedMenuMove(GameManager controller, KeyboardState state, KeyboardState previousState, MenuOption menuStateIn, out GameState newGameState)
        {
            if (state.IsKeyDown(downKey) && previousState.IsKeyUp(downKey))
            {
                PlayCursorSoundEffect();
                newGameState = GameState.PAUSEDMENU;
                switch (menuStateIn)
                {
                    //case MenuOption.PLAY_GAME:
                    //    return MenuOption.SETTINGS;
                    //case MenuOption.SETTINGS:
                    //    return MenuOption.EXIT;
                    //case MenuOption.EXIT:
                    //    return MenuOption.EXIT;

                    case MenuOption.PLAY_GAME:
                        return MenuOption.EXIT;
                    case MenuOption.EXIT:
                        return MenuOption.EXIT;
                }
            }
            else if (state.IsKeyDown(upKey) && previousState.IsKeyUp(upKey))
            {
                PlayCursorSoundEffect();
                newGameState = GameState.PAUSEDMENU;
                switch (menuStateIn)
                {
                    //case MenuOption.PLAY_GAME:
                    //    return MenuOption.PLAY_GAME;
                    //case MenuOption.SETTINGS:
                    //    return MenuOption.PLAY_GAME;
                    //case MenuOption.EXIT:
                    //    return MenuOption.SETTINGS;

                    case MenuOption.PLAY_GAME:
                        return MenuOption.PLAY_GAME;
                    case MenuOption.EXIT:
                        return MenuOption.PLAY_GAME;
                }
            }
            else if (state.IsKeyDown(Keys.Enter) && previousState.IsKeyUp(Keys.Enter))
            {
                PlayEnterSoundEffect();
                switch (menuStateIn)
                {
                    case MenuOption.PLAY_GAME:
                        newGameState = GameState.PLAY;
                        return MenuOption.PLAY_GAME;
                    case MenuOption.SETTINGS:
                        newGameState = GameState.PAUSEDMENU;
                        controller.ScreenManager.SetScreen(ScreenView.SETTINGS);
                        return MenuOption.SETTINGS;
                    case MenuOption.EXIT:
                        newGameState = GameState.EXIT;
                        return MenuOption.EXIT;
                }
            }
            newGameState = GameState.PAUSEDMENU;
            return menuStateIn;
        }
    }
}
