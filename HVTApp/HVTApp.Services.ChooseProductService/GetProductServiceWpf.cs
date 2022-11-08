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
            var bank = _bankFactory.CreateBank(originProduct);

            //�������������� ��������� �������
            var selectedProduct = originProduct == null 
                ? null 
                : bank.Products.Single(product => product.Id == originProduct.Id);

            var productSelector = new ProductSelector(bank, bank.Parameters, selectedProduct);
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = owner };
            window.ShowDialog();

            //���� ���������� ������� ����� �������
            if (window.ShoodCreateNew)
            {
                var productNew = Container.Resolve<INewProductService>().GetNewProduct();
                return productNew ?? originProduct;
            }

            //���� ���������� ������� ��������
            if (window.ShoodSelectComplect)
            {
                return Container.Resolve<IGetProductService>().GetComplect(originProduct);
            }

            //�������, ���� ������������ ������� ����� ��������.
            if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return originProduct;

            var result = productSelector.SelectedProduct;
            productSelector.Dispose();

            //�������� ���������� ���������
            //���� ���������� �������� ��� � ����
            if (((IProductRepository)UnitOfWork.Repository<Product>()).CanAdd(result).OperationCompletedSuccessfully)
            {
                var products = UnitOfWork.Repository<Product>().GetAll();
                SubstitutionProducts(result, products);
                if (UnitOfWork.SaveEntity(result).OperationCompletedSuccessfully)
                {
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductEvent>().Publish(result);
                }
                else
                {
                    throw new Exception("������ ��� ���������� ������ �������� � ���� ������.");
                }
            }

            return result;
        }

        public Product GetProduct(IUnitOfWork unitOfWork, Product product)
        {
            //�������� ���������� ���������
            var products = unitOfWork.Repository<Product>().GetAll();

            var result = products.SingleOrDefault(x => x.Equals(product));
            if (result != null) return result;

            //���� ���������� �������� ��� � ����
            result = product;
            SubstitutionProducts(result, products);
            //SubstitutionBlocks(result, unitOfWork.Repository<ProductBlock>().GetAll());
            if (unitOfWork.SaveEntity(result).OperationCompletedSuccessfully)
            {
                Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductEvent>().Publish(result);
            }
            else
            {
                throw new Exception("������ ��� ���������� ������ �������� � ���� ������.");
            }

            return result;
        }

        public Product GetProduct(IEnumerable<Parameter> requiredParameters)
        {
            var bank = _bankFactory.CreateBank(requiredParameters.Select(x => UnitOfWork.Repository<Parameter>().GetById(x.Id)));

            //�������������� ��������� �������
            Product selectedProduct = null;

            var productSelector = new ProductSelector(bank, bank.Parameters);
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = owner };
            window.ShowDialog();


            //�������, ���� ������������ ������� ����� ��������.
            if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return null;

            var result = productSelector.SelectedProduct;
            productSelector.Dispose();

            //�������� ���������� ���������
            var products = UnitOfWork.Repository<Product>().GetAll();
            //���� ���������� �������� ��� � ����
            if (products.Contains(result) == false)
            {
                SubstitutionProducts(result, products);
                if (UnitOfWork.SaveEntity(result).OperationCompletedSuccessfully)
                {
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductEvent>().Publish(result);
                }
                else
                {
                    throw new Exception("������ ��� ���������� ������ �������� � ���� ������.");
                }
            }

            return result;
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
        /// <param name="uniqProducts"></param>
        private void SubstitutionProducts(Product product, ICollection<Product> uniqProducts)
        {
            foreach (var dependentProduct in product.DependentProducts)
            {
                if (uniqProducts.Contains(dependentProduct.Product))
                {
                    dependentProduct.Product = uniqProducts.Single(product1 => product1.Equals(dependentProduct.Product));
                }

                SubstitutionProducts(dependentProduct.Product, uniqProducts);
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