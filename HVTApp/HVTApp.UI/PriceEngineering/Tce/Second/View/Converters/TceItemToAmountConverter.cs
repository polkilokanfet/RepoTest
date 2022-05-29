using System;
using System.Globalization;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Tce.Second.View.Converters
{
    [ValueConversion(typeof(TasksTceItem), typeof(int))]
    public class TceItemToAmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TasksTceItem tasksTceItem)
            {
                return tasksTceItem.Model.SalesUnits.Count;
            }

            return default(int);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}