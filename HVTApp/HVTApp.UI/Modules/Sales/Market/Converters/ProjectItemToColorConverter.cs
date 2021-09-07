using System;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.UI.Modules.Sales.Market.Items;

namespace HVTApp.UI.Modules.Sales.Market.Converters
{
    [ValueConversion(typeof(ProjectItem), typeof(Color))]
    public class ProjectItemToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ProjectItem projectItem)
            {
                //если оборудование выиграно
                if (projectItem.IsWon)
                    return Colors.ForestGreen;

                //если оборудование проиграно
                if (projectItem.IsLoosen)
                    return Colors.Gray;

                //по количеству дней до запуска производства
                if (projectItem.DaysToStartProduction.HasValue)
                {
                    if (projectItem.DaysToStartProduction.Value <= 0) return Colors.OrangeRed;
                    if (projectItem.DaysToStartProduction.Value < 30) return Colors.Orange;
                }

                return Colors.Black;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}