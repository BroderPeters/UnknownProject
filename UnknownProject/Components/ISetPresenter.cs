﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownProject.Components
{
    public interface ISetPresenter<T>
    {
        void SetPresenter(T presenter);
    }
}
