using CookComputing.XmlRpc;
using OpenOptions.dnaFusion.Flex.V1.Cameras;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace IFlexV1_Camera_Sample
{
    public class ViewModel : INotifyPropertyChanging, INotifyPropertyChanged, IFlexAPI
    {

        public ViewModel()
        {
            ApiKey = "a214ce66-ff77-4aee-8964-406f9817758e";
            //ServiceUrl = string.Format(@"http://{0}/xmlrpc", System.Environment.MachineName); // So we can use fiddler
            ServiceUrl = @"https://flex.ooinc.com/xmlrpc";

            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                LoadCameras();

                PropertyChanging += ViewModel_PropertyChanging;
                PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private MJPEGSourceAsync videoSource;

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedCamera")
            {
                var query = new NameValueCollection();
                query.Add("ApiKey", ApiKey);
                query.Add("Camera", SelectedCamera.Index.ToString());

                var url = new UriBuilder(ServiceUrl)
                {
                    Path = "/FlexV1_MJpeg",
                    Query = query.ToQueryString(),
                };

                videoSource = new MJPEGSourceAsync();
                videoSource.NewFrame += (s, frame) =>
                {
                    CurrentFrame = frame;
                };
                videoSource.VideoSource = url.ToString();
                videoSource.Start();
            }
        }

        void ViewModel_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == "SelectedCamera" && videoSource != null)
            {
                videoSource.Stop();
                CurrentFrame = null;
            }
        }

        public string ApiKey
        {
            get;
            set;
        }

        public string ServiceUrl
        {
            get;
            set;
        }

        private ObservableCollection<DNACamera> _Cameras;
        public ObservableCollection<DNACamera> Cameras
        {
            get
            {
                return _Cameras;
            }
            set
            {
                RaisePropertyChanging("Cameras");
                _Cameras = value;
                RaisePropertyChanged("Cameras");
            }
        }

        private DNACamera _SelectedCamera;
        public DNACamera SelectedCamera
        {
            get
            {
                return _SelectedCamera;
            }
            set
            {
                RaisePropertyChanging("SelectedCamera");
                _SelectedCamera = value;
                RaisePropertyChanged("SelectedCamera");
            }
        }

        private Byte[] _CurrentFrame;
        public Byte[] CurrentFrame
        {
            get
            {
                return _CurrentFrame;
            }
            set
            {
                RaisePropertyChanging("CurrentFrame");
                _CurrentFrame = value;
                RaisePropertyChanged("CurrentFrame");
            }
        }

        public void LoadCameras()
        {
            var service= XmlRpcProxy.Create<IFlexV1_Camera_Async>(ServiceUrl);
            service.BeginFindCameras(ApiKey,
                lAsyncResult =>
                {
                    try
                    {
                        var result = service.EndFindCameras(lAsyncResult)
                            .Select(p =>
                            {
                                p.ServiceUrl = ServiceUrl;
                                p.ApiKey = ApiKey;
                                return p;
                            });
                        Cameras = new ObservableCollection<DNACamera>(result);
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
    }
}
