using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnknownProject.Engine.Particle
{
    public class ParticleEffect
    {
        private Random random;
        private List<Particle> particles;
        private Texture2D[] textures;
        private TimeSpan newEvery;
        private TimeSpan elapsed = TimeSpan.Zero;
        private Camera cam;
        private MinMaxFloat offsetX;
        private MinMaxFloat offsetY;
        private MinMaxFloat size;
        private Color[] colors;
        private bool randomColors;
        private MinMaxFloat timeToLife;
        private MinMaxFloat angularVelocity;
        private MinMaxFloat startAngle;
        private MinMaxFloat newEveryCount;

        public ParticleEffect(Texture2D[] textures, MinMaxFloat offsetX, MinMaxFloat offsetY, MinMaxFloat size, MinMaxFloat timeToLife, TimeSpan newEvery, MinMaxFloat newEveryCount, Camera cam, MinMaxFloat startAngle, MinMaxFloat angularVelocity, bool randomColors = true)
        {
            this.textures = textures;
            this.particles = new List<Particle>();
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.timeToLife = timeToLife;
            this.size = size;
            this.newEvery = newEvery;
            this.cam = cam;
            this.randomColors = randomColors;
            this.startAngle = startAngle;
            this.angularVelocity = angularVelocity;
            this.newEveryCount = newEveryCount;
            this.random = new Random();
        }

        public ParticleEffect(Texture2D[] textures, Color[] colors, MinMaxFloat offsetX, MinMaxFloat offsetY, MinMaxFloat size, MinMaxFloat timeToLife, TimeSpan newEvery, MinMaxFloat newEveryCount, Camera cam, MinMaxFloat startAngle, MinMaxFloat angularVelocity) :
            this(textures, offsetX, offsetY, size, timeToLife, newEvery, newEveryCount, cam, startAngle, angularVelocity, false)
        {
            this.colors = colors;
        }

        public void Update(GameTime gameTime, Point emitterLocation)
        {
            elapsed += gameTime.ElapsedGameTime;
            CreateNewParticles(emitterLocation.ToVector2());
            RemoveDeadParticles(gameTime);
        }

        private void CreateNewParticles(Vector2 emitterLocation)
        {
            if (elapsed >= newEvery)
            {
                int total = (int)newEveryCount.RandBetween(random);
                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticle(emitterLocation));
                }
                elapsed -= newEvery;
            }
        }

        private void RemoveDeadParticles(GameTime gameTime)
        {
            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update(gameTime);
                if (particles[particle].IsDead())
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        private Particle GenerateNewParticle(Vector2 emitterLocation)
        {
            var texture = textures[random.Next(textures.Length)];
            var velocity = new Vector2(offsetX.RandBetween(random) * (float)cam.Zoom, offsetY.RandBetween(random) * (float)cam.Zoom);
            var angle = startAngle.RandBetween(random);
            var angularVelocity = this.angularVelocity.RandBetween(random);
            var color = GetColor();
            var size = this.size.RandBetween(random);
            var ttl = TimeSpan.FromSeconds(timeToLife.RandBetween(random));

            return new Particle(texture, emitterLocation, velocity, angle, angularVelocity, color, size, ttl, cam);
        }

        private Color GetColor()
        {
            if (randomColors)
            {
                return new Color(
                        (float)random.NextDouble(),
                        (float)random.NextDouble(),
                        (float)random.NextDouble()
                );
            }
            else
            {
                return colors[random.Next(colors.Length)];
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }
    }
}
