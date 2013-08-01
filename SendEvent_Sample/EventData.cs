using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SendEvent_Sample
{
    class EventData : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public EventData()
        {
            SystemID = 9;
            CredentialID = -1;
        }

        private int _SystemID;
        public int SystemID
        {
            get
            {
                return _SystemID;
            }
            set
            {
                RaisePropertyChanging("SystemID");
                _SystemID = value;
                RaisePropertyChanged("SystemID");
            }
        }

        private int _CredentialID;
        public int CredentialID
        {
            get
            {
                return _CredentialID;
            }
            set
            {
                RaisePropertyChanging("CredentialID");
                _CredentialID = value;
                RaisePropertyChanged("CredentialID");
            }
        }

        private int _DnaEventID;
        public int DnaEventID
        {
            get
            {
                return _DnaEventID;
            }
            set
            {
                RaisePropertyChanging("DnaEventID");
                _DnaEventID = value;
                RaisePropertyChanged("DnaEventID");
            }
        }

        private int _ApplicationEventID;
        public int ApplicationEventID
        {
            get
            {
                return _ApplicationEventID;
            }
            set
            {
                RaisePropertyChanging("ApplicationEventID");
                _ApplicationEventID = value;
                RaisePropertyChanged("ApplicationEventID");
            }
        }

        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                RaisePropertyChanging("Description");
                _Description = value;
                RaisePropertyChanged("Description");
            }
        }

        private System.DateTime _Occured;
        public System.DateTime Occured
        {
            get
            {
                return _Occured;
            }
            set
            {
                RaisePropertyChanging("Occured");
                _Occured = value;
                RaisePropertyChanged("Occured");
            }
        }

        private long _PackedAddress;
        public long PackedAddress
        {
            get
            {
                return _PackedAddress;
            }
            set
            {
                RaisePropertyChanging("PackedAddress");
                _PackedAddress = value;
                RaisePropertyChanged("PackedAddress");
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
