using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TaskViewModelBackManager), typeof(Visibility))]
    public class PriceEngineeringTaskViewModelBackManagerVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModelBackManager viewModel)
            {
                if (viewModel.Model.Statuses.Any(x => x.StatusEnum == ScriptStep.LoadToTceStart.Value))
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}