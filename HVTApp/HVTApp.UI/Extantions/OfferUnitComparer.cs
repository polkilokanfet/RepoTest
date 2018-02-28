using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Extantions
{
    public class OfferUnitComparer : IEqualityComparer<OfferUnit>
    {
        public bool Equals(OfferUnit x, OfferUnit y)
        {
            return x != null &&
                   y != null &&
                   Equals(x.Facility.Id, y.Facility.Id) &&
                   Equals(x.CommonUnit.Product.Id, y.CommonUnit.Product.Id) &&
                   Equals(x.Offer.Id, y.Offer.Id) &&
                   Equals(x.CommonUnit.Cost, y.CommonUnit.Cost);
        }

        public int GetHashCode(OfferUnit offerUnit)
        {
            var propInfos = offerUnit.GetType().GetProperties(BindingFlags.Public);
            return propInfos.Select(propertyInfo => propertyInfo.GetValue(offerUnit)).Where(val => !Equals(val, null)).Sum(val => val.GetHashCode());
        }
    }
}