using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// Хранилище параметров и т.д.
    /// </summary>
    public class Bank
    {
        public Bank(List<Product> products, List<ProductBlock> productBlocks, List<Parameter> parameters, List<ProductRelation> productRelations)
        {
            Products = products;
            ProductBlocks = productBlocks;
            Parameters = parameters;
            ProductRelations = productRelations;
        }

        public List<Product> Products { get; }
        public List<ProductBlock> ProductBlocks { get; }
        public List<Parameter> Parameters { get; }
        public List<ProductRelation> ProductRelations { get; }
    }
}