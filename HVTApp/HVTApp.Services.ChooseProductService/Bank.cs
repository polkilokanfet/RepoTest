using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// ’ранилище параметров и т.д.
    /// </summary>
    public readonly struct Bank
    {
        public HashSet<Product> Products { get; }
        public HashSet<ProductBlock> Blocks { get; }
        public HashSet<Parameter> Parameters { get; }
        public HashSet<ProductRelation> Relations { get; }

        public Bank(IEnumerable<Product> products, 
                    IEnumerable<ProductBlock> blocks, 
                    IEnumerable<Parameter> parameters, 
                    IEnumerable<ProductRelation> relations)
        {
            Products = products.ToHashSet();
            Blocks = blocks.ToHashSet();
            Parameters = parameters.ToHashSet();
            Relations = relations.ToHashSet();
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
            if (existsProduct != null)
                return existsProduct;

            Products.Add(product);
            return product;
        }

        public ProductBlock GetBlock(IEnumerable<Parameter> parameters)
        {
            //создание нового блока
            var block = new ProductBlock { Parameters = parameters.ToList() };

            //поиск в существующих блоках
            var existsBlock = Blocks.SingleOrDefault(x => x.Equals(block));
            if (existsBlock != null)
            {
                return existsBlock;
            }

            //добавление блока в банк
            Blocks.Add(block);
            return block;
        }

        /// <summary>
        /// јктуальные св€зи с дочерними продуктами.
        /// </summary>
        /// <param name="product">–одительский продукт.</param>
        /// <returns>—в€зи к дочерним продуктам.</returns>
        public IEnumerable<ProductRelation> GetActualRelationsToChildProducts(Product product)
        {
            return Relations
                .Where(relation => relation.ParentProductParameters.AllContainsIn(product.ProductBlock.Parameters));
        }
    }
}