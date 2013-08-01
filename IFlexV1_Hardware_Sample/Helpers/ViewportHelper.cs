using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;

namespace IFlexV1_Hardware_Sample
{
    public static class ViewportHelper
    {
        public static bool IsInViewport(Control item, ScrollViewer scrollViewer)
        {
            if (item == null)
                return false;

            ScrollContentPresenter scrollContentPresenter = (ScrollContentPresenter)scrollViewer.Template.FindName("PART_ScrollContentPresenter", scrollViewer);
            MethodInfo isInViewportMethod = scrollViewer.GetType().GetMethod("IsInViewport", BindingFlags.NonPublic | BindingFlags.Instance);

            return (bool)isInViewportMethod.Invoke(scrollViewer, new object[] { scrollContentPresenter, item });
        }
    }
}
