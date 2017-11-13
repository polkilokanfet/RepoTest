using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(ILookupItem), typeof(string))]
    public class LookupToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "-";
            return ((ILookupItem) value).DisplayMember;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}