using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IFlexV1_Hardware_Sample
{
    /// <summary>
    /// Interaction logic for CircularProgressBar.xaml
    /// </summary>
    public partial class CircularProgressBar : UserControl
    {
        public CircularProgressBar()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                base.Background = Brushes.Transparent;
        }

        public static readonly DependencyProperty ProgressBarForegroundProperty = DependencyProperty.Register("ProgressBarForeground",
            typeof(Brush), typeof(CircularProgressBar), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(ProgressBarForegroundChanged)));

        private static void ProgressBarForegroundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CircularProgressBar bar = sender as CircularProgressBar;
            if ((bar != null) && (bar.ParentCanvas != null))
                foreach (UIElement element in bar.ParentCanvas.Children)
                    element.SetValue(Shape.FillProperty, e.NewValue);
        }

        public Brush ProgressBarForeground
        {
            get
            {
                return (Brush)base.GetValue(ProgressBarForegroundProperty);
            }
            set
            {
                base.SetValue(ProgressBarForegroundProperty, value);
            }
        }
    }
}
