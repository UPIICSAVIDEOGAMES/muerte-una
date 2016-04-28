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
    class Zombie
    {
        //      CLASE QUE EJECUTA LOS FOTOGRAMAS DEL SPRITE
        public PlayerAnimacion animacionPlayer;


        //      RICHTER: SPRITES QUE SERAN USADOS EN EL JUEGO
        Animacion agachar;
        Animacion atacar;
        Animacion bflip; //aventarse
        Animacion caminar;
        Animacion crouchAtk; //barrida
        Animacion dies; //muere
        Animacion stand; //parado
        Animacion hurts; //pegar y echar para atras
        Animacion iThrow; //lanza bola de fuego        
        Animacion saltar;

        //Oz
        Texture2D Oz;
        float PosOzX = 0;
        float PosOzY = 0;
        Int32 velocidadOz = 10;
        public bool LanzarOz = false;


        //      RECTANGULOS DE COLISION DE RICHTER (atacado, pararse en plataforma)
        Texture2D hitText; //tronco
        public Rectangle hitRect;
        Vector2 getHitted; //ser golpeado 

        Texture2D standText; //los pies
        public Rectangle standRect;        
        Vector2 getStand; //estar parado


        //      BOOLEANOS DE LAS ACCIONES A EJECUTAR;
        public bool salto = false;
        public bool backflip = false;
        public bool flip = false;
        public bool icrush = false;
        public bool die = false;
        public bool gethit = false;
        public bool siguiendo = false;
        public bool piso = false;
        public bool crouch = false;
        public bool atack = false;
        public bool cattack = false;
        public bool shoot = false;
        public bool gready = false;
        public bool right = false;
        public bool caminando = false;
        public bool levitando = false;

        float timeCountRandom = 0;
        float timeCounter = 0;

        Random r = new Random();
        double aleat;
        //aleat = r.NextDouble(); 

        float playerDistanceX;
        float playerDistanceY;

        //      EFECTO DE SPRITE; EN ESTE CASO GIRAR DERECHA O IZQUIERDA
        bool flipeado = false;
        
        //score para verificar
        SpriteFont fon;
        int score = 0;
       
       //      VALOR DE LA VELOCIDAD DE TRASLACION APLICADO EN Acciones()
        float velValor = 0.15f;  


        //      POSICION DE INICIO Y VARIABLE DE VELOCIDAD DEL PLAYER
        public Vector2 posicion = new Vector2(300, GraphicsDeviceManager.DefaultBackBufferHeight - 300);
        public Vector2 velocity;


        //      ATRIBUTOS POR DEFECTO AL CREAR UN PLAYER
        public Zombie()
        {
            salto = false;
            flip = true;
            backflip = false;
            icrush = false;
            die = false;
            gready = false;
            gethit = false;
            siguiendo = true;
            caminando = true;
            piso = false;
            levitando = true;
        }

        public void setPosition(Vector2 position)
        {
            this.posicion = position;
        }
        public Vector2 getPosition()
        {
            return this.posicion;
        }
        //+++++++++++++++++++++++++    METODOS OBLIGADOS DEL JUEGO    ++++++++++++++++++++++++++++++++++++++
        public void Load(ContentManager Content)
        {
            caminar = new Animacion(Content.Load<Texture2D>("Acciones/zombieDer"), 61, 0.12f, true);
            agachar = new Animacion(Content.Load<Texture2D>("Acciones/Agachar"), 61, 0.12f, false);
            saltar = new Animacion(Content.Load<Texture2D>("Acciones/Saltar"), 61, 0.12f, false);
            bflip = new Animacion(Content.Load<Texture2D>("Acciones/Backflip"), 80, 0.12f, false);
            stand = new Animacion(Content.Load<Texture2D>("Acciones/Stand"), 80, 0.10f, false);
            dies = new Animacion(Content.Load<Texture2D>("Acciones/Die"), 80, 0.10f, false);
            hurts = new Animacion(Content.Load<Texture2D>("Acciones/Herir"), 60, 0.10f, true);
            iThrow = new Animacion(Content.Load<Texture2D>("Acciones/ItemThrow"), 70, 0.10f, false);
            atacar = new Animacion(Content.Load<Texture2D>("Acciones/SimAtk"), 80, 0.12f, false);
            
            //      ATAQUE AGACHADO
            crouchAtk = new Animacion(Content.Load<Texture2D>("Acciones/CAttack"), 70, 0.05f, false);

            //      RECTANGULOS UTILIZADOS PARA LAS COLISIONES
            hitText = Content.Load<Texture2D>("Rectangles/RectangleHit");
            standText = Content.Load<Texture2D>("Rectangles/RectangleStand");
            fon = Content.Load<SpriteFont>("fuente");

            Oz = Content.Load<Texture2D>("Rectangles/Platform");


        }


        public void Update( GameTime gameTime, Player death)
        {

           //animacionPlayer.PlayAnimation(caminar);
            posicion += velocity;

            // DISTANCIA ENTRE ENEMIGO(RITCHER) Y LOBITO
            playerDistanceX = death.posicion.X - posicion.X;
            playerDistanceY = death.posicion.Y - posicion.Y;

            #region "Movimientos Aleatorios"

            timeCountRandom += 0.25F;

            if (die == false)
            {
                if (timeCountRandom >= 50)
                {
                    aleat = r.NextDouble();

                    if (aleat < 0.50)
                    {
                        LanzarOz = true;
                    }
                    else if (aleat > 0.50)
                    {
                        LanzarOz = true;
                    }


                    timeCountRandom = 0;
                }
            }

            #endregion

            //
            siguiendo = false;
            piso = false;
            caminando = false;
            levitando = false;

            standRect = new Rectangle((int)posicion.X, (int)posicion.Y, standRect.Width, standRect.Height);
            hitRect = new Rectangle((int)posicion.X, (int)posicion.Y, hitRect.Width, hitRect.Height);
       

            //      POSICIONES DE LOS RECTANGULOS DE COLISION
            getHitted.X = posicion.X - 10;
            getHitted.Y = posicion.Y - 85;
            getStand.X = posicion.X - 23;
            getStand.Y = posicion.Y - 18;


            acciones(gameTime,death);

         }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fon, "Puto" + score, new Vector2(0, 10), Color.White);


            SpriteEffects flip = SpriteEffects.None;

            if (flipeado == false)
                flip = SpriteEffects.None;
            else if (flipeado == true)
                flip = SpriteEffects.FlipHorizontally;

           /* //Pintando Oz
            if (LanzarOz == true)
            {
                spriteBatch.Draw(Oz, new Rectangle((int)PosOzX + 30 , (int)PosOzY, Oz.Width, Oz.Height), Color.White);
            }
            */
            spriteBatch.Draw(hitText, hitRect, Color.White);
            spriteBatch.Draw(standText, standRect, Color.White); 
            animacionPlayer.Draw(gameTime, spriteBatch, posicion, flip);
        }


        // ++++++++++++++++++++++++++++    ACCIONES A REALIZAR    ++++++++++++++++++++++++++++++++++++++++  //
        public void acciones(GameTime gametime, Player death)
        {
            //      ***SELECCION ACCIONES***      //

            #region "Entorno"

            // AL NO ESTAR EN EL PISO

            
            /*if (posicion.Y >= GraphicsDeviceManager.DefaultBackBufferHeight - 50)
            {
                velocity.Y = 0f;
                
                caminando = true;
                siguiendo = true;
            }*/

                // CUANDO YA ESTÁS SOBRE EL PISO "Y" NO SE MUEVE
            

           

            #endregion

            #region "Salto Normal"

            if (salto == true)
            {
                float i = 2f;
                velocity.Y += velValor * i;
            }

            if (salto == false)
            {
                velocity.Y = 0f;
            }

            #endregion

            #region "Backflip"

            if (backflip == true)
            {
                crouch = false;

                timeCounter += 0.25F;

                posicion.Y -= 8f;
                velocity.Y = -0.001f;
                velocity.X = -0.10f;

                animacionPlayer.PlayAnimation(bflip);
                salto = true;

                if (flipeado == false)
                {
                    posicion.X -= 22f;
                }

                else if (flipeado == true)
                {
                    posicion.X += 22f;
                }

                if (timeCounter >= 1.55)
                {
                    backflip = false;
                    timeCounter = 0;
                }
            }

            #endregion

            #region "Flip"

            if (flip == true)
            {
                animacionPlayer.PlayAnimation(saltar);
                flip = false;
            }

            #endregion

            #region "Agacharse"

            if (crouch == true)
            {
                timeCounter += 0.25F;
                velocity.X = 0f;

                animacionPlayer.PlayAnimation(agachar);
                
                if (timeCounter >= 7)
                {
                    crouch = false;
                    timeCounter = 0;
                }
            }

            #endregion

            #region "Muerte"

            if (die == true)
            {
                animacionPlayer.PlayAnimation(dies);
            }

            #endregion

            #region "Ataque"

            if (atack == true)
            {
                timeCounter += 0.25F;
                //velocity.X = 0f;
               
                velocity.Y = -0.001f;
                velocity.X = 0f;

                animacionPlayer.PlayAnimation(atacar);
                
                if (timeCounter >=11)
                {
                    atack = false;
                    timeCounter = 0;
                }
            }

            #endregion

            #region "Ser Atacado"

            if (gethit == true)
            {
                atack = false;
                //backflip = false;

                timeCounter += 0.25F;

                posicion.Y -= 21f;
                velocity.Y = -0.85f;
                
                animacionPlayer.PlayAnimation(hurts);
                salto = true;

                if (flipeado == false)
                {
                    posicion.X -= 27f;
                }

                else if (flipeado == true)
                {
                    posicion.X += 27f;
                }

                if (timeCounter >= 1)
                {
                    gethit = false;
                    timeCounter = 0;
                }

            }

            #endregion

            #region "Lanzar Oz"

            if (LanzarOz == true)
            {
                siguiendo = false;

                timeCounter += 0.25F;
                velocity.Y = -0.001f;
                velocity.X = 0f;
                score += 1;

                PosOzY -= 1;
                PosOzX -= 1;

                if (timeCounter >= 23)
                {
                    LanzarOz = false;
                    caminando = true;
                    siguiendo = true;
                    timeCounter = 0;
                }
            }
            else
            {
                PosOzX = posicion.X;
                PosOzY = posicion.Y;
            }

            #endregion


            /*
            //      LANZAR ITEM
            if (shoot == true)
            {
                animacionPlayer.PlayAnimation(iThrow);
                shoot = false;
            }

            //      ATAQUE BAJO
            if (cattack == true)
            {
                animacionPlayer.PlayAnimation(crouchAtk);
                cattack = false;
            } */

            #region "Buscar Posición del Jugador(Ritcher)"

            
                if (die == false)
                {
                    if (LanzarOz == false) { if (atack == false) { siguiendo = true; caminando = true; } } 
                    

                //flip para saber tu orientacion
                if (posicion.X < death.posicion.X)
                {
                    right = true;
                    flipeado = false;
                    //  velocity.X = 1.5f;
                }

                else if (posicion.X > death.posicion.X)
                {
                    right = false;
                    flipeado = true;
                    //  velocity.X = -1.5f;
                }
                }
            

            #endregion

            // ** ACCIÓN DE CAMINAR ** //
            if (caminando == true)
            {
                animacionPlayer.PlayAnimation(caminar);
                caminando = false;
            }

            // *** SEGUIR AL ENEMIGO (JUGADOR RITCHER) *** //
            if (siguiendo == true)
            {
                

                if (playerDistanceX >= -900 && playerDistanceX <= 900 && playerDistanceY >= -900 && playerDistanceY <= 900)
                {
                    if (playerDistanceX < 10 && playerDistanceY < 10)
                    {
                        velocity.X = -1f;
                   // velocity.Y = -1f;
                    }
                    else if (playerDistanceX < 10 && playerDistanceY > 10)
                    {
                        velocity.X = -1f;
                       // velocity.Y = 1f;
                    }
                    else if (playerDistanceX > 10 && playerDistanceY > 10)
                    {
                        velocity.X = 1f;
                       // velocity.Y = 1f;
                    }
                    else if (playerDistanceX > 10 && playerDistanceY < 10)
                    {
                        velocity.X = 1f;
                       // velocity.Y = -1f;
                    }

                    //atacar cuando estés cerca del enemigo
                    else if (playerDistanceX < 0.2f)
                    {
                        atack = true;
                    }
                    
                }
                siguiendo = false;

            }
            else if(siguiendo == false) { velocity.X = 0f; }
       }

   }

}
