using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace DXReminder
{
    public class DayOfWeekConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            List<int> cd = value as List<int>;
            //if (cd != null)
            //    Debug.Print(value.ToString());
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var lstIn = value as List<object>;
            if (lstIn == null)
                return null;
            return lstIn.Cast<int>().ToList();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    public class TimeListConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var lstIn = value as List<object>;
            if (lstIn == null)
                return null;
            return lstIn.Cast<DateTime>().ToList();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
