using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Services.ProductDesignationService;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// Хранилище параметров и т.д.
    /// </summary>
    public class Bank
    {
        public Bank(List<Product> products, 
                    List<ProductBlock> blocks, 
                    List<Parameter> parameters, 
                    List<ProductRelation> productRelations, 
                    IProductDesignationService designator)
        {
            Products = products;
            Blocks = blocks;
            Parameters = parameters;
            ProductRelations = productRelations;
            Designator = designator;
        }

        public List<Product> Products { get; }
        public List<ProductBlock> Blocks { get; }
        public List<Parameter> Parameters { get; }
        public List<ProductRelation> ProductRelations { get; }
        public IProductDesignationService Designator { get; }
    }
}