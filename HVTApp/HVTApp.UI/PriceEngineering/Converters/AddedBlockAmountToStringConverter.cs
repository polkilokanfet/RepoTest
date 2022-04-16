using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskProductBlockAdded), typeof(string))]
    public class AddedBlockAmountToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskProductBlockAdded blockAdded)
            {
                return blockAdded.IsOnBlock
                    ? $"{blockAdded.Amount} шт. на каждый блок"
                    : $"{blockAdded.Amount} шт. на весь заказ";
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}