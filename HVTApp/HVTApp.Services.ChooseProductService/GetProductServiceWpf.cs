using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.GetProductService.Complects;
using HVTApp.Services.ProductDesignationService;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventAggregator _eventAggregator;
        private Bank _bank;

        public GetProductServiceWpf(IUnityContainer container)
        {
            _container = container;
            _unitOfWork = container.Resolve<IUnitOfWork>();
            _eventAggregator = container.Resolve<IEventAggregator>();
        }

        public void Load(Product originProduct)
        {
            var parameters = _unitOfWork.Repository<Parameter>().GetAll();
            var products = _unitOfWork.Repository<Product>().GetAll();
            var productRelations = _unitOfWork.Repository<ProductRelation>().GetAll();
            var productBlocks = _unitOfWork.Repository<ProductBlock>().GetAll();

            _bank = new Bank(products, productBlocks, ParametersWithoutComplectsParameters(parameters, originProduct), productRelations);
        }

        /// <summary>
        /// ��������� ��� ���������� "������� � ����������"
        /// </summary>
        /// <param name="parameters1"></param>
        private List<Parameter> ParametersWithoutComplectsParameters(IEnumerable<Parameter> parameters1, Product selectedProduct)
        {
            var parameters = parameters1.ToList();

            //�������� "����������� ���������"
            var complectDesignationParameters = parameters.Where(x => x.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectDesignationGroup.Id).ToList();

            //��������� "��� ���������"
            var complectTypeParameters = parameters.Where(x => x.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectsGroup.Id).ToList();

            var parametersToExclude = complectTypeParameters.Union(complectDesignationParameters).ToList();

            //�������� "��������� � ������"
            var complectsParameter = parameters.SingleOrDefault(x => x.Id == GlobalAppProperties.Actual.ComplectsParameter.Id);
            if (complectsParameter != null)
                parametersToExclude.Add(complectsParameter);

            if (selectedProduct != null)
            {
                var ids = selectedProduct.ProductBlock.Parameters.Select(x => x.Id).ToList();
                parametersToExclude = parametersToExclude.Where(x => !ids.Contains(x.Id)).ToList();
            }

            return parameters.Except(parametersToExclude).ToList();
        }

        public Product GetProduct(Product originProduct = null)
        {
            Load(originProduct);

            var selectedProduct =
                originProduct == null
                    ? null
                    : _bank.Products.Single(x => x.Id == originProduct.Id);

            var productSelector = new ProductSelector(_bank, _bank.Parameters, selectedProduct);
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = owner };
            window.ShowDialog();

            //���� ���������� ������� ����� �������
            if (window.ShoodCreateNew)
            {
                var productNew = _container.Resolve<INewProductService>().GetNewProduct();
                return productNew ?? originProduct;
            }

            //���� ���������� ������� ��������
            if (window.ShoodSelectComplect)
            {
                var complectViewModel = _container.Resolve<ComplectsViewModel>();
                complectViewModel.ShowDialog();
                return complectViewModel.IsSelected 
                    ? complectViewModel.SelectedItem.Product 
                    : originProduct;
            }

            //�������, ���� ������������ ������� ����� ��������.
            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return originProduct;

            var result = productSelector.SelectedProduct;

            //��������� ������ ���������� �����
            //var blocks = result.GetBlocks().Distinct().ToList();
            //SubstitutionBlocks(result, blocks);

            //�������� ���������� ���������
            var products = _unitOfWork.Repository<Product>().GetAll();
            //���� ���������� �������� ��� � ����
            if (!products.Contains(result))
            {
                SubstitutionProducts(result, products);
                _unitOfWork.Repository<Product>().Add(result);
                _unitOfWork.SaveChanges();
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
    }
}