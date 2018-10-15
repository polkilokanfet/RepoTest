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
        private readonly IEnumerable<ProductDesignation> _designations;
        private readonly IEnumerable<ProductTypeDesignation> _typeDesignations;

        public ProductDesignator(IUnitOfWork unitOfWork)
        {
            _designations = unitOfWork.Repository<ProductDesignation>().Find(x => true);
            _typeDesignations = unitOfWork.Repository<ProductTypeDesignation>().Find(x => true);
        }

        public string GetDesignation(ProductBlock block)
        {
            var designations = _designations.Where(pd => pd.Parameters.AllContainsIn(block.Parameters, new ParameterComparer())).ToList();

            if (designations.Any())
                return designations.OrderBy(x => x.Parameters.Count).Last().Designation;

            return block.ParametersToString();
        }

        public ProductType GetProductType(ProductBlock block)
        {
            var designations = _typeDesignations.Where(pd => pd.Parameters.AllContainsIn(block.Parameters, new ParameterComparer())).ToList();

            if (designations.Any())
                return designations.OrderBy(x => x.Parameters.Count).Last().ProductType;

            return null;
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
