using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using OpenOptions.dnaFusion.Flex.V1;

namespace IFlexV1_Hardware_Sample
{
    public class AtFaultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DNAFault)
                return (DNAFault)value != DNAFault.Inactive;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
