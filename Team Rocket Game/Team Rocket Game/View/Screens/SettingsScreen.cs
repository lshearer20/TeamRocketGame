using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Controller;
using Team_Rocket_Game.General;

namespace Team_Rocket_Game.View.Screens
{
    public class SettingsScreen : Screen
    {
        private readonly GameManager gameManager;

        //Constructor
        public SettingsScreen(GameManager data)
        {
            gameManager = data;
        }

        //Draw function
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.GetTexture("Settings_background"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Difficulty"), new Vector2(47, 300), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Arrow_keys"), new Vector2(50, 375), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Wasd_keys"), new Vector2(47, 450), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Return_to_Main_Menu"), new Vector2(50, 525), Color.White);

            float selectionX = 0;
            float selectionY = 0;

            switch (gameManager.SettingsMenuOption)
            {
                case MenuOption.DIFFICULTY:
                    selectionX = 270;
                    selectionY = 300;
                    break;
                case MenuOption.ARROWKEYS:
                    selectionX = 290;
                    selectionY = 375;
                    break;
                case MenuOption.WASDKEYS:
                    selectionX = 285;
                    selectionY = 450;
                    break;
                case MenuOption.MENU:
                    selectionX = 380;
                    selectionY = 525;
                    break;
            }

            spriteBatch.Draw(Game1.GetTexture("menu_arrow"), new Vector2(selectionX, selectionY), Color.White);
        }
    }
}
