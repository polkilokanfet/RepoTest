using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TaskViewModel), typeof(string))]
    public class ToFacilityOwnerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModel taskViewModel)
            {
                if (taskViewModel.Model.SalesUnits.Any() == false) return "unit removed by manager";
                return taskViewModel.Model.SalesUnits.First().Facility.OwnerCompany.ToString();
            }

            return default(string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}