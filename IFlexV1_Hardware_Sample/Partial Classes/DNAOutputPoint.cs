using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IFlexV1_Hardware_Sample;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public partial class DNAOutputPoint : IDNAStatus
    {
        public DNAOutputPoint()
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

        private void SetControlPointMode(DNAControlPointMode AMode)
        {
            IFlexV1_DNAFusion_Async service = XmlRpcProxy.Create<IFlexV1_DNAFusion_Async>(MainWindow.ServiceUrl);
            service.BeginControlPointMode(MainWindow.ApiKey, Site, Controller, SubController, PointNumber, AMode,
                lAysncResult =>
                {
                    service.EndControlPointMode(lAysncResult);
                }, null
            );
        }

        private DelegateCommand _ControlPointModeCommand;
        public DelegateCommand ControlPointModeCommand
        {
            get
            {
                if (_ControlPointModeCommand == null)
                    _ControlPointModeCommand = new DelegateCommand(
                        p => SetControlPointMode((DNAControlPointMode)p),
                        p => Status.Online)
                        .ListenOn(this, p => p.Status);
                return _ControlPointModeCommand;
            }
        }
    }
}
