using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace UnknownProject.Components.Miscellaneous
{
    public class FPSCounterComponentView : ComponentView<FPSCounterComponent>, FPSCounterComponent.IFPSCounterComponentView
    {
        private const String FontName = "FPSFont";
        private SpriteFont spriteFont;

        public override void LoadContent(ContentManager contentManager)
        {
            spriteFont = contentManager.Load<SpriteFont>(FontName);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            string fps = presenter.CurrentFrameRate.ToString();

            spriteBatch.DrawString(spriteFont, fps, presenter.TextPosition, Color.Black);
            spriteBatch.DrawString(spriteFont, fps, presenter.TextPositionWithOffset, Color.White);
        }

        public Vector2 getSizeFromString(String fps)
        {
            return spriteFont.MeasureString(fps);
        }
    }
}
