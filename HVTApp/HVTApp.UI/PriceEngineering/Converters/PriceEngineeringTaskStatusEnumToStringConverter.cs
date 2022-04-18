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
                        return "Выполнение задачи остановлено менеджером";
                    case PriceEngineeringTaskStatusEnum.RejectedByManager:
                        return "Проработка не принята менеджером (дорабатывается исполнителем)";
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                        return "Задача отклонена исполнителем (дорабатывается менеджером)";
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                        return "Исполнитель завершил проработку задачи";
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return "Проработка принята менеджером";
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification:
                        return "На проверке у руководителя";
                    case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                        return "Проработка принята руководителем";
                    case PriceEngineeringTaskStatusEnum.VerificationRejectededByHead:
                        return "На доработке у исполнителя (отклонено руководителем)";
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