using OpenOptions.dnaFusion.Flex.V1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public class ViewModel : IDisposable, INotifyPropertyChanging, INotifyPropertyChanged
    {
        public static ViewModel Current
        {
            get;
            private set;
        }

        public ViewModel()
        {
            Current = this;

            ServiceUrl = "https://flex.ooinc.com/xmlrpc";
            //ServiceUrl = string.Format(@"http://{0}:8099/xmlrpc", System.Environment.MachineName); // So we can use fiddler

            Events = new ObservableCollection<DNAFusionEvent>();
            Alarms = new ObservableCollection<DNAFusionAlarm>();
            EventDescriptions = new List<DNAEventDescription>();

            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                GetEventDescriptions();
                GetCurrentAlarms();
                StartEventReceiver();
                StartStatusTimer();
            }
        }
  
        private void StartEventReceiver()
        {
            EventReceiver = new FlexV1EventReceiver()
            {
                ApiKey = this.ApiKey,
                ServiceUrl = this.ServiceUrl,
                SubscribeTo = SubscribeTo.Both
            };
            EventReceiver.OnEvent += (@event) =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        (Action)delegate()
                        {
                            Events.Insert(0, @event);
                        });
                };
            EventReceiver.OnAlarm += (alarm) =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        (Action)delegate()
                        {
                            if (alarm.Notification == DNAAlarmNotification.ClearAll)
                                Alarms.Clear();
                            else
                            {
                                var exists = Alarms.FirstOrDefault(p => p.AlarmID == alarm.AlarmID);
                                if (exists == null)
                                    Alarms.Insert(0, alarm);
                                else
                                {
                                    if (alarm.Status == DNAAlarmStatus.Normal || alarm.Notification == DNAAlarmNotification.Purge || alarm.Notification == DNAAlarmNotification.Remove)
                                        Alarms.Remove(exists);
                                    else
                                    {
                                        exists.Transaction = alarm.Transaction;
                                        exists.Status = alarm.Status;
                                        exists.Notification = alarm.Notification;
                                        if (alarm.Status == DNAAlarmStatus.Alarm)
                                            exists.Count++;
                                    }
                                }
                            }
                        });
                };
            EventReceiver.Subscribe();
            EventReceiver.StartListening();
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
            get;
            set;
        }

        public ObservableCollection<DNAFusionEvent> Events
        {
            get;
            set;
        }

        public ObservableCollection<DNAFusionAlarm> Alarms
        {
            get;
            set;
        }

        private DNAFusionAlarm _SelectedAlarm;
        public DNAFusionAlarm SelectedAlarm
        {
            get
            {
                return _SelectedAlarm;
            }
            set
            {
                RaisePropertyChanging("SelectedAlarm");
                _SelectedAlarm = value;
                RaisePropertyChanged("SelectedAlarm");
            }
        }

        public List<DNAEventDescription> EventDescriptions
        {
            get;
            set;
        }

        private FlexV1EventReceiver EventReceiver
        {
            get;
            set;
        }

        public void SendPendingAlarm(DNAFusionAlarm alarm, PendingAlarm pending, string dispatchText = "")
        {
            var service = new FlexV1_DNAFusion() { Url = ServiceUrl };
            service.BeginPendingAlarm(ApiKey, SelectedAlarm, pending, dispatchText,
                lAsyncResult =>
                {
                    service.EndPendingAlarm(lAsyncResult);
                }, null);
        }

        public void GetEventDescriptions()
        {
            var service = XmlRpcProxy.Create<IFlexV1_Events_Async>(ServiceUrl);
            service.BeginRetrieveEventDescriptions(ApiKey,
                lAsyncResult =>
                {
                    try
                    {
                        var result = service.EndRetrieveEventDescriptions(lAsyncResult);
                        System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            (Action)delegate()
                            {
                                EventDescriptions.AddRange(result);
                            });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }, null);
        }

        public void GetCurrentAlarms()
        {
            var service = new FlexV1_HardwareStatus() { Url = ServiceUrl };
            service.BeginCurrentAlarms(ApiKey,
                lAsyncResult =>
                {
                    try
                    {
                        var result = service.EndCurrentAlarms(lAsyncResult);
                        System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            (Action)delegate()
                            {
                                result.ForEach(p => Alarms.Add(p));
                            });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }, null);
        }

        #region Commands
        private DelegateCommand _AcknowledgeCommand;
        public DelegateCommand AcknowledgeComand
        {
            get
            {
                if (_AcknowledgeCommand == null)
                {
                    _AcknowledgeCommand = new DelegateCommand(
                        (p) => SendPendingAlarm(SelectedAlarm, PendingAlarm.Acknowledge),
                        (p) =>
                        {
                            return SelectedAlarm != null && (SelectedAlarm.Status == DNAAlarmStatus.Alarm || SelectedAlarm.Status == DNAAlarmStatus.Trouble);
                        }).ListenOn(this, p => p.SelectedAlarm);
                }
                return _AcknowledgeCommand;
            }
        }

        private DelegateCommand _ClearCommand;
        public DelegateCommand ClearComand
        {
            get
            {
                if (_ClearCommand == null)
                {
                    _ClearCommand = new DelegateCommand(
                        (p) => SendPendingAlarm(SelectedAlarm, PendingAlarm.Clear),
                        (p) =>
                        {
                            return SelectedAlarm != null && SelectedAlarm.Status == DNAAlarmStatus.Clear;
                        }).ListenOn(this, p => p.SelectedAlarm);
                }
                return _ClearCommand;
            }
        }

        private DelegateCommand _DismissCommand;
        public DelegateCommand DismissComand
        {
            get
            {
                if (_DismissCommand == null)
                {
                    _DismissCommand = new DelegateCommand(
                        (p) => SendPendingAlarm(SelectedAlarm, PendingAlarm.Dismiss))
                        .ListenOn(this, p => p.SelectedAlarm);
                }
                return _DismissCommand;
            }
        }
        #endregion

        #region Status Handling Routines
        private DateTime LastUpdate;
        private DispatcherTimer _DispatcherTimer;
        private bool PollActive;

        private void StopPolling()
        {
            PollActive = false;
            if (_DispatcherTimer != null)
            {
                _DispatcherTimer.Stop();
                _DispatcherTimer.Tick -= _DispatcherTimer_Tick;
            }
        }

        public void UpdateStatus(DNAStatus[] AStatus)
        {
            foreach (DNAStatus status in AStatus)
            {
                var alarm = Alarms.First(d => d.PackedAddress == status.Address);
                if (alarm != null)
                {
                    alarm.HardwareState = status.HardwareState;
                    if (status.LastUpdate > LastUpdate)
                        LastUpdate = status.LastUpdate;
                }
            }
        }

        public TimeSpan PollRate()
        {
            int result = 5;
            return TimeSpan.FromSeconds(result);
        }

        public void StartStatusTimer()
        {
            PollActive = true;
            _DispatcherTimer = new DispatcherTimer();
            _DispatcherTimer.Interval = PollRate();
            _DispatcherTimer.Tick += new EventHandler(_DispatcherTimer_Tick);
            _DispatcherTimer.Start();
        }

        void _DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Alarms.Count == 0)
            {
                if (PollActive)
                    _DispatcherTimer.Start();
                return;
            }

            long[] hardware = Alarms
                .Where(p => p.SourceSystem == 0 /*Mercury*/)
                .Select(p => Convert.ToInt64(p.PackedAddress))
                .ToArray();

            IFlexV1_HardwareStatus_Async service = XmlRpcProxy.Create<IFlexV1_HardwareStatus_Async>(ServiceUrl);
            service.BeginRetrieveStatus(ApiKey, hardware,
                lAysncResult =>
                {
                    if (service != null)
                    {
                        try
                        {
                            DNAStatus[] status = service.EndRetrieveStatus(lAysncResult);
                            System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                                (Action)delegate()
                                {
                                    UpdateStatus(status);
                                    if (PollActive)
                                        _DispatcherTimer.Start();
                                });
                            }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }, null);
        }
        #endregion

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

        #region IDisposable
        public void Dispose()
        {
            if (EventReceiver != null)
            {
                EventReceiver.Dispose();
                EventReceiver = null;
            }
        }
        #endregion
    }
}
