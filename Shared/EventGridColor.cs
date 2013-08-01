using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace OpenOptions.dnaFusion.Flex.V1
{
    public class EventGridColor
    {
        static Dictionary<EventTypes, EventColor> colors = new Dictionary<EventTypes, EventColor>();

        static EventGridColor()
        {
            PopluateList();
        }

        private static void PopluateList()
        {
            colors.Add(EventTypes.Door, new EventColor(Colors.Black));
            colors.Add(EventTypes.Arm, new EventColor(Colors.Purple));
            colors.Add(EventTypes.Disarm, new EventColor(Color.FromArgb(255, 0, 128, 128)));
            colors.Add(EventTypes.Secure, new EventColor(Color.FromArgb(255, 56, 116, 175)));
            colors.Add(EventTypes.Alarms, new EventColor(Colors.Red));
            colors.Add(EventTypes.Comm, new EventColor(Colors.Green));
            colors.Add(EventTypes.Areas, new EventColor(Color.FromArgb(255, 56, 116, 175)));
            colors.Add(EventTypes.Mpg, new EventColor(Color.FromArgb(255, 0, 105, 128)));
            colors.Add(EventTypes.Time, new EventColor(Colors.Black));
            colors.Add(EventTypes.Asset, new EventColor(Colors.Black));
            colors.Add(EventTypes.TransOk, new EventColor(Colors.Blue));
            colors.Add(EventTypes.Trans, new EventColor(Colors.Red));
            colors.Add(EventTypes.Mode, new EventColor(Color.FromArgb(255, 166, 202, 240)));
            colors.Add(EventTypes.User, new EventColor(Colors.Purple));
            colors.Add(EventTypes.Other, new EventColor(Colors.Black));
        }

        public EventColor this[int Index]
        {
            get
            {
                var value = (EventTypes)Enum.ToObject(typeof(EventTypes), Index);
                if (colors.ContainsKey(value))
                    return colors[value];
                else
                    return new EventColor(Colors.Black);
            }
        }

        public EventColor this[EventTypes Index]
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
