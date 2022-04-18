using System;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskStatusEnum), typeof(Color))]
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is PriceEngineeringTaskStatusEnum statusEnum)
            {
                switch (statusEnum)
                {
                    case PriceEngineeringTaskStatusEnum.Created:
                        return Colors.White;
                    case PriceEngineeringTaskStatusEnum.Started:
                        return Colors.LightSkyBlue;
                    case PriceEngineeringTaskStatusEnum.Stopped:
                        return Colors.LightGray;
                    case PriceEngineeringTaskStatusEnum.RejectedByManager:
                        return Colors.Red;
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                        return Colors.Yellow;
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                        return Colors.GreenYellow;
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return Colors.LightGreen;
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification:
                        return Colors.DarkSeaGreen;
                    case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                        return Colors.GreenYellow;
                    case PriceEngineeringTaskStatusEnum.VerificationRejectededByHead:
                        return Colors.OrangeRed;
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