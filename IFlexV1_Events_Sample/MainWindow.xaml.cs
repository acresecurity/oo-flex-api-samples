﻿using OpenOptions.dnaFusion.Flex.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IFlexV1_Events_Sample
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

        private void FilterListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = LayoutRoot.DataContext as ViewModel;
            if (viewModel != null)
            {
                viewModel.Filters = FilterListBox.SelectedItems.OfType<DNAEventFilter>().ToList();
                viewModel.GetRecentEvents();
            }
        }
    }
}
