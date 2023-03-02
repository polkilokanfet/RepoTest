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
                    return "Задача создана";
                if (step.Equals(ScriptStep2.Started)) 
                    return "Задача запущена на проработку";
                if (step.Equals(ScriptStep2.Stopped)) 
                    return "Задача остановлена менеджером";
                if (step.Equals(ScriptStep2.RejectedByManager)) 
                    return "Проработка не принята менеджером (дорабатывается исполнителем)";
                if (step.Equals(ScriptStep2.RejectedByConstructor)) 
                    return "Задача отклонена исполнителем (дорабатывается менеджером)";
                if (step.Equals(ScriptStep2.FinishedByConstructor)) 
                    return "Исполнитель завершил проработку задачи";
                if (step.Equals(ScriptStep2.Accepted)) 
                    return "Проработка принята менеджером";
                if (step.Equals(ScriptStep2.VerificationRequestedByConstructor)) 
                    return "Проработка задачи направлена на проверку руководителю";
                if (step.Equals(ScriptStep2.VerificationAcceptedByHead)) 
                    return "Проработка задачи принята руководителем";
                if (step.Equals(ScriptStep2.VerificationRejectedByHead)) 
                    return "Проработка задачи не принята руководителем (отправлена на доработку исполнителю)";
            }

            return "Статус не добавлен в коде";
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}