using System;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.UI.Modules.Sales.Market.Items;

namespace HVTApp.UI.Modules.Sales.Market.Converters
{
    public class ProjectItemToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = value as ProjectItem;
            if (item != null)
            {
                if (item.IsLoosen) return Colors.LightPink;
                if (!item.DaysToStartProduction.HasValue) return Colors.LightGreen;

                if (item.DaysToStartProduction.Value <= 0) return Colors.DarkGoldenrod;
                if (item.DaysToStartProduction.Value < 30) return Colors.Goldenrod;
                if (item.DaysToStartProduction.Value < 60) return Colors.PaleGoldenrod;

                return Colors.White;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}