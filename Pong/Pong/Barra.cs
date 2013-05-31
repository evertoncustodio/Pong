using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    class Barra
    {
        private Texture2D textura;
        private Vector2 tamanhoTela;
        private Vector2 velocidade;
        private Vector2 posicao;
        private Keys up;
        private Keys down;

        public Barra (Texture2D textura, Vector2 velocidade, Vector2 posicao, Vector2 tamanhoTela, Keys up, Keys down)
        {
            this.textura = textura;
            this.velocidade = velocidade;
            this.posicao = posicao;
            this.tamanhoTela = tamanhoTela;
            this.up = up;
            this.down = down;
        }
        

        public Texture2D Textura { get { return textura; } set { textura = value; } }
        public Vector2 Velocidade { get { return velocidade; } set { velocidade = value; } }
        public Vector2 Posicao { get { return posicao; } set { posicao = value; } }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textura, Posicao, Color.White);
        }

        public void Move(KeyboardState keyboard)
        {
            if(keyboard.IsKeyDown(up))
            {
                int novaPosicao = (int) (Posicao.Y - Velocidade.Y);
                if (novaPosicao > 0)
                {
                    Posicao = new Vector2(Posicao.X, Posicao.Y - Velocidade.Y);
                }
            }
            if (keyboard.IsKeyDown(down))
            {
                int novaPosicao = (int)(Posicao.Y + Velocidade.Y);
                if (novaPosicao + Textura.Height < tamanhoTela.Y)
                {
                    Posicao = new Vector2(Posicao.X, Posicao.Y + Velocidade.Y);
                }
            }
        }

    }
}
