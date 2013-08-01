using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using OpenOptions.dnaFusion.Flex;
using OpenOptions.dnaFusion.Flex.V1;
using System;
using CookComputing.XmlRpc;

namespace IFlexV1_Sample
{
    /// <summary>
    /// Interaction logic for SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl
    {
        private string CurrentSeletion = null;

        public SearchBox()
        {
            InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                acbPersonnel.KeyUp += (s, e) =>
                    {
                        if (e.Key == System.Windows.Input.Key.Enter && CurrentSeletion != acbPersonnel.Text)
                            if (OnSearch != null)
                            {
                                CurrentSeletion = acbPersonnel.Text;
                                OnSearch(this, new SearchBoxEventArgs(CurrentSeletion));
                            }
                    };

                acbPersonnel.DropDownClosed += (s, e) =>
                    {
                        if (acbPersonnel.SelectedItem != null && CurrentSeletion != acbPersonnel.Text)
                        {
                            if (OnSearch != null)
                            {
                                CurrentSeletion = acbPersonnel.Text;
                                OnSearch(this, new SearchBoxEventArgs(CurrentSeletion));
                            }
                        }
                    };

                acbPersonnel.TextFilter += (search, item) =>
                    {
                        InstantSearch instant = new InstantSearch(search);
                        if (instant.HasKeyword)
                            if (instant.HasMember)
                                return item.ToUpper().Contains(instant.Member.Last().ToUpper());
                            else
                                return item.ToUpper().Contains(instant.Criteria.Last().ToUpper());

                        if (instant.Criteria.Length == 1)
                            return item.ToUpper().Contains(search.ToUpper());
                        else
                            return item.ToUpper().Contains(instant.Criteria.Last().ToUpper());
                    };

                acbPersonnel.Populating += (s, e) =>
                    {
                        IFlexV1_Async svr = XmlRpcProxy.Create<IFlexV1_Async>(ViewModel.Current.ServiceUrl);
                        svr.BeginInstantSearchAutoComplete(ViewModel.ApiKey, e.Parameter,
                            lAsyncResult =>
                            {
                                try
                                {
                                    string[] result = svr.EndInstantSearchAutoComplete(lAsyncResult);

                                    Dispatcher.Invoke(DispatcherPriority.Background, (System.Windows.Forms.MethodInvoker)(
                                        () =>
                                        {
                                            acbPersonnel.ItemsSource = result.AsQueryable();
                                            acbPersonnel.PopulateComplete();
                                        }));
                                }
                                catch (XmlRpcFaultException ex)
                                {

                                }
                            }, null);
                    };
            }
        }

        public event SearchBoxEventHandler OnSearch;

        public void ManualSearch(string SearchText)
        {
            if (OnSearch != null)
            {
                acbPersonnel.Text = SearchText;
                CurrentSeletion = SearchText;
                OnSearch(this, new SearchBoxEventArgs(CurrentSeletion));
            }
        }
    }
}
