using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace UnknownProject.Components
{
    public class Component : IGameComponent, IUpdateable
    {
        private bool _enabled = true;
        private int _updateOrder = 0;

        public bool Enabled {
            get { return _enabled; }
            set {
                if (_enabled != value)
                {
                    _enabled = value;
                    EnabledChanged?.Invoke(this, EventArgs.Empty);
                    OnEnabledChanged(this, null);
                }
            }
        }
        
        public int UpdateOrder {
            get { return _updateOrder; }
            set {
                if (_updateOrder != value)
                {
                    _updateOrder = value;
                    UpdateOrderChanged?.Invoke(this, EventArgs.Empty);
                    OnUpdateOrderChanged(this, null);
                }
            }
        }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        public virtual void Initialize() { }
        public virtual void Update(GameTime gameTime) { }
        protected virtual void OnEnabledChanged(object sender, EventArgs args) { }
        protected virtual void OnUpdateOrderChanged(object sender, EventArgs args) { }
    }
}
