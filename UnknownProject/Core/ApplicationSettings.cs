﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownProject
{
    [DependencyInjection.Singleton]
    public class ApplicationSettings
    {
        public int Width { get; set; }
        // maybe load from file
    }
}
