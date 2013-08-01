using System;
using System.ComponentModel;
using OpenOptions.dnaFusion.Flex.V1;
using CookComputing.XmlRpc;

namespace IFlexV1_Sample
{
    public class DNACredential : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public DNACredential()
        {
            BaseCredential = new OpenOptions.dnaFusion.Flex.V1.DNACredential();
        }

        public DNACredential(OpenOptions.dnaFusion.Flex.V1.DNACredential dnaCredential)
        {
            BaseCredential = dnaCredential;
        }

        public OpenOptions.dnaFusion.Flex.V1.DNACredential BaseCredential;

        #region BaseDnaStruct
        public DateTime LastModified
        {
            get
            {
                return BaseCredential.LastModified;
            }
            set
            {
                RaisePropertyChanging("LastModified");
                BaseCredential.LastModified = value;
                RaisePropertyChanged("LastModified");
            }
        }

        public int UniqueKey
        {
            get
            {
                return BaseCredential.UniqueKey;
            }
            set
            {
                RaisePropertyChanging("UniqueKey");
                BaseCredential.UniqueKey = value;
                RaisePropertyChanged("UniqueKey");
            }
        }
        #endregion

        #region DNACredential Members
        public DateTime Activation
        {
            get
            {
                return BaseCredential.Activation;
            }
            set
            {
                RaisePropertyChanging("Activation");
                BaseCredential.Activation = value;
                RaisePropertyChanged("Activation");
            }
        }

        public string CardNumber
        {
            get
            {
                return BaseCredential.CardNumber;
            }
            set
            {
                RaisePropertyChanging("CardNumber");
                BaseCredential.CardNumber = value;
                RaisePropertyChanged("CardNumber");
            }
        }

        public int CardType
        {
            get
            {
                return BaseCredential.CardType;
            }
            set
            {
                RaisePropertyChanging("CardType");
                BaseCredential.CardType = value;
                RaisePropertyChanged("CardType");
            }
        }

        public int DisabledReason
        {
            get
            {
                return BaseCredential.DisabledReason;
            }
            set
            {
                RaisePropertyChanging("DisabledReason");
                BaseCredential.DisabledReason = value;
                RaisePropertyChanged("DisabledReason");
            }
        }

        public DateTime Expiration
        {
            get
            {
                return BaseCredential.Expiration;
            }
            set
            {
                RaisePropertyChanging("Expiration");
                BaseCredential.Expiration = value;
                RaisePropertyChanged("Expiration");
            }
        }

        public string LegacyCardNumber
        {
            get
            {
                return BaseCredential.LegacyCardNumber;
            }
            set
            {
                RaisePropertyChanging("LegacyCardNumber");
                BaseCredential.LegacyCardNumber = value;
                RaisePropertyChanged("LegacyCardNumber");
            }
        }

        public string LegacyFacilityCode
        {
            get
            {
                return BaseCredential.LegacyFacilityCode;
            }
            set
            {
                RaisePropertyChanging("LegacyFacilityCode");
                BaseCredential.LegacyFacilityCode = value;
                RaisePropertyChanged("LegacyFacilityCode");
            }
        }

        public int UserID
        {
            get
            {
                return BaseCredential.UserID;
            }
            set
            {
                RaisePropertyChanging("UserID");
                BaseCredential.UserID = value;
                RaisePropertyChanged("UserID");
            }
        }
        #endregion

        #region Methods and Properties that have nothing to do with the service but help the test app
        // Only used as way to identify what records have changed and how
        private EditStates _EditState;
        public EditStates EditState
        {
            get
            {
                return _EditState;
            }
            set
            {
                RaisePropertyChanging("EditState");
                _EditState = value;
                
                if (value != EditStates.None)
                    LastModified = DateTime.Now;

                RaisePropertyChanged("EditState");
            }
        }

        private TypeRecord _SelectedAccessLevel;
        public TypeRecord SelectedAccessLevel
        {
            get
            {
                return _SelectedAccessLevel;
            }
            set
            {
                RaisePropertyChanging("SelectedAccessLevel");
                _SelectedAccessLevel = value;
                RaisePropertyChanged("SelectedAccessLevel");
            }
        }

        public void ApplyAccessLevel()
        {
            IFlexV1_Async service = XmlRpcProxy.Create<IFlexV1_Async>(ViewModel.Current.ServiceUrl);
            service.BeginApplyAccessLevelGroup(ViewModel.ApiKey, SelectedAccessLevel.ID, UniqueKey,
                lAsyncResult =>
                {
                    try
                    {
                        service.EndApplyAccessLevelGroup(lAsyncResult);
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }, null);
        }

        private DelegateCommand _ApplyAccessLevelCommand;
        public DelegateCommand ApplyAccessLevelCommand
        {
            get
            {
                if (_ApplyAccessLevelCommand == null)
                    _ApplyAccessLevelCommand = new DelegateCommand(
                        p => ApplyAccessLevel(),
                        p => UniqueKey > -1 && EditState != EditStates.Add && SelectedAccessLevel != null && CardType != 3)
                        .ListenOn(this, p => p.UniqueKey)
                        .ListenOn(this, p => p.EditState)
                        .ListenOn(this, p => p.SelectedAccessLevel)
                        .ListenOn(this, p => p.CardType);
                return _ApplyAccessLevelCommand;
            }
        }
        #endregion

        #region INotifyPropertyChanged, INotifyPropertyChanging
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

                if (propertyName != "EditState" && propertyName != "SelectedAccessLevel")
                    if (EditState == EditStates.None)
                        EditState = EditStates.Modified;
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
