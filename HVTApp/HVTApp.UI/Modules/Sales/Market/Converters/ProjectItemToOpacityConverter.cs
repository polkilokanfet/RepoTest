using System;
using System.Windows.Data;
using HVTApp.UI.Modules.Sales.Market.Items;

namespace HVTApp.UI.Modules.Sales.Market.Converters
{
    [ValueConversion(typeof(ProjectItem), typeof(double))]
    public class ProjectItemToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ProjectItem projectItem)
            {
                if (projectItem.IsLoosen)
                    return 0.5;

                if (projectItem.IsDone)
                    return 0.6;

                return 1.0;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}