using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskStatusEnum), typeof(string))]
    public class PriceEngineeringTaskStatusEnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskStatusEnum status)
            {
                switch (status)
                {
                    case PriceEngineeringTaskStatusEnum.Created:
                        return "Задача создана";
                    case PriceEngineeringTaskStatusEnum.Started:
                        return "Задача запущена на проработку";
                    case PriceEngineeringTaskStatusEnum.Stopped:
                        return "Задача остановлена менеджером";
                    case PriceEngineeringTaskStatusEnum.RejectedByManager:
                        return "Проработка не принята менеджером (дорабатывается исполнителем)";
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                        return "Задача отклонена исполнителем (дорабатывается менеджером)";
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                        return "Исполнитель завершил проработку задачи";
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return "Проработка принята менеджером";
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification:
                        return "Проработка задачи направлена на проверку руководителю";
                    case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                        return "Проработка задачи принята руководителем";
                    case PriceEngineeringTaskStatusEnum.VerificationRejectededByHead:
                        return "Проработка задачи не принята руководителем (отправлена на доработку исполнителю)";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}