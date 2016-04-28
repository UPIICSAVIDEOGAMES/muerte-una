using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
namespace Richter
{
    class HUD
    {
        Texture2D hpR;
        Texture2D barR;

        Vector2 posicionHPR, posicionBarR;
        Rectangle rectangleHPR, rectangleBarR;

        public Vector2 position = new Vector2(75, 100);

        public void Load(ContentManager Content)
        {
            hpR = Content.Load<Texture2D>("Health Bar/Health");
            posicionHPR = new Vector2(30, 25);
            rectangleHPR = new Rectangle(0,0,hpR.Width, hpR.Height);

            barR = Content.Load<Texture2D>("Health Bar/LifeBar");
            posicionBarR = new Vector2(22, 20);
            rectangleBarR = new Rectangle(0, 0, barR.Width, barR.Height);
       
        }
        public void Update(Mictlantecuhtli mic, Player jugador)
        {

            if (mic.mictRect.Intersects(jugador.hitRect))
            {
                if (rectangleHPR.Height >= 0)
                {
                    rectangleHPR.Height -= 25;
                    jugador.gethit = true;
                }

                else if (rectangleHPR.Height <= 0)
                {
                    jugador.die = true;
                }
            }
        }
            public void Update(Death death, Player ritcher)
            {

                if (death.reaperRect.Intersects(ritcher.hitRect))
                {
                    if (rectangleHPR.Height >= 0)
                    {
                        rectangleHPR.Height -= 25;
                        ritcher.gethit = true;
                    }

                    else if (rectangleHPR.Height <= 0)
                    {
                        ritcher.die = true;
                    }
                }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(hpR, posicionHPR, rectangleHPR,Color.White);
                spriteBatch.Draw(barR, posicionBarR, rectangleBarR, Color.White);
            }

    }
}
