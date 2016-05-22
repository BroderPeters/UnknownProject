using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownProject.Engine
{
    [DependencyInjection.Singleton]
    public class Camera
    {
        public Vector2 Point { get; set; }
        public Camera()
        {
            Point = new Vector2();
        }

        public void AddOffset(float x, float y)
        {
            if(Point.X + x < 0 || Point.Y + y < 0)
            {
                return;
            }
            Point = new Vector2(Point.X + x, Point.Y + y);
        }
    }
}
