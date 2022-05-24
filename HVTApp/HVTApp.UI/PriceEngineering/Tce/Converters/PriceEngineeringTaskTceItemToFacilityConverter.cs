using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Tce.Unit;

namespace HVTApp.UI.PriceEngineering.Tce.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskTceItem), typeof(string))]
    public class PriceEngineeringTaskTceItemToFacilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskTceItem priceEngineeringTaskTceItem)
            {
                var facility = priceEngineeringTaskTceItem
                    .TceStructureCostVersions
                    .First()
                    .ParentPriceEngineeringTask
                    .SalesUnits
                    .First()
                    .Facility;

                return $"{facility} (владелец: {facility.OwnerCompany}; адрес: {facility.Address};)";
            }

            return default(string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}