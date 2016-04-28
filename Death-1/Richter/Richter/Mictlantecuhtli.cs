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
    class Mictlantecuhtli
    {
        //esta bandera sirve para finjir que eres una estatua cuando llega el peronaje principal la primera vez
        bool banderaPrincipio;

        List<Cierra> cierras;
        List<Zombie> zombies;
        PlayerAnimacion animacionMictl; 
        Animacion mictSentado;
        Animacion mictEnfadado;
        Animacion mictMandarAtaque;

        public Vector2 posicion;

        Texture2D mictText;
        public Rectangle mictRect;
        public Vector2 mictlPos;
      
        Player player;


        ContentManager Content;
        GameTime gameTime;
        SpriteBatch spriteBatch;
      
        public Mictlantecuhtli()
        {
         cierras=new List<Cierra>();
         zombies = new List<Zombie>();
        }

        public void Load(ContentManager Content)
        {
            //Animaciones
            mictSentado = new Animacion(Content.Load<Texture2D>("Acciones/Mictlantecuhtli"), 236, 0.25f, false);
            mictEnfadado = new Animacion(Content.Load<Texture2D>("Acciones/MictlantecuhtliEnfado"), 236, 0.25f, false);
            mictMandarAtaque = new Animacion(Content.Load<Texture2D>("Acciones/MicltanzLanar"), 236, 0.25f, true);
            mictText = Content.Load<Texture2D>("Rectangles/RectangleMictlantecuhtli");
            

            this.Content = Content;
        }

        public void Update(GameTime gameTime, Player player)
        {
            this.player = player;
            mictRect = new Rectangle((int)mictlPos.X, (int)mictlPos.Y, mictText.Width, mictText.Height);

            mictlPos.X = GraphicsDeviceManager.DefaultBackBufferWidth-(236);
            mictlPos.Y = 100;
            posicion.X = GraphicsDeviceManager.DefaultBackBufferWidth-(236/2);
            posicion.Y = GraphicsDeviceManager.DefaultBackBufferHeight - (100);
            if (player.hitRect.X < GraphicsDeviceManager.DefaultBackBufferWidth / 4 && !banderaPrincipio)
            {
                animacionMictl.PlayAnimation(mictSentado);
                banderaPrincipio = true;
            }
            else if (player.hitRect.X < GraphicsDeviceManager.DefaultBackBufferWidth / 2 && banderaPrincipio)
            {
                animacionMictl.PlayAnimation(mictEnfadado);
              

            }
            else
                animacionMictl.PlayAnimation(mictMandarAtaque);
          
            Ataque(gameTime, player);
           
            foreach (Cierra cierra in cierras)
            {
                
                cierra.Update(gameTime, player);
            }
            foreach (Zombie zombie in zombies)
            {

                zombie.Update(gameTime, player);
            }
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            SpriteEffects flip = SpriteEffects.None;
            spriteBatch.Draw(mictText, mictRect, Color.White);
            animacionMictl.Draw(gameTime, spriteBatch, posicion, flip);
            foreach (Cierra cierra in cierras)
            {
                cierra.Draw(gameTime, spriteBatch);
            }
            foreach (Zombie zombie in zombies)
            {
               
                zombie.Update(gameTime, player);
            }
        }

        public void Ataque(GameTime gameTime, Player player)
        {
            Random random = new Random();
            double ataqueProb = random.NextDouble();
            //Ataque zombie tiene una probabilidad de 0.4
            if (ataqueProb<=0.4)
            {
                if (cierras.Count < 6)
                {
                    Cierra cierra = new Cierra();
                    cierra.Load(Content);
                    cierras.Add(cierra);
                    
                    
                }
                if (cierras[0].getPosition().X < 0 || cierras[0].getPosition().X > mictlPos.X)
                {
                    cierras.Remove(cierras[0]);
                }
            }

            //Ataque cierra tiene una probabilidad de 0.1
            if (ataqueProb <= 0.5)
            {
                if (zombies.Count < 5)
                {
                    Zombie zombie = new Zombie();
                    zombie.Load(Content);
                    zombies.Add(zombie);


                }
                if (zombies[0].getPosition().X < 0 || zombies[0].getPosition().X > mictlPos.X)
                {
                    zombies.Remove(zombies[0]);
                }
            }
            //Ataque lanza tiene una probabilidad de 0.2
            
            //Ataque succionar el alma tiene una probabilidad de 0.2
            
            //Ataque comer tiene una probabilidad de 0.1

        }

    }
}
