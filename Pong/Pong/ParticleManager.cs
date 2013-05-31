using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class ParticleManager
    {
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private Texture2D texture;
        private int TotalParticles { get; set; }
        private int opacidade = 255;

        public ParticleManager(Texture2D texture, Vector2 location)
        {
            EmitterLocation = location;
            this.texture = texture;
            TotalParticles = 1;
            this.particles = new List<Particle>();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < TotalParticles; i++)
            {
                particles.Add(createParticle());
                opacidade -= 80;
            }

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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in particles)
            {
                p.Draw(spriteBatch);
            }
        }

        private Particle createParticle()
        {
            Vector2 position = EmitterLocation;
            int lifeTime = 20;
            return new Particle(texture, position, lifeTime);
        }

    }
}
