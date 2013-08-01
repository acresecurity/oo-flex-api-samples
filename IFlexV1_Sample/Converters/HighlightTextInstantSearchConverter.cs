using System;
using System.Linq;
using System.Windows.Data;
using OpenOptions.dnaFusion.Flex.V1;

namespace IFlexV1_Sample
{
    public class HighlightTextInstantSearchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                string item = (string)value;
                InstantSearch instant = new InstantSearch(item);
                if (instant.HasKeyword && instant.HasMember)
                    return instant.Member.Last();
                return instant.Criteria.Last();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
