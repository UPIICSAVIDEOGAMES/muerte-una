using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Richter
{
    class Health
    {
        Texture2D hpR;
        Texture2D hpD;

        Texture2D barR;
        Texture2D barD;

        Vector2 posicionHPR, posicionHPD, posicionBarR, posicionBarD;
        Rectangle rectangleHPR, rectangleHPD, rectangleBarR, rectangleBarD;
        
        public void Load(ContentManager Content)
        {
            hpR = Content.Load<Texture2D>("Acciones/Health");
            posicionHPR = new Vector2(30, 25);
            rectangleHPR = new Rectangle(0, 0, hpR.Width, hpR.Height);

            barR = Content.Load<Texture2D>("Acciones/LifeBar");
            posicionBarR = new Vector2(22, 20);
            rectangleBarR = new Rectangle(0, 0, barR.Width, barR.Height);



            hpD = Content.Load<Texture2D>("Death/Health");
            posicionHPD = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth - 55, 25);
            rectangleHPD = new Rectangle(0, 0, hpD.Width, hpD.Height);

            barD = Content.Load<Texture2D>("Death/BossMetter");
            posicionBarD = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth - 100, 25);
            rectangleBarD = new Rectangle(0, 0, barD.Width, barD.Height);
                        
        }


        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                rectangleHPR.Height -= 1;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                if(rectangleHPR.Height <= 217)
                rectangleHPR.Height += 1;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                rectangleHPD.Height -= 1;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                if (rectangleHPD.Height <= 293)
                rectangleHPD.Height += 1;
            }

        }
        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(hpR, posicionHPR, rectangleHPR, Color.White);
            spriteBatch.Draw(barR, posicionBarR, rectangleBarR, Color.White);

            spriteBatch.Draw(hpD, posicionHPD, rectangleHPD, Color.White);
            spriteBatch.Draw(barD, posicionBarD, rectangleBarD, Color.White);
        }
    }
}
