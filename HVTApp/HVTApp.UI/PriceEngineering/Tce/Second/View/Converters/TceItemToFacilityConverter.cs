using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Tce.Second.View.Converters
{
    [ValueConversion(typeof(TasksTceItem), typeof(string))]
    public class TceItemToFacilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TasksTceItem taskTceItem)
            {
                if (taskTceItem.Model.SalesUnits.Any() == false) return "unit removed by manager";
                return taskTceItem.Model.SalesUnits.First().Facility.ToString();
            }

            return default(string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}