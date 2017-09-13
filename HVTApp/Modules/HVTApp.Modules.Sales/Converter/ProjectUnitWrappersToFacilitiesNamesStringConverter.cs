using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.Sales.Converter
{
    [ValueConversion(typeof(IEnumerable<ProjectUnitWrapper>), typeof(string))]
    public class ProjectUnitWrappersToFacilitiesNamesStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var projectUnits = value as IEnumerable<ProjectUnitWrapper>;
            if (projectUnits == null) throw new ArgumentException();

            var facilities = projectUnits.Select(x => x.Facility).Distinct();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var facility in facilities)
            {
                stringBuilder.Append(facility + "; ");
            }
            return stringBuilder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(IEnumerable<ProjectUnitWrapper>), typeof(IEnumerable<ProjectUnitsGroup>))]
    public class ProjectUnitWrappersToGroupsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var projectUnits = value as IEnumerable<ProjectUnitWrapper>;
            if (projectUnits == null) throw new ArgumentException();

            List<ProjectUnitsGroup> projectUnitsGroups = new List<ProjectUnitsGroup>();
            foreach (var unit in projectUnits)
            {
                projectUnitsGroups.Add(new ProjectUnitsGroup() {Product = unit.Product, Count = 2});
            }
            return projectUnitsGroups;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ProjectUnitsGroup
    {
        public ProductWrapper Product { get; set; }
        public int Count { get; set; }
    }
}