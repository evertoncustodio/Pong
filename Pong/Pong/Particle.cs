using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pong
{
    class Particle
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float LifeTime { get; set; }
        private static int op = 255;
        private int opacidade = 155;
        private float tamanho = 1;

        public Particle(Texture2D texture, Vector2 position, float lifeTime)
        {
            Texture = texture;
            Position = position;
            LifeTime = lifeTime;
        }

        public void Update(GameTime gameTime)
        {
            LifeTime--;
            opacidade -= 10;
            tamanho -= 0.05f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Texture, Position, Color.White);
            //spriteBatch.Draw(this.texture, Vector2.Zero, new Color(1f, 1f, 1f, 0.5f));
            //int cor = Opacidade();
            int cor = opacidade;
            //spriteBatch.Draw(Texture, Position, new Color(cor, cor, cor, cor));
            spriteBatch.Draw(Texture, Position, null, new Color(cor, cor, cor, cor), 0f, Vector2.Zero, tamanho, SpriteEffects.None, 0f);
            
        }

        private static int Opacidade()
        {
            op -= 20;
            if (op <= 0)
            {
                op = 255;
            }
            return op;
        }
    }
}
