using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HVTApp.Model;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TaskViewModel), typeof(Visibility))]
    public class PriceEngineeringTaskViewModelConstructorIsTargetTaskVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModelConstructor priceEngineeringTaskViewModel)
            {
                if (priceEngineeringTaskViewModel.IsTarget)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(TaskViewModel), typeof(Visibility))]
    public class PriceEngineeringTaskViewModelConstructorIsTargetSubTaskVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModelConstructor viewModel)
            {
                if (viewModel.Model.UserConstructorInitiator?.Id == GlobalAppProperties.User.Id)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}