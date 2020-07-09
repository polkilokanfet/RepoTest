using System;
using System.Windows.Data;
using System.Windows.Media;

namespace HVTApp.UI.Modules.Sales.Market.Converters
{
    public class DaysToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Colors.LightGray;
            int dblValue;
            if (int.TryParse(value.ToString(), out dblValue))
            {
                if (dblValue <= 0) return Colors.Red;
                if (dblValue < 30) return Colors.HotPink;
                if (dblValue < 60) return Colors.LightPink;

                return Colors.White;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}