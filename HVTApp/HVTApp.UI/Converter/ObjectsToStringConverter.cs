using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(IEnumerable<object>), typeof(string))]
    public class ObjectsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var objects = value as IEnumerable<object>;
            return objects == null 
                ? string.Empty 
                : objects.ConvertToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public static class ObjectsToStringConverterExt
    {
        public static string ConvertToString(this IEnumerable<object> objects1)
        {
            if(objects1 == null) throw new ArgumentNullException(nameof(objects1));

            var objects = objects1.ToList();
            var builder = new StringBuilder();
            objects.ForEach(x => builder.Append("; ").Append($"{x}"));
            return builder.Length < 2
                ? string.Empty
                : builder.Remove(0, 2).ToString();
        }
    }
}