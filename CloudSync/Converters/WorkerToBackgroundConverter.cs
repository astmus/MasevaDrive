using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CloudSync.Converters
{
    [ValueConversion(typeof(int), typeof(Brush))]
    class WorkerToBackgroundConverter : IValueConverter
    {
		private static SolidColorBrush FailColorBrush = new SolidColorBrush(Colors.OrangeRed);
		private static SolidColorBrush DefaultColorBrush = new SolidColorBrush(Colors.Transparent);
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value) < 0 ? FailColorBrush : DefaultColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
