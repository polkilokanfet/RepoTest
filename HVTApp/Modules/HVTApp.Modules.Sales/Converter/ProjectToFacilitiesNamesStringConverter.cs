using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.Sales.Converter
{
    [ValueConversion(typeof(ProjectWrapper), typeof(string))]
    public class ProjectToFacilitiesNamesStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "asd";
            var projectUnits = value as ProjectWrapper;
            if (projectUnits == null) throw new ArgumentException("Передан не ProjectWrapper.");

            return projectUnits.ProjectUnits.Select(x => x.Facility).Distinct();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}