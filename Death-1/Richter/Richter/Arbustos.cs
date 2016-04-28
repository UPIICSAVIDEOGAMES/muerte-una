using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Richter
{
    class Arbustos
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 position;
        Vector2 origin;
        Vector2 velocity;

        int currentFrame = 0;
        int frameHeight;
        int frameWidth;

        float timer;
        float interval = 60;
        float Locate;

        public Arbustos(Texture2D newTexture, Vector2 newPosition, int newFrameHeight, int newFrameWidth)
        {
            texture = newTexture;
            position = newPosition;
            frameHeight = newFrameHeight;
            frameWidth = newFrameWidth;
        }


        public void Update(GameTime gameTime, Background Location)
        {
            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            position = position + velocity;

            Animated(gameTime);
            Locate = Location.velocity.X;

            velocity.X = Locate;

        }


        public void Animated(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 8 || currentFrame < 0)
                    currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }


    }
}
