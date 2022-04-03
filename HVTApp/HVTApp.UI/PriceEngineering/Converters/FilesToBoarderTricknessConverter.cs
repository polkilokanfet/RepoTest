using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(IEnumerable<PriceEngineeringTaskFileTechnicalRequirementsWrapper>), typeof(Thickness))]
    public class FilesToBoarderTricknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<PriceEngineeringTaskFileTechnicalRequirementsWrapper> enumerable)
            {
                return enumerable.Any()
                    ? new Thickness(0)
                    : new Thickness(1);
            }
            
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}