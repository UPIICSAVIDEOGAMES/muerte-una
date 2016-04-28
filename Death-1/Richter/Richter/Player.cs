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
    class Player
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

        //Elipse

        Punto puntoPosicion;
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
        public Vector2 posicion;
        public Vector2 velocity;

        //      PUNTOS PARA DEFINIR EL MOVIMIENTO ELIPTICO DE LA MUERTE Y OTROS OBJETOS

        private Punto vertice1, vertice2, foco1, foco2;
        private bool movimientoCompleto = false;
        private Elipse elipse;
        int randomX1, randomX2, randomF1, randomF2;
        int vertice; // Toma el valor de 1 y 2 para indicar en que veritce estas
        


        //      ATRIBUTOS POR DEFECTO AL CREAR UN PLAYER
        public Player()
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


        //+++++++++++++++++++++++++    METODOS OBLIGADOS DEL JUEGO    ++++++++++++++++++++++++++++++++++++++
        public void Load(ContentManager Content)
        {
            caminar = new Animacion(Content.Load<Texture2D>("Acciones/Caminar"), 61, 0.12f, true);
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
            calularMetricas(0);
            posicion = new Vector2(randomX1+2, GraphicsDeviceManager.DefaultBackBufferHeight - 300);
            puntoPosicion = elipse.movePunto(posicion.X);
            //      Vertice 1
            vertice = 1;

        }

        public void calularMetricas(int posicionInicial)
        {

            Random random = new Random();
            int posicionFinal = GraphicsDeviceManager.DefaultBackBufferWidth/2;

            //      CONOCER DESDE QUE PUNTO DEBE COMENZAR A CALCULAR EL V1
            if (randomX1 <= 0)
            {
                if (posicionInicial < posicionFinal)
                    randomX1 = random.Next(posicionInicial, GraphicsDeviceManager.DefaultBackBufferWidth - 236);
                else
                    randomX1 = random.Next(0, posicionInicial);
                //      VERTICES ALEATORIOS DE LA ELIPSE
                randomX2 = random.Next(randomX1, GraphicsDeviceManager.DefaultBackBufferWidth - 236);
            }

            else
            {
                int numero = random.Next(0, GraphicsDeviceManager.DefaultBackBufferWidth - 236);
                switch(vertice)
                {
                    case 1:
                        if (numero < randomX1)
                        {
                            randomX2 = randomX1;
                            randomX1 = numero;
                            
                        }
                        else
                        {
                            randomX2 = numero;
                        
                        }
                    break;
                    case 2:
                        if (numero > randomX2)
                        {
                            randomX1 = randomX2;
                            randomX2 = numero;
                        }
                        else
                        {
                            randomX1 = numero;
                        }
                    break;
                }
                //      VERTICES ALEATORIOS DE LA ELIPSE
              
            }

            
          
            //      FOCOS ALEATORIOS DE LA ELIPSE

             randomF1 = random.Next(randomX1, randomX2);
             randomF2 = random.Next(randomF1, randomX2);
      
            vertice1 = new Punto(randomX1, GraphicsDeviceManager.DefaultBackBufferHeight - 300);
            vertice2 = new Punto(randomX2, GraphicsDeviceManager.DefaultBackBufferHeight - 300);
            foco1 = new Punto(randomF1, GraphicsDeviceManager.DefaultBackBufferHeight - 300);
            foco2 = new Punto(randomF2, GraphicsDeviceManager.DefaultBackBufferHeight - 300);

            elipse = new Elipse();
            elipse.setF1(foco1);
            elipse.setF2(foco2);
            elipse.setV1(vertice1);
            elipse.setV2(vertice2);
            elipse.calCentro(vertice1, vertice2);
            elipse.calcCA();
            elipse.calcB();
        }

        public void Update(GameTime gameTime, Death death)
        {


            switch (vertice)
            {
                    //      LA MUERTE SE MUEVE AL VERTICE 2
                case 1:
                    if (posicion.X < vertice2.GetX()-1 && posicion.X > vertice1.GetX()-1)
                        puntoPosicion = elipse.movePunto(posicion.X + 1);
                    else
                    {
                        vertice = 2;
                        calularMetricas((int)posicion.X);
                    }
                    break;
                    //      LA MUERTE SE MUEVE AL VERTICE 1
                case 2:

                    if (posicion.X < vertice2.GetX() && posicion.X > vertice1.GetX())
                        puntoPosicion = elipse.movePunto(posicion.X - 1);
                    else
                    {
                        vertice = 1;
                        calularMetricas((int)posicion.X);
                    }   
                    break;
                default:
                    break;
            }
           /* if ((posicion.X < vertice2.GetX() && posicion.X >= vertice1.GetX()) && movimientoCompleto==false)
                puntoPosicion = elipse.movePunto(posicion.X + 1);
           
            else
            {
                movimientoCompleto = true;
               if (posicion.X >= vertice2.GetX()-1)
               {
                   puntoPosicion = elipse.movePunto(posicion.X - 1);
               }
               else if (posicion.X <= vertice1.GetX() + 1)
                   puntoPosicion = elipse.movePunto(posicion.X + 1);
               else
               {
                   movimientoCompleto = false;
                   calularMetricas((int)posicion.X);
               }
            }
            */
            //animacionPlayer.PlayAnimation(caminar);
            posicion.X = puntoPosicion.GetX();
            posicion.Y = puntoPosicion.GetY();

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


            acciones(gameTime, death);

        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fon, "Puntos en V1 (" + randomX1 + "," + (GraphicsDeviceManager.DefaultBackBufferWidth - 1) + "), V2 (" + randomX2 + "," + (GraphicsDeviceManager.DefaultBackBufferWidth - 1) + ") \n Focos:  Puntos en F1 (" + randomF1 + "," + (GraphicsDeviceManager.DefaultBackBufferWidth - 1) + "), F2 (" + randomF1 + "," + (GraphicsDeviceManager.DefaultBackBufferWidth - 1) + ")" + "\n         puntoPosicion.GetX() " + puntoPosicion.GetX() + ",puntoPosicion.GetY()" + puntoPosicion.GetY(), new Vector2(0, 10), Color.White);


            SpriteEffects flip = SpriteEffects.None;

            if (flipeado == false)
                flip = SpriteEffects.None;
            else if (flipeado == true)
                flip = SpriteEffects.FlipHorizontally;

            //Pintando Oz
            if (LanzarOz == true)
            {
                spriteBatch.Draw(Oz, new Rectangle((int)PosOzX + 30, (int)PosOzY, Oz.Width, Oz.Height), Color.White);
            }

            spriteBatch.Draw(hitText, hitRect, Color.White);
            spriteBatch.Draw(standText, standRect, Color.White);
            animacionPlayer.Draw(gameTime, spriteBatch, posicion, flip);
        }


        // ++++++++++++++++++++++++++++    ACCIONES A REALIZAR    ++++++++++++++++++++++++++++++++++++++++  //
        public void acciones(GameTime gametime, Death death)
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

                if (timeCounter >= 11)
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

              
                /* if (playerDistanceX >= -900 && playerDistanceX <= 900 && playerDistanceY >= -900 && playerDistanceY <= 900)
                 {
                     if (playerDistanceX < 10 && playerDistanceY < 10)
                     {


                         velocity.X = -1f;
                          velocity.Y = -1f;
                     }
                     else if (playerDistanceX < 10 && playerDistanceY > 10)
                     {
                         velocity.X = -1f;
                         velocity.Y = 1f;
                     }
                     else if (playerDistanceX > 10 && playerDistanceY > 10)
                     {
                         velocity.X = 1f;
                         velocity.Y = 1f;
                     }
                     else if (playerDistanceX > 10 && playerDistanceY < 10)
                     {
                         velocity.X = 1f;
                         velocity.Y = -1f;
                     }

                     //atacar cuando estés cerca del enemigo
                     else if (playerDistanceX < 0.2f)
                     {
                         atack = true;
                     }
                    
                 }
                 siguiendo = false;

             }
             else if(siguiendo == false) { velocity.X = 0f; }*/

            }

        }

    }
}
