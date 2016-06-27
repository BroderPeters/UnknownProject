using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownProject.Engine.Particle
{
    public struct OpacityChangeMode
    {
        public const double StartAtThreeQuartersOfLifeTime = 3d / 4;
        public const double StartAtSevenEightsOfLifeTime = 7d / 8;
        public const double StartAtOneQuartersOfLifeTime = 1d / 4;
        public const double StartAtHalfLifeTime = 1d / 2;
        public const double StartAtStart = 0;

    }
}
