using NUnit.Framework;
using UnknownProject.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnknownProject.Components.Core;

namespace UnknownProject.Components.Tests
{
    /// <summary>
    /// Tests for the Component Collection
    /// </summary>
    [TestFixture()]
    public class ComponentCollectionTests
    {
        private DrawableComponent drawableComponent;

        private Component component;

        private ComponentPresenter presenter;
        private IView<IPresenter> viewForPresenter;

        private ComponentCollection collection;

        [SetUp()]
        public void Init()
        {
            drawableComponent = A.Fake<DrawableComponent>();

            component = A.Fake<Component>();

            presenter = A.Fake<ComponentPresenter>();
            viewForPresenter = A.Fake<IView<IPresenter>>();
            A.CallTo(() => viewForPresenter.Visible).Returns(true);

            A.CallTo(() => presenter.AsDrawable()).Returns(viewForPresenter);

            collection = new ComponentCollection();

            collection.Add(drawableComponent);
            collection.Add(component);
            collection.Add(presenter);
        }

        [Test()]
        public void UpdateShouldNotGetCalledWhenNotInitialized()
        {
            collection.Update(null);

            A.CallTo(() => component.Update(null)).MustNotHaveHappened();
            A.CallTo(() => presenter.Update(null)).MustNotHaveHappened();
            A.CallTo(() => drawableComponent.Update(null)).MustNotHaveHappened();
        }

        [Test()]
        public void DrawShouldNotGetCalledWhenContentNotLoaded()
        {
            collection.Draw(null, null);

            A.CallTo(() => drawableComponent.Draw(null, null)).MustNotHaveHappened();
            A.CallTo(() => viewForPresenter.Draw(null, null)).MustNotHaveHappened();
        }

        [Test()]
        public void UpdateShouldGetCalledWhenInitialized()
        {
            collection.Initialize();

            collection.Update(null);

            A.CallTo(() => component.Update(null)).MustHaveHappened();
            A.CallTo(() => presenter.Update(null)).MustHaveHappened();
            A.CallTo(() => drawableComponent.Update(null)).MustHaveHappened();
        }

        [Test()]
        public void DrawShouldGetCalledWhenContentLoaded()
        {
            collection.LoadContent(null);

            collection.Draw(null, null);

            A.CallTo(() => drawableComponent.Draw(null, null)).MustHaveHappened();
            A.CallTo(() => viewForPresenter.Draw(null, null)).MustHaveHappened();
        }

        [Test()]
        public void LoadContentShouldNotGetCalledMoreThanOneTimes()
        {
            var timesToTest = 3;
            for (int i = 0; i < timesToTest; i++)
            {
                collection.LoadContent(null);
            }

            A.CallTo(() => drawableComponent.LoadContent(null)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => viewForPresenter.LoadContent(null)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test()]
        public void InitializeShouldNotGetCalledMoreThanOneTimes()
        {
            var timesToTest = 3;
            for (int i = 0; i < timesToTest; i++)
            {
                collection.Initialize();
            }

            A.CallTo(() => component.Initialize()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => presenter.Initialize()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => drawableComponent.Initialize()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test()]
        public void UnloadContentShouldNotGetCalledWhenContentNotLoaded()
        {
            collection.UnloadContent();

            A.CallTo(() => drawableComponent.UnloadContent()).MustNotHaveHappened();
            A.CallTo(() => viewForPresenter.UnloadContent()).MustNotHaveHappened();
        }

        [Test()]
        public void UnloadContentShouldGetCalledWhenContentLoaded()
        {
            collection.LoadContent(null);
            collection.UnloadContent();

            A.CallTo(() => drawableComponent.UnloadContent()).MustHaveHappened();
            A.CallTo(() => viewForPresenter.UnloadContent()).MustHaveHappened();
        }

        [Test()]
        public void DontUpdateWhenComponentIsDisabled()
        {
            Component disabledComponent = A.Fake<Component>();
            disabledComponent.Enabled = false; // it is a fake, but it cannot fake enabled because its sealed

            collection.Add(disabledComponent);

            collection.Initialize();
            collection.Update(null);

            A.CallTo(() => disabledComponent.Update(null)).MustNotHaveHappened();
        }

        [Test()]
        public void DontDrawWhenComponentIsInvisible()
        {
            DrawableComponent disabledComponent = A.Fake<DrawableComponent>();
            disabledComponent.Visible = false; // it is a fake, but it cannot fake visible because its sealed

            collection.Add(disabledComponent);

            collection.LoadContent(null);
            collection.Draw(null, null);

            A.CallTo(() => disabledComponent.Draw(null, null)).MustNotHaveHappened();
        }

        [Test()]
        public void UpdateInOrder()
        {
            presenter.UpdateOrder = 1;
            drawableComponent.UpdateOrder = 2;
            component.UpdateOrder = 3;

            using (var scope = Fake.CreateScope())
            {
                collection.Initialize();
                collection.Update(null);

                using (scope.OrderedAssertions())
                {
                    A.CallTo(() => presenter.Update(null)).MustHaveHappened();
                    A.CallTo(() => drawableComponent.Update(null)).MustHaveHappened();
                    A.CallTo(() => component.Update(null)).MustHaveHappened();
                }
            }
        }

        [Test()]
        public void DrawInOrder()
        {
            DrawableComponent drawableComponentTwo = A.Fake<DrawableComponent>();

            drawableComponent.DrawOrder = 1;
            drawableComponentTwo.DrawOrder = 2;
            A.CallTo(() => viewForPresenter.DrawOrder).Returns(3);

            collection.Add(drawableComponentTwo);
            using (var scope = Fake.CreateScope())
            {
                collection.LoadContent(null);
                collection.Draw(null, null);

                using (scope.OrderedAssertions())
                {
                    A.CallTo(() => drawableComponent.Draw(null, null)).MustHaveHappened();
                    A.CallTo(() => drawableComponentTwo.Draw(null, null)).MustHaveHappened();
                    A.CallTo(() => viewForPresenter.Draw(null, null)).MustHaveHappened();
                }
            }
        }
    }
}