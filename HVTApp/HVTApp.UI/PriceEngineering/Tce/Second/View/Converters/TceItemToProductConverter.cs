using System;
using System.Globalization;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Tce.Second.View.Converters
{
    [ValueConversion(typeof(TasksTceItem), typeof(string))]
    public class TceItemToProductConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TasksTceItem taskTceItem)
            {
                return taskTceItem.Model.ProductBlockEngineer.ToString();
            }

            return default(string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}