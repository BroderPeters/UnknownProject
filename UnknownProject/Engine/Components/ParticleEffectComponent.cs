using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using UnknownProject.Components.Core;
using UnknownProject.Engine.Particle;
using Microsoft.Xna.Framework.Input;

namespace UnknownProject.Engine.Components
{
    public class ParticleEffectComponent : DrawableComponent
    {
        private Camera cam;
        ParticleEffect effect;
        public ParticleEffectComponent(Camera cam)
        {
            this.cam = cam;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            effect.Draw(spriteBatch);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            var particle = contentManager.Load<Texture2D>("particles/pixel");
            effect = new ParticleEffect(
                textures: new[] { particle },
                colors: new[] { Color.Red, Color.Orange, Color.Yellow, Color.OrangeRed, Color.DarkRed },
                offsetX: new MinMaxFloat(-22, 22),
                offsetY: new MinMaxFloat(-22, 22),
                size: new MinMaxFloat(.1f, 10f),
                timeToLife: new MinMaxFloat(0.2f, 1f),
                newEvery: TimeSpan.FromMilliseconds(1),
                newEveryCount: new MinMaxFloat(1, 5),
                cam: cam,
                startAngle: new MinMaxFloat(0, 360),
                angularVelocity: new MinMaxFloat(-5, 5)
            );
        }

        public override void Update(GameTime gameTime)
        {
            effect.Update(gameTime, Mouse.GetState().Position);
        }
    }
}
