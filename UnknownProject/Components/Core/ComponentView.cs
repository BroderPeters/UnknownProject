using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace UnknownProject.Components.Core
{
    public abstract class ComponentView<T> : IDrawable, ISetPresenter<T>
    {
        private int _drawOrder = 0;
        private bool _visible = true;

        public int DrawOrder {
            get { return _drawOrder; }
            set {
                if (_drawOrder != value)
                {
                    _drawOrder = value;
                    DrawOrderChanged?.Invoke(this, null);
                }
            }
        }

        public bool Visible {
            get { return _visible; }
            set {
                if (_visible != value)
                {
                    _visible = value;
                    VisibleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        protected T presenter;

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public abstract void LoadContent(ContentManager contentManager);

        public virtual void SetPresenter(T presenter) {
            this.presenter = presenter;
        }

        public virtual void UnloadContent() { }

        public virtual void Initialize() { }
    }
}
