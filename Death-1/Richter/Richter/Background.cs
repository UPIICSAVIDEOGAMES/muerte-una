using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Richter
{
    class Background
    {
        Animacion fondo;
        PlayerAnimacion level;

        Vector2 posicion = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth - 140, GraphicsDeviceManager.DefaultBackBufferHeight + 90);
        public Vector2 velocity;

        
        public void Load(ContentManager Content)
        {
            fondo = new Animacion(Content.Load<Texture2D>("Level/Background"), 1330, 685, false);
        }


        public void Update()
        {
            level.PlayAnimation(fondo);
            posicion += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = -3f;

                if (posicion.X <= GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 200)
                    velocity.X = 0f;

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = 3f;

                if (posicion.X >= GraphicsDeviceManager.DefaultBackBufferWidth - 145)
                    velocity.X = 0f;
            }

            else velocity.X = 0f;


        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;
            level.Draw(gameTime, spriteBatch, posicion, flip);
        }

    }
}
