using OpenOptions.dnaFusion.Flex.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace IFlexV1_Events_Subscription_Sample
{
    public class AlarmStatusToDNAAlarmsGridText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var status = DNAAlarmStatus.Unknown;
            if (value is DNAFusionAlarm)
                status = ((DNAFusionAlarm)value).Status;
            else
                if (value is DNAAlarmStatus)
                    status = (DNAAlarmStatus)value;

            switch (status)
            {
                case DNAAlarmStatus.Acknowledge:
                    return "ACK";
                case DNAAlarmStatus.Alarm:
                    return "Alarm";
                case DNAAlarmStatus.Clear:
                    return "Clear";
                case DNAAlarmStatus.Normal:
                    return "Normal";
                case DNAAlarmStatus.Trouble:
                    return "RTN";
                case DNAAlarmStatus.Unknown:
                    return "Unknown";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
