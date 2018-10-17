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
        private readonly IEnumerable<ProductTypeDesignation> _typeDesignations;

        private readonly Dictionary<List<Parameter>, string> _designationDictionary;

        public ProductDesignator(IUnitOfWork unitOfWork)
        {
            _typeDesignations = unitOfWork.Repository<ProductTypeDesignation>().Find(x => true);

            var designations = unitOfWork.Repository<ProductDesignation>().Find(x => true);
            _designationDictionary = new Dictionary<List<Parameter>, string>();
            foreach (var designation in designations)
            {
                foreach (var kvp in designation.GetDesignationDictionary())
                {
                    _designationDictionary.Add(kvp.Key.ToList(), kvp.Value);
                }
            }
            _designationDictionary = _designationDictionary.OrderByDescending(x => x.Key.Count).ToDictionary(x => x.Key, x => x.Value);
        }

        #region Designation

        public string GetDesignation(ProductBlock block)
        {
            if (!string.IsNullOrEmpty(block.DesignationSpecial))
                return block.DesignationSpecial;

            var designation = _designationDictionary.FirstOrDefault(pd => pd.Key.AllContainsIn(block.Parameters, new ParameterComparer()));

            if (!Equals(designation, default(KeyValuePair<List<Parameter>, string>)))
                return designation.Value;

            return block.ParametersToString();
        }

        public string GetDesignation(Product product)
        {
            return GetDesignation(product.ProductBlock);
        }

        #endregion

        #region Type

        public ProductType GetProductType(ProductBlock block)
        {
            var designations = _typeDesignations.Where(pd => pd.Parameters.AllContainsIn(block.Parameters, new ParameterComparer())).ToList();

            if (designations.Any())
                return designations.OrderBy(x => x.Parameters.Count).Last().ProductType;

            return null;
        }

        public ProductType GetProductType(Product product)
        {
            return GetProductType(product.ProductBlock);
        }

        #endregion

    }
}
