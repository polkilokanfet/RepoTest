using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Services.ProductDesignationService;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductDesignationService _designator;
        private readonly IEventAggregator _eventAggregator;
        private Bank _bank;

        public GetProductServiceWpf(IUnityContainer container)
        {
            _container = container;
            _unitOfWork = container.Resolve<IUnitOfWork>();
            _designator = container.Resolve<IProductDesignationService>();
            _eventAggregator = container.Resolve<IEventAggregator>();
        }

        public async Task LoadAsync()
        {
            var parameters = await _unitOfWork.Repository<Parameter>().GetAllAsync();
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            var productRelations = await _unitOfWork.Repository<ProductRelation>().GetAllAsync();
            var productBlocks = await _unitOfWork.Repository<ProductBlock>().GetAllAsync();

            _bank = new Bank(products, productBlocks, parameters, productRelations, _container.Resolve<IProductDesignationService>());
        }

        public async Task<Product> GetProductAsync(Product originProduct = null)
        {
            await LoadAsync();

            var selectedProduct = originProduct == null ? null : _bank.Products.Single(x => x.Id == originProduct.Id);

            var productSelector = new ProductSelector(_bank, _bank.Parameters, selectedProduct);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = Application.Current.MainWindow };
            window.ShowDialog();

            //���� ���������� ������� ����� �������
            if (window.ShoodCreateNew)
            {
                return (await CreateNewProduct()) ?? originProduct;
            }

            //�������, ���� ������������ ������� ����� ��������.
            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return originProduct;

            var result = productSelector.SelectedProduct;

            //��������� ������ ���������� �����
            //var blocks = result.GetBlocks().Distinct().ToList();
            //SubstitutionBlocks(result, blocks);

            //�������� ���������� ���������
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            //���� ���������� �������� ��� � ����
            if (!products.Contains(result))
            {
                SubstitutionProducts(result, products);
                _unitOfWork.Repository<Product>().Add(result);
                await _unitOfWork.SaveChangesAsync();
                _eventAggregator.GetEvent<AfterSaveProductEvent>().Publish(result);
            }

            return result;
        }

        /// <summary>
        /// ������� ������ �� ����������
        /// </summary>
        /// <param name="product">������� �������</param>
        /// <param name="uniqBlocks">���������� �����</param>
        private void SubstitutionBlocks(Product product, ICollection<ProductBlock> uniqBlocks)
        {
            if (uniqBlocks.Contains(product.ProductBlock))
            {
                product.ProductBlock = uniqBlocks.Single(x => x.Equals(product.ProductBlock));
            }

            foreach (var dependentProduct in product.DependentProducts)
            {
                SubstitutionBlocks(dependentProduct.Product, uniqBlocks);
            }
        }

        private void SubstitutionProducts(Product product, ICollection<Product> uniqProducts)
        {
            foreach (var dependentProduct in product.DependentProducts)
            {
                if (uniqProducts.Contains(dependentProduct.Product))
                {
                    dependentProduct.Product = uniqProducts.Single(x => x.Equals(dependentProduct.Product));
                }

                SubstitutionProducts(dependentProduct.Product, uniqProducts);
            }
        }

        private async Task<Product> CreateNewProduct()
        {
            //������� �������
            var tsk = new CreateNewProductTask();

            //���� ������������ ������� �������� - ���������� null.
            if (!await _container.Resolve<IUpdateDetailsService>().UpdateDetails(tsk))
                return null;

            //������� ����� �������� ��� ��������� ��� � ����
            var parameter = new Parameter {Value = tsk.Designation };
            //���� ������� ������
            var parentGroup = _bank.Parameters.First(x => !x.ParameterRelations.Any()).ParameterGroup;
            //���� ������������ �������� ��� ����������
            var parentParameter = _bank.Parameters.Where(x => Equals(x.ParameterGroup, parentGroup))
                                                  .SingleOrDefault(x => x.Value == "����� ������������");

            //���� ������ ���, ������� ���
            if (parentParameter == null)
            {
                parentParameter = new Parameter {ParameterGroup = parentGroup, Value = "����� ������������"};
                parameter.ParameterGroup = new ParameterGroup {Name = "�����������"};
            }
            else
            {
                parameter.ParameterGroup = _bank.Parameters.Select(x => x.ParameterGroup).Distinct().Single(x => x.Name == "�����������");
            }
            parameter.ParameterRelations.Add(new ParameterRelation {ParameterId = parameter.Id, RequiredParameters = new List<Parameter> {parentParameter} });

            var product = new Product {ProductBlock = new ProductBlock {Parameters = new List<Parameter> {parentParameter, parameter} }};
            product.ProductBlock.StructureCostNumber = tsk.StructureCostNumber;
            product.ProductBlock.DesignationSpecial = tsk.Designation;

            tsk = await _unitOfWork.Repository<CreateNewProductTask>().GetByIdAsync(tsk.Id);
            tsk.Product = product;

            await _unitOfWork.SaveChangesAsync();

            return product;
        }
    }
}