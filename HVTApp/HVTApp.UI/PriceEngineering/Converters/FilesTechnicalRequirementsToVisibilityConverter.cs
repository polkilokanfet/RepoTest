using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.PriceEngineering.Converters
{
    //[ValueConversion(typeof(PriceEngineeringTasksViewModel), typeof(Visibility))]
    public class FilesTechnicalRequirementsToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IValidatableChangeTrackingCollection<PriceEngineeringTasksFileTechnicalRequirementsWrapper> collection)
            {
                if (collection.Any())
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}