using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using HVTApp.Infrastructure;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(IEnumerable<ILookupItem>), typeof(string))]
    public class LookupsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lookups = value as IEnumerable<ILookupItem>;
            if (lookups == null) return string.Empty;
            var builder = new StringBuilder();
            lookups.ForEach(x => builder.Append("; ").Append($"{x}"));
            return builder.Length < 2
                ? string.Empty
                : builder.Remove(0, 2).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}