using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;
using IFlexV1_Hardware_Sample;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public partial class DNASubController : IDNAStatus, IFlexV1_Hardware_Sample.IPopulation
    {
        public DNASubController()
        {
            Status = new DNAStatus();
            Populate = () => { RetrievePoints(); };

            ObservableCollection<object> children = new ObservableCollection<object>();
            children.Add(new DummyNode());
            Children = children;
        }

        private DNAPoints Points
        {
            get;
            set;
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

        private ObservableCollection<object> _Children;
        public ObservableCollection<object> Children
        {
            get
            {
                return _Children;
            }
            set
            {
                _Children = value;
                TriggerPropertyChanged("Children");
            }
        }

        public void RetrievePoints()
        {
            ObservableCollection<object> children = new ObservableCollection<object>();
            children.Add(new DummyLoadingObject());
            Children = children;

            var service = XmlRpcProxy.Create<IFlexV1_Hardware_Async>(MainWindow.ServiceUrl);
            service.BeginFindSubControllerPoints(MainWindow.ApiKey, Site, Controller, SubController,
                lAsyncResult =>
                {
                    try
                    {
                        var result = service.EndFindSubControllerPoints(lAsyncResult);
                        System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            (Action)delegate()
                            {
                                Points = result;

                                List<object> points = new List<object>();
                                points.AddRange(Points.InputPoints);
                                points.AddRange(Points.OutputPoints);
                                points.AddRange(Points.Readers);
                                Children = new ObservableCollection<object>(points);
                            });
                    }
                    catch (XmlRpcFaultException ex)
                    {

                    }
                }, null);
        }

        public bool NeedsToByPopulated()
        {
            return Points == null;
        }

        public Action Populate
        {
            get;
            set;
        }
    }
}
