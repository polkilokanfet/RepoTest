using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Products.ViewModels
{
    public class StructureCostsViewModel : BindableBase
    {
        private ProductBlockStructureCostWrapper _selectedProductBlock;
        public IValidatableChangeTrackingCollection<ProductBlockStructureCostWrapper> ProductBlocks { get; }

        public ProductBlockStructureCostWrapper SelectedProductBlock
        {
            get => _selectedProductBlock;
            set
            {
                _selectedProductBlock = value;
                RaisePropertyChanged();
                PrintProductBlockCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateLogCommand SaveCommand { get; }
        public DelegateLogCommand CancelCommand { get; }
        public DelegateLogCommand PrintProductBlockCommand { get; }

        public StructureCostsViewModel(IUnityContainer container)
        {
            var unitOfWork = container.Resolve<IUnitOfWork>();

            var requaredParametersLists = GlobalAppProperties.User.RoleCurrent == Role.Constructor
                ? unitOfWork.Repository<ConstructorsParameters>().Find(x => x.Constructors.ContainsById(GlobalAppProperties.User)).SelectMany(x => x.PatametersLists).ToList()
                : new List<ConstructorParametersList>();

            //загружаем блоки
            var blocks = unitOfWork.Repository<ProductBlock>().Find(productBlock => !productBlock.IsService && !productBlock.IsNew);
            if (requaredParametersLists.Any())
            {
                blocks = blocks.Where(block => requaredParametersLists.Any(rpl => rpl.Parameters.Select(p => p.Id).AllContainsIn(block.Parameters.Select(p => p.Id)))).ToList();
            }
            var blockWrappers = blocks
                .OrderBy(x => x.Designation)
                .Select(x => new ProductBlockStructureCostWrapper(x));
            ProductBlocks = new ValidatableChangeTrackingCollection<ProductBlockStructureCostWrapper>(blockWrappers);

            //сохранение
            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    ProductBlocks.AcceptChanges();
                    unitOfWork.SaveChanges();
                },
                () => ProductBlocks != null && ProductBlocks.IsValid && ProductBlocks.IsChanged);

            //отмена изменений
            CancelCommand = new DelegateLogCommand(() =>
            {
                ProductBlocks.RejectChanges();
            },
            () => ProductBlocks != null && ProductBlocks.IsChanged);

            //печать блока в контексте оборудования
            PrintProductBlockCommand = new DelegateLogCommand(
                () =>
                {
                    var block = SelectedProductBlock;
                    var products = unitOfWork.Repository<Product>().GetAll();
                    products = products.Where(x => x.GetBlocks().Contains(block.Model)).Distinct().ToList();
                    container.Resolve<IPrintProductService>().PrintProducts(products, block.Model);
                },
                () => SelectedProductBlock != null);

            //подписка на изменение параметров
            ProductBlocks.PropertyChanged += (sender, args) =>
            {
                SaveCommand.RaiseCanExecuteChanged();
                CancelCommand.RaiseCanExecuteChanged();
            };


        }
    }
}