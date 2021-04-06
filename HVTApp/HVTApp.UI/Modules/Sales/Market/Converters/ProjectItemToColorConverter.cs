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
            if (value is ProjectItem item)
            {
                if (item.IsLoosen) return Colors.LightPink;

                //если оборудование запущено в производство
                if (item.DaysToStartProduction.HasValue == false)
                {
                    if (item.IsDone)
                    {
                        return Colors.DarkSeaGreen;
                    }
                    return Colors.LightGreen;
                }

                if (item.DaysToStartProduction.Value <= 0) return Colors.Orange;
                if (item.DaysToStartProduction.Value < 30) return Colors.Yellow;
                if (item.DaysToStartProduction.Value < 60) return Colors.LightYellow;

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