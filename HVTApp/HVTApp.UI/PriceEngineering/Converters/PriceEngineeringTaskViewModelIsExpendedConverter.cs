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
    [ValueConversion(typeof(PriceEngineeringTaskViewModel), typeof(bool))]
    public class PriceEngineeringTaskViewModelIsExpendedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskViewModel priceEngineeringTaskViewModel)
            {
                if (priceEngineeringTaskViewModel is PriceEngineeringTaskViewModelManager)
                {
                    return true;
                }

                if (priceEngineeringTaskViewModel is PriceEngineeringTaskViewModelConstructor viewModelConstructor)
                {
                    return viewModelConstructor.UserConstructor != null && viewModelConstructor.Model.GetSuitableTasksForWork(GlobalAppProperties.User).Any();
                }

                if (priceEngineeringTaskViewModel is PriceEngineeringTaskViewModelDesignDepartmentHead viewModelDesignDepartmentHead)
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