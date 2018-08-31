using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.ProductDesignationService
{
    public class ProductDesignator : IProductDesignationService
    {
        private readonly IEnumerable<ProductDesignation> _productDesignations;
        private readonly IEnumerable<ProductTypeDesignation> _productTypeDesignations;

        public ProductDesignator(IUnitOfWork unitOfWork)
        {
            _productDesignations = unitOfWork.GetRepository<ProductDesignation>().Find(x => x != null);
            _productTypeDesignations = unitOfWork.GetRepository<ProductTypeDesignation>().Find(x => x != null);
        }

        public string GetDesignation(Product product)
        {
            var designations = _productDesignations.Where(pd => pd.Parameters.AllContainsIn(product.ProductBlock.Parameters)).ToList();
            if (designations.Any()) return designations.OrderBy(x => x.Parameters.Count).Last().Designation;
            return product.ProductBlock.ParametersToString();
        }

        public ProductType GetProductType(Product product)
        {
            var designations = _productTypeDesignations.Where(pd => pd.Parameters.AllContainsIn(product.ProductBlock.Parameters)).ToList();
            if (designations.Any()) return designations.OrderBy(x => x.Parameters.Count).Last().ProductType;
            return null;
        }
    }
}
