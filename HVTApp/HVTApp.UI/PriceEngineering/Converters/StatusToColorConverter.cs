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
                if(step.Equals(ScriptStep2.Create))
                    return Colors.White;
                if(step.Equals(ScriptStep2.Start))
                    return Colors.LightSkyBlue;
                if(step.Equals(ScriptStep2.Stop))
                    return Colors.LightGray;
                if(step.Equals(ScriptStep2.RejectByManager))
                    return Colors.Red;
                if(step.Equals(ScriptStep2.RejectByConstructor))
                    return Colors.Yellow;
                if(step.Equals(ScriptStep2.FinishByConstructor))
                    return Colors.GreenYellow;
                if(step.Equals(ScriptStep2.Accept))
                    return Colors.LightGreen;
                if(step.Equals(ScriptStep2.VerificationRequestByConstructor))
                    return Colors.DarkSeaGreen;
                if(step.Equals(ScriptStep2.VerificationAcceptByHead))
                    return Colors.GreenYellow;
                if(step.Equals(ScriptStep2.VerificationRejectByHead))
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