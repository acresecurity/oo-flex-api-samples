using OpenOptions.dnaFusion.Flex.V1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IFlexV1_Events_Subscription_Sample
{
    public class EventDescription : IMultiValueConverter
    {
        #region IValueConverter Members
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (values != null && values.Length == 2 && values[0] is int && values[1] is IEnumerable)
                {
                    int index = System.Convert.ToInt32(values[0]);
                    var eventDescriptions = values[1] as List<DNAEventDescription>;
                    if (eventDescriptions != null)
                    {
                        var item = eventDescriptions.FirstOrDefault(q => q.Index == index);
                        if (item != null)
                            return item.Description;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
