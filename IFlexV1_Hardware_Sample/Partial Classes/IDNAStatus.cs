using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public interface IDNAStatus
    {
        DNAStatus Status
        {
            get;
            set;
        }

        int PackedAddress
        {
            get;
            set;
        }
    }
}
