using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// ��������� ���������� � �.�.
    /// </summary>
    public readonly struct Bank
    {
        private readonly List<ProductRelation> _relations;
        private readonly Dictionary<int, string> _specialDesignationsDictionary;

        public HashSet<Parameter> Parameters { get; }

        public Bank(IEnumerable<Parameter> parameters,
            Dictionary<int, string> specialDesignationsDictionary, 
                    IEnumerable<ProductRelation> relations)
        {
            Parameters = parameters.ToHashSet();
            _relations = relations.ToList();
            _specialDesignationsDictionary = specialDesignationsDictionary;
        }

        /// <summary>
        /// ���������� ����� � ��������� ����������.
        /// </summary>
        /// <param name="product">������������ �������.</param>
        /// <returns>����� � �������� ���������.</returns>
        public IEnumerable<ProductRelation> GetActualRelationsToChildProducts(Product product)
        {
            return _relations
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