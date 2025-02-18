using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Services.ProductDesignationService
{
    public class ProductDesignator : IProductDesignationService
    {
        private readonly List<ProductCategory> _productCategories;
        private readonly List<ProductTypeDesignation> _designationsOfProductTypes;
        private readonly List<DesignationOfBlock> _designationsOfBlocks = new List<DesignationOfBlock>();

        public ProductDesignator(IUnitOfWork unitOfWork)
        {
            //загрузка всех категорий оборудования
            _productCategories = unitOfWork.Repository<ProductCategory>().GetAll().OrderByDescending(productCategory => productCategory.Parameters.Count).ToList();

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
            _designationsOfBlocks = _designationsOfBlocks.OrderByDescending(designationOfBlock => designationOfBlock.Parameters.Count()).ToList();
        }

        #region Designation

        readonly Dictionary<Guid, string> _dictionaryBlockDesignations = new Dictionary<Guid, string>();
        public string GetDesignation(ProductBlock block)
        {
            if (block == null)
                return "block is null!";

            //если обозначение уже содержится в словаре
            if (_dictionaryBlockDesignations.ContainsKey(block.Id))
            {
                return _dictionaryBlockDesignations[block.Id];
            }

            if (!string.IsNullOrEmpty(block.DesignationSpecial))
            {
                _dictionaryBlockDesignations.Add(block.Id, block.DesignationSpecial);
                return _dictionaryBlockDesignations[block.Id];
            }

            if (!block.Parameters.Any())
            {
                _dictionaryBlockDesignations.Add(block.Id, "block has no parameters!");
                return _dictionaryBlockDesignations[block.Id];
            }

            var designation = _designationsOfBlocks.FirstOrDefault(desOfBlock => desOfBlock.Parameters.AllContainsIn(block.Parameters, new ParameterComparer()));

            var result = !Equals(designation, default(DesignationOfBlock)) 
                ? designation.Designation 
                : block.ParametersToString();

            _dictionaryBlockDesignations.Add(block.Id, result);

            return result;
        }

        public string GetDesignation(Product product)
        {
            return GetDesignation(product.ProductBlock);
        }

        #endregion

        #region Type

        private readonly ProductType _emptyProductType = new ProductType { Name = "Тип не определен" };
        readonly Dictionary<Guid, ProductType> _dictionaryBlockTypes = new Dictionary<Guid, ProductType>();

        public ProductType GetProductType(ProductBlock block)
        {
            if (_dictionaryBlockTypes.ContainsKey(block.Id))
                return _dictionaryBlockTypes[block.Id];

            var designations = _designationsOfProductTypes.Where(pd => pd.Parameters.AllContainsIn(block.Parameters, new ParameterComparer())).ToList();

            var result = designations.Any() 
                ? designations.OrderBy(x => x.Parameters.Count).Last().ProductType 
                : _emptyProductType;

            _dictionaryBlockTypes.Add(block.Id, result);

            return result;
        }

        public ProductType GetProductType(Product product)
        {
            return GetProductType(product.ProductBlock);
        }

        #endregion

        #region Category

        private readonly ProductCategory _emptyCategory = new ProductCategory { IsStub = true, NameFull = "Категория не найдена", NameShort = "no" };
        readonly Dictionary<Guid, ProductCategory> _dictionaryBlockCategories = new Dictionary<Guid, ProductCategory>();

        public ProductCategory GetProductCategory(Product product)
        {
            if (_dictionaryBlockCategories.ContainsKey(product.ProductBlock.Id))
                return _dictionaryBlockCategories[product.ProductBlock.Id];

            var result = _productCategories.FirstOrDefault(category => category.Parameters.AllContainsInById(product.ProductBlock.Parameters)) ?? _emptyCategory;

            _dictionaryBlockCategories.Add(product.ProductBlock.Id, result);

            return result;
        }

        #endregion
    }
}
