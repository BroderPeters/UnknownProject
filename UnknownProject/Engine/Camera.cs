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
        private const double MAX_ZOOM = 10;
        private const double MIN_ZOOM = 0.1;

        public Vector2 Point { get; private set; }

        private double zoom = 1;
        public double Zoom {
            get { return zoom; }
            set {
                if (value >= MIN_ZOOM && value <= MAX_ZOOM)
                {
                    zoom = value;
                }
            }
        }

        public Camera()
        {
            Point = new Vector2();
        }

        public void AddOffset(float x, float y)
        {
            if (Point.X + x < 0 || Point.Y + y < 0)
            {
                return;
            }
            Point = new Vector2(Point.X + x, Point.Y + y);
        }
    }
}
