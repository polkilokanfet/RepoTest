using System;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(ScriptStep2), typeof(Color))]
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ScriptStep2 step)
            {
                if(step.Equals(ScriptStep2.Created))
                    return Colors.White;
                if(step.Equals(ScriptStep2.Started))
                    return Colors.LightSkyBlue;
                if(step.Equals(ScriptStep2.Stopped))
                    return Colors.LightGray;
                if(step.Equals(ScriptStep2.RejectedByManager))
                    return Colors.Red;
                if(step.Equals(ScriptStep2.RejectedByConstructor))
                    return Colors.Yellow;
                if(step.Equals(ScriptStep2.FinishedByConstructor))
                    return Colors.GreenYellow;
                if(step.Equals(ScriptStep2.Accepted))
                    return Colors.LightGreen;
                if(step.Equals(ScriptStep2.VerificationRequestedByConstructor))
                    return Colors.DarkSeaGreen;
                if(step.Equals(ScriptStep2.VerificationAcceptedByHead))
                    return Colors.GreenYellow;
                if(step.Equals(ScriptStep2.VerificationRejectedByHead))
                    return Colors.OrangeRed;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}