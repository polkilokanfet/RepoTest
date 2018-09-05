using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
            return String.IsNullOrEmpty(lookup.DisplayMember) ? lookup.ToString() : lookup.DisplayMember;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(IEnumerable<ILookupItem>), typeof(string))]
    public class LookupsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lookups = value as IEnumerable<ILookupItem>;
            if (lookups == null) return String.Empty;
            var builder = new StringBuilder();
            foreach (var lookup in lookups)
                builder.Append($"{lookup}; ");
            return builder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}