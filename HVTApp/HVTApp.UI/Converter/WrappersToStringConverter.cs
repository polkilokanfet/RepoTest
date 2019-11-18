using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(IWrapper<IBaseEntity>), typeof(string))]
    public class WrapperToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var wrapper = value as IWrapper<IBaseEntity>;
            return wrapper?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(IEnumerable<IWrapper<IBaseEntity>>), typeof(string))]
    public class WrappersToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var wrappers = value as IEnumerable<IWrapper<IBaseEntity>>;
            string result = string.Empty;
            foreach (var wrapper in wrappers)
            {
                result += wrapper.ToString() + ", ";
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}