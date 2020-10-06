using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.View.Screens
{
    public class LoseScreen : Screen
    {
        private readonly Controller.GameManager gameManager;

        //Constructor
        public LoseScreen(Controller.GameManager data)
        {
            gameManager = data;
        }

        //Draw function
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.GetTexture("lose_background"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Return_to_Main_Menu"), new Vector2(270, 375), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Exit_Game"), new Vector2(345, 450), Color.White);
            float selectionY = 0;
            float selectionX = 0;
            switch (gameManager.GameOverMenuOption)
            {
                case MenuOption.MENU:
                    selectionY = 375;
                    selectionX = 600;
                    break;
                case MenuOption.EXIT:
                    selectionY = 450;
                    selectionX = 520;
                    break;
            }
            spriteBatch.Draw(Game1.GetTexture("menu_arrow"), new Vector2(selectionX, selectionY), Color.White);
        }
    }
}
