using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// ’ранилище параметров и т.д.
    /// </summary>
    public class Bank
    {
        public List<Product> Products { get; }
        public List<ProductBlock> Blocks { get; }
        public List<Parameter> Parameters { get; }
        public List<ProductRelation> Relations { get; }

        public Bank(List<Product> products, 
                    List<ProductBlock> blocks, 
                    List<Parameter> parameters, 
                    List<ProductRelation> relations)
        {
            Products = products;
            Blocks = blocks;
            Parameters = parameters;
            Relations = relations;
        }

        /// <summary>
        /// ¬озвращает продукт, либо создает новый.
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

            //добавление блока в банк
            Blocks.Add(block);
            return block;

        }

        /// <summary>
        /// јктуальные св€зи.
        /// </summary>
        /// <param name="product">–одительский продукт.</param>
        /// <returns>—в€зи к дочерним продуктам.</returns>
        public List<ProductRelation> RelationsToChildProducts(Product product)
        {
            return Relations.Where(x => x.ParentProductParameters.AllContainsIn(product.ProductBlock.Parameters)).ToList();
        }

    }
}