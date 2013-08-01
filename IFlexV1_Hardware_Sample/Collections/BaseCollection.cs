using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace IFlexV1_Hardware_Sample
{
    public class BaseCollection : ObservableCollection<object>, IPopulation
    {
        public BaseCollection(ControllerViewModel parent, Action populate)
        {
            Parent = parent;
            Populate = populate;
        }

        public ControllerViewModel Parent
        {
            get;
            private set;
        }

        public Action Populate
        {
            get;
            set;
        }

        public bool NeedsToByPopulated()
        {
            return Items.Count == 1 && Items[0] is DummyNode;
        }
    }
}
