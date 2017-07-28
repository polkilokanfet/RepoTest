using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Modules.Sales.Converter
{
    [ValueConversion(typeof(IList<ProjectUnitWrapper>), typeof(ObservableCollection<ProjectUnitGroup>))]
    public class ProjectUnitListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<ProjectUnitGroup> result = new ObservableCollection<ProjectUnitGroup>();

            var units = value as IList<ProductionUnitWrapper>;
            while (units.Any())
            {
                var firstMember = units.First();
                var members = units.Where(x => x.Product == firstMember.Product);

                result.Add(new ProjectUnitGroup {Product = firstMember.Product.Model, Count = members.Count()});

                members.ForEach(x => units.Remove(x));
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ProjectUnitGroup
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
