using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.ProductDesignationService;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// Хранилище параметров и т.д.
    /// </summary>
    public class Bank
    {
        public List<Product> Products { get; }
        public List<ProductBlock> Blocks { get; }
        public List<Parameter> Parameters { get; }
        public List<ProductRelation> Relations { get; }
        public IProductDesignationService Designator { get; }

        public Bank(List<Product> products, 
                    List<ProductBlock> blocks, 
                    List<Parameter> parameters, 
                    List<ProductRelation> relations, 
                    IProductDesignationService designator)
        {
            Products = products;
            Blocks = blocks;
            Parameters = parameters;
            Relations = relations;
            Designator = designator;
        }

        /// <summary>
        /// Возвращает продукт, либо создает новый.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="productsDependent"></param>
        /// <returns></returns>
        public Product GetProduct(ProductBlock block, IEnumerable<ProductDependent> productsDependent)
        {
            var product = new Product
            {
                ProductBlock = block,
                DependentProducts = productsDependent.ToList()
            };
            //если такой продукт существует - возвращаем его
            var existsProduct = Products.SingleOrDefault(x => x.Equals(product));
            if (existsProduct != null) return existsProduct;

            //если продукт еще не существовал
            //обозначение и тип нового продукта
            product.Designation = Designator.GetDesignation(product);
            product.ProductType = Designator.GetProductType(product);

            Products.Add(product);
            return product;
        }

        public ProductBlock GetBlock(IEnumerable<Parameter> parameters)
        {

            //создание нового блока
            var block = new ProductBlock { Parameters = parameters.ToList() };

            //поиск в существующих блоках
            var exist = Blocks.SingleOrDefault(x => x.Equals(block));
            if (exist != null)
            {
                return exist;
            }

            block.Designation = Designator.GetDesignation(block);
            block.StructureCostNumber = "blank";

            //добавление блока в банк
            Blocks.Add(block);
            return block;

        }

        /// <summary>
        /// Актуальные связи.
        /// </summary>
        /// <param name="product">Родительский продукт.</param>
        /// <returns>Связи к дочерним продуктам.</returns>
        public List<ProductRelation> RelationsToChildProducts(Product product)
        {
            return Relations.Where(x => x.ParentProductParameters.AllContainsIn(product.ProductBlock.Parameters)).ToList();
        }

    }
}