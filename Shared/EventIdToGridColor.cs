using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public class EventIdToGridColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isBackground = false;
            if (parameter != null)
                isBackground = parameter.ToString() == "Background";

            if (value != null)
            {
                int Index = System.Convert.ToInt32(value);

                var description = ViewModel.Current.EventDescriptions.FirstOrDefault(p => p.Index == Index);
                if (description != null)
                {
                    EventGridColor color = new EventGridColor();
                    if (isBackground)
                        return color[description.GroupIndex].Background;
                    return color[description.GroupIndex].Foreground;
                }
            }

            return new SolidColorBrush(isBackground ? Colors.White : Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
