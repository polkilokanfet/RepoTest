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
                   Equals(x.Product.Id, y.Product.Id) &&
                   Equals(x.Facility.Id, y.Facility.Id) &&
                   Equals(x.DeliveryDate, y.DeliveryDate) &&
                   Equals(x.Producer?.Id, y.Producer?.Id) &&
                   Equals(x.Cost, y.Cost);
        }

        public int GetHashCode(ProjectUnit projectUnit)
        {
            var propInfos = projectUnit.GetType().GetProperties(BindingFlags.Public);
            return propInfos.Select(propertyInfo => propertyInfo.GetValue(projectUnit)).Where(val => !Equals(val, null)).Sum(val => val.GetHashCode());
        }
    }

    public class OfferUnitComparer : IEqualityComparer<OfferUnit>
    {
        public bool Equals(OfferUnit x, OfferUnit y)
        {
            return x != null &&
                   y != null &&
                   Equals(x.Facility.Id, y.Facility.Id) &&
                   Equals(x.Product.Id, y.Product.Id) &&
                   Equals(x.Offer.Id, y.Offer.Id) &&
                   Equals(x.Cost, y.Cost);
        }

        public int GetHashCode(OfferUnit offerUnit)
        {
            var propInfos = offerUnit.GetType().GetProperties(BindingFlags.Public);
            return propInfos.Select(propertyInfo => propertyInfo.GetValue(offerUnit)).Where(val => !Equals(val, null)).Sum(val => val.GetHashCode());
        }
    }
}