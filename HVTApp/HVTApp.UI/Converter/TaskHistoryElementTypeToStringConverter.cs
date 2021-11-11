using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(TechnicalRequrementsTaskHistoryElementType), typeof(string))]
    public class TaskHistoryElementTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TechnicalRequrementsTaskHistoryElementType historyElementType)
            {
                switch (historyElementType)
                {
                    case TechnicalRequrementsTaskHistoryElementType.Create:
                        return "Создано";
                    case TechnicalRequrementsTaskHistoryElementType.Start:
                        return "Запущено";
                    case TechnicalRequrementsTaskHistoryElementType.Finish:
                        return "Завершено";
                    case TechnicalRequrementsTaskHistoryElementType.Reject:
                        return "Отклонено БМ";
                    case TechnicalRequrementsTaskHistoryElementType.RejectByFrontManager:
                        return "Отклонено ФМ";
                    case TechnicalRequrementsTaskHistoryElementType.Stop:
                        return "Остановлено";
                    case TechnicalRequrementsTaskHistoryElementType.Instruct:
                        return "Запущено";
                    case TechnicalRequrementsTaskHistoryElementType.Accept:
                        return "Принято";
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