using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CookComputing.XmlRpc;
using OpenOptions.dnaFusion.Flex.V1;

namespace IFlexV1_Sample
{
    public class CredentialsViewModel : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public DNAPerson Personnel
        {
            get;
            private set;
        }

        public ICommand OKCommand
        {
            get;
            private set;
        }

        public ICommand CancelCommand
        {
            get;
            private set;
        }

        public CredentialsViewModel(DNAPerson personnel, ICommand okCommand, ICommand cancelCommand)
		{
            this.Personnel = personnel;
			this.OKCommand = okCommand;
			this.CancelCommand = cancelCommand;

            Init();
		}

        private ObservableCollection<DNACredential> _Credentials;
        public ObservableCollection<DNACredential> Credentials
        {
            get
            {
                return _Credentials;
            }
            set
            {
                RaisePropertyChanging("Credentials");
                _Credentials = value;
                RaisePropertyChanged("Credentials");
            }
        }

        private DNACredential _SelectedCredential;
        public DNACredential SelectedCredential
        {
            get
            {
                return _SelectedCredential;
            }
            set
            {
                RaisePropertyChanging("SelectedCredential");
                _SelectedCredential = value;
                RaisePropertyChanged("SelectedCredential");
            }
        }
        
        private void Init()
        {
            IFlexV1_Async service = XmlRpcProxy.Create<IFlexV1_Async>(ViewModel.Current.ServiceUrl);
            service.BeginRetrieveCardTypes(ViewModel.ApiKey,
                lAsyncResult =>
                {
                    try
                    {
                        TypeRecord[] result = service.EndRetrieveCardTypes(lAsyncResult);
                        if (result == null)
                            CardTypes = new List<TypeRecord>();
                        else
                            CardTypes = new List<TypeRecord>(result);
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }, null);

            service.BeginRetrieveDisabledReasons(ViewModel.ApiKey,
                lAsyncResult =>
                {
                    try
                    {
                        TypeRecord[] result = service.EndRetrieveDisabledReasons(lAsyncResult);
                        if (result == null)
                            DisabledReasons = new List<TypeRecord>();
                        else
                            DisabledReasons = new List<TypeRecord>(result);
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }, null);

            service.BeginRetrieveAccessLevels(ViewModel.ApiKey,
                lAsyncResult =>
                {
                    try
                    {
                        TypeRecord[] result = service.EndRetrieveAccessLevels(lAsyncResult);
                        if (result == null)
                            AccessLevels = new List<TypeRecord>();
                        else
                            AccessLevels = new List<TypeRecord>(result);
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }, null);

            service.BeginCredentialsByUserID(ViewModel.ApiKey, Personnel.UniqueKey,
                lAsyncResult =>
                {
                    try
                    {
                        var result = service.EndCredentialsByUserID(lAsyncResult);
                        if (result != null)
                        {
                            Credentials = new ObservableCollection<DNACredential>(
                                result.Select(p => new DNACredential(p)));
                        }
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }, null);
        }

        private List<TypeRecord> _CardTypes;
        public List<TypeRecord> CardTypes
        {
            get
            {
                return _CardTypes;
            }
            set
            {
                RaisePropertyChanging("CardTypes");
                _CardTypes = value;
                RaisePropertyChanged("CardTypes");
            }
        }

        private List<TypeRecord> _DisabledReasons;
        public List<TypeRecord> DisabledReasons
        {
            get
            {
                return _DisabledReasons;
            }
            set
            {
                RaisePropertyChanging("DisabledReasons");
                _DisabledReasons = value;
                RaisePropertyChanged("DisabledReasons");
            }
        }

        private List<TypeRecord> _AccessLevels;
        public List<TypeRecord> AccessLevels
        {
            get
            {
                return _AccessLevels;
            }
            set
            {
                RaisePropertyChanging("AccessLevels");
                _AccessLevels = value;
                RaisePropertyChanged("AccessLevels");
            }
        }

        public void AddCredential()
        {
            DNACredential credential = new DNACredential()
            {
                UniqueKey = -1,
                EditState = EditStates.Add,
                UserID = Personnel.UniqueKey, // Must be specified to link this credential to the personnel record
            };
            if (Credentials == null)
                Credentials = new ObservableCollection<DNACredential>();
            Credentials.Add(credential);
        }

        private DelegateCommand _AddCredentialCommand;
        public DelegateCommand AddCredentialCommand
        {
            get
            {
                if (_AddCredentialCommand == null)
                    _AddCredentialCommand = new DelegateCommand(
                        p => AddCredential());
                return _AddCredentialCommand;
            }
        }

        private DelegateCommand _DeleteCredentialCommand;
        public DelegateCommand DeleteCredentialCommand
        {
            get
            {
                if (_DeleteCredentialCommand == null)
                    _DeleteCredentialCommand = new DelegateCommand(
                        p => SelectedCredential.EditState = EditStates.Delete,
                        p => SelectedCredential != null && SelectedCredential.EditState != EditStates.Delete)
                        .ListenOn(this, p => p.SelectedCredential);
                return _DeleteCredentialCommand;
            }
        }

        public void SaveChanges()
        {
            List<DNACredential> remove = new List<DNACredential>();

            foreach (DNACredential credential in Credentials.Where(p => p.EditState != EditStates.None))
            {
                if (credential.EditState == EditStates.Add)
                {
                    try
                    {
                        credential.UniqueKey = XmlRpcProxy.Create<IFlexV1>(ViewModel.Current.ServiceUrl).AddCredential(ViewModel.ApiKey, credential.BaseCredential);
                        credential.EditState = EditStates.None;
                    }
                    catch (XmlRpcFaultException e)
                    {
                        if (e.Message.Contains("Duplicate card numbers are not supported") || e.Message.Contains("Duplicate legacy card numbers are not supported"))
                            throw e;
                    }
                }
                else if (credential.EditState == EditStates.Modified)
                {
                    try
                    {
                        credential.LastModified = DateTime.Now;
                        if (XmlRpcProxy.Create<IFlexV1>(ViewModel.Current.ServiceUrl).ModifyCredential(ViewModel.ApiKey, credential.BaseCredential))
                            credential.EditState = EditStates.None;
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }
                else if (credential.EditState == EditStates.Delete)
                {
                    try
                    {
                        if (XmlRpcProxy.Create<IFlexV1>(ViewModel.Current.ServiceUrl).DeleteCredential(ViewModel.ApiKey, credential.UniqueKey))
                            remove.Add(credential);
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }
            }

            remove.ForEach(p => Credentials.Remove(p));
        }

        private DelegateCommand _SaveChangesCommand;
        public DelegateCommand SaveChangesCommand
        {
            get
            {
                if (_SaveChangesCommand == null)
                    _SaveChangesCommand = new DelegateCommand(
                        p => SaveChanges());
                return _SaveChangesCommand;
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
