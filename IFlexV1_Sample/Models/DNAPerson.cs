using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using CookComputing.XmlRpc;
using OpenOptions.dnaFusion.Flex.V1;

namespace IFlexV1_Sample
{
    public class DNAPerson : INotifyPropertyChanging, INotifyPropertyChanged
    {
        static DNAPerson()
        {
            LoadMissingImage();
        }

        public DNAPerson()
        {
            BasePerson = new OpenOptions.dnaFusion.Flex.V1.DNAPerson();
        }

        public DNAPerson(OpenOptions.dnaFusion.Flex.V1.DNAPerson dnaPersion)
        {
            BasePerson = dnaPersion;
        }

        public OpenOptions.dnaFusion.Flex.V1.DNAPerson BasePerson;

        #region BaseDnaStruct
        public DateTime LastModified
        {
            get
            {
                return BasePerson.LastModified;
            }
            set
            {
                RaisePropertyChanging("LastModified");
                BasePerson.LastModified = value;
                RaisePropertyChanged("LastModified");
            }
        }

        public int UniqueKey
        {
            get
            {
                return BasePerson.UniqueKey;
            }
            set
            {
                RaisePropertyChanging("UniqueKey");
                BasePerson.UniqueKey = value;
                RaisePropertyChanged("UniqueKey");
            }
        }
        #endregion

        #region DNAPerson Members
        public string Email
        {
            get
            {
                return BasePerson.Email;
            }
            set
            {
                RaisePropertyChanging("Email");
                BasePerson.Email = value;
                RaisePropertyChanged("Email");
            }
        }

        public int EmployeeID
        {
            get
            {
                return BasePerson.EmployeeID;
            }
            set
            {
                RaisePropertyChanging("EmployeeID");
                BasePerson.EmployeeID = value;
                RaisePropertyChanged("EmployeeID");
            }
        }

        public string FirstName
        {
            get
            {
                return BasePerson.FirstName;
            }
            set
            {
                RaisePropertyChanging("FirstName");
                BasePerson.FirstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return BasePerson.LastName;
            }
            set
            {
                RaisePropertyChanging("LastName");
                BasePerson.LastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        public string MiddleName
        {
            get
            {
                return BasePerson.MiddleName;
            }
            set
            {
                RaisePropertyChanging("MiddleName");
                BasePerson.MiddleName = value;
                RaisePropertyChanged("MiddleName");
            }
        }

        public int PersonnelType
        {
            get
            {
                return BasePerson.PersonnelType;
            }
            set
            {
                RaisePropertyChanging("PersonnelType");
                BasePerson.PersonnelType = value;
                RaisePropertyChanged("PersonnelType");
            }
        }

        public Byte[] Photo
        {
            get
            {
                return BasePerson.Photo;
            }
            set
            {
                RaisePropertyChanging("Photo");
                BasePerson.Photo = value;
                RaisePropertyChanged("Photo");
            }
        }

        public string Site
        {
            get
            {
                return BasePerson.Site;
            }
            set
            {
                RaisePropertyChanging("Site");
                BasePerson.Site = value;
                RaisePropertyChanged("Site");
            }
        }
        #endregion

        #region Methods and Properties that have nothing to do with the service but help the test app
        private bool RetrievingPhoto = false;
        private bool RetrievingThumbnail = false;

        private Byte[] _Thumbnail;
        public Byte[] Thumbnail
        {
            get
            {
                if (_Thumbnail == null && !RetrievingThumbnail)
                {
                    if (Photo == null && UniqueKey > -1)
                    {
                        RetrievingThumbnail = true;
                        IFlexV1_Async svr = XmlRpcProxy.Create<IFlexV1_Async>(ViewModel.Current.ServiceUrl);
                        svr.BeginPersonnelPhoto(ViewModel.ApiKey, UniqueKey, true,
                            lAsyncResult =>
                            {
                                Byte[] result = svr.EndPersonnelPhoto(lAsyncResult);
                                Thumbnail = result == null ? _missingImage : result;
                                RetrievingThumbnail = false;
                            }, null);
                    }
                    else
                    {
                        Thumbnail = ResizePhotoToThumbnail(Photo);
                    }
                }
                return _Thumbnail;
            }
            private set
            {
                RaisePropertyChanging("Thumbnail");
                _Thumbnail = value;
                RaisePropertyChanged("Thumbnail");
            }
        }

        private Byte[] ResizePhotoToThumbnail(Byte [] photo)
        {
            using (MemoryStream result = new MemoryStream())
            {
                using (MemoryStream source = new MemoryStream(photo))
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    BitmapFrame frame = BitmapFrame.Create(source).Resize(50, 50);
                    encoder.Frames.Add(frame);
                    encoder.Save(result);
                    result.Seek(0, SeekOrigin.Begin);
                    return result.ToArray();
                }
            }
        }

        private static Byte[] _missingImage;
        private static void LoadMissingImage()
        {
            if (_missingImage == null)
            {
                Uri imageUri;
                if (Uri.TryCreate("/IFlexV1_Sample;component/Assets/Photo Not Available.png", UriKind.RelativeOrAbsolute, out imageUri))
                {
                    using (Stream stream = Application.GetResourceStream(imageUri).Stream)
                    {
                        _missingImage = stream.ToArray();
                    }
                }
            }
        }

        private Byte[] _WrappedPhoto;
        public Byte[] WrappedPhoto
        {
            get
            {
                if (BasePerson.Photo != null)
                    return BasePerson.Photo;

                if (_WrappedPhoto == null && !RetrievingPhoto && UniqueKey > -1)
                {
                    RetrievingPhoto = true;
                    IFlexV1_Async svr = XmlRpcProxy.Create<IFlexV1_Async>(ViewModel.Current.ServiceUrl);
                    svr.BeginPersonnelPhoto(ViewModel.ApiKey, UniqueKey, false,
                        lAsyncResult =>
                        {
                            Byte[] result = svr.EndPersonnelPhoto(lAsyncResult);
                            if (result == null)
                                WrappedPhoto = _missingImage;
                            else
                            {
                                Photo = result;
                                WrappedPhoto = null;                                
                            }
                            RetrievingPhoto = false;
                        }, null);
                }
                return _WrappedPhoto;
            }
            private set
            {
                RaisePropertyChanging("WrappedPhoto");
                _WrappedPhoto = value;
                RaisePropertyChanged("WrappedPhoto");
            }
        }

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
                LastModified = DateTime.Now;
                RaisePropertyChanged("EditState");
            }
        }

        public void ChangeImage()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog()
            {
                Title = "Select JPEG Image",
                DefaultExt = ".jpg",
                Filter = "JPEG Image (.jpg)|*.jpg",
                CheckFileExists = true
            };
            if (dlg.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(dlg.FileName, FileMode.Open))
                {
                    Photo = stream.ToArray();
                    WrappedPhoto = null;
                    Thumbnail = null;
                }
            }
        }

        private DelegateCommand _ChangeImageCommand;
        public DelegateCommand ChangeImageCommand
        {
            get
            {
                if (_ChangeImageCommand == null)
                    _ChangeImageCommand = new DelegateCommand(
                        p => ChangeImage());
                return _ChangeImageCommand;
            }
        }

        public void ViewCredentials()
        {
            CredentialsWindow window = new CredentialsWindow()
            {
                Owner = Application.Current.MainWindow,
            };
            window.DataContext = new CredentialsViewModel(
                this,
                new DelegateCommand(
                    p => 
                        {
                            window.Close();
                        }),
                new DelegateCommand(
                    p => 
                        {
                            window.Close();
                        }));
            window.ShowDialog();
        }

        private DelegateCommand _ViewCredentialsCommand;
        public DelegateCommand ViewCredentialsCommand
        {
            get
            {
                if (_ViewCredentialsCommand == null)
                    _ViewCredentialsCommand = new DelegateCommand(
                        p => ViewCredentials());
                return _ViewCredentialsCommand;
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

                if (propertyName != "Thumbnail" && propertyName != "WrappedPhoto" && propertyName != "EditState" && !RetrievingPhoto && !RetrievingThumbnail)
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
