using System;
using System.Windows.Data;
using System.Windows.Media;
using HVTApp.Infrastructure.Converters;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(ScriptStep), typeof(Color))]
    public class StatusToColorConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ScriptStep step)
            {
                if(step.Equals(ScriptStep.Create))
                    return Colors.White;
                if(step.Equals(ScriptStep.Start))
                    return Colors.LightSkyBlue;
                if(step.Equals(ScriptStep.Stop))
                    return Colors.LightGray;
                if(step.Equals(ScriptStep.RejectByManager))
                    return Colors.Red;
                if(step.Equals(ScriptStep.RejectByConstructor))
                    return Colors.Yellow;
                if(step.Equals(ScriptStep.FinishByConstructor))
                    return Colors.GreenYellow;
                if(step.Equals(ScriptStep.Accept))
                    return Colors.LightGreen;
                if(step.Equals(ScriptStep.VerificationRequestByConstructor))
                    return Colors.DarkSeaGreen;
                if(step.Equals(ScriptStep.VerificationAcceptByHead))
                    return Colors.GreenYellow;
                if(step.Equals(ScriptStep.VerificationRejectByHead))
                    return Colors.OrangeRed;
            }

            return Colors.Transparent;
        }
    }
}