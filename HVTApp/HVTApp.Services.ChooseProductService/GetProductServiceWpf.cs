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
            var parameters = await _unitOfWork.GetRepository<Parameter>().GetAllAsync();
            var products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            var productRelations = await _unitOfWork.GetRepository<ProductRelation>().GetAllAsync();
            var productBlocks = await _unitOfWork.GetRepository<ProductBlock>().GetAllAsync();

            _bank = new Bank(products, productBlocks, parameters, productRelations);
        }

        public async Task<Product> GetProductAsync(Product originProduct = null)
        {
            await LoadAsync();

            var selectedProduct = originProduct == null ? null : _bank.Products.Single(x => x.Id == originProduct.Id);

            var productSelector = new ProductSelector(_bank, _designator, _bank.Parameters, selectedProduct);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = Application.Current.MainWindow };
            window.ShowDialog();

            //если необходимо создать новый продукт
            if (window.ShoodCreateNew)
            {
                return (await CreateNewProduct()) ?? originProduct;
            }

            //выходим, если пользователь отменил выбор продукта.
            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return originProduct;

            var result = productSelector.SelectedProduct;

            //если выбранного продукта нет в базе
            if (!_bank.Products.Contains(result))
            {
                _unitOfWork.GetRepository<Product>().Add(result);
                await _unitOfWork.SaveChangesAsync();
                _eventAggregator.GetEvent<AfterSaveProductEvent>().Publish(result);
            }

            return result;
        }

        private async Task<Product> CreateNewProduct()
        {
            //создаем задание
            var block = new ProductBlock();
            var product = new Product {ProductBlock = block};
            var tsk = new CreateNewProductTask {Product = product};

            //если пользователь отменил создание - возвращаем null.
            if (!await _container.Resolve<IUpdateDetailsService>().UpdateDetails(tsk))
                return null;

            //создаем новый параметр дл€ включени€ его в базу
            var parameter = new Parameter {Value = tsk.Designation};
            //ищем главную группу
            var parentGroup = _bank.Parameters.First(x => !x.ParameterRelations.Any()).ParameterGroup;
            //ищем родительский параметр дл€ созданного
            var parentParameter = _bank.Parameters.Where(x => Equals(x.ParameterGroup, parentGroup))
                                                  .SingleOrDefault(x => x.Value == "Ќовое оборудование");

            //если такого нет, создаем его
            if (parentParameter == null)
            {
                parameter.ParameterGroup = new ParameterGroup {Name = "ќбозначение"};
                parentParameter = new Parameter {ParameterGroup = parentGroup, Value = "Ќовое оборудование"};
            }
            else
            {
                parentParameter.ParameterGroup = _bank.Parameters.Select(x => x.ParameterGroup).Distinct().Single(x => x.Name == "ќбозначение");
            }
            parameter.ParameterRelations.Add(new ParameterRelation {ParameterId = parameter.Id, RequiredParameters = new List<Parameter> {parentParameter} });

            block.StructureCostNumber = tsk.StructureCostNumber;
            block.Name = tsk.Designation;
            block.Parameters.Add(parameter);

            //_unitOfWork.GetRepository<CreateNewProductTask>().Add(tsk);
            await _unitOfWork.SaveChangesAsync();

            return product;
        }
    }
}