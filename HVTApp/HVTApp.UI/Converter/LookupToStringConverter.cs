using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(ILookupItem), typeof(string))]
    public class LookupToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ILookupItem lookup)) return string.Empty;
            return string.IsNullOrEmpty(lookup.DisplayMember) ? lookup.ToString() : lookup.DisplayMember;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}