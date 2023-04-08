using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure.Converters;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TaskViewModelConstructor), typeof(bool))]
    public class BlockAddedIsEnabledConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModelConstructor viewModel)
            {
                if (viewModel.IsTarget && viewModel.IsEditMode)
                {
                    return true;
                }
            }

            return false;
        }
    }
}