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
    public class MenuScreen : Screen
    {
        private readonly GameManager gameManager;
        private SoundEffectInstance song;

        //Constructor
        public MenuScreen(GameManager data)
        {
            gameManager = data;

            //Initialize menu music and sound effects
            song = Game1.songs["menu"].CreateInstance();
            song.IsLooped = true;
        }

        public void StartMusic()
        {
            song.Play();
        }

        public void StopMusic()
        {
            song.Stop();
        }

        //Draw function
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.GetTexture("menu_background"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Play_game"), new Vector2(325, 375), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Settings"), new Vector2(325, 450), Color.White);
            spriteBatch.Draw(Game1.GetTexture("Exit"), new Vector2(325, 525), Color.White);
            float selectionY = 0;
            float selectionX = 0;
            switch (gameManager.MenuOption)
            {
                case MenuOption.PLAY_GAME:
                    selectionY = 375;
                    selectionX = 550;
                    break;
                case MenuOption.SETTINGS:
                    selectionY = 450;
                    selectionX = 535;
                    break;
                case MenuOption.EXIT:
                    selectionY = 525;
                    selectionX = 480;
                    break;
            }
            spriteBatch.Draw(Game1.GetTexture("menu_arrow"), new Vector2(selectionX, selectionY), Color.White);
        }
    }
}
