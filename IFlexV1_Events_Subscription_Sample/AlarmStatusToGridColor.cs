using OpenOptions.dnaFusion.Flex.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace IFlexV1_Events_Subscription_Sample
{
    public class AlarmStatusToGridColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isBackground = false;
            if (parameter != null)
                isBackground = parameter.ToString() == "Background";

            if (value != null)
            {
                var status = (DNAAlarmStatus)value;

                var color = new AlarmToGridColor();
                if (isBackground)
                    return color[status].Background;
                return color[status].Foreground;
            }

            return new SolidColorBrush(isBackground ? Colors.White : Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
