using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.View.Screens
{
    //For Parallax background effect
    //Used http://www.jgallant.com/how-to-create-a-parallax-effect-in-monogame/
    public class Background
    {
        public Texture2D texture;
        public Vector2 offset;
        public Vector2 speed;
        private Viewport Viewport;
        public float zoom;

        //Determine our rectangle
        public Rectangle Rectangle
        {
            get { return new Rectangle((int)(offset.X), (int)(offset.Y), (int)(Viewport.Width / zoom), (int)(Viewport.Height / zoom)); }
        }

        public Background(Texture2D newTexture, Vector2 newSpeed, float newZoom)
        {
            this.texture = newTexture;
            this.offset = Vector2.Zero;
            this.speed = newSpeed;
            this.zoom = newZoom;
        }

        public void Update(GameTime gameTime, Vector2 direction, Viewport view)
        {
            //Grab current game time
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Store view
            this.Viewport = view;

            //Find distance to move image by
            Vector2 distance = direction * this.speed * time;

            //Update
            offset += distance;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(this.Viewport.X, this.Viewport.Y), Rectangle, Color.White, 0, Vector2.Zero, zoom, SpriteEffects.None, 1);
        }
    }
}
