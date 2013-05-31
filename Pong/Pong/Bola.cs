using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pong
{
    class Bola
    {
        private Texture2D textura;
        private Vector2 tamanhoTela;
        private Vector2 velocidade;
        private Vector2 posicao;
        private Barra barraEsquerda;
        private Barra barraDireita;
        private int pontosEsquerda;
        private int pontosDireita;
        public Texture2D TexturaColisao { get; set; }
        private Random random = new Random();

        // Colisão
        private ColisaoParticleManager colisaoParticleManager;

        // Som
        public AudioEngine AudioEngine { get; set; }
        public WaveBank WaveBank { get; set; }
        public SoundBank SoundBank { get; set; }

        // Controle para que a ação da tecla enter não se repetia enquanto esta estiver pressionada
        private bool acaoExecutada;

        public Bola(Texture2D textura, Vector2 velocidade, Vector2 posicao, Vector2 tamanhoTela, Barra barraEsquerda, Barra barraDireita)
        {
            this.textura = textura;
            this.velocidade = velocidade;
            this.posicao = posicao;
            this.tamanhoTela = tamanhoTela;
            this.barraEsquerda = barraEsquerda;
            this.barraDireita = barraDireita;
        }


        public Texture2D Textura { get { return textura; } set { textura = value; } }
        public Vector2 Velocidade { get { return velocidade; } set { velocidade = value; } }
        public Vector2 Posicao { get { return posicao; } set { posicao = value; } }
        public int PontosEsquerda { get { return pontosEsquerda; } }
        public int PontosDireita { get { return pontosDireita; } }
        public Texture2D efeito { get; set; }
        private float angulo = 1;

        public void Update(GameTime gameTime)
        {
            if (colisaoParticleManager != null)
            {
                colisaoParticleManager.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Textura, Posicao, Color.Blue);

            // Montando o quadrado que gira em torno da 'Bola'
            // Variáveis para o efeito
            Rectangle rectangle = new Rectangle(0, 0, efeito.Width, efeito.Height);
            Vector2 posicaoEfeito = new Vector2(Posicao.X + (Textura.Width / 2), Posicao.Y + (Textura.Height / 2));
            Vector2 origem = new Vector2(efeito.Width / 2, efeito.Height / 2);

            spriteBatch.Draw(efeito, posicaoEfeito, rectangle, Color.Red, angulo, origem, 1, SpriteEffects.None, 0.0f); 
            angulo += 0.1f;

            // Colisão
            if (colisaoParticleManager != null)
            {
                colisaoParticleManager.Draw(spriteBatch);
            }
        }

        public void Move(KeyboardState keyboard)
        {
            // Superior
            if (Posicao.Y + Velocidade.Y - 30 <= 0)
            {
                SoundBank.PlayCue("som_barras");
                colisao(new Vector2(posicao.X, Posicao.Y + Velocidade.Y), Color.Blue);                
                Velocidade = new Vector2(Velocidade.X, Velocidade.Y * -1);
                
            }
            // Inferior
            if (Posicao.Y + Velocidade.Y + Textura.Height + 25 >= tamanhoTela.Y)
            {
                colisao(new Vector2(posicao.X, Posicao.Y + Velocidade.Y), Color.Red);
                SoundBank.PlayCue("som_barras");
                Velocidade = new Vector2(Velocidade.X, Velocidade.Y * -1);
            }
            // Barra Esquerda
            int posX = (int) (Posicao.X + Velocidade.X);
            int posY = (int) (Posicao.Y + Velocidade.Y);
            if (posX <= barraEsquerda.Posicao.X + barraEsquerda.Textura.Width)
            {
                if (posY + Textura.Height >= barraEsquerda.Posicao.Y && posY <= barraEsquerda.Posicao.Y + barraEsquerda.Textura.Height)
                {
                    
                    SoundBank.PlayCue("som_superior");
                    colisao(new Vector2(Posicao.X, Posicao.Y), Color.Green);
                    Velocidade *= 1.20f;
                    Velocidade = new Vector2(Velocidade.X * -1, Velocidade.Y);
                }
            }
            

            // Barra Direita
            if (posX + Textura.Width >= barraDireita.Posicao.X)
            {
                if (posY + Textura.Height >= barraDireita.Posicao.Y && posY <= barraDireita.Posicao.Y + barraDireita.Textura.Height)
                {
                    SoundBank.PlayCue("som_superior");
                    colisao(new Vector2(Posicao.X, Posicao.Y), new Color(127, 0, 55));
                    Velocidade *= 1.20f;
                    Velocidade = new Vector2(Velocidade.X * -1, Velocidade.Y);
                }
            }

            // Esquerda
            if (Posicao.X + Velocidade.X <= 0)
            {
                //Velocidade = new Vector2(Velocidade.X * -1, Velocidade.Y);
                Posicao = new Vector2(tamanhoTela.X / 2, tamanhoTela.Y / 2);
                Velocidade = new Vector2(0, 0);
                pontosDireita++;
            }
            // Direita
            if (Posicao.X + Velocidade.X + Textura.Width >= tamanhoTela.X)
            {
                //Velocidade = new Vector2(Velocidade.X * -1, Velocidade.Y);
                Posicao = new Vector2(tamanhoTela.X / 2, tamanhoTela.Y / 2);
                Velocidade = new Vector2(0, 0);
                pontosEsquerda++;
            }

            // Reiniciando a partida
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                if (!acaoExecutada)
                {

                    float n = random.Next(0, 100);
                    int nx = 1;
                    int ny = 1;
                    if (n <= 25)
                    {
                        nx = 1;
                        ny = 1;
                    }
                    else if (n <= 50)
                    {
                        nx = -1;
                        ny = 1;
                    }
                    else if (n <= 75)
                    {
                        nx = 1;
                        ny = -1;
                    }
                    else
                    {
                        nx = -1;
                        ny = -1;
                    }
                    Velocidade = new Vector2(5 * nx, 5 * ny);
                    Console.Write(Velocidade);
                    acaoExecutada = true;
                }
            }
            if (keyboard.IsKeyUp(Keys.Enter))
            {
                acaoExecutada = false;
            }

            Posicao += Velocidade;
        }

        private void colisao(Vector2 position)
        {
            colisaoParticleManager = new ColisaoParticleManager(TexturaColisao, position);
        }
        private void colisao(Vector2 position, Color color)
        {
            colisaoParticleManager = new ColisaoParticleManager(TexturaColisao, position, color);
            
        }
    }
}
