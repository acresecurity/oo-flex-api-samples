using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IFlexV1_Hardware_Sample;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public partial class DNATimeSchedule : IDNAStatus
    {
        public DNATimeSchedule()
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
                return Convert.ToInt32(V1.PackedAddress.Encode(HardwareTypes.Timezone, Site, Controller, TimeSchedule, 0));
            }
            set
            {

            }
        }

        public void SetTimeScheduleMode(DNATimeScheduleControl mode)
        {
            IFlexV1_DNAFusion_Async service = XmlRpcProxy.Create<IFlexV1_DNAFusion_Async>(MainWindow.ServiceUrl);
            service.BeginControlTimeSchedule(MainWindow.ApiKey, Site, Controller, TimeSchedule, mode,
                lAysncResult =>
                {
                    service.EndControlTimeSchedule(lAysncResult);
                }, null
            );
        }

        private DelegateCommand _TimeScheduleModeCommand;
        public DelegateCommand TimeScheduleModeCommand
        {
            get
            {
                if (_TimeScheduleModeCommand == null)
                    _TimeScheduleModeCommand = new DelegateCommand(
                        p => SetTimeScheduleMode((DNATimeScheduleControl)p),
                        p => Status.Online)
                        .ListenOn(this, p => p.Status);
                return _TimeScheduleModeCommand;
            }
        }
    }
}
