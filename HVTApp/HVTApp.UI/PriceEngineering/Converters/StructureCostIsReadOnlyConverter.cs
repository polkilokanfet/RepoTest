using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure.Converters;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TaskViewModel), typeof(bool))]
    public class StructureCostIsReadOnlyConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModelConstructor viewModel)
            {
                if (viewModel.IsTarget)
                {
                    return false;
                }
            }

            return true;
        }
    }
}