using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownProject.Character
{
    class Jobs : Specieses
    {
        protected Specieses species;

        public void setSpecies(Specieses species)
        {
            this.species = species;
        }

        public override void operation()
        {
            if (species != null)
            {
                species.operation();
            }
        }
    }
}
