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
        private readonly List<ProductTypeDesignation> _designationsOfProductTypes;

        private readonly List<DesignationOfBlock> _designationsOfBlocks = new List<DesignationOfBlock>();

        public ProductDesignator(IUnitOfWork unitOfWork)
        {
            //загрузка всех типов оборудования
            _designationsOfProductTypes = unitOfWork.Repository<ProductTypeDesignation>().GetAll();

            //загрузка всех обозначений блоков оборудования
            var designationsOfBlocks = unitOfWork.Repository<ProductDesignation>().GetAll();

            foreach (var designationOfBlock in designationsOfBlocks)
            {
                foreach (var kvp in designationOfBlock.GetDesignationDictionary())
                {
                    _designationsOfBlocks.Add(new DesignationOfBlock(kvp.Key.ToList(), kvp.Value));
                }
            }
            _designationsOfBlocks = _designationsOfBlocks.OrderByDescending(x => x.Parameters.Count()).ToList();
        }

        #region Designation

        public string GetDesignation(ProductBlock block)
        {
            if (block == null)
                return "block is null!";

            if (!string.IsNullOrEmpty(block.DesignationSpecial))
                return block.DesignationSpecial;

            if (!block.Parameters.Any())
                return "block has no parameters!";

            var designation = _designationsOfBlocks.FirstOrDefault(desOfBlock => desOfBlock.Parameters.AllContainsIn(block.Parameters, new ParameterComparer()));

            return !Equals(designation, default(DesignationOfBlock)) 
                ? designation.Designation 
                : block.ParametersToString();
        }

        public string GetDesignation(Product product)
        {
            return GetDesignation(product.ProductBlock);
        }

        #endregion

        #region Type

        private readonly ProductType _productTypeNotDef = new ProductType { Name = "Тип не определен" };

        public ProductType GetProductType(ProductBlock block)
        {
            var designations = _designationsOfProductTypes.Where(pd => pd.Parameters.AllContainsIn(block.Parameters, new ParameterComparer())).ToList();

            return designations.Any() 
                ? designations.OrderBy(x => x.Parameters.Count).Last().ProductType 
                : _productTypeNotDef;
        }

        public ProductType GetProductType(Product product)
        {
            return GetProductType(product.ProductBlock);
        }

        #endregion

    }
}
