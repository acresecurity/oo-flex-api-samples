using IFlexV1_Camera_Sample;
using System;
using System.Collections.Specialized;
using System.Net;

namespace OpenOptions.dnaFusion.Flex.V1.Cameras
{
    public partial class DNACamera : IFlexAPI
    {
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

        private bool loadingThumbnail;
        private byte[] _Thumbnail;
        public byte[] Thumbnail
        {
            get
            {
                if (_Thumbnail == null && !loadingThumbnail)
                    LoadThumbnail();
                return _Thumbnail;
            }
            set
            {
                _Thumbnail = value;
                TriggerPropertyChanged("Thumbnail");
            }
        }

        private DelegateCommand _MoveCameraCommand;
        public DelegateCommand MoveCameraCommand
        {
            get
            {
                if (_MoveCameraCommand == null)
                {
                    _MoveCameraCommand = new DelegateCommand(
                        p =>
                        {
                            if (p is PTZMovement)
                                MoveCamera((PTZMovement)p);
                        },
                        p =>
                        {
                            return PTZControl && p is PTZMovement;
                        });
                }
                return _MoveCameraCommand;
            }
        }

        private void MoveCamera(PTZMovement movement)
        {
            var service = new FlexV1_Camera() { Url = ServiceUrl };
            service.BeginMoveCamera(ApiKey, Index, movement,
                lAsyncResult =>
                {
                    service.EndMoveCamera(lAsyncResult);
                }, null);
        }

        private void LoadThumbnail()
        {
            loadingThumbnail = true;
            var query = new NameValueCollection();
            query.Add("ApiKey", ApiKey);
            query.Add("Camera", Index.ToString());
            query.Add("Single", "1");

            var url = new UriBuilder(ServiceUrl)
            {
                Path = "/FlexV1_MJpeg",
                Query = query.ToQueryString(),
            };

            using (var wc = new WebClient() { Proxy = null })
            {
                wc.Headers.Add("user-agent", "OpenOptions.dnaFusion.Flex.V1.Cameras");
                wc.DownloadDataCompleted += (s, e) =>
                    {
                        if (!e.Cancelled && e.Error == null)
                            Thumbnail = e.Result;
                        loadingThumbnail = false;
                    };
                wc.DownloadDataAsync(url.Uri);
            }
        }
    }
}
