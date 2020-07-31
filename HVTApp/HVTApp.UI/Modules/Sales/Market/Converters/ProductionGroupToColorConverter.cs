using System;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.UI.Modules.Sales.ViewModels;

namespace HVTApp.UI.Modules.Sales.Market.Converters
{
    public class ProductionGroupToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var productionGroup = value as ProductionGroup;
            if (productionGroup != null)
            {
                if (productionGroup.DifContract > 0 && productionGroup.DifExpected > 0)
                    return Colors.HotPink;

                if (productionGroup.DifContract > 0 || productionGroup.DifExpected > 0)
                    return Colors.Gold;

                return Colors.LightGreen;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}