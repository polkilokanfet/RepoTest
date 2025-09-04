using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.Market.Converters
{
    public class ProjectToProjectTypeNameConverter : IValueConverter
    {
        public static IEnumerable<ProjectType> ProductTypes;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Project project)
            {
                return ProductTypes.Single(x => x.Id == project.ProjectTypeId).Name;
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}