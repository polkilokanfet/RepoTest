using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.GetProductService.Complects;
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
            //�������������� ��������� �������
            var selectedProduct = originProduct == null
                ? null
                : bank.Products.Single(product => product.Id == originProduct.Id);

            var productSelector = new ProductSelector(bank, bank.Parameters, selectedProduct);
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = owner };
            window.ShowDialog();

            //�������, ���� ������������ ������� ����� ��������.
            if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return originProduct;

            //���� ���������� ������� ��������
            if (window.ShouldSelectComplect)
            {
                return Container.Resolve<IGetProductService>().GetComplect(originProduct);
            }

            var result = productSelector.SelectedProduct;
            productSelector.Dispose();

            //�������� ���������� ���������
            //���� ���������� �������� ��� � ����
            if (((IProductRepository)UnitOfWork.Repository<Product>()).CanAdd(result).OperationCompletedSuccessfully)
            {
                SaveProduct(result);
            }

            return result;
        }

        public Product GetProduct(IUnitOfWork unitOfWork, Product product)
        {
            //�������� ���������� ���������
            var products = unitOfWork.Repository<Product>().GetAll();

            var result = products.SingleOrDefault(x => x.Equals(product));
            if (result != null) return result;

            //���� ���������� �������� ��� � ����, ��������� ���
            result = product;
            this.SaveProduct(result);

            return result;
        }


        private void SaveProduct(Product product)
        {
            var products = UnitOfWork.Repository<Product>().GetAll();

            SubstitutionDependentProducts(product, products);

            var operationResult = UnitOfWork.SaveEntity(product);

            if (operationResult.OperationCompletedSuccessfully)
            {
                Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductEvent>().Publish(product);
            }
            else
            {
                throw new Exception("������ ��� ���������� ������ �������� � ���� ������.", operationResult.Exception);
            }
        }

        public Product GetComplect(Product originProduct = null)
        {
            var complectViewModel = Container.Resolve<ComplectsViewModel>();
            complectViewModel.ShowDialog();

            return complectViewModel.IsSelected
                ? complectViewModel.SelectedItem.Product
                : originProduct;
        }


        /// <summary>
        /// ������ ����� ��������� �� ���������� ��������
        /// </summary>
        /// <param name="product"></param>
        /// <param name="savedProducts">����������� ��������</param>
        private void SubstitutionDependentProducts(Product product, ICollection<Product> savedProducts)
        {
            //��� ������� ����������� ��������
            foreach (var dependentProduct in product.DependentProducts)
            {
                //���� ������� ���� � �����������, ������ ���
                if (savedProducts.Contains(dependentProduct.Product))
                {
                    dependentProduct.Product = savedProducts.Single(product1 => product1.Equals(dependentProduct.Product));
                }

                SubstitutionDependentProducts(dependentProduct.Product, savedProducts);
            }
        }

        public ProductBlock GetProductBlock(ProductBlock originProductBlock = null, IEnumerable<Parameter> requiredParameters = null)
        {
            var bank = _bankFactory.CreateBank(requiredParameters?.Select(parameter => UnitOfWork.Repository<Parameter>().GetById(parameter.Id)));
            
            //�������������� ��������� ���� ��������
            var selectedProductBlock = originProductBlock == null
                ? null
                : bank.Blocks.Single(block => block.Id == originProductBlock.Id);

            var productBlockSelector = new ProductBlockSelector(bank.Parameters, bank, selectedProductBlock);
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
                }
                else
                {
                    throw new Exception("������ ��� ���������� ������ ����� �������� � ���� ������.");
                }
            }

            return result;
        }

        public ProductBlock GetProductBlock(IEnumerable<IParametersContainer> parametersContainers, ProductBlock originProductBlock = null)
        {
            var designDepartmentParametersAddedBlocksEnumerable = parametersContainers.ToList();
            var banks = designDepartmentParametersAddedBlocksEnumerable
                .Select(x => _bankFactory.CreateBank(x.Parameters.Select(p => UnitOfWork.Repository<Parameter>().GetById(p.Id))))
                .ToList();
            var bankParameters = banks.SelectMany(x => x.Parameters).Distinct().ToList();

            //������� �� ����� ������������ ���������� ��, ����� ������������ ����������
            var reqParIds = designDepartmentParametersAddedBlocksEnumerable
                .SelectMany(x => x.Parameters)
                .Select(x => x.Id).Distinct().ToList();
            var reqGrpIds = designDepartmentParametersAddedBlocksEnumerable
                .SelectMany(x => x.Parameters)
                .Select(x => x.ParameterGroup.Id).Distinct().ToList();
            foreach (var parameter in bankParameters.ToList())
            {
                if (reqParIds.Contains(parameter.Id))
                {
                    continue;
                }

                if (reqGrpIds.Contains(parameter.ParameterGroup.Id))
                {
                    bankParameters.Remove(parameter);
                }
            }

            var firstBank = banks.First();
            var bank = new Bank(firstBank.Products, firstBank.Blocks, bankParameters, firstBank.Relations);

            //�������������� ��������� ���� ��������
            ProductBlock selectedProductBlock = originProductBlock == null
                ? null
                : bank.Blocks.Single(block => block.Id == originProductBlock.Id);

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
                }
                else
                {
                    throw new Exception("������ ��� ���������� ������ ����� �������� � ���� ������.");
                }
            }

            return result;
        }
    }

}