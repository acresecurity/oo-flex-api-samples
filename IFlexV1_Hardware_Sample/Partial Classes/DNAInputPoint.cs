using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IFlexV1_Hardware_Sample;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public partial class DNAInputPoint : IDNAStatus
    {
        public DNAInputPoint()
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

        public void Mask(bool AArm)
        {
            IFlexV1_DNAFusion_Async service = XmlRpcProxy.Create<IFlexV1_DNAFusion_Async>(MainWindow.ServiceUrl);
            service.BeginControlPointMask(MainWindow.ApiKey, Site, Controller, SubController, PointNumber, AArm,
                lAysncResult =>
                {
                    service.EndControlPointMask(lAysncResult);
                }, null
            );
        }

        private DelegateCommand _MaskPointCommand;
        public DelegateCommand MaskPointCommand
        {
            get
            {
                if (_MaskPointCommand == null)
                    _MaskPointCommand = new DelegateCommand(
                        p => Mask((bool)p),
                        p => Status.Online)
                        .ListenOn(this, p => p.Status);
                return _MaskPointCommand;
            }
        }
    }
}
