using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Tce.Second.View.Converters
{
    [ValueConversion(typeof(TasksTceItem), typeof(string))]
    public class TceItemToDeliveryTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TasksTceItem tasksTceItem)
            {
                if (tasksTceItem.Model.SalesUnits.Any() == false) return 0;

                var deliveryCost = tasksTceItem.Model.SalesUnits.First().CostDelivery;
                if (deliveryCost > 0)
                {
                    return "Доставка";
                }

                return "Самовывоз";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}