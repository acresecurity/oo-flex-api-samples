using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public class EventColor
    {
        public Brush Foreground { get; set; }
        public Brush Background { get; set; }

        public EventColor()
        {

        }

        public EventColor(Color foreground)
        {
            Foreground = new SolidColorBrush(foreground);
            Background = new SolidColorBrush(Colors.White);
        }

        public EventColor(Color foreground, Color background)
        {
            Foreground = new SolidColorBrush(foreground);
            Background = new SolidColorBrush(background);
        }
    }
}
