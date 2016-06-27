using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownProject.Character.jobs
{
    class Warrior : Jobs
    {
        public override void operation()
        {
            base.operation();
            Console.WriteLine("... und von Beruf Krieger!");
        }
    }
}
