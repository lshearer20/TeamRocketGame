using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller;

namespace Team_Rocket_Game.View.Screens
{
    public class DifficultySettingsScreen : Screen
    {
        private readonly GameManager gameManager;

        //Constructor
        public DifficultySettingsScreen(GameManager data)
        {
            gameManager = data;
        }

        //Draw function
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.GetTexture("Difficulty_Settings_background"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Easy"), new Vector2(50, 225), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Medium"), new Vector2(47, 300), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Hard"), new Vector2(50, 375), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Insane"), new Vector2(50, 450), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Return_to_Main_Menu"), new Vector2(50, 525), Color.White);

            float selectionX = 0;
            float selectionY = 0;

            switch (gameManager.DifficultySettingsMenuOption)
            {
                case DifficultyMenuOption.EASY:
                    selectionX = 145;
                    selectionY = 225;
                    break;
                case DifficultyMenuOption.MEDIUM:
                    selectionX = 190;
                    selectionY = 300;
                    break;
                case DifficultyMenuOption.HARD:
                    selectionX = 150;
                    selectionY = 375;
                    break;
                case DifficultyMenuOption.INSANE:
                    selectionX = 175;
                    selectionY = 450;
                    break;
                case DifficultyMenuOption.MENU:
                    selectionX = 380;
                    selectionY = 525;
                    break;
            }

            spriteBatch.Draw(Game1.GetTexture("menu_arrow"), new Vector2(selectionX, selectionY), Color.White);
        }
    }
}
