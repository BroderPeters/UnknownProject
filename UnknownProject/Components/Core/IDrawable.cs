using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownProject.Components.Core
{
    public interface IDrawable : IGameComponent
    {
        int DrawOrder { get; }
        bool Visible { get; }

        event EventHandler<EventArgs> DrawOrderChanged;
        event EventHandler<EventArgs> VisibleChanged;

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void LoadContent(ContentManager contentManager);
        void UnloadContent();
    }
}
