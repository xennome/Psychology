using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace psychology
{
    class SpriteAnimado
    {
        protected Texture2D textureImage;
        protected Point frameSize;
        Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        public Vector2 position;
        public bool caminar = false;

        //Para gestionar animaciones
        Dictionary<String, Animacion> mAnimaciones = new Dictionary<string, Animacion>();
        public Animacion mAnimacion;
        protected bool mAnimFinished;
        protected bool mSpriteCaminando = false;
        protected bool mSpriteParado = false;


        




        public SpriteAnimado(Texture2D textureImage, Vector2 position, Point frameSize, Point currentFrame, Point sheetSize, int millisecondsPerFrame) {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.millisecondsPerFrame = millisecondsPerFrame;
        
        
        }


        public void AddAnimation(string name, int fila, int primerFrame, int numeroFrames, bool loop)
        {
            Animacion anim = new Animacion();
            anim.fila = fila;
            anim.frameInicial = primerFrame;
            anim.numeroFrames = numeroFrames;
            anim.Loop = loop;
            mAnimaciones[name] = anim;
        }

        public void SetActualAnimation(string name)
        {
            if (name == null)
            {
                mAnimacion = null;
                return;
            }

            mAnimFinished = false;

            Animacion anim = mAnimaciones[name];
            if (anim != null && anim != mAnimacion)
            {
                currentFrame.X = anim.frameInicial;
                currentFrame.Y = anim.fila;
                mAnimacion = anim;
                //Calculo esto por si tengo distintos tiempos de animaciones
                millisecondsPerFrame = (int) (1000.0f / mAnimacion.velocidad);
            }
        }


        public void updateAnim(GameTime gameTime) {

            if (mAnimacion != null)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    timeSinceLastFrame = 0;
                    ++currentFrame.X;
                    if (currentFrame.X >= mAnimacion.numeroFrames)
                    {
                        if (mAnimacion.Loop)
                        {
                            currentFrame.X = 0;
                        }
                        else
                        {
                            mAnimFinished = true;
                        }
                    }
                }
            }
        }


        public virtual void update(GameTime gameTime) {

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                SetActualAnimation("IZQUIERDA");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                SetActualAnimation("ARRIBA");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                SetActualAnimation("DERECHA");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                SetActualAnimation("ABAJO");
            }


        }



        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            spriteBatch.Draw(textureImage, position, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }





    }

    
}
