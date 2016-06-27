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
        private float endOpacity;
        private float opacity;
        private float opacityVelocity;
        private TimeSpan startOpaciting;
        private float sizeVelocity;

        public Particle(Texture2D texture,
            Vector2 position,
            Vector2 velocity,
            float angle,
            float angularVelocity,
            Color color,
            float size,
            float sizeVelocity,
            TimeSpan timeToLife,
            float opacity,
            float endOpacity,
            double opacityChangeMode,
            Camera cam)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            color.A = (byte)opacity;
            this.opacity = opacity;
            this.color = color;
            this.size = size;
            this.timeToLife = timeToLife;
            this.cam = cam;
            this.sizeVelocity = sizeVelocity;
            this.endOpacity = endOpacity;
            var opacityDuration =  (float)timeToLife.TotalSeconds * (1d - opacityChangeMode);
            this.startOpaciting = TimeSpan.FromSeconds(opacityDuration);
            this.opacityVelocity = (float) (opacity / opacityDuration);
            this.origin = new Vector2(this.texture.Width / 2, this.texture.Height / 2);
        }

        public void Update(GameTime gameTime)
        {

            timeToLife -= gameTime.ElapsedGameTime;
            position += velocity * new Vector2((float) gameTime.ElapsedGameTime.TotalSeconds, (float) gameTime.ElapsedGameTime.TotalSeconds);
            angle += angularVelocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            size += sizeVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (startOpaciting >= timeToLife)
            {
                var x = opacityVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                opacity -= x;
                if(opacity < 0)
                {
                    opacity = 0;   
                }
                color.A = (byte)opacity;
            }
            
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
