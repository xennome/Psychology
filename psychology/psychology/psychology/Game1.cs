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

namespace psychology
{
    /// <summary>
    /// Tipo principal del juego
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        enum Juegostate
        {

            Menu,
            Juego1,
            Intermedio,
            Juego2,
            Juego3,
            Score,
            Pausa,
            Logo
        };
        Juegostate StateJuego;

        int screenWidth=1024;
        int screenHeight=768;


        Random rnd;
        int simboloElegido;
        int palabraElegida;

        float tiempoLogo;
        float tiempoLimiteLogo;
        int fallosObjetos;
        int fallosPalabras;



        SpriteFont tizaFont;
        public SpriteFont tizaFontPalabras;


        SoundEffect soundEngine;
        SoundEffectInstance WhootGames;
        SoundEffectInstance EligePalabra;
        SoundEffectInstance EncuentraSimbolo;
        SoundEffectInstance RecuerdaPalabra;
        SoundEffectInstance Wrong;
        SoundEffectInstance Correct;

        bool sonarMusica;
        
        Texture2D Logo;
        Texture2D explicacionSimbolo0;
        Texture2D explicacionSimbolo1;
        Texture2D explicacionSimbolo2;
        Texture2D explicacionSimbolo3;
        Texture2D explicacionSimbolo4;
        Texture2D explicacionSimbolo5;
        Texture2D explicacionSimbolo6;
        Texture2D explicacionSimbolo7;
        Texture2D explicacionSimbolo8;
        Texture2D explicacionSimbolo9;

        Texture2D texturaSimbolo;

        Sprite[] listaObjetos = new Sprite[10];
        Sprite[] listaPalabras = new Sprite[20];

        SpriteAnimado ameba;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = screenWidth;
            this.graphics.PreferredBackBufferHeight = screenHeight;
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Permite que el juego realice la inicialización que necesite para empezar a ejecutarse.
        /// Aquí es donde puede solicitar cualquier servicio que se requiera y cargar todo tipo de contenido
        /// no relacionado con los gráficos. Si se llama a base.Initialize, todos los componentes se enumerarán
        /// e inicializarán.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: agregue aquí su lógica de inicialización
            StateJuego = Juegostate.Logo;
            rnd = new Random();

            tiempoLogo = 0;
            tiempoLimiteLogo = 3;



            fallosObjetos = 0;
            fallosPalabras = 0;


            sonarMusica = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent se llama una vez por juego y permite cargar
        /// todo el contenido.
        /// </summary>
        protected override void LoadContent()
        {
            // Crea un SpriteBatch nuevo para dibujar texturas.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tizaFont = Content.Load<SpriteFont>("Fuentes/tizaFont");
            tizaFontPalabras = Content.Load<SpriteFont>("Fuentes/tizaFontPalabras");
            Logo = Content.Load<Texture2D>("Texturas/Pantallas/WG");
            explicacionSimbolo0=Content.Load<Texture2D>("Texturas/Pantallas/localizaCirculo");
            explicacionSimbolo1=Content.Load<Texture2D>("Texturas/Pantallas/localizaCirculopalo");
            explicacionSimbolo2=Content.Load<Texture2D>("Texturas/Pantallas/localizaCuadrado");
            explicacionSimbolo3=Content.Load<Texture2D>("Texturas/Pantallas/localizaDobleFlecha");
            explicacionSimbolo4=Content.Load<Texture2D>("Texturas/Pantallas/localizaPentagono");
            explicacionSimbolo5=Content.Load<Texture2D>("Texturas/Pantallas/localizaRectangulo");
            explicacionSimbolo6=Content.Load<Texture2D>("Texturas/Pantallas/localizaRombo");
            explicacionSimbolo7=Content.Load<Texture2D>("Texturas/Pantallas/localizaRuna");
            explicacionSimbolo8 = Content.Load<Texture2D>("Texturas/Pantallas/localizaTriangulo");
            explicacionSimbolo9 = Content.Load<Texture2D>("Texturas/Pantallas/localizaX");


            soundEngine = Content.Load<SoundEffect>("Sonidos/WhootGames");
            WhootGames = soundEngine.CreateInstance();
            soundEngine =  Content.Load<SoundEffect>("Sonidos/EligeUnaPalabra");
            EligePalabra = soundEngine.CreateInstance();
            soundEngine = Content.Load<SoundEffect>("Sonidos/EncuentraEsteSimbolo");
            EncuentraSimbolo = soundEngine.CreateInstance();
            soundEngine = Content.Load<SoundEffect>("Sonidos/SeleccionaPalabra");
            RecuerdaPalabra = soundEngine.CreateInstance();
            soundEngine = Content.Load<SoundEffect>("Sonidos/Wrong");
            Wrong = soundEngine.CreateInstance();
            soundEngine = Content.Load<SoundEffect>("Sonidos/Correct");
            Correct = soundEngine.CreateInstance();




            ameba = new SpriteAnimado(Content.Load<Texture2D>("Texturas/Ameba/ameba2"), new Vector2(400, 400), new Point(256,256), new Point(0, 0), new Point(2048,1280),100);
            ameba.AddAnimation("ARRIBA", 1, 0, 7, true);
            ameba.AddAnimation("ABAJO", 4, 0, 7, true);
            ameba.AddAnimation("IZQUIERDA", 2, 0, 7, true);
            ameba.AddAnimation("DERECHA", 3, 0, 7, true);


            simboloElegido = rnd.Next(0, 9);
            int cont = 0;
            for (int i = 0; i < listaObjetos.Length; i++)
            {
                String rutaTextura = "Texturas/Simbolos/" + cont;
                texturaSimbolo = Content.Load<Texture2D>(rutaTextura);
                listaObjetos[cont] = new Sprite(texturaSimbolo, new Vector2(rnd.Next(0, screenWidth - texturaSimbolo.Width), rnd.Next(0, screenHeight - texturaSimbolo.Height)), rnd.Next(-200, 200), rnd.Next(-200, 200));
                if (simboloElegido == i)
                    listaObjetos[cont].esElegido = true;
                cont++;

            }


            palabraElegida = rnd.Next(0, 20);
            cont = 0;
            Vector2 posLetras = new Vector2(10, 30);
            for (int i = 0; i < listaPalabras.Length; i++)
            {
                String rutaTextura = "Texturas/Palabras/" + cont;
                texturaSimbolo = Content.Load<Texture2D>(rutaTextura);
                listaPalabras[cont] = new Sprite(texturaSimbolo, posLetras, 0, 0);
                posLetras.Y += 65;
                cont++;
                if (cont == 10)
                {
                    posLetras.Y = 30;
                    posLetras.X = 512;
                }
            }



            // TODO: use this.Content para cargar aquí el contenido del juego
        }

        /// <summary>
        /// UnloadContent se llama una vez por juego y permite descargar
        /// todo el contenido.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: descargue aquí todo el contenido que no pertenezca a ContentManager
        }

        /// <summary>
        /// Permite al juego ejecutar lógica para, por ejemplo, actualizar el mundo,
        /// buscar colisiones, recopilar entradas y reproducir audio.
        /// </summary>
        /// <param name="gameTime">Proporciona una instantánea de los valores de tiempo.</param>
        protected override void Update(GameTime gameTime)
        {
            // Permite salir del juego
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: agregue aquí su lógica de actualización


            if (StateJuego == Juegostate.Logo) {
                

                if (sonarMusica)
                {
                    WhootGames.Play();
                    sonarMusica = false;
                }
               
            }

            if (StateJuego == Juegostate.Juego1) {
                if (sonarMusica)
                {
                    EligePalabra.Play();
                    sonarMusica = false;
                }
                foreach (Sprite palabra in listaPalabras)
                {

                    Rectangle areaObjeto = new Rectangle((int)palabra.posicion.X, (int)palabra.posicion.Y, (int)palabra.textura.Width, (int)palabra.textura.Height);

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (areaObjeto.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
                        {
                            palabra.esElegido = true;
                            StateJuego = Juegostate.Intermedio;
                            sonarMusica = true;
                        }
                    }
                }
            }

            if (StateJuego == Juegostate.Juego2)
            {
                foreach (Sprite objeto in listaObjetos)
                {
                    objeto.Update(gameTime);
                    Rectangle areaObjeto = new Rectangle((int)objeto.posicion.X, (int)objeto.posicion.Y, (int)objeto.textura.Width, (int)objeto.textura.Height);

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (areaObjeto.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
                        {
                            if (objeto.esElegido == true)
                            {
                                Correct.Play();
                                StateJuego = Juegostate.Juego3;
                            }
                            else
                            {
                                fallosObjetos++;
                                Wrong.Play();
                            }
                        }
                    }

                }

            }

            if (StateJuego == Juegostate.Juego3) {

                if (sonarMusica)
                {
                    RecuerdaPalabra.Play();
                    sonarMusica = false;
                }
                foreach (Sprite palabra in listaPalabras)
                {
                    Rectangle areaObjeto = new Rectangle((int)palabra.posicion.X, (int)palabra.posicion.Y, (int)palabra.textura.Width, (int)palabra.textura.Height);

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (areaObjeto.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
                        {
                            if (palabra.esElegido == true)
                            {
                                Correct.Play();
                                StateJuego = Juegostate.Score;
                            }
                            else
                            {
                                fallosPalabras++;
                                Wrong.Play();
                            }
                        }
                    }

                }

            }


            if (StateJuego == Juegostate.Score) {
                ameba.update(gameTime);
            
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Se llama cuando el juego debe realizar dibujos por sí mismo.
        /// </summary>
        /// <param name="gameTime">Proporciona una instantánea de los valores de tiempo.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            if (StateJuego == Juegostate.Logo)
            {
                
                spriteBatch.Begin();
                spriteBatch.Draw(Logo, Vector2.Zero, Color.White);
                spriteBatch.End();

                tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;


                if (tiempoLogo >= tiempoLimiteLogo)
                {
                    StateJuego = Juegostate.Juego1;
                    sonarMusica = true;
                    tiempoLogo = 0;
                }
            }

            if (StateJuego == Juegostate.Juego1)
            {

                foreach (Sprite palabra in listaPalabras) {
                    palabra.Draw(spriteBatch);
                }
            }


            if (StateJuego == Juegostate.Intermedio) {


                if (sonarMusica)
                {
                    EncuentraSimbolo.Play();
                    sonarMusica = false;
                }
                switch (simboloElegido)
                {
                    case 0:
                        spriteBatch.Begin();
                        spriteBatch.Draw(explicacionSimbolo0, Vector2.Zero, Color.White);
                        spriteBatch.End();
                        tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tiempoLogo >= tiempoLimiteLogo)
                        {
                            StateJuego = Juegostate.Juego2;
                            sonarMusica = true;
                            tiempoLogo = 0;
                        }
                        break;
                    case 1:
                        spriteBatch.Begin();
                        spriteBatch.Draw(explicacionSimbolo1, Vector2.Zero, Color.White);
                        spriteBatch.End();
                        tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tiempoLogo >= tiempoLimiteLogo)
                        {
                            StateJuego = Juegostate.Juego2;
                            sonarMusica = true;
                            tiempoLogo = 0;
                        }
                        break;
                    case 2:
                        spriteBatch.Begin();
                        spriteBatch.Draw(explicacionSimbolo2, Vector2.Zero, Color.White);
                        spriteBatch.End();
                        tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tiempoLogo >= tiempoLimiteLogo)
                        {
                            StateJuego = Juegostate.Juego2;
                            sonarMusica = true;
                            tiempoLogo = 0;
                        }
                        break;
                    case 3:
                        spriteBatch.Begin();
                        spriteBatch.Draw(explicacionSimbolo3, Vector2.Zero, Color.White);
                        spriteBatch.End();
                        tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tiempoLogo >= tiempoLimiteLogo)
                        {
                            StateJuego = Juegostate.Juego2;
                            sonarMusica = true;
                            tiempoLogo = 0;
                        }
                        break;
                    case 4:
                        spriteBatch.Begin();
                        spriteBatch.Draw(explicacionSimbolo4, Vector2.Zero, Color.White);
                        spriteBatch.End();
                        tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tiempoLogo >= tiempoLimiteLogo)
                        {
                            StateJuego = Juegostate.Juego2;
                            sonarMusica = true;
                            tiempoLogo = 0;
                        }
                        break;
                    case 5:
                        spriteBatch.Begin();
                        spriteBatch.Draw(explicacionSimbolo5, Vector2.Zero, Color.White);
                        spriteBatch.End();
                        tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tiempoLogo >= tiempoLimiteLogo)
                        {
                            StateJuego = Juegostate.Juego2;
                            sonarMusica = true;
                            tiempoLogo = 0;
                        }
                        break;
                    case 6:
                        spriteBatch.Begin();
                        spriteBatch.Draw(explicacionSimbolo6, Vector2.Zero, Color.White);
                        spriteBatch.End();
                        tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tiempoLogo >= tiempoLimiteLogo)
                        {
                            StateJuego = Juegostate.Juego2;
                            sonarMusica = true;
                            tiempoLogo = 0;
                        }
                        break;
                    case 7:
                        spriteBatch.Begin();
                        spriteBatch.Draw(explicacionSimbolo7, Vector2.Zero, Color.White);
                        spriteBatch.End();
                        tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tiempoLogo >= tiempoLimiteLogo)
                        {
                            StateJuego = Juegostate.Juego2;
                            sonarMusica = true;
                            tiempoLogo = 0;
                        }
                        break;
                    case 8:
                        spriteBatch.Begin();
                        spriteBatch.Draw(explicacionSimbolo8, Vector2.Zero, Color.White);
                        spriteBatch.End();
                        tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tiempoLogo >= tiempoLimiteLogo)
                        {
                            StateJuego = Juegostate.Juego2;
                            sonarMusica = true;
                            tiempoLogo = 0;
                        }
                        break;
                    case 9:
                        spriteBatch.Begin();
                        spriteBatch.Draw(explicacionSimbolo9, Vector2.Zero, Color.White);
                        spriteBatch.End();
                        tiempoLogo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tiempoLogo >= tiempoLimiteLogo)
                        {
                            StateJuego = Juegostate.Juego2;
                            sonarMusica = true;
                            tiempoLogo = 0;
                        }
                        break;
                }
            
            
            }


            if (StateJuego == Juegostate.Juego2)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(tizaFont, "Fallos:"+fallosObjetos, Vector2.Zero, Color.White);
                spriteBatch.End();
                foreach (Sprite objeto in listaObjetos) {
                    objeto.Draw(spriteBatch);
                
                }
            }

            if (StateJuego == Juegostate.Juego3) {
                foreach (Sprite palabra in listaPalabras)
                {
                    palabra.Draw(spriteBatch);
                }
            
            }


            if (StateJuego == Juegostate.Score) {
                spriteBatch.Begin();
                spriteBatch.DrawString(tizaFont, "Jugador", new Vector2(20,20), Color.White);
                spriteBatch.DrawString(tizaFont, "Fallos Simbolos:" + fallosObjetos, new Vector2(20, 50), Color.White);
                spriteBatch.DrawString(tizaFont, "Fallos Palabra:" + fallosPalabras, new Vector2(20, 80), Color.White);
                spriteBatch.DrawString(tizaFont, "Tiempo Simbolos:" + (fallosObjetos + fallosPalabras), new Vector2(20, 110), Color.White);
                spriteBatch.DrawString(tizaFont, "Tiempo Palabra:" + (fallosObjetos + fallosPalabras), new Vector2(20, 140), Color.White);
                spriteBatch.DrawString(tizaFont, "Fallos Totales:" + (fallosObjetos + fallosPalabras), new Vector2(20, 170), Color.White);
                spriteBatch.DrawString(tizaFont, "Tiempo Total:" + (fallosObjetos + fallosPalabras), new Vector2(20, 200), Color.White);
                spriteBatch.End();
                ameba.Draw(gameTime, spriteBatch);
            }
            
                


            // TODO: agregue aquí el código de dibujo

            base.Draw(gameTime);
        }
    }
}
