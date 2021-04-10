using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
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
                OnPropertyChanged();
                ((DelegateCommand) PrintProductBlockCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand PrintProductBlockCommand { get; }

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
                blocks = blocks.Where(block => requaredParametersLists.Any(rpl => rpl.Parameters.Select(p => p.Id).AllContainsIn(block.Parameters.Select(p => p.Id)))).ToArray();
            }
            var blockWrappers = blocks
                .OrderBy(productBlock => productBlock.Designation)
                .Select(productBlock => new ProductBlockStructureCostWrapper(productBlock));
            ProductBlocks = new ValidatableChangeTrackingCollection<ProductBlockStructureCostWrapper>(blockWrappers);

            //сохранение
            SaveCommand = new DelegateCommand(
                () =>
                {
                    ProductBlocks.AcceptChanges();
                    unitOfWork.SaveChanges();
                },
                () => ProductBlocks != null && ProductBlocks.IsValid && ProductBlocks.IsChanged);

            //отмена изменений
            CancelCommand = new DelegateCommand(() =>
            {
                ProductBlocks.RejectChanges();
            },
            () => ProductBlocks != null && ProductBlocks.IsChanged);

            //печать блока в контексте оборудования
            PrintProductBlockCommand = new DelegateCommand(
                () =>
                {
                    var block = SelectedProductBlock;
                    var products = unitOfWork.Repository<Product>().GetAll();
                    products = products.Where(product => product.GetBlocks().Contains(block.Model)).Distinct().ToArray();
                    container.Resolve<IPrintProductService>().PrintProducts(products, block.Model);
                },
                () => SelectedProductBlock != null);

            //подписка на изменение параметров
            ProductBlocks.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) CancelCommand).RaiseCanExecuteChanged();
            };


        }
    }
}