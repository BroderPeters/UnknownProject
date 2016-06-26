using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UnknownProject.Engine.Particle
{
    public class Particle
    {
        private Camera cam;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private float angle;
        private float angularVelocity;
        private Color color;
        private float size;
        private TimeSpan timeToLife;
        private Vector2 origin;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Color color, float size, TimeSpan timeToLife, Camera cam)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            this.color = color;
            this.size = size;
            this.timeToLife = timeToLife;
            this.cam = cam;
            this.origin = new Vector2(this.texture.Width / 2, this.texture.Height / 2);
        }

        public void Update(GameTime gameTime)
        {
            timeToLife -= gameTime.ElapsedGameTime;
            position += velocity * new Vector2((float) gameTime.ElapsedGameTime.TotalSeconds, (float) gameTime.ElapsedGameTime.TotalSeconds);
            angle += angularVelocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color,
                angle, origin, size * (float) cam.Zoom, SpriteEffects.None, 0f);
        }

        public bool IsDead()
        {
            return timeToLife <= TimeSpan.Zero;
        }
    }
}
