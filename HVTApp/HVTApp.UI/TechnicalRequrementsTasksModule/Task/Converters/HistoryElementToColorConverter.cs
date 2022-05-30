using System;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.TechnicalRequrementsTasksModule.Converters
{
    //[ValueConversion(typeof(TechnicalRequrementsTaskHistoryElement), typeof(Color))]
    public class HistoryElementToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is TechnicalRequrementsTaskHistoryElement historyElement)
            {
                switch (historyElement.Type)
                {
                    case TechnicalRequrementsTaskHistoryElementType.Create:
                    {
                        return Colors.LightGray;
                    }
                    case TechnicalRequrementsTaskHistoryElementType.Start:
                    {
                        return Colors.LightSkyBlue;
                    }
                    case TechnicalRequrementsTaskHistoryElementType.Finish:
                    {
                        return Colors.LightSkyBlue;
                    }
                    case TechnicalRequrementsTaskHistoryElementType.Reject:
                    {
                        return Colors.LightPink;
                    }
                    case TechnicalRequrementsTaskHistoryElementType.Stop:
                    {
                        return Colors.Gray;
                    }
                    case TechnicalRequrementsTaskHistoryElementType.Instruct:
                    {
                        return Colors.LightSkyBlue;
                    }
                    case TechnicalRequrementsTaskHistoryElementType.Accept:
                    {
                        return Colors.LightGreen;
                    }
                    case TechnicalRequrementsTaskHistoryElementType.RejectByFrontManager:
                    {
                        return Colors.LightPink;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }

            if (value is PriceCalculationHistoryItem historyItem)
            {
                switch (historyItem.Type)
                {
                    case PriceCalculationHistoryItemType.Start:
                        return Colors.LightSkyBlue;
                    case PriceCalculationHistoryItemType.Stop:
                        return Colors.Gray;
                    case PriceCalculationHistoryItemType.Reject:
                        return Colors.LightPink;
                    case PriceCalculationHistoryItemType.Finish:
                        return Colors.LightGreen;
                    case PriceCalculationHistoryItemType.Create:
                        return Colors.LightGray;
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