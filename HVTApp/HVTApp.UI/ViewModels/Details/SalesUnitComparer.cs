using System.Collections.Generic;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public class SalesUnitComparer : IEqualityComparer<SalesUnitWrapper>
    {
        private readonly IEnumerable<string> _propertyNames;

        public SalesUnitComparer(IEnumerable<string> propertyNames)
        {
            _propertyNames = propertyNames;
        }

        public bool Equals(SalesUnitWrapper x, SalesUnitWrapper y)
        {
            if (x == null || y == null) return false;
            foreach (var propertyName in _propertyNames)
            {
                var propInfo = typeof(SalesUnitWrapper).GetProperty(propertyName);
                if (!Equals(propInfo.GetValue(x), propInfo.GetValue(y)))
                    return false;
            }
            return true;
        }

        public int GetHashCode(SalesUnitWrapper salesUnitWrapper)
        {
            int result = 0;
            foreach (var propertyName in _propertyNames)
            {
                result += salesUnitWrapper.GetType().GetProperty(propertyName).GetValue(salesUnitWrapper).GetHashCode();
            }

            foreach (var dependentSalesUnit in salesUnitWrapper.DependentSalesUnits)
            {
                result += dependentSalesUnit.Product.Id.GetHashCode();
            }

            return result;
        }
    }
}