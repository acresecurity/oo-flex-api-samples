using OpenOptions.dnaFusion.Flex.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IFlexV1_Events_Sample
{
    public class EventDescription : IMultiValueConverter
    {
        #region IValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null && values.Length == 2 && values[0] is int)
            {
                int Index = System.Convert.ToInt32(values[0]);
                var eventDescriptions = (List<DNAEventDescription>)values[1];

                var item = eventDescriptions.FirstOrDefault(q => q.Index == Index);
                if (item != null)
                    return item.Description;
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
