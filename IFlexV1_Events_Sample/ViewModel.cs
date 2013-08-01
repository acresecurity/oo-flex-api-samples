using CookComputing.XmlRpc;
using OpenOptions.dnaFusion.Flex.V1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public class ViewModel : INotifyPropertyChanging, INotifyPropertyChanged
    {
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

            Events = new ObservableCollection<DNAFusionEvent>();
            Filters = new List<DNAEventFilter>();
            EventDescriptions = new List<DNAEventDescription>();

            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                GetEventDescriptions();
                GetRecentEvents();
            }
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

        public ObservableCollection<DNAFusionEvent> Events
        {
            get;
            set;
        }

        public List<DNAEventFilter> Filters
        {
            get;
            set;
        }

        public List<DNAEventDescription> EventDescriptions
        {
            get;
            set;
        }

        public void GetEventDescriptions()
        {
            var service = XmlRpcProxy.Create<IFlexV1_Events_Async>(ServiceUrl);
            service.BeginRetrieveEventDescriptions(ApiKey,
                lAsyncResult =>
                {
                    var result = service.EndRetrieveEventDescriptions(lAsyncResult);
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        (Action)delegate()
                        {
                            EventDescriptions.AddRange(result);
                        });
                }, null);
        }

        public void GetRecentEvents()
        {
            StopPolling();
            var service = XmlRpcProxy.Create<IFlexV1_Events_Async>(ServiceUrl);
            service.BeginGet(ApiKey, -1, -1, 20, Filters.ToArray(),
                lAsyncResult =>
                {
                    var result = service.EndGet(lAsyncResult);
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        (Action)delegate()
                        {
                            Events.Clear();
                            LastIndex = -1;
                            if (result != null && result.Length > 0)
                            {
                                LastIndex = result.First().UniqueKey + 1;
                                result.ForEach(p => Events.Add(p));
                            }
                            StartTimer();
                        });
                }, null);
        }

        public void ShowMoreEvents()
        {
            var service = XmlRpcProxy.Create<IFlexV1_Events_Async>(ServiceUrl);
            service.BeginGet(ApiKey, -1, Events.Last().UniqueKey - 1, 20, Filters.ToArray(),
                lAsyncResult =>
                {
                    var result = service.EndGet(lAsyncResult);
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        (Action)delegate()
                        {
                            result.ForEach(p => Events.Add(p));
                        });
                }, null);
        }

        #region Event Polling
        private long LastIndex;
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

        public TimeSpan PollRate()
        {
            int result = 5;
            return TimeSpan.FromSeconds(result);
        }

        public void StartTimer()
        {
            PollActive = true;
            _DispatcherTimer = new DispatcherTimer();
            _DispatcherTimer.Interval = PollRate();
            _DispatcherTimer.Tick += new EventHandler(_DispatcherTimer_Tick);
            _DispatcherTimer.Start();
        }

        void _DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var service = XmlRpcProxy.Create<IFlexV1_Events_Async>(ServiceUrl);
            service.BeginGet(ApiKey, LastIndex, -1, 200, Filters.ToArray(),
                lAysncResult =>
                {
                    var result = service.EndGet(lAysncResult);
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        (Action)delegate()
                        {
                            if (result != null && result.Length > 0)
                            {
                                LastIndex = result.First().UniqueKey + 1;
                                int index = 0;
                                result.ForEach(p =>
                                {
                                    Events.Insert(index, p);
                                    index++;
                                });
                            }
                            if (PollActive)
                                _DispatcherTimer.Start();
                        });
                }, null);
        }
        #endregion

        private DelegateCommand _ShowMoreEventsCommand;
        public DelegateCommand ShowMoreEventsCommand
        {
            get
            {
                if (_ShowMoreEventsCommand == null)
                    _ShowMoreEventsCommand = new DelegateCommand(
                        p => ShowMoreEvents(),
                        p => Events.Count > 0)
                        .ListenOn(this, p => p.Events);
                return _ShowMoreEventsCommand;
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
