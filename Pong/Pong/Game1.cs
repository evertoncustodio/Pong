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

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Barra barraEsquerda;
        Barra barraDireita;
        Bola bola;
        Vector2 tela;
        ParticleManager particleManager;
        Texture2D fundo;
        Texture2D fundo2;
        Vector2 posicaoFundo = Vector2.Zero;
        Vector2 posicaoFundo2;
        Vector2 posicaoLogo;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            tela = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            posicaoLogo = new Vector2((tela.X / 2) - 75, 0);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("Font");
            fundo = Content.Load<Texture2D>("fundo");
            fundo2 = Content.Load<Texture2D>("fundo");
            posicaoFundo2 = new Vector2(posicaoFundo.X - fundo.Width, posicaoFundo.Y); 
            Texture2D texturaBarra = Content.Load<Texture2D>("barra");
            barraEsquerda = new Barra(Content.Load<Texture2D>("barra_esquerda"), new Vector2(10, 10), new Vector2(20, tela.Y / 2), tela, Keys.W, Keys.S);
            barraDireita = new Barra(Content.Load<Texture2D>("barra_direita"), new Vector2(10, 10), new Vector2(tela.X - (20 + texturaBarra.Width) , tela.Y / 2), tela, Keys.Up, Keys.Down);
            bola = new Bola(Content.Load<Texture2D>("bola"), new Vector2(5, 5), new Vector2(tela.X / 2, tela.Y / 2), tela, barraEsquerda, barraDireita);
            bola.TexturaColisao = Content.Load<Texture2D>("bola-efeito");
            particleManager = new ParticleManager(Content.Load<Texture2D>("efeito"), bola.Posicao);
            bola.efeito = Content.Load<Texture2D>("efeito");

            // Som
            bola.AudioEngine = new AudioEngine(@"Content\Sounds.xgs");
            bola.WaveBank = new WaveBank(bola.AudioEngine, @"Content\Wave Bank.xwb");
            bola.SoundBank = new SoundBank(bola.AudioEngine, @"Content\Sound Bank.xsb");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            barraEsquerda.Textura.Dispose();
            barraDireita.Textura.Dispose();
            bola.Textura.Dispose();
            fundo.Dispose();
            fundo2.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState keyboard = Keyboard.GetState();
            // Movimentando a barra esquerda
            barraEsquerda.Move(keyboard);
            barraDireita.Move(keyboard);
            bola.Move(keyboard);
            bola.Update(gameTime);
            particleManager.EmitterLocation = bola.Posicao;
            particleManager.Update(gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here            
            spriteBatch.Begin();
            spriteBatch.Draw(fundo, posicaoFundo, Color.White);
            spriteBatch.Draw(fundo2, posicaoFundo2, Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("fundo2"), Vector2.Zero, Color.White);
            posicaoFundo2.X += 0.5f;
            posicaoFundo.X += 0.5f;

            if (posicaoFundo.X >= graphics.PreferredBackBufferWidth)
            {
                posicaoFundo = new Vector2(posicaoFundo2.X - fundo2.Width, posicaoFundo.Y);
            }
            if (posicaoFundo2.X >= graphics.PreferredBackBufferWidth)
            {
                posicaoFundo2 = new Vector2(posicaoFundo.X - fundo2.Width, posicaoFundo.Y);
            }

            string placar = bola.PontosEsquerda + "  " + bola.PontosDireita;
            spriteBatch.DrawString(font, placar, new Vector2((tela.X / 2) - (font.MeasureString(placar).X / 2), 65), Color.Orange);
            barraEsquerda.Draw(spriteBatch);
            barraDireita.Draw(spriteBatch);
            particleManager.Draw(spriteBatch);
            bola.Draw(spriteBatch);
            spriteBatch.Draw(Content.Load<Texture2D>("logo"), posicaoLogo, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
