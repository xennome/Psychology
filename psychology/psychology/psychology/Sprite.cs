using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace psychology
{
    class Sprite
    {
        public Texture2D textura;
        public Vector2 posicion;
        float velocidadX;
        float velocidadY;
        public bool esElegido;






    public Sprite(Texture2D textura, Vector2 posicion, float velocidadX, float velocidadY)
    {
        this.textura = textura;
        this.posicion = posicion;
        this.velocidadX = velocidadX;
        this.velocidadY = velocidadY;
        this.esElegido = false;
    }


    public void Update(GameTime gameTime)
    {
        posicion.X += velocidadX * (float)gameTime.ElapsedGameTime.TotalSeconds;
        posicion.Y += velocidadY * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (posicion.X < 0 || (posicion.X + textura.Width) > 1024)
            velocidadX *= -1;
        if (posicion.Y < 0 || (posicion.Y + textura.Height) > 768)
            velocidadY *= -1;
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(this.textura, this.posicion, Color.White);
        spriteBatch.End();

    }

    }
}
