using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.GetProductService.Kits;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private IUnityContainer Container { get; }
        private IUnitOfWork UnitOfWork { get; }

        private readonly BankFactory _bankFactory;

        public GetProductServiceWpf(IUnityContainer container)
        {
            Container = container;
            UnitOfWork = container.Resolve<IUnitOfWork>();
            _bankFactory = new BankFactory(UnitOfWork);
        }

        public Product GetProduct(Product originProduct = null)
        {
            return this.GetProduct(_bankFactory.CreateBank(originProduct), originProduct);
        }

        public Product GetProduct(IEnumerable<Parameter> requiredParameters)
        {
            return this.GetProduct(_bankFactory.CreateBank(requiredParameters.ChangeUnitOfWork(UnitOfWork)));
        }

        private Product GetProduct(Bank bank, Product originProduct = null)
        {
            try
            {
                //�������������� ��������� �������
                var selectedProduct = originProduct?.ChangeUnitOfWork(UnitOfWork);

                var productSelector = new ProductSelector(bank, bank.Parameters, selectedProduct);
                var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                var window = new SelectProductWindow { DataContext = productSelector, Owner = owner };
                window.ShowDialog();

                //���� ���������� ������� ��������
                if (window.ShouldSelectComplect)
                {
                    return Container.Resolve<IGetProductService>().GetKit(originProduct);
                }

                //�������, ���� ������������ ������� ����� ��������.
                if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return originProduct;

                var result = productSelector.SelectedProduct;
                productSelector.Dispose();

                //���� ���������� �������� ��� � ����
                if (((IProductRepository)UnitOfWork.Repository<Product>()).CanAdd(result).OperationCompletedSuccessfully)
                {
                    SaveProduct(result);
                }
                else
                {
                    var result1 = result;
                    result = UnitOfWork.Repository<Product>().FindAsNoTracking(x => x.Equals(result1)).Single();
                }

                return result;

            }
            catch (DependencyParameterException e)
            {
                Container.Resolve<IMessageService>().Message("Exception", e.Message);
                return this.GetProduct(originProduct: null);
            }
            catch (Exception e)
            {
                Container.Resolve<IMessageService>().Message("Exception", e.Message);
                return this.GetProduct(originProduct: null);
            }
        }

        public Product GetProduct(IUnitOfWork unitOfWork, Product product)
        {
            //�������� ���������� ���������
            var products = unitOfWork.Repository<Product>().GetAll();

            var result = products.SingleOrDefault(x => x.Equals(product));
            if (result != null) return result;

            //���� ���������� �������� ��� � ����, ��������� ���
            result = product;
            this.SaveProduct(result, unitOfWork);

            return result;
        }


        private void SaveProduct(Product product, IUnitOfWork unitOfWork = null)
        {
            unitOfWork = unitOfWork ?? UnitOfWork;
            var products = unitOfWork.Repository<Product>().GetAll();
            var blocks = unitOfWork.Repository<ProductBlock>().GetAll();

            SubstitutionBlocksAndProducts(product, products, blocks);

            var operationResult = unitOfWork.SaveEntity(product);

            if (operationResult.OperationCompletedSuccessfully == false)
                throw new Exception("������ ��� ���������� ������ �������� � ���� ������.", operationResult.Exception);

            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductEvent>().Publish(product);
        }

        public Product GetKit(Product originProduct = null)
        {
            var kitsViewModel = Container.Resolve<KitsViewModel>();
            kitsViewModel.Load();
            return GetKitBase(kitsViewModel, originProduct);
        }

        public Product GetKit(DesignDepartment designDepartment, Product originProduct = null)
        {
            var kitsViewModel = Container.Resolve<KitsViewModel>();
            kitsViewModel.Load(designDepartment);
            return GetKitBase(kitsViewModel, originProduct);
        }

        private Product GetKitBase(KitsViewModel kitsViewModel, Product originProduct = null)
        {
            kitsViewModel.ShowDialog();

            return kitsViewModel.IsSelected
                ? kitsViewModel.SelectedItem.Product
                : originProduct;
        }


        /// <summary>
        /// ������ ����� ������ � ��������� �� �����������
        /// </summary>
        /// <param name="product"></param>
        /// <param name="savedProducts">����������� ��������</param>
        /// <param name="savedBlocks"></param>
        private void SubstitutionBlocksAndProducts(Product product, ICollection<Product> savedProducts, ICollection<ProductBlock> savedBlocks)
        {
            //������ ������ �� �����������
            var block = savedBlocks.SingleOrDefault(productBlock => product.ProductBlock.Equals(productBlock));
            if (block != null)
            {
                product.ProductBlock = block;
            }
            else
            {
                savedBlocks.Add(product.ProductBlock);
            }

            //��� ������� ����������� ��������
            foreach (var dependentProduct in product.DependentProducts)
            {
                var savedProduct = savedProducts.SingleOrDefault(product1 => product1.Equals(dependentProduct.Product));
                //���� ������� ���� � �����������, ������ ���
                if (savedProduct != null)
                {
                    dependentProduct.Product = savedProduct;
                }
                else
                {
                    savedProducts.Add(dependentProduct.Product);
                }

                SubstitutionBlocksAndProducts(dependentProduct.Product, savedProducts, savedBlocks);
            }
        }

        public ProductBlock GetProductBlock(ProductBlock originProductBlock = null, IEnumerable<Parameter> requiredParameters = null)
        {
            var bank = _bankFactory.CreateBank(requiredParameters?.ChangeUnitOfWork(UnitOfWork));
            
            //�������������� ��������� ���� ��������
            var selectedProductBlock = originProductBlock?.ChangeUnitOfWork(UnitOfWork);

            var productBlockSelector = new ProductBlockSelector(bank.Parameters, bank, selectedProductBlock);
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductBlockWindow { DataContext = productBlockSelector, Owner = owner };
            window.ShowDialog();

            //�������, ���� ������������ ������� ����� ����� ��������.
            if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return originProductBlock;

            var result = productBlockSelector.SelectedBlock;

            //�������� ���������� ������ ���������
            var productBlocks = UnitOfWork.Repository<ProductBlock>().GetAll();
            //���� ���������� ����� �������� ��� � ����
            if (productBlocks.Contains(result) == false)
            {
                if (UnitOfWork.SaveEntity(result).OperationCompletedSuccessfully)
                {
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductBlockEvent>().Publish(result);
                    return result;
                }
                else
                {
                    throw new Exception("������ ��� ���������� ������ ����� �������� � ���� ������.");
                }
            }

            return productBlocks.Single(x => x.Equals(result));
        }

        public ProductBlock GetProductBlock(IEnumerable<IParametersContainer> parametersContainers, ProductBlock originProductBlock = null)
        {
            var parameterContainers = parametersContainers as IParametersContainer[] ?? parametersContainers.ToArray();
            var banks = parameterContainers
                .Select(x => _bankFactory.CreateBank(x.Parameters.ChangeUnitOfWork(UnitOfWork)))
                .ToList();

            //������������ ��������� � �������
            var requiredParameters = parameterContainers
                .SelectMany(x => x.Parameters)
                .Distinct()
                .ToList();

            //������� �� ����� ������������ ���������� ��, ����� ������������ ����������
            var bankParameters = banks
                .SelectMany(x => x.Parameters)
                .Distinct()
                .LeaveParametersAloneInGroup(requiredParameters)
                .RemoveUnreachable()
                .ToList();

            var bank = _bankFactory.CreateBank(bankParameters);

            //�������������� ��������� ���� ��������
            var selectedProductBlock = originProductBlock?.ChangeUnitOfWork(UnitOfWork);

            var productBlockSelector = new ProductBlockSelector(bank.Parameters, bank, selectedProductBlock);
            var originParameterSelector = productBlockSelector.ParameterSelectors.FirstOrDefault(x => x.ParametersFlaged.Any(p => p.Parameter.IsOrigin));
            if (originParameterSelector != null)
            {
                originParameterSelector.SelectedParameterFlaged = originParameterSelector.ParametersFlaged.First();
            }

            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductBlockWindow() { DataContext = productBlockSelector, Owner = owner };
            window.ShowDialog();

            //�������, ���� ������������ ������� ����� ����� ��������.
            if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return originProductBlock;

            var result = productBlockSelector.SelectedBlock;

            //�������� ���������� ������ ���������
            var productBlocks = UnitOfWork.Repository<ProductBlock>().GetAll();
            //���� ���������� ����� �������� ��� � ����
            if (productBlocks.Contains(result) == false)
            {
                if (UnitOfWork.SaveEntity(result).OperationCompletedSuccessfully)
                {
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductBlockEvent>().Publish(result);
                    return result;
                }
                else
                {
                    throw new Exception("������ ��� ���������� ������ ����� �������� � ���� ������.");
                }
            }

            return productBlocks.Single(x => x.Equals(result));
        }

        public IEnumerable<ProductBlock> GenerateBlocks()
        {
            var parameters = this.Container.Resolve<IUnitOfWork>().Repository<Parameter>().GetAll();
            var nodes = PathNodesGenerator.GetPathNodes(parameters);
            return PathNodesGenerator.GetAllBlocks(nodes).Distinct();
        }
    }
}