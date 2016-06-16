using NUnit.Framework;
using UnknownProject.Components.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnknownProject.Core;
using FakeItEasy;
using Microsoft.Xna.Framework;
using MonoGame.Framework;
using UnknownProject.Core.Configurations;

namespace UnknownProject.Components.Miscellaneous.Tests
{
    [TestFixture()]
    public class FPSCounterComponentTests
    {
        private GraphicConfiguration graphicConf;

        private FPSCounterComponent presenter;
        private FPSCounterComponent.IFPSCounterComponentView view;
        private Vector2 textSize = new Vector2(20, 20); 

        [SetUp]
        public void Init()
        {
            view = A.Fake<FPSCounterComponent.IFPSCounterComponentView>();
            A.CallTo(() => view.getSizeFromString(A<string>.Ignored)).Returns(textSize);

            graphicConf = new GraphicConfiguration();
            graphicConf.Width = 100;

            presenter = new FPSCounterComponent(view, graphicConf);
            presenter.Update(new GameTime()); // update first time as this is counted as a second, so the fps are now 1.
        }

        [Test()]
        public void CalculateFramerateTest60FPS()
        {
            CalculateFrameRateTest(60);
        }

        [Test()]
        public void CalculateFramerateTest150FPS()
        {
            CalculateFrameRateTest(150);
        }

        private void CalculateFrameRateTest(int fps)
        {
            // we need to actually round cause a long have no decimal places
            long ticksPerFrame = Convert.ToInt64(Math.Round(TimeSpan.TicksPerSecond / (double)fps));

            GameTime gameTime = new GameTime();
            gameTime.ElapsedGameTime = TimeSpan.FromTicks(ticksPerFrame);

            for (int i = 0; i < fps; i++)
            {
                presenter.Update(gameTime);
            }

            Assert.AreEqual(fps, presenter.CurrentFrameRate);
        } 
    }
}