using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFlexV1_Hardware_Sample
{
    public interface IPopulation
    {
        bool NeedsToByPopulated();

        Action Populate
        {
            get;
            set;
        }
    }
}
