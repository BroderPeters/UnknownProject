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

namespace UnknownProject.Components.Core
{
    /// <summary>
    /// The Component Collection is for storing and grouping components, the collection itself is an component so it can be added to another collection
    /// Update and draw get executed in the order specified in IUpdateable#UpdateOrder/IDrawable#DrawOrder
    /// and filtered with IUpdateable#Enabled and IDrawable#Visible.
    /// </summary>
    public class ComponentCollection : DrawableComponent, ICollection<IGameComponent>
    {
        private ISet<IGameComponent> all = new HashSet<IGameComponent>();

        private MonoGameSortingFilteringCollection<IDrawable> _drawables = new MonoGameSortingFilteringCollection<IDrawable>(
                d => d.Visible,
                (d, handler) => d.VisibleChanged += handler,
                (d, handler) => d.VisibleChanged -= handler,
                (d1, d2) => Comparer<int>.Default.Compare(d1.DrawOrder, d2.DrawOrder),
                (d, handler) => d.DrawOrderChanged += handler,
                (d, handler) => d.DrawOrderChanged -= handler);
        private MonoGameSortingFilteringCollection<IUpdateable> _updateables = new MonoGameSortingFilteringCollection<IUpdateable>(
                u => u.Enabled,
                (u, handler) => u.EnabledChanged += handler,
                (u, handler) => u.EnabledChanged -= handler,
                (u1, u2) => Comparer<int>.Default.Compare(u1.UpdateOrder, u2.UpdateOrder),
                (u, handler) => u.UpdateOrderChanged += handler,
                (u, handler) => u.UpdateOrderChanged -= handler);

        private ISet<IGameComponent> _needInit = new HashSet<IGameComponent>();
        private ISet<IDrawable> _needInitDrawable = new HashSet<IDrawable>();

        public int Count { get { return all.Count; } }

        public bool IsReadOnly { get { return false; } }

        public override void Initialize()
        {
            foreach (IGameComponent component in _needInit)
            {
                component.Initialize();
                IUpdateable asUpdateable = component as IUpdateable;
                if (asUpdateable != null)
                {
                    _updateables.Add(asUpdateable);
                }
            }
            _needInit.Clear();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            foreach (IDrawable drawable in _needInitDrawable)
            {
                drawable.LoadContent(contentManager);
                _drawables.Add(drawable);
            }
            _needInitDrawable.Clear();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            List<IDrawable> orderedDrawables = _drawables.WithOrderAndFilter();
            for (int i = 0; i < orderedDrawables.Count; i++)
            {
                orderedDrawables[i].Draw(spriteBatch, gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            List<IUpdateable> orderedUpdateables = _updateables.WithOrderAndFilter();
            for (int i = 0; i < orderedUpdateables.Count; i++)
            {
                orderedUpdateables[i].Update(gameTime);
            }
        }

        public override void UnloadContent()
        {
            foreach (IDrawable drawable in _drawables)
            {
                drawable.UnloadContent();
            }
            _drawables.Clear();
        }

        public void Add(IGameComponent item)
        {
            all.Add(item);
            _needInit.Add(item);

            IDrawable asDrawable = getDrawableOrNull(item);
            if (asDrawable != null)
            {
                _needInitDrawable.Add(asDrawable);
            }
        }

        private IDrawable getDrawableOrNull(IGameComponent item)
        {
            IDrawable asDrawable = item as IDrawable;
            if (asDrawable == null)
            {
                return (item as IHasDrawable)?.AsDrawable();
            }
            return asDrawable;
        }

        public void Clear()
        {
            all.Clear();
            _needInit.Clear();
            _needInitDrawable.Clear();
            _drawables.Clear();
            _updateables.Clear();
        }

        public bool Contains(IGameComponent item)
        {
            return all.Contains(item);
        }

        public void CopyTo(IGameComponent[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public bool Remove(IGameComponent item)
        {
            _needInit.Remove(item);

            IDrawable asDrawable = getDrawableOrNull(item);
            if (asDrawable != null)
            {
                _needInitDrawable.Remove(asDrawable);
                _drawables.Remove(asDrawable);
            }

            IUpdateable asUpdateable = item as IUpdateable;
            if (asUpdateable != null)
            {
                _updateables.Remove(asUpdateable);
            }
            return all.Remove(item);
        }

        public IEnumerator<IGameComponent> GetEnumerator()
        {
            return all.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return all.GetEnumerator();
        }
    }
}
