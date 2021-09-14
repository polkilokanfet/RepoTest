using System;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.TechnicalRequrementsTasksModule.Converters
{
    [ValueConversion(typeof(TechnicalRequrementsTaskLookup), typeof(Color))]
    public class TechnicalRequrementsTaskToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is TechnicalRequrementsTaskLookup lookup)
            {
                //если требуемой даты нет
                if (lookup.DesiredFinishDate.HasValue == false)
                {
                    return Colors.Black;
                }

                //если требуемая дата есть

                //если задача завершена или не стартована или остановлена
                if (lookup.IsFinished == true || lookup.IsStarted == false || lookup.IsStopped)
                {
                    return Colors.Black;
                }

                //если задача просрочена
                if (DateTime.Today > lookup.DesiredFinishDate.Value)
                {
                    return Colors.OrangeRed;
                }

                //если остался день до просрочки
                if ((lookup.DesiredFinishDate.Value - DateTime.Today).Days <= 1)
                {
                    return Colors.Orange;
                }

                return Colors.Black;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}