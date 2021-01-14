using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Comparers;
using HVTApp.UI.PriceCalculations.ViewModel;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class SalesUnitForTechnicalRequrementsTaskComparer : IEqualityComparer<SalesUnitEmptyWrapper>
    {
        public bool Equals(SalesUnitEmptyWrapper x, SalesUnitEmptyWrapper y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!Equals(x.Model.Cost, y.Model.Cost)) return false;
            if (!Equals(x.Model.Comment, y.Model.Comment)) return false;
            if (!Equals(x.Model.CostDelivery, y.Model.CostDelivery)) return false;
            if (!Equals(x.Model.Project.Id, y.Model.Project.Id)) return false;
            if (!Equals(x.Model.Facility.Id, y.Model.Facility.Id)) return false;
            if (!Equals(x.Model.Product.Id, y.Model.Product.Id)) return false;
            if (!Equals(x.Model.Specification?.Id, y.Model.Specification?.Id)) return false;
            if (!Equals(x.Model.OrderInTakeDate, y.Model.OrderInTakeDate)) return false;
            if (!Equals(x.Model.RealizationDateCalculated, y.Model.RealizationDateCalculated)) return false;

            var productsInclX = x.Model.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();
            var productsInclY = y.Model.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();

            if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
            if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;

            return true;
        }

        public int GetHashCode(SalesUnitEmptyWrapper salesUnit)
        {
            return 0;
        }
    }
}