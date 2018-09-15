using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.ProductDesignationService;

namespace HVTApp.DataAccess
{
    public static class Extansions
    {
        public static void DesignateProduct(this Product product, IProductDesignationService designationService)
        {
            product.Designation = designationService.GetDesignation(product);
            product.ProductType = designationService.GetProductType(product);
            foreach (var dependent in product.DependentProducts)
            {
                dependent.Product.DesignateProduct(designationService);
            }
        }


        public static void DesignateProduct(this IUnitPoco unit, IProductDesignationService designationService)
        {
            unit.Product.DesignateProduct(designationService);
            foreach (var productIncluded in unit.ProductsIncluded)
            {
                productIncluded.Product.DesignateProduct(designationService);
            }
        }

        public static void DesignateProducts(this IEnumerable<IUnitPoco> units, IProductDesignationService designationService)
        {
            units.ToList().ForEach(x => x.DesignateProduct(designationService));
        }

    }
}