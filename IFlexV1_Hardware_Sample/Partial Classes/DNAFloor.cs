using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public partial class DNAFloor : IDNAStatus
    {
        public DNAFloor()
        {
            Status = new DNAStatus();
        }

        private DNAStatus _Status;
        public DNAStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                TriggerPropertyChanged("Status");
            }
        }

        public int PackedAddress
        {
            get
            {
                return Convert.ToInt32(V1.PackedAddress.Encode(HardwareTypes.Timezone, Site, Controller, Floor, 0));
            }
            set
            {

            }
        }
    }
}
