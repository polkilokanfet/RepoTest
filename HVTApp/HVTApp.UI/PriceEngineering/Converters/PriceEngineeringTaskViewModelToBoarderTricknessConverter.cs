using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(IEnumerable<PriceEngineeringTaskFileTechnicalRequirementsWrapper>), typeof(Thickness))]
    public class PriceEngineeringTaskViewModelToBoarderTricknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModel priceEngineeringTaskViewModel)
            {
                if (value is TaskViewModelManager)
                    return new Thickness(0);

                return priceEngineeringTaskViewModel.IsTarget
                    ? new Thickness(3)
                    : new Thickness(0);
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}