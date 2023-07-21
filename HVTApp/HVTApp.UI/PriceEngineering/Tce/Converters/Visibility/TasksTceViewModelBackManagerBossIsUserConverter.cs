using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.PriceEngineering.Tce.Second;

namespace HVTApp.UI.PriceEngineering.Tce.Converters.Visibility
{
    [ValueConversion(typeof(TasksTceViewModelBackManagerBoss), typeof(bool))]
    public class TasksTceViewModelBackManagerBossIsUserConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is TasksTceViewModelBackManagerBoss && GlobalAppProperties.UserIsBackManagerBoss);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}