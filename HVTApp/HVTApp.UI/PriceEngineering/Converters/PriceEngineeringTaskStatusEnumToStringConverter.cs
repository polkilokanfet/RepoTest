using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure.Converters;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(ScriptStep), typeof(string))]
    public class PriceEngineeringTaskStatusEnumToStringConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ScriptStep step)
            {
                return step.Description;

                //if (step.Equals(ScriptStep2.Create)) 
                //    return "������ �������";
                //if (step.Equals(ScriptStep2.Start)) 
                //    return "������ �������� �� ����������";
                //if (step.Equals(ScriptStep2.Stop)) 
                //    return "������ ����������� ����������";
                //if (step.Equals(ScriptStep2.RejectByManager)) 
                //    return "���������� �� ������� ���������� (�������������� ������������)";
                //if (step.Equals(ScriptStep2.RejectByConstructor)) 
                //    return "������ ��������� ������������ (�������������� ����������)";
                //if (step.Equals(ScriptStep2.FinishByConstructor)) 
                //    return "����������� �������� ���������� ������";
                //if (step.Equals(ScriptStep2.Accept)) 
                //    return "���������� ������� ����������";
                //if (step.Equals(ScriptStep2.VerificationRequestByConstructor)) 
                //    return "���������� ������ ���������� �� �������� ������������";
                //if (step.Equals(ScriptStep2.VerificationAcceptByHead)) 
                //    return "���������� ������ ������� �������������";
                //if (step.Equals(ScriptStep2.VerificationRejectByHead)) 
                //    return "���������� ������ �� ������� ������������� (���������� �� ��������� �����������)";
            }

            return "";
            throw new ArgumentException();
        }
    }
}