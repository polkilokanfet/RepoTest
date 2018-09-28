using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Services.ProductDesignationService
{
    public class ProductDesignator : IProductDesignationService
    {
        private readonly IEnumerable<ProductDesignation> _productDesignations;
        private readonly IEnumerable<ProductTypeDesignation> _productTypeDesignations;
        private readonly IEnumerable<ProductBlockIsService> _blockIsServices;

        public ProductDesignator(IUnitOfWork unitOfWork)
        {
            _productDesignations = unitOfWork.Repository<ProductDesignation>().Find(x => true);
            _productTypeDesignations = unitOfWork.Repository<ProductTypeDesignation>().Find(x => true);
            _blockIsServices = unitOfWork.Repository<ProductBlockIsService>().Find(x => true);
        }

        public string GetDesignation(ProductBlock block)
        {
            var designations = _productDesignations.Where(pd => pd.Parameters.AllContainsIn(block.Parameters, new ParameterComparer())).ToList();
            if (designations.Any()) return designations.OrderBy(x => x.Parameters.Count).Last().Designation;
            return block.ParametersToString();
        }

        public ProductType GetProductType(ProductBlock block)
        {
            var designations = _productTypeDesignations.Where(pd => pd.Parameters.AllContainsIn(block.Parameters, new ParameterComparer())).ToList();
            if (designations.Any()) return designations.OrderBy(x => x.Parameters.Count).Last().ProductType;
            return null;
        }

        public bool IsService(ProductBlock block)
        {
            return _blockIsServices.Any(x => x.Parameters.AllContainsIn(block.Parameters, new ParameterComparer()));
        }



        public string GetDesignation(Product product)
        {
            return GetDesignation(product.ProductBlock);
        }

        public ProductType GetProductType(Product product)
        {
            return GetProductType(product.ProductBlock);
        }
    }
}
