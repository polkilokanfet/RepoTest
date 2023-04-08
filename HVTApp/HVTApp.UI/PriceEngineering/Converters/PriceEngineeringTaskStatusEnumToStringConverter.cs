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
                //    return "Задача создана";
                //if (step.Equals(ScriptStep2.Start)) 
                //    return "Задача запущена на проработку";
                //if (step.Equals(ScriptStep2.Stop)) 
                //    return "Задача остановлена менеджером";
                //if (step.Equals(ScriptStep2.RejectByManager)) 
                //    return "Проработка не принята менеджером (дорабатывается исполнителем)";
                //if (step.Equals(ScriptStep2.RejectByConstructor)) 
                //    return "Задача отклонена исполнителем (дорабатывается менеджером)";
                //if (step.Equals(ScriptStep2.FinishByConstructor)) 
                //    return "Исполнитель завершил проработку задачи";
                //if (step.Equals(ScriptStep2.Accept)) 
                //    return "Проработка принята менеджером";
                //if (step.Equals(ScriptStep2.VerificationRequestByConstructor)) 
                //    return "Проработка задачи направлена на проверку руководителю";
                //if (step.Equals(ScriptStep2.VerificationAcceptByHead)) 
                //    return "Проработка задачи принята руководителем";
                //if (step.Equals(ScriptStep2.VerificationRejectByHead)) 
                //    return "Проработка задачи не принята руководителем (отправлена на доработку исполнителю)";
            }

            return "";
            throw new ArgumentException();
        }
    }
}