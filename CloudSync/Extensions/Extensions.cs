using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CloudSync.Extensions
{
	public static class LongExt
	{
		public static long AsKB(this long value)
		{
			return (long)Math.Round(value / 1024.0);
		}

		public static long AsMB(this long value)
		{
			return (long)Math.Round(value.AsKB() / 1024.0);
		}

		public static long AsGB(this long value)
		{
			return (long)Math.Round(value.AsMB() / 1024.0);
		}		
	}

	static class VisualExtensions
    {
        public static T FindVisualChild<T>(this DependencyObject obj)
                  where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static T FindVisualChildWithName<T>(this DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T && (child as FrameworkElement)?.Name == childName)
                    return (T)child;
                else
                {
                    T childOfChild = child.FindVisualChildWithName<T>(childName);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;            
        }
    }
}
