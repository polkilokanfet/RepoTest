using System;
using System.Globalization;
using System.Windows.Data;

namespace HVTApp.Infrastructure.Converters
{
    public abstract class ValueConverterBase : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Обратная конвертация не поддерживается.");
        }
    }
}