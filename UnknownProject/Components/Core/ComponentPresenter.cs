using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace UnknownProject.Components.Core
{
    public abstract class ComponentPresenter : Component, IPresenter
    {
        public abstract IDrawable AsDrawable();
    }
}
