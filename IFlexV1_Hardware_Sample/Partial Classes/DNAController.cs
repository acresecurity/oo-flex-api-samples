using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public partial class DNAController : IDNAStatus
    {
        public DNAController()
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
    }
}
