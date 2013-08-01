using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IFlexV1_Hardware_Sample;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public partial class DNADoor : IDNAStatus
    {
        public DNADoor()
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

        public void ControlDoorMode(DNADoorMode doorMode)
        {
            IFlexV1_DNAFusion_Async service = XmlRpcProxy.Create<IFlexV1_DNAFusion_Async>(MainWindow.ServiceUrl);
            service.BeginControlDoorMode(MainWindow.ApiKey, Site, Controller, DoorNumber, doorMode,
                lAsyncResult =>
                {
                    service.EndControlDoorMode(lAsyncResult);
                }, null);
        }

        private DelegateCommand _ControlDoorModeCommand;
        public DelegateCommand ControlDoorModeCommand
        {
            get
            {
                if (_ControlDoorModeCommand == null)
                    _ControlDoorModeCommand = new DelegateCommand(
                        p => ControlDoorMode((DNADoorMode)p),
                        p => Status.Online)
                        .ListenOn(this, p => p.Status);

                return _ControlDoorModeCommand;
            }
        }

        public void MomentaryUnlock()
        {
            IFlexV1_DNAFusion_Async service = XmlRpcProxy.Create<IFlexV1_DNAFusion_Async>(MainWindow.ServiceUrl);
            service.BeginMomentaryUnlockDoor(MainWindow.ApiKey, Site, Controller, DoorNumber,
                lAsyncResult =>
                {
                    service.EndMomentaryUnlockDoor(lAsyncResult);
                }, null);
        }

        private DelegateCommand _MomentaryUnlockCommand;
        public DelegateCommand MomentaryUnlockCommand
        {
            get
            {
                if (_MomentaryUnlockCommand == null)
                    _MomentaryUnlockCommand = new DelegateCommand(
                        p => MomentaryUnlock(),
                        p => Status.Online)
                        .ListenOn(this, p => p.Status);

                return _MomentaryUnlockCommand;
            }
        }

        public void DoorMask(DNADoorAlarm ADoorAlarm, bool AArm)
        {
            IFlexV1_DNAFusion_Async service = XmlRpcProxy.Create<IFlexV1_DNAFusion_Async>(MainWindow.ServiceUrl);
            service.BeginControlDoorMask(MainWindow.ApiKey, Site, Controller, DoorID, ADoorAlarm, AArm,
                lAysncResult =>
                {
                    service.EndControlDoorMode(lAysncResult);
                }, null
            );
        }

        private DelegateCommand _DoorForcedMaskCommand;
        public DelegateCommand DoorForcedMaskCommand
        {
            get
            {
                if (_DoorForcedMaskCommand == null)
                    _DoorForcedMaskCommand = new DelegateCommand(
                        p => DoorMask(DNADoorAlarm.Forced, !Status.ForcedMasked),
                        p => Status.Online)
                        .ListenOn(this, p => p.Status);
                return _DoorForcedMaskCommand;
            }
        }

        private DelegateCommand _DoorForcedHeldCommand;
        public DelegateCommand DoorHeldMaskCommand
        {
            get
            {
                if (_DoorForcedHeldCommand == null)
                    _DoorForcedHeldCommand = new DelegateCommand(
                        p => DoorMask(DNADoorAlarm.Held, !Status.ForcedMasked),
                        p => Status.Online)
                        .ListenOn(this, p => p.Status);
                return _DoorForcedHeldCommand;
            }
        }
    }
}
