using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class ColisaoParticleManager
    {
        public Vector2 EmitterLocation { get; set; }
        private List<ColisaoParticle> particles;
        private Texture2D texture;
        private int TotalParticles { get; set; }
        private float tamanho = 1.5f;
        private int cor = 0;
        private int time = 30;
        private Color color;

        public ColisaoParticleManager(Texture2D texture, Vector2 location)
        {
            EmitterLocation = location;
            this.texture = texture;
            TotalParticles = 5;
            this.particles = new List<ColisaoParticle>();
            

            for (int i = 0; i < TotalParticles; i++)
            {
                particles.Add(CreateParticle());
            }
            particles[TotalParticles - 1].LifeTime = 10;
        }

        public ColisaoParticleManager(Texture2D texture, Vector2 location, Color color)
        {
            EmitterLocation = location;
            this.texture = texture;
            TotalParticles = 5;
            this.particles = new List<ColisaoParticle>();
            this.color = color;

            for (int i = 0; i < TotalParticles; i++)
            {
                particles.Add(CreateParticle());
            }
            particles[TotalParticles - 1].LifeTime = 10;
        }

        public void Update(GameTime gameTime)
        {
            if (particles.Count <= TotalParticles)
            {
                for (int particle = 0; particle < particles.Count; particle++)
                {
                    particles[particle].Update(gameTime);
                    if (particles[particle].LifeTime <= 0)
                    {
                        particles.RemoveAt(particle);
                        particle--;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //foreach (ColisaoParticle p in particles)
            //{
            //   p.Draw(spriteBatch);
            //}            
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                if (i == particles.Count - 1)
                {
                    particles[i].Draw(spriteBatch);
                }
            }
        }

        private ColisaoParticle CreateParticle()
        {
            // Posição do emissor de partículas
            Vector2 position = EmitterLocation;

            // Tamanho randomico da partícula
            float size = tamanho;
            tamanho -= 0.30f;
            // Tempo de vida da partícula
            int lifeTime = time;
            time -= 10;
            if (this.color == null)
            {
                Color color = new Color(cor, cor, cor, cor);
                cor += 50;
                return new ColisaoParticle(texture, position, Vector2.Zero, color, size, lifeTime);
            }
            else
            {
                return new ColisaoParticle(texture, position, Vector2.Zero, this.color, size, lifeTime);
            }            
        }
    }
}
