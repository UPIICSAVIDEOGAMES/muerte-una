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
    class Boss
    {
        PlayerAnimacion animacionBoss;
        Animacion caminar;
        Animacion atacar;

        float playerDistance;
        float newdistance = 150;
        float oldDistance = 150;

        bool right;
        bool flipeado = true;

        Vector2 posicion = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth + 60, GraphicsDeviceManager.DefaultBackBufferHeight - 45);
        public Vector2 velocity;

        public void Load(ContentManager Content)
        {
            caminar = new Animacion(Content.Load<Texture2D>("Death/Death"), 130, 0.26f, true);
            atacar = new Animacion(Content.Load<Texture2D>("Death/DeathAtt"), 360, 0.12f, true);
        }

        public void Update(Player richter)
        {
            animacionBoss.PlayAnimation(caminar);
            posicion += velocity;

            playerDistance = richter.posicion.X - posicion.X;


            if (newdistance <= 0)
            {
                right = true;
                flipeado = false;
                velocity.X = 1.5f;
            }

            else if (newdistance >= oldDistance)
            {
                right = false;
                flipeado = true;
                velocity.X = -1.5f;
            }

            if (right) newdistance += 1; 
            else newdistance -= 1;

            if (playerDistance >= -200 && playerDistance <= 200)
            {
                if (playerDistance < -3)
                    velocity.X = -1.5f;
                else if (playerDistance > 3)
                    velocity.X = 1.5f;
                else if (playerDistance == 0)
                    velocity.X = 0f;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.FlipHorizontally;

            if (flipeado == false)
                flip = SpriteEffects.None;
       
            animacionBoss.Draw(gameTime, spriteBatch, posicion, flip);
        }

    }
}
