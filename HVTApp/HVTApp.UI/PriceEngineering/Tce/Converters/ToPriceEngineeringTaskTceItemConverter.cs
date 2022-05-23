using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.UI.PriceEngineering.Tce.Unit;

namespace HVTApp.UI.PriceEngineering.Tce.Converters
{
    [ValueConversion(typeof(IEnumerable<TceStructureCostVersion>), typeof(IEnumerable<PriceEngineeringTaskTceItem>))]
    public class ToPriceEngineeringTaskTceItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<TceStructureCostVersion> structureCostVersions)
            {
                return structureCostVersions
                    .GroupBy(x => x.ParentPriceEngineeringTask)
                    .Select(x => new PriceEngineeringTaskTceItem(x))
                    .ToList();
            }

            throw new Exception();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}