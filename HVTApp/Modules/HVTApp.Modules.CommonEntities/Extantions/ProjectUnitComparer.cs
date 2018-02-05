using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Extantions
{
    public class ProjectUnitComparer : IEqualityComparer<ProjectUnit>
    {
        public bool Equals(ProjectUnit x, ProjectUnit y)
        {
            return y != null && 
                   x != null && 
                   Equals(x.Product.Id, y.Product.Id) &&
                   Equals(x.Facility.Id, y.Facility.Id) &&
                   Equals(x.DeliveryDate, y.DeliveryDate) &&
                   Equals(x.Producer?.Id, y.Producer?.Id) &&
                   Equals(x.Cost, y.Cost);
        }

        public int GetHashCode(ProjectUnit projectUnit)
        {
            return projectUnit.Product.Id.GetHashCode() + 
                   projectUnit.Facility.Id.GetHashCode() + 
                   projectUnit.Cost.GetHashCode();
        }
    }
}