using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Comparers;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PrintService
{
    public class Unit
    {
        private readonly IUnit _unit;

        public Facility Facility => _unit.Facility;
        public Product Product => _unit.Product;
        public double Cost => _unit.Cost;
        public double? CostDelivery => _unit.CostDelivery;
        public bool CostDeliveryIncluded => _unit.CostDeliveryIncluded;
        public int ProductionTerm => _unit.ProductionTerm;
        public string Comment => _unit.Comment;
        public List<ProductIncluded> ProductsIncluded => _unit.ProductsIncluded;
        public PaymentConditionSet PaymentConditionSet => _unit.PaymentConditionSet;

        public Unit(IUnit unit)
        {
            _unit = unit;
        }

        public class Comparer : IEqualityComparer<Unit>
        {
            public virtual bool Equals(Unit x, Unit y)
            {
                if (x == null) throw new ArgumentNullException(nameof(x));
                if (y == null) throw new ArgumentNullException(nameof(y));

                if (!Equals(x.Cost, y.Cost)) return false;
                if (!Equals(x.ProductionTerm, y.ProductionTerm)) return false;
                if (!Equals(x.Product.Id, y.Product.Id)) return false;
                if (!Equals(x.Facility.Id, y.Facility.Id)) return false;
                if (!Equals(x.PaymentConditionSet.Id, y.PaymentConditionSet.Id)) return false;
                if (!Equals(x.CostDelivery, y.CostDelivery)) return false;
                if (!Equals(x.Comment, y.Comment)) return false;

                var productsInclX = x.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();
                var productsInclY = y.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();

                if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
                if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;

                return true;
            }

            public int GetHashCode(Unit obj)
            {
                return 0;
            }
        }

        /// <summary>
        /// Сравнение сначала по продукту, затем по цене
        /// </summary>
        public class ProductCostComparer : IComparer<Unit>
        {
            public int Compare(Unit productCostX, Unit productCostY)
            {
                if (productCostX == null) throw new ArgumentNullException(nameof(productCostX));
                if (productCostY == null) throw new ArgumentNullException(nameof(productCostY));

                //сравнение по продукту
                var productComparingResult = new ProductComparer().Compare(productCostX.Product, productCostY.Product);
                if (productComparingResult != 0)
                {
                    return productComparingResult;
                }

                //сравнение по стоимости за единицу
                return (int)(productCostY.Cost - productCostX.Cost);
            }
        }

    }
}