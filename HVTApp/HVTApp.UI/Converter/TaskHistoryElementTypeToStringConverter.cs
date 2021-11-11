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
                        return "�������";
                    case TechnicalRequrementsTaskHistoryElementType.Start:
                        return "��������";
                    case TechnicalRequrementsTaskHistoryElementType.Finish:
                        return "���������";
                    case TechnicalRequrementsTaskHistoryElementType.Reject:
                        return "��������� ��";
                    case TechnicalRequrementsTaskHistoryElementType.RejectByFrontManager:
                        return "��������� ��";
                    case TechnicalRequrementsTaskHistoryElementType.Stop:
                        return "�����������";
                    case TechnicalRequrementsTaskHistoryElementType.Instruct:
                        return "��������";
                    case TechnicalRequrementsTaskHistoryElementType.Accept:
                        return "�������";
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