using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.PriceEngineering.Messages
{
    [ValueConversion(typeof(PriceEngineeringTaskMessage), typeof(SolidColorBrush))]
    public class PriceEngineeringTaskMessageToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskMessage message)
            {
                if (message.Author.Id == GlobalAppProperties.User.Id)
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