using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Richter
{
    struct PlayerAnimacion
    {
        Animacion animacion;
        Rectangle rectangulo;

        public Animacion Animacion
        {
            get { return animacion; }
        }

        int frameIndex;
        public int FrameIndex
        {
            get { return frameIndex; }
            set { frameIndex = value; }
        }

        private float timer;

        public Vector2 Origin
        {
            get { return new Vector2(animacion.FrameWidth / 2, animacion.FrameHeight); }
        }

        public void PlayAnimation(Animacion newAnimacion)
        {
            if (animacion == newAnimacion)
                return;

            animacion = newAnimacion;
            frameIndex = 0;
            timer = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spritebatch, Vector2 posicion, SpriteEffects spriteEffects)
        {
            if (Animacion == null)
                throw new NotSupportedException("Que triste se nos fue la vida...");
            
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (timer >= animacion.FrameTime)
            {
                timer -= animacion.FrameTime;

                if (animacion.IsLooping)
                    frameIndex = (frameIndex + 1) % animacion.FrameCount;

                else frameIndex = Math.Min(frameIndex + 1, animacion.FrameCount - 1);
            }


            rectangulo = new Rectangle(FrameIndex * Animacion.FrameWidth, 0, Animacion.FrameWidth, Animacion.FrameHeight);
            spritebatch.Draw(Animacion.Textura, posicion, rectangulo, Color.White, 0f, Origin, 1f, spriteEffects, 0f);

        }

    }
}
