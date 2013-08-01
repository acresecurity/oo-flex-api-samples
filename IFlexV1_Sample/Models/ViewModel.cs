using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using System.Windows;
using CookComputing.XmlRpc;
using OpenOptions.dnaFusion.Flex.V1;

namespace IFlexV1_Sample
{
    public class ViewModel : INotifyPropertyChanging, INotifyPropertyChanged
    {
        static ViewModel()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                HttpChannel channel = new HttpChannel(null, new XmlRpcClientFormatterSinkProvider(), null);
                ChannelServices.RegisterChannel(channel, false);
            }
        }

        public static ViewModel Current
        {
            get;
            private set;
        }

        public ViewModel()
        {
            Current = this;

            ServiceUrl = @"https://flex.ooinc.com/xmlrpc";
            //ServiceUrl = string.Format(@"http://{0}:8099/xmlrpc", System.Environment.MachineName); // So we can use fiddler

            IsInitialized = false;
        }

        public void Init()
        {
            IFlexV1_Async service = XmlRpcProxy.Create<IFlexV1_Async>(ServiceUrl);
            service.BeginRetrievePersonnelTypes(ApiKey,
                lAsyncResult =>
                {
                    try
                    {
                        TypeRecord[] result = service.EndRetrievePersonnelTypes(lAsyncResult);
                        if (result == null)
                            PersonnelTypes = new List<TypeRecord>();
                        else
                            PersonnelTypes = new List<TypeRecord>(result);
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }, null);
            IsInitialized = true;
        }

        public static string ApiKey
        {
            get
            {
                return "a214ce66-ff77-4aee-8964-406f9817758e";
            }
        }

        private string _ServiceUrl;
        public string ServiceUrl
        {
            get
            {
                return _ServiceUrl;
            }
            set
            {
                RaisePropertyChanging("ServiceUrl");
                _ServiceUrl = value;
                RaisePropertyChanged("ServiceUrl");
            }
        }

        private bool _IsInitialized;
        public bool IsInitialized
        {
            get
            {
                return _IsInitialized;
            }
            set
            {
                RaisePropertyChanging("IsInitialized");
                _IsInitialized = value;
                RaisePropertyChanged("IsInitialized");
            }
        }

        private DelegateCommand _InitCommand;
        public DelegateCommand InitCommand
        {
            get
            {
                if (_InitCommand == null)
                    _InitCommand = new DelegateCommand(
                        p => Init(),
                        p => !String.IsNullOrEmpty(ServiceUrl) && !IsInitialized)
                    .ListenOn(this, p => p.ServiceUrl)
                    .ListenOn(this, p => p.IsInitialized);
                return _InitCommand;
            }
        }

        private ObservableCollection<DNAPerson> _PersonnelRecords;
        public ObservableCollection<DNAPerson> PersonnelRecords
        {
            get
            {
                return _PersonnelRecords;
            }
            set
            {
                RaisePropertyChanging("PersonnelRecords");
                _PersonnelRecords = value;
                RaisePropertyChanged("PersonnelRecords");
            }
        }

        private DNAPerson _SelectedPerson;
        public DNAPerson SelectedPerson
        {
            get
            {
                return _SelectedPerson;
            }
            set
            {
                RaisePropertyChanging("SelectedPerson");
                _SelectedPerson = value;
                RaisePropertyChanged("SelectedPerson");
            }
        }

        public void AddPerson()
        {
            DNAPerson person = new DNAPerson()
            {
                UniqueKey = -1,
                EditState = EditStates.Add,
            };
            if (PersonnelRecords == null)
                PersonnelRecords = new ObservableCollection<DNAPerson>();
            PersonnelRecords.Add(person);
        }

        private DelegateCommand _AddPersonCommand;
        public DelegateCommand AddPersonCommand
        {
            get
            {
                if (_AddPersonCommand == null)
                    _AddPersonCommand = new DelegateCommand(
                        p => AddPerson(),
                        p => IsInitialized)
                    .ListenOn(this, p => p.IsInitialized);
                return _AddPersonCommand;
            }
        }

        private DelegateCommand _DeletePersonCommand;
        public DelegateCommand DeletePersonCommand
        {
            get
            {
                if (_DeletePersonCommand == null)
                    _DeletePersonCommand = new DelegateCommand(
                        p => SelectedPerson.EditState = EditStates.Delete,
                        p => SelectedPerson != null && SelectedPerson.EditState != EditStates.Delete)
                    .ListenOn(this, p => p.SelectedPerson);
                return _DeletePersonCommand;
            }
        }
        
        public void SaveAllChanges()
        {
            List<DNAPerson> remove = new List<DNAPerson>();

            foreach (DNAPerson person in PersonnelRecords.Where(p => p.EditState != EditStates.None))
            {
                if (person.EditState == EditStates.Add)
                {
                    try
                    {
                        person.UniqueKey = XmlRpcProxy.Create<IFlexV1>(ServiceUrl).AddPersonnelRecord(ApiKey, person.BasePerson);
                        person.EditState = EditStates.None;
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }
                else if (person.EditState == EditStates.Modified)
                {
                    try
                    {
                        person.LastModified = DateTime.Now;
                        if (XmlRpcProxy.Create<IFlexV1>(ServiceUrl).ModifyPersonnelRecord(ApiKey, person.BasePerson))
                            person.EditState = EditStates.None;
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }
                else if (person.EditState == EditStates.Delete)
                {
                    try
                    {
                        if (XmlRpcProxy.Create<IFlexV1>(ServiceUrl).DeletePersonnelRecord(ApiKey, person.UniqueKey))
                            remove.Add(person);
                    }
                    catch (XmlRpcFaultException e)
                    {
                    }
                }
            }

            remove.ForEach(p => PersonnelRecords.Remove(p));
        }

        private DelegateCommand _SaveAllChangesCommand;
        public DelegateCommand SaveAllChangesCommand
        {
            get
            {
                if (_SaveAllChangesCommand == null)
                    _SaveAllChangesCommand = new DelegateCommand(
                        p => SaveAllChanges(),
                        p => IsInitialized)
                    .ListenOn(this, p => p.IsInitialized);
                return _SaveAllChangesCommand;
            }
        }

        private List<TypeRecord> _PersonnelTypes;
        public List<TypeRecord> PersonnelTypes
        {
            get
            {
                return _PersonnelTypes;
            }
            set
            {
                RaisePropertyChanging("PersonnelTypes");
                _PersonnelTypes = value;
                RaisePropertyChanged("PersonnelTypes");
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
