using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;

namespace HVTApp.UI.PriceEngineering.Converters
{
    /// <summary>
    /// Развернуть дочерние задачи при открытии?
    /// </summary>
    [ValueConversion(typeof(PriceEngineeringTaskViewModel), typeof(bool))]
    public class PriceEngineeringTaskViewModelIsExpendedChildTasksConverter : IValueConverter
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
                    var priceEngineeringTasks = viewModelConstructor.Model.GetSuitableTasksForWork(GlobalAppProperties.User).ToList();
                    priceEngineeringTasks.RemoveIfContainsById(viewModelConstructor.Model);
                    return priceEngineeringTasks.Any();
                }

                if (priceEngineeringTaskViewModel is PriceEngineeringTaskViewModelDesignDepartmentHead viewModelDesignDepartmentHead)
                {
                    var priceEngineeringTasks = viewModelDesignDepartmentHead.Model.GetSuitableTasksForInstruct(GlobalAppProperties.User).ToList();
                    priceEngineeringTasks.RemoveIfContainsById(viewModelDesignDepartmentHead.Model);
                    return priceEngineeringTasks.Any();
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