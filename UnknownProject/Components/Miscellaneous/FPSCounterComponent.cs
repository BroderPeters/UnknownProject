using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using UnknownProject.Core;

namespace UnknownProject.Components.Miscellaneous
{
    /// <summary>
    /// Display's the frames per second in the upper right corner.
    /// </summary>
    public class FPSCounterComponent : ComponentPresenter, IPresenter
    {
        private const float Padding = 10f;
        private static readonly  TimeSpan OneSecond = TimeSpan.FromSeconds(1);
        private TimeSpan elapsedTime = OneSecond;
        private GraphicConfiguration graphicConf;
        public Vector2 TextPosition { get; set; }
        public Vector2 TextPositionWithOffset { get; set; }
        public int CurrentFrameRate { get; private set; }
        private int framesInCurrentSecond = 0;
        private IFPSCounterComponentView view;

        public FPSCounterComponent(IFPSCounterComponentView view, GraphicConfiguration graphicConf)
        {
            this.graphicConf = graphicConf;
            this.view = view;
            view.SetPresenter(this);
        }

        public override void Update(GameTime gameTime)
        {
            framesInCurrentSecond++;
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime >= OneSecond)
            {
                elapsedTime -= OneSecond;
                CurrentFrameRate = framesInCurrentSecond;
                framesInCurrentSecond = 0;

                calculateTextPosition();
            }
        }

        private void calculateTextPosition()
        {
            string fps = CurrentFrameRate.ToString();
            TextPosition = getPosition(fps);
            TextPositionWithOffset = new Vector2(TextPosition.X + 1, TextPosition.Y + 1);
        }

        private Vector2 getPosition(string fps)
        {
            Vector2 fpsSize = view.getSizeFromString(fps);
            float x = graphicConf.Width - Padding - fpsSize.X;
            float y = Padding;
            return new Vector2(x, y);
        }

        public override IDrawable AsDrawable()
        {
            return view;
        }

        public interface IFPSCounterComponentView : IView<FPSCounterComponent>
        {
            Vector2 getSizeFromString(String fps);
        }
    }
}
