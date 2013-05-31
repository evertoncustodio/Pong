using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pong
{
    class ColisaoParticle
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public float LifeTime { get; set; }

        public ColisaoParticle(Texture2D texture, Vector2 position, Vector2 velocity, Color color, float size, float lifeTime)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Color = color;
            Size = size;
            LifeTime = lifeTime;
        }

        public void Update(GameTime gameTime)
        {
            if (LifeTime > 0)
            {
                LifeTime--;
                Position += Velocity;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Define o retângulo de recorte da imagem,
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            // Define a origem da imagem
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            // Desenha a imagem
            spriteBatch.Draw(Texture,   // Imagem
                Position,               // Posição
                sourceRectangle,        // Retangulo de recorte
                Color,                  // Cor
                //Color.White,
                0f,                  // Angulo
                origin,                 // Origem de desenho
                Size,                   // Tamanho
                SpriteEffects.None,     // Efeito de imagens (flip horizontal ou vertical)
                0.0f);                  // Layer de pronfundidade
        }
    }
}
