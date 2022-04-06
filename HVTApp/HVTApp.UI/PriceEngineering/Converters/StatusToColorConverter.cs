using System;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskViewModel), typeof(Color))]
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is PriceEngineeringTaskViewModel priceEngineeringTask)
            {
                switch (priceEngineeringTask.Status)
                {
                    case PriceEngineeringTaskStatusEnum.Created:
                        return Colors.White;
                    case PriceEngineeringTaskStatusEnum.Started:
                        return Colors.LightSkyBlue;
                    case PriceEngineeringTaskStatusEnum.Stopped:
                        return Colors.LightGray;
                    case PriceEngineeringTaskStatusEnum.RejectedByManager:
                        return Colors.DeepPink;
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                        return Colors.Orange;
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                        return Colors.LightGreen;
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return Colors.Green;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}