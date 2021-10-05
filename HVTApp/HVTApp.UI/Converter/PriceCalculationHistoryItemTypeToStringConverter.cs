using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(PriceCalculationHistoryItemType), typeof(string))]
    public class PriceCalculationHistoryItemTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceCalculationHistoryItemType historyItemType)
            {
                switch (historyItemType)
                {
                    case PriceCalculationHistoryItemType.Stop:
                        return "Остановлено";
                    case PriceCalculationHistoryItemType.Start:
                        return "Запущено";
                    case PriceCalculationHistoryItemType.Finish:
                        return "Завершено";
                    case PriceCalculationHistoryItemType.Reject:
                        return "Отклонено";
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