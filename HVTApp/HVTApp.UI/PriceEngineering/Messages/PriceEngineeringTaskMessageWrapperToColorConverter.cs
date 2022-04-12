using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.PriceEngineering.Messages
{
    [ValueConversion(typeof(PriceEngineeringTaskMessageWrapper), typeof(SolidColorBrush))]
    public class PriceEngineeringTaskMessageWrapperToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskMessageWrapper messageViewModel)
            {
                if (messageViewModel.Author.Id == GlobalAppProperties.User.Id)
                {
                    return new SolidColorBrush(Colors.LightGray);
                }
            }

            return new SolidColorBrush(Colors.LightSkyBlue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}