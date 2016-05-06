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

namespace UnknownProject.Components.Tests
{
    [TestFixture()]
    public class ComponentCollectionTests
    {
        DrawableComponent drawableComponent;

        Component component;

        ComponentPresenter presenter;
        IView<IPresenter> viewForPresenter;

        ComponentCollection collection;

        [SetUp()]
        public void Init()
        {
            drawableComponent = A.Fake<DrawableComponent>();

            component = A.Fake<Component>();

            presenter = A.Fake<ComponentPresenter>();
            viewForPresenter = A.Fake<IView<IPresenter>>();

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

            A.CallTo(() => drawableComponent.Draw(null, null)).WithAnyArguments().MustNotHaveHappened();
            A.CallTo(() => viewForPresenter.Draw(null, null)).WithAnyArguments().MustNotHaveHappened();
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

            A.CallTo(() => drawableComponent.Draw(null, null)).WithAnyArguments().MustHaveHappened();
            A.CallTo(() => viewForPresenter.Draw(null, null)).WithAnyArguments().MustHaveHappened();
        }

        [Test()]
        public void LoadContentShouldNotGetCalledMoreThanOneTimes()
        {
            var timesToTest = 3;
            for (int i = 0; i < timesToTest; i++)
            {
                collection.LoadContent(null);
            }

            A.CallTo(() => drawableComponent.LoadContent(null)).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => viewForPresenter.LoadContent(null)).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Once);
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
    }
}