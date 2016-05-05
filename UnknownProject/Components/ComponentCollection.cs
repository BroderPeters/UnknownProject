using System;
using System.Collections.Generic;
using System.Diagnostics;
#if WINRT
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
#endif
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using System.Collections;

namespace UnknownProject.Components
{
    public class ComponentCollection : DrawableComponent
    {
        private List<IDrawable> _drawables = new List<IDrawable>();
        private List<IUpdateable> _updateables = new List<IUpdateable>();

        private ISet<Component> _needInit = new HashSet<Component>();
        private ISet<IDrawable> _needInitDrawable = new HashSet<IDrawable>();

        public void Add(Component component)
        {
            _needInit.Add(component);

            IDrawable asDrawable = component as IDrawable;
            if(asDrawable == null)
            {
                asDrawable = (component as IHasDrawable)?.AsDrawable();
            }

            if (asDrawable != null)
            {
                _needInitDrawable.Add(asDrawable);
            }
        }

        public override void Initialize()
        {
            foreach (Component component in _needInit)
            {
                component.Initialize();
            }
            _updateables.AddRange(_needInit);
            _needInit.Clear();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            foreach (IDrawable drawable in _needInitDrawable)
            {
                drawable.LoadContent(contentManager);
            }
            _drawables.AddRange(_needInitDrawable);
            _needInitDrawable.Clear();
        }

        public override void Draw(SpriteBatch sprieBatch, GameTime gameTime)
        {
            foreach (IDrawable drawable in _drawables) 
            {
                drawable.Draw(sprieBatch, gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IUpdateable updateable in _updateables)
            {
                updateable.Update(gameTime);
            }
        }

        public override void UnloadContent()
        {
            foreach (IDrawable drawable in _drawables)
            {
                drawable.UnloadContent();
            }
        }
    }
}
