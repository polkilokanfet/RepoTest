using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(IEnumerable<IUnitWrapper>), typeof(string))]
    public class ProductUnitsToFacilitiesNamesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var projectUnits = value as IEnumerable<IUnitWrapper>;
            if (projectUnits == null) throw new ArgumentException();

            var facilities = projectUnits.Select(x => x.Facility).Distinct();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var facility in facilities)
            {
                stringBuilder.Append(facility + "; ");
            }
            return stringBuilder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}