using OpenOptions.dnaFusion.Flex.V1;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace IFlexV1_Events_Subscription_Sample
{
    public class AlarmToGridColor
    {
        static Dictionary<DNAAlarmStatus, EventColor> colors = new Dictionary<DNAAlarmStatus, EventColor>();

        static AlarmToGridColor()
        {
            PopluateList();
        }

        private static void PopluateList()
        {
            colors.Add(DNAAlarmStatus.Alarm, new EventColor(Colors.White, Color.FromArgb(255, 240, 120, 120)));
            colors.Add(DNAAlarmStatus.Trouble, new EventColor(Color.FromArgb(255, 0, 0, 128), Color.FromArgb(255, 254, 255, 128)));
            colors.Add(DNAAlarmStatus.Acknowledge, new EventColor(Colors.White, Color.FromArgb(255, 135, 206, 250)));
        }

        public EventColor this[int Index]
        {
            get
            {
                var value = (DNAAlarmStatus)Enum.ToObject(typeof(EventTypes), Index);
                if (colors.ContainsKey(value))
                    return colors[value];
                else
                    return new EventColor(Colors.Black);
            }
        }

        public EventColor this[DNAAlarmStatus Index]
        {
            get
            {
                if (colors.ContainsKey(Index))
                    return colors[Index];
                else
                    return new EventColor(Colors.Black);
            }
        }
    }
}
