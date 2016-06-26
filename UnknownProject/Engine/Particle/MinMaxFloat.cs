using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnknownProject.Extensions;

namespace UnknownProject.Engine.Particle
{
    public struct MinMaxFloat
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public MinMaxFloat(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float RandBetween(Random random)
        {
            return (float) random.NextDouble(Min, Max);
        }
    }
}
