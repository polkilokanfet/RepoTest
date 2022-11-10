using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// ��������� ���������� � �.�.
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
        /// ���������� �������, ���� ������� �����.
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
            //���� ����� ������� ���������� - ���������� ���
            var existsProduct = Products.SingleOrDefault(x => x.Equals(product));
            if (existsProduct != null)
                return existsProduct;

            Products.Add(product);
            return product;
        }

        public ProductBlock GetBlock(IEnumerable<Parameter> parameters)
        {
            //�������� ������ �����
            var block = new ProductBlock { Parameters = parameters.ToList() };

            //����� � ������������ ������
            var existsBlock = Blocks.SingleOrDefault(x => x.Equals(block));
            if (existsBlock != null)
            {
                return existsBlock;
            }

            //���������� ����� � ����
            Blocks.Add(block);
            return block;
        }

        /// <summary>
        /// ���������� ����� � ��������� ����������.
        /// </summary>
        /// <param name="product">������������ �������.</param>
        /// <returns>����� � �������� ���������.</returns>
        public IEnumerable<ProductRelation> GetActualRelationsToChildProducts(Product product)
        {
            return Relations
                .Where(relation => relation.ParentProductParameters.AllContainsIn(product.ProductBlock.Parameters));
        }
    }
}