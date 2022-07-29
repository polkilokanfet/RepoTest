using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HVTApp.Services.GetProductService.Converter
{
    [ValueConversion(typeof(ParameterSelector), typeof(string))]
    public class ParameterSelectorToParameterGroupNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Binding.DoNothing;
            }

            if (value is ParameterSelector parameterSelector)
            {
                return parameterSelector.ParametersFlaged.First().Parameter.ParameterGroup.Name;
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}