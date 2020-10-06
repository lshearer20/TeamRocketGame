using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Model;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.General;

namespace Team_Rocket_Game.View.Screens
{
    // Our "View" class of our MVC architecturee
    public class GameScreen : Screen
    {
        private readonly GameData gameData;
        SoundEffectInstance song;

        //Constructor
        public GameScreen (GameData data)
        {
            this.gameData = data;

            //Initialize game music
            song = Game1.songs["battle"].CreateInstance();
            song.IsLooped = true;
        }

        public void StartMusic()
        {
            song.Play();
        }

        public void PauseMusic()
        {
            song.Pause();
        }

        public void StopMusic()
        {
            song.Stop();
        }

        //General draw function
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            //Draw enemies
            if (gameData.CurrentEnemies != null)
            {
                foreach (Enemy enemy in gameData.CurrentEnemies)
                {
                    enemy.Draw(spriteBatch);
                }
            }

            //Draw player
            Player.Instance.Draw(spriteBatch);

            //Draw player bullets
            if(gameData.playerBullets.Count != 0)
            {
                foreach(Bullet bullet in gameData.playerBullets)
                {
                    bullet.Draw(spriteBatch);
                }
            }

            //Draw enemy bullets
            if(gameData.enemyBullets.Count != 0)
            {
                foreach(Bullet bullet in gameData.enemyBullets)
                {
                    bullet.Draw(spriteBatch);
                }
            }

            this.DrawPlayerLives(graphics, spriteBatch);
        }

        public void DrawPlayerLives(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            int x_FromLeft = 5, y_FromBottom=55;
            for (int lives = 1; lives <= Player.Instance.Health; lives++)
            {
                Rectangle destinationRectangle = new Rectangle(x: x_FromLeft, y: GameConfig.Height - y_FromBottom, width: 50, height: 50);
                spriteBatch.Draw(Game1.GetTexture("health"), destinationRectangle, Color.White);
                y_FromBottom += 55;
            }
        }
    }
}
