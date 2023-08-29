using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.UI.PriceEngineering.Items;

namespace HVTApp.UI.PriceEngineering.View.Converters
{
    public class ToSignalStartProductionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTasksListItemPlanMaker tasks)
            {
                return tasks.ChildPriceEngineeringTasks
                    .SelectMany(x => x.Entity.SalesUnits)
                    .Select(x => x.SignalToStartProduction)
                    .Where(x => x != null)
                    .OrderBy(x => x)
                    .LastOrDefault();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}