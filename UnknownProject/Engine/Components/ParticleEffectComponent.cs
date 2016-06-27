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
        private ParticleEffect particles;
        private ParticleEffect pixel;
        private ParticleEffect smoke;

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
            var pixelTexture = contentManager.Load<Texture2D>("particles/pixel");
            var smokeTexture = contentManager.Load<Texture2D>("particles/Cloud001");
            var particleTexture = contentManager.Load<Texture2D>("particles/particle");
            pixel = new ParticleEffect(
                textures: new[] { pixelTexture },
                colors: new[] { Color.Red, Color.Orange, Color.Yellow, Color.OrangeRed, Color.DarkRed },
                offsetX: new MinMaxFloat(-42, 42),
                offsetY: new MinMaxFloat(-42, 42),
                size: new MinMaxFloat(3f, 5),
                sizeScale: 3f,
                sizeVelocity: new MinMaxFloat(1, 11),
                timeToLife: new MinMaxFloat(.2f, 1f),
                newEvery: TimeSpan.FromMilliseconds(1),
                newEveryCount: new MinMaxFloat(1, 5),
                cam: cam,
                startAngle: new MinMaxFloat(0, 360),
                angularVelocity: new MinMaxFloat(-5, 5),
                startOpacity: new MinMaxFloat(255, 255),
                opacityChangeMode: OpacityChangeMode.StartAtHalfLifeTime,
                endOpacity: new MinMaxFloat(0, 0)
            );
            smoke = new ParticleEffect(
                textures: new[] { smokeTexture },
                colors: new[] { Color.White },
                offsetX: new MinMaxFloat(-7, 7),
                offsetY: new MinMaxFloat(-7, 7),
                size: new MinMaxFloat(.1f, 1),
                sizeVelocity: new MinMaxFloat(1, 3),
                sizeScale: 0.4f,
                timeToLife: new MinMaxFloat(1f,1),
                newEvery: TimeSpan.FromMilliseconds(6),
                newEveryCount: new MinMaxFloat(1,1),
                cam: cam,
                startAngle: new MinMaxFloat(0, 360),
                angularVelocity: new MinMaxFloat(-2, 2),
                startOpacity: new MinMaxFloat(55, 55),
                opacityChangeMode: OpacityChangeMode.StartAtStart,
                endOpacity: new MinMaxFloat(0, 0),
                drawOrder: Particle.DrawOrder.OldToNew
            );
            particles = new ParticleEffect(
                textures: new[] { particleTexture },
                offsetX: new MinMaxFloat(-82, 82),
                offsetY: new MinMaxFloat(-82, 82),
                size: new MinMaxFloat(2f, 3),
                sizeScale: 2f,
                sizeVelocity: new MinMaxFloat(-1f, -0.5f),
                timeToLife: new MinMaxFloat(.2f, 1f),
                newEvery: TimeSpan.FromMilliseconds(1),
                newEveryCount: new MinMaxFloat(1, 1),
                cam: cam,
                startAngle: new MinMaxFloat(0, 360),
                angularVelocity: new MinMaxFloat(-5, 5),
                startOpacity: new MinMaxFloat(255, 255),
                opacityChangeMode: OpacityChangeMode.StartAtHalfLifeTime,
                endOpacity: new MinMaxFloat(0, 0)
            );


            effect = smoke;
        }

        public override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();

            if(state.IsKeyDown(Keys.D1))
            {
                effect = pixel;
            }
            if(state.IsKeyDown(Keys.D2))
            {
                effect = smoke;
            }
            if(state.IsKeyDown(Keys.D3))
            {
                effect = particles;
            }

            effect.Update(gameTime, Mouse.GetState().Position);
        }
    }
}
