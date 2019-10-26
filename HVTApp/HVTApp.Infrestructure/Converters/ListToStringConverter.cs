using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Infrastructure.Converters
{
    [ValueConversion(typeof(IEnumerable<object>), typeof(string))]
    public class ListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var objects = value as IEnumerable<object>;
            if (objects == null) return string.Empty;
            var builder = new StringBuilder();
            objects.ForEach(x => builder.Append("; ").Append($"{x.ToString()}"));
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