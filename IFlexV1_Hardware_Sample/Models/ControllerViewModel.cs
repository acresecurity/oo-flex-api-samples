using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using CookComputing.XmlRpc;
using OpenOptions.dnaFusion.Flex.V1;

namespace IFlexV1_Hardware_Sample
{
    public class ControllerViewModel : INotifyPropertyChanging, INotifyPropertyChanged, IDNAStatus, IPopulation
    {
        public ControllerViewModel(DNAController controller)
        {
            Controller = controller;

            Status = new DNAStatus();

            Doors = new DoorCollection(this, RetrieveDoors);
            Doors.Add(new DummyNode());
            TimeSchedules = new TimeScheduleCollection(this, RetrieveTimeSchedules);
            TimeSchedules.Add(new DummyNode());
            MonitorPointGroups = new MonitorPointGroupCollection(this, RetrieveMonitorPointGroups);
            MonitorPointGroups.Add(new DummyNode());

            Populate = () => { RetrieveSubControllers(); };
        }

        public DNAController Controller
        {
            get;
            private set;
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
                RaisePropertyChanging("Status");
                _Status = value;
                RaisePropertyChanged("Status");
            }
        }

        public int PackedAddress
        {
            get
            {
                return Controller.PackedAddress;
            }
            set
            {
                Controller.PackedAddress = value;
            }
        }

        private DoorCollection _Doors;
        public DoorCollection Doors
        {
            get
            {
                return _Doors;
            }
            set
            {
                RaisePropertyChanging("Doors");
                _Doors = value;
                RaisePropertyChanged("Doors");
            }
        }

        private TimeScheduleCollection _TimeSchedules;
        public TimeScheduleCollection TimeSchedules
        {
            get
            {
                return _TimeSchedules;
            }
            set
            {
                RaisePropertyChanging("TimeSchedules");
                _TimeSchedules = value;
                RaisePropertyChanged("TimeSchedules");
            }
        }

        private MonitorPointGroupCollection _MonitorPointGroups;
        public MonitorPointGroupCollection MonitorPointGroups
        {
            get
            {
                return _MonitorPointGroups;
            }
            set
            {
                RaisePropertyChanging("MonitorPointGroups");
                _MonitorPointGroups = value;
                RaisePropertyChanged("MonitorPointGroups");
            }
        }

        private ObservableCollection<object> _Children;
        public ObservableCollection<object> Children
        {
            get
            {
                if (_Children == null)
                {
                    List<object> result = new List<object>();
                    result.Add(Doors);
                    result.Add(MonitorPointGroups);
                    result.Add(TimeSchedules);
                    _Children = new ObservableCollection<object>(result);
                }

                return _Children;
            }
        }

        public void RetrieveDoors()
        {
            Doors.Clear();
            Doors.Add(new DummyLoadingObject());

            var service = XmlRpcProxy.Create<IFlexV1_Hardware_Async>(MainWindow.ServiceUrl);
            service.BeginFindDoors(MainWindow.ApiKey, Controller.Site, Controller.Controller,
                lAsyncResult =>
                {
                    try
                    {
                        var result = service.EndFindDoors(lAsyncResult);
                        System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            (Action)delegate()
                            {
                                Doors.Clear();
                                result.ForEach(p => Doors.Add(p));
                            });
                    }
                    catch (XmlRpcFaultException ex)
                    {

                    }
                }, null);
        }

        public void RetrieveTimeSchedules()
        {
            TimeSchedules.Clear();
            TimeSchedules.Add(new DummyLoadingObject());

            var service = XmlRpcProxy.Create<IFlexV1_Hardware_Async>(MainWindow.ServiceUrl);
            service.BeginFindTimeSchedules(MainWindow.ApiKey, Controller.Site, Controller.Controller,
                lAsyncResult =>
                {
                    try
                    {
                        var result = service.EndFindTimeSchedules(lAsyncResult);
                        System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            (Action)delegate()
                            {
                                TimeSchedules.Clear();
                                result.ForEach(p => 
                                    {
                                        TimeSchedules.Add(p);
                                    });
                            });
                    }
                    catch (XmlRpcFaultException ex)
                    {
                    }
                }, null);
        }

        public void RetrieveSubControllers()
        {
            if (Children.Count == 3)
                Children.Add(new DummyLoadingObject());

            var service = XmlRpcProxy.Create<IFlexV1_Hardware_Async>(MainWindow.ServiceUrl);
            service.BeginFindSubControllers(MainWindow.ApiKey, Controller.Site, Controller.Controller,
                lAsyncResult =>
                {
                    try
                    {
                        var result = service.EndFindSubControllers(lAsyncResult);
                        System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            (Action)delegate()
                            {
                                if (Children.Count == 4 && Children[3] is DummyLoadingObject)
                                    Children.RemoveAt(3);

                                result.ForEach(p => Children.Add(p));
                            });
                    }
                    catch (XmlRpcFaultException ex)
                    {
                    }
                }, null);
        }

        public void RetrieveMonitorPointGroups()
        {
            MonitorPointGroups.Clear();
            MonitorPointGroups.Add(new DummyLoadingObject());

            var service = XmlRpcProxy.Create<IFlexV1_Hardware_Async>(MainWindow.ServiceUrl);
            service.BeginFindMonitorPointGroups(MainWindow.ApiKey, Controller.Site, Controller.Controller,
                lAsyncResult =>
                {
                    try
                    {
                        var result = service.EndFindMonitorPointGroups(lAsyncResult);
                        System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            (Action)delegate()
                            {
                                MonitorPointGroups.Clear();
                                result.ForEach(p => MonitorPointGroups.Add(p));
                            });
                    }
                    catch (XmlRpcFaultException ex)
                    {
                    }
                }, null);
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

        #region IPopulation
        public bool NeedsToByPopulated()
        {
            return Children.Count == 3;
        }

        public Action Populate
        {
            get;
            set;
        }
        #endregion
    }
}
