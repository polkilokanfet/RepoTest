using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Comparers
{
    /// <summary>
    /// Сравнение сначала по продукту, затем по цене
    /// </summary>
    public class ProductCostComparer : IComparer<IProductCost>
    {
        public int Compare(IProductCost productCostX, IProductCost productCostY)
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