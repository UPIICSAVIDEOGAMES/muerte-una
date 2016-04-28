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
    class Cierra
    {
        //variables de la cirra

        PlayerAnimacion animacionCierra;
        Animacion cierraGirando;
        Vector2 position;
        Vector2 velocity;
        bool lanzar = true;
        int x;
        bool hasJumped=true;
        int caso;
        Texture2D cierra;
        bool toma = false;

        public Vector2 getPosition()
        {
            return position;
        }
        public void setPosition(Vector2 position)
        {
            this.position = position;
        }
        public Cierra(){
        

        }
        public void Load(ContentManager Content)
        {
            cierraGirando = new Animacion(Content.Load<Texture2D>("Acciones/cierra"), 62, 0.25f, true);
            cierra = Content.Load<Texture2D>("Rectangles/RectangleCierra");
          
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;   
            spriteBatch.Draw(cierra, position, Color.White);
            animacionCierra.Draw(gameTime, spriteBatch, position, flip);
        }
        public void reposicion(float posicionx) {
            velocity.X = posicionx;
            position.X = velocity.X;

        } 
        public void Update(GameTime gameTime, Player player) {

            if (lanzar)
            {
                position.X = player.hitRect.X ;

                lanzar = false;
                if (player.backflip == true)
                    caso = 1;
                else
                    caso = 2;
            }
            switch (caso)
            {
                case 1:
                    position.X += 1;
                    break;
                case 2:
                    position.X -= 1;
                    break;
            }
          
            position += velocity;

            /*if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocity.X = 2f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocity.X = -2f;

            else velocity.X = 0f;

            /*if (Keyboard.GetState().IsKeyDown(Keys.Z) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -5f;
                hasJumped = true;
            }
            */
            if (hasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i;
            }

            if (position.Y + cierra.Height >= 450)
                hasJumped = false;

            if (hasJumped == false)
                velocity.Y = 0f;

            if (velocity.Y == 0)
            {
                toma = true;
               
            }

            animacionCierra.PlayAnimation(cierraGirando);
    
                
            
            
}
      }
    
}
