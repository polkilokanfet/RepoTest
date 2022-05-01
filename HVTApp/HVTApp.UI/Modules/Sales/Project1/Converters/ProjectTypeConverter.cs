using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Converters
{
    [ValueConversion(typeof(ProjectTypeSimpleWrapper), typeof(ProjectType))]
    public class ProjectTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ProjectTypeSimpleWrapper projectTypeSimpleWrapper)
            {
                return projectTypeSimpleWrapper.Model;
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ProjectType projectType)
            {
                return new ProjectTypeSimpleWrapper(projectType);
            }

            throw new NotImplementedException();
        }
    }
}