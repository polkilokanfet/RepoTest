using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Tce.Second.View.Converters
{
    [ValueConversion(typeof(TasksTceItem), typeof(string))]
    public class TceItemToRealizationDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TasksTceItem tasksTceItem)
            {
                if (tasksTceItem.Model.SalesUnits.Any() == false) return "unit removed by manager";
                return tasksTceItem.Model.SalesUnits.First().RealizationDateCalculated.ToShortDateString();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}