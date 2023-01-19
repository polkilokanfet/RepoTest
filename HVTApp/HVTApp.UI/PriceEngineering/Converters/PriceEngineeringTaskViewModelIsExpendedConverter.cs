using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.Model;

namespace HVTApp.UI.PriceEngineering.Converters
{
    /// <summary>
    /// Развернуть задачу при открытии?
    /// </summary>
    [ValueConversion(typeof(TaskViewModel), typeof(bool))]
    public class PriceEngineeringTaskViewModelIsExpendedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModel priceEngineeringTaskViewModel)
            {
                if (priceEngineeringTaskViewModel is TaskViewModelManager)
                {
                    return true;
                }

                if (priceEngineeringTaskViewModel is TaskViewModelConstructor viewModelConstructor)
                {
                    return viewModelConstructor.Model.GetSuitableTasksForWork(GlobalAppProperties.User).Any();
                }

                if (priceEngineeringTaskViewModel is TaskViewModelDesignDepartmentHead viewModelDesignDepartmentHead)
                {
                    return viewModelDesignDepartmentHead.Model.GetSuitableTasksForInstruct(GlobalAppProperties.User).Any();
                }

                return true;
            }

            throw new ArgumentException("В конвертер передан не тот тип");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}