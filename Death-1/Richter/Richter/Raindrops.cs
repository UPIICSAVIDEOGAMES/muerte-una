using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Richter
{
    class Raindrops
    {
        Texture2D texture;
        float spawnWidth;
        float density;

        List<Rain> rain = new List<Rain>();

        float timer;
        Random rand1, rand2;


        public Raindrops(Texture2D newTexture, float newSpawnWidth, float newDensity)
        {
            texture = newTexture;
            spawnWidth = newSpawnWidth;
            density = newDensity;

            rand1 = new Random();
            rand2 = new Random();
        }

        public void createParticle()
        {
            //double thing = rand1.Next();
            rain.Add(new Rain(texture, new Vector2(-50 + (float)rand1.NextDouble()*spawnWidth,0),new Vector2(1, rand2.Next(5,8))));
        }

        public void Update(GameTime gametime, GraphicsDevice graphics)
        {
            timer += (float)gametime.ElapsedGameTime.TotalSeconds;

            while (timer > 0)
            {
                timer -= 1f / density;
                createParticle();
            }

            for (int i = 0; i < rain.Count; i++)
            {
                rain[i].Update();

                if (rain[i].Position.Y > graphics.Viewport.Height)
                {
                    rain.RemoveAt(i);
                    i--;
                }

            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Rain lluvia in rain)
                lluvia.Draw(spritebatch);
        }
    }
}
