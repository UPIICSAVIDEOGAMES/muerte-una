using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Richter
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       
        Player player;
        Death GR;
        Zombie zombie;
        //Cierra cierra;
        HUD health;
        Mictlantecuhtli mic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
           
        }

       
        protected override void Initialize()
        {
            player = new Player();
            GR = new Death();
            zombie = new Zombie();
            mic = new Mictlantecuhtli();
            health = new HUD();
         //   cierra = new Cierra();
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.Load(Content);
            GR.Load(Content);
            mic.Load(Content);
            
           // cierra.Load(Content);
            zombie.Load(Content);
            health.Load(Content);
          
        }
        

        protected override void UnloadContent()
        {
            
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            player.Update(gameTime, GR);
            GR.Update(gameTime, player);
            health.Update(mic, player);
            zombie.Update(gameTime, player);
          //  cierra.Update(gameTime, player);
            
            health.Update(GR, player);
            mic.Update(gameTime, player);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            player.Draw(gameTime, spriteBatch);
          //  cierra.Draw(gameTime, spriteBatch);
         
            GR.Draw(gameTime, spriteBatch);
            zombie.Draw(gameTime,spriteBatch);
            mic.Draw(gameTime, spriteBatch);
            health.Draw(gameTime, spriteBatch);
           
         spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

