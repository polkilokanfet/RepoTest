using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.UI.PriceEngineering.Tce.Unit;

namespace HVTApp.UI.PriceEngineering.Tce.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskTceItem), typeof(int))]
    public class PriceEngineeringTaskTceItemToAmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskTceItem priceEngineeringTaskTceItem)
            {
                return priceEngineeringTaskTceItem
                    .TceStructureCostVersions
                    .First()
                    .ParentPriceEngineeringTask
                    .SalesUnits
                    .Count;
            }

            return default(int);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}