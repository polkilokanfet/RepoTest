using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Extantions
{
    public class ProjectUnitComparer : IEqualityComparer<ProjectUnit>
    {
        public bool Equals(ProjectUnit x, ProjectUnit y)
        {
            return y != null && 
                   x != null &&
                   Equals(x.Project.Id, y.Project.Id) &&
                   Equals(x.CommonUnit.Product.Id, y.CommonUnit.Product.Id) &&
                   Equals(x.Facility.Id, y.Facility.Id) &&
                   Equals(x.DeliveryDate, y.DeliveryDate) &&
                   Equals(x.Producer?.Id, y.Producer?.Id) &&
                   Equals(x.CommonUnit.Cost, y.CommonUnit.Cost);
        }

        public int GetHashCode(ProjectUnit projectUnit)
        {
            var propInfos = projectUnit.GetType().GetProperties(BindingFlags.Public);
            return propInfos.Select(propertyInfo => propertyInfo.GetValue(projectUnit)).Where(val => !Equals(val, null)).Sum(val => val.GetHashCode());
        }
    }
}