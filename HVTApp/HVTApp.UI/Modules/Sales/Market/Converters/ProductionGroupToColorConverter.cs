using System;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.Model.ProductionViewModelEntities;

namespace HVTApp.UI.Modules.Sales.Market.Converters
{
    public class ProductionGroupToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ProductionGroup productionGroup)
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