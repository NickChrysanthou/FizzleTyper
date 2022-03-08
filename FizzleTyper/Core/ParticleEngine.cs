using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FizzleGame.ParticleSystem
{
    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;
        public Color color;
        public bool AddColor { get; set; }
        private float particleDensity { get; set; }
        private float particleAngle { get; set; }
        private float size { get; set; } = 0f;
        public ParticleEngine(List<Texture2D> textures, bool randomColor, Color color, float ParticleDensity, float ParticleAngle, float Size)
        {
            this.size = Size;
            this.color = color;
            this.AddColor = randomColor;
            this.textures = textures;
            this.particles = new List<Particle>();
            this.particleDensity = ParticleDensity;
            this.particleAngle = ParticleAngle;
            random = new Random();
        }
        public void Update()
        {
            for (int i = 0; i < particleDensity; i++)
            {
                particles.Add(GenerateNewParticle());
            }
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update();
                if (particles[i].TTL <= 0)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
             1f * (float)(random.NextDouble() * 2 - 1) + particleAngle,
             1f * (float)(random.NextDouble() * 2 - 1) + particleAngle);
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            if (AddColor)
            {
                color = new Color(
                            (float)random.NextDouble(),
                            (float)random.NextDouble(),
                            (float)random.NextDouble());
            }

            else if (!AddColor && color == Color.White)
            {
                color = Color.White;
            }
            float pSize = (float)random.NextDouble() * size;
            int ttl = 20 + random.Next(40);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, pSize, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }
        }
    }
}