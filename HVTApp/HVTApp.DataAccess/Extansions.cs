using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Services.ProductDesignationService;

namespace HVTApp.DataAccess
{
    public static class Extansions
    {
        public static void DesignateProducts(this IEnumerable<IUnitPoco> units, IProductDesignationService designationService)
        {
            foreach (var unit in units)
            {
                unit.Product.Designation = designationService.GetDesignation(unit.Product);
                unit.Product.ProductType = designationService.GetProductType(unit.Product);
                foreach (var productIncluded in unit.ProductsIncluded)
                {
                    productIncluded.Product.Designation = designationService.GetDesignation(productIncluded.Product);
                    productIncluded.Product.ProductType = designationService.GetProductType(productIncluded.Product);
                }
            }
        }
    }
}