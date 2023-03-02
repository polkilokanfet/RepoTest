using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(ScriptStep2), typeof(string))]
    public class PriceEngineeringTaskStatusEnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ScriptStep2 step)
            {
                if (step.Equals(ScriptStep2.Created)) 
                    return "������ �������";
                if (step.Equals(ScriptStep2.Started)) 
                    return "������ �������� �� ����������";
                if (step.Equals(ScriptStep2.Stopped)) 
                    return "������ ����������� ����������";
                if (step.Equals(ScriptStep2.RejectedByManager)) 
                    return "���������� �� ������� ���������� (�������������� ������������)";
                if (step.Equals(ScriptStep2.RejectedByConstructor)) 
                    return "������ ��������� ������������ (�������������� ����������)";
                if (step.Equals(ScriptStep2.FinishedByConstructor)) 
                    return "����������� �������� ���������� ������";
                if (step.Equals(ScriptStep2.Accepted)) 
                    return "���������� ������� ����������";
                if (step.Equals(ScriptStep2.VerificationRequestedByConstructor)) 
                    return "���������� ������ ���������� �� �������� ������������";
                if (step.Equals(ScriptStep2.VerificationAcceptedByHead)) 
                    return "���������� ������ ������� �������������";
                if (step.Equals(ScriptStep2.VerificationRejectedByHead)) 
                    return "���������� ������ �� ������� ������������� (���������� �� ��������� �����������)";
            }

            return "������ �� �������� � ����";
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}