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
            var lookup = value as ILookupItem;
            if (lookup == null) return String.Empty;
            return String.IsNullOrEmpty(lookup.DisplayMember) ? lookup.ToString() : ((ILookupItem) value).DisplayMember;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}