using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// Хранилище параметров и т.д.
    /// </summary>
    public readonly struct Bank
    {
        public HashSet<ProductBlock> Blocks { get; }
        public HashSet<Parameter> Parameters { get; }
        public List<ProductRelation> Relations { get; }

        private readonly Dictionary<int, string> _specialDesignationsDictionary;

        public Bank(IEnumerable<ProductBlock> blocks, 
                    IEnumerable<Parameter> parameters, 
                    IEnumerable<ProductRelation> relations)
        {
            Blocks = blocks.ToHashSet();
            Parameters = parameters.ToHashSet();
            Relations = relations.ToList();

            _specialDesignationsDictionary = Blocks
                .Where(block => block.DesignationSpecial != null)
                .ToDictionary(block => block.GetHashCode(), block => block.DesignationSpecial);
        }

        /// <summary>
        /// Актуальные связи с дочерними продуктами.
        /// </summary>
        /// <param name="product">Родительский продукт.</param>
        /// <returns>Связи к дочерним продуктам.</returns>
        public IEnumerable<ProductRelation> GetActualRelationsToChildProducts(Product product)
        {
            return Relations
                .Where(relation => relation.ParentProductParameters.AllContainsIn(product.ProductBlock.Parameters));
        }

        public string GetBlockSpecialDesignation(int hash)
        {
            return _specialDesignationsDictionary.ContainsKey(hash) 
                ? _specialDesignationsDictionary[hash] 
                : null;
        }
    }
}