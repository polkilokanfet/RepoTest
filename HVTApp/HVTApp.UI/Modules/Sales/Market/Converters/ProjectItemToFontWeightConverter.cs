using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.UI.Modules.Sales.Market.Items;

namespace HVTApp.UI.Modules.Sales.Market.Converters
{
    [ValueConversion(typeof(ProjectItem), typeof(FontWeight))]
    public class ProjectItemToFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ProjectItem projectItem)
            {
                if (projectItem.InWork)
                    return FontWeights.Bold;

                return Binding.DoNothing;
            }

            return Binding.DoNothing;
            //throw new ArgumentException($"Передан в конвертер не {typeof(ProjectItem)}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}