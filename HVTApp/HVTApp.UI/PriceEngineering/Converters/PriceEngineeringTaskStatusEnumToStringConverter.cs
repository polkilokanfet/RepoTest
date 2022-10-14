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
                        return "������ �������";
                    case PriceEngineeringTaskStatusEnum.Started:
                        return "������ �������� �� ����������";
                    case PriceEngineeringTaskStatusEnum.Stopped:
                        return "������ ����������� ����������";
                    case PriceEngineeringTaskStatusEnum.RejectedByManager:
                        return "���������� �� ������� ���������� (�������������� ������������)";
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                        return "������ ��������� ������������ (�������������� ����������)";
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                        return "����������� �������� ���������� ������";
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return "���������� ������� ����������";
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification:
                        return "���������� ������ ���������� �� �������� ������������";
                    case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                        return "���������� ������ ������� �������������";
                    case PriceEngineeringTaskStatusEnum.VerificationRejectededByHead:
                        return "���������� ������ �� ������� ������������� (���������� �� ��������� �����������)";
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