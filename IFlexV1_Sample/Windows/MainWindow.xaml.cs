using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using CookComputing.XmlRpc;
using OpenOptions.dnaFusion.Flex.V1;

namespace IFlexV1_Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void searchBox_OnSearch(object sender, SearchBoxEventArgs e)
        {
            IFlexV1_Async svr = XmlRpcProxy.Create<IFlexV1_Async>(ViewModel.Current.ServiceUrl);
            svr.BeginInstantSearch(ViewModel.ApiKey, e.Text, false, false,
                lAsyncResult =>
                {
                    try
                    {
                        OpenOptions.dnaFusion.Flex.V1.DNAPerson[] result = svr.EndInstantSearch(lAsyncResult);

                        Dispatcher.Invoke(DispatcherPriority.Background, (System.Windows.Forms.MethodInvoker)(
                            () =>
                            {
                                ViewModel context = LayoutRoot.DataContext as ViewModel;
                                if (context != null)
                                    context.PersonnelRecords = new ObservableCollection<DNAPerson>(
                                        result.Select(q => new DNAPerson(q)));
                            }));
                    }
                    catch (XmlRpcFaultException ex)
                    {
                    }
                }, null);
        }
    }
}
