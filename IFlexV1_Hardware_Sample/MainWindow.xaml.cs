using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using CookComputing.XmlRpc;
using OpenOptions.dnaFusion.Flex.V1;

namespace IFlexV1_Hardware_Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanging, INotifyPropertyChanged
    {
        public static string ApiKey
        {
            get
            {
                return "a214ce66-ff77-4aee-8964-406f9817758e";
            }
        }

        public static string ServiceUrl
        {
            get
            {
                return @"https://flex.ooinc.com/xmlrpc";
                //return string.Format(@"http://{0}:8099/xmlrpc", System.Environment.MachineName); // So we can use fiddler
            }
        }

        private List<ControllerViewModel> _ViewModel;
        public List<ControllerViewModel> ViewModel
        {
            get
            {
                return _ViewModel;
            }
            private set
            {
                RaisePropertyChanging("ViewModel");
                _ViewModel = value;
                RaisePropertyChanged("ViewModel");
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (s, e) =>
                {
                    HardwareTreeView.AddHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(ItemExpanded));
                    HardwareTreeView.SetBinding(TreeView.ItemsSourceProperty, new Binding("ViewModel")
                    {
                        Source = this,
                    });

                    var service = XmlRpcProxy.Create<IFlexV1_Hardware_Async>(ServiceUrl);
                    service.BeginFindControllers(ApiKey, 1,
                        lAsyncResult =>
                        {
                            try
                            {
                                List<ControllerViewModel> result = new List<ControllerViewModel>();
                                service.EndFindControllers(lAsyncResult)
                                    .ForEach(p => result.Add(new ControllerViewModel(p)));
                                ViewModel = result;

                                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                                (Action)delegate()
                                {
                                    StartStatusTimer();
                                });
                            }
                            catch (XmlRpcFaultException ex)
                            {

                            }
                        }, null);
                };
        }

        private void ItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item != null)
            {
                IPopulation populate = item.DataContext as IPopulation;
                if (populate != null && populate.NeedsToByPopulated())
                    populate.Populate();
            }
        }

        private void VisibleItems(ItemCollection items, List<IDNAStatus> visibleItems, ScrollViewer scrollViewer)
        {
            foreach(var i in items)
            {
                TreeViewItem container = HardwareTreeView.ContainerFromItem(i);
                if (ViewportHelper.IsInViewport(container, scrollViewer) && container.DataContext != null && !(container.DataContext is DummyLoadingObject))
                {
                    if (container.DataContext is IDNAStatus)
                        visibleItems.Add(container.DataContext as IDNAStatus);
                    if (container.HasItems && container.IsExpanded)
                        VisibleItems(container.Items, visibleItems, scrollViewer);
                }
            }
        }

        public List<IDNAStatus> GetVisibleItems()
        {
            List<IDNAStatus> result = new List<IDNAStatus>();
            VisibleItems(HardwareTreeView.Items, result, VisualTreeHelper.GetVisualChild<ScrollViewer, ItemsControl>(HardwareTreeView));
            return result;
        }

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

        public void UpdateStatus(DNAStatus[] AStatus, List<IDNAStatus> visibleItems)
        {
            foreach (DNAStatus status in AStatus)
            {
                IDNAStatus dnaStatus = visibleItems.First(d => d.PackedAddress == status.Address);
                if (dnaStatus != null)
                {
                    dnaStatus.Status = status;
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
            _DispatcherTimer.Tick +=new EventHandler(_DispatcherTimer_Tick);
            _DispatcherTimer.Start();
        }

        void _DispatcherTimer_Tick(object sender, EventArgs e)
        {
            List<IDNAStatus> visibleItems = GetVisibleItems();
            if (visibleItems.Count == 0)
            {
                if (PollActive)
                    _DispatcherTimer.Start();
                return;
            }

            long[] hardware = visibleItems.Select(
                p => Convert.ToInt64(p.PackedAddress))
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
                            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            (Action)delegate()
                            {
                                UpdateStatus(status, visibleItems);
                                if (PollActive)
                                    _DispatcherTimer.Start();
                            });
                        }
                        catch (System.Exception ex)
                        {
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
    }
}
