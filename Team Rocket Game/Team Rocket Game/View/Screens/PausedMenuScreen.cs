using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Team_Rocket_Game.General;

namespace Team_Rocket_Game.View.Screens
{
    class PausedMenuScreen : Screen
    {
        private readonly Controller.GameManager gameManager;

        //Constructor
        public PausedMenuScreen(Controller.GameManager data)
        {
            gameManager = data;
        }

        //Draw function
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.GetTexture("Paused_background"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Continue"), new Vector2(325, 375), Color.White);
            //spriteBatch.Draw(Game1.GetTexture("Settings"), new Vector2(320, 450), Color.White);
            //spriteBatch.Draw(Game1.GetTexture("Exit_Game"), new Vector2(355, 525), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Exit_Game"), new Vector2(355, 450), Color.White);
            float selectionY = 0;
            float selectionX = 0;
            switch (gameManager.MenuOption)
            {
                //case MenuOption.PLAY_GAME:
                //    selectionY = 375;
                //    selectionX = 540;
                //    break;
                //case MenuOption.SETTINGS:
                //    selectionY = 450;
                //    selectionX = 540;
                //    break;
                //case MenuOption.EXIT:
                //    selectionY = 525;
                //    selectionX = 540;
                //    break;
                case MenuOption.PLAY_GAME:
                    selectionY = 375;
                    selectionX = 540;
                    break;
                case MenuOption.EXIT:
                    selectionY = 450;
                    selectionX = 540;
                    break;
            }
            spriteBatch.Draw(Game1.GetTexture("menu_arrow"), new Vector2(selectionX, selectionY), Color.White);
        }
    }
}
