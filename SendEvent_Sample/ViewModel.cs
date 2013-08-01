using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenOptions.dnaFusion.Flex.V1;
using CookComputing.XmlRpc;

namespace SendEvent_Sample
{
    class ViewModel : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public ViewModel()
        {
            EventData = new DNASendEvent()
            {
                CameraID = -1,
                EventIndex = -1,
            };
            LoadEventDescriptions();
        }

        public string ApiKey
        {
            get
            {
                return "a214ce66-ff77-4aee-8964-406f9817758e";
            }
        }

        public string ServiceUrl
        {
            get
            {
                return @"https://flex.ooinc.com/xmlrpc";
                //return @"http://localhost:8099/xmlrpc";
            }
        }

        private DNAEventDescription[] _EventDescriptions;
        public DNAEventDescription[] EventDescriptions
        {
            get
            {
                return _EventDescriptions;
            }
            set
            {
                RaisePropertyChanging("EventDescriptions");
                _EventDescriptions = value;
                RaisePropertyChanged("EventDescriptions");
            }
        }

        private DNASendEvent _EventData;
        public DNASendEvent EventData
        {
            get
            {
                return _EventData;
            }
            set
            {
                RaisePropertyChanging("EventData");
                _EventData = value;
                RaisePropertyChanged("EventData");
            }
        }

        private void LoadEventDescriptions()
        {
            var service = XmlRpcProxy.Create<IFlexV1_Events_Async>(ServiceUrl);
            service.BeginRetrieveEventDescriptions(ApiKey,
                lAsyncResult =>
                {
                    try
                    {
                        EventDescriptions = service.EndRetrieveEventDescriptions(lAsyncResult)
                            .OrderBy(p => p.Description)
                            .ToArray();
                    }
                    catch (XmlRpcFaultException ex)
                    {
                    }
                }, null);
        }

        public void SendEvent()
        {
            EventData.EventDateTime = DateTime.Now;

            IFlexV1_DNAFusion_Async service = XmlRpcProxy.Create<IFlexV1_DNAFusion_Async>(ServiceUrl);
            service.BeginSendEvent(ApiKey, EventData,
                lAsyncResult =>
                {
                    service.EndSendEvent(lAsyncResult);
                }, null);
        }

        private DelegateCommand _SendEventCommand;
        public DelegateCommand SendEventCommand
        {
            get
            {
                if (_SendEventCommand == null)
                    _SendEventCommand = new DelegateCommand(p => SendEvent());
                return _SendEventCommand;
            }
        }

        #region INotifyPropertyChanged, INotifyPropertyChanging
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        private void RaisePropertyChanging(string propertyName)
        {
            if (null != PropertyChanging)
            {
                this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
        #endregion
    }
}
