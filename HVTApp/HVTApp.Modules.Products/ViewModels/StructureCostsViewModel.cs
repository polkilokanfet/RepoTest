using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Modules.Products.ViewModels
{
    public class StructureCostsViewModel : BindableBase
    {
        private ProductBlockWrapper _selectedProductBlock;
        public IValidatableChangeTrackingCollection<ProductBlockWrapper> ProductBlocks { get; }

        public ProductBlockWrapper SelectedProductBlock
        {
            get { return _selectedProductBlock; }
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

            //загружаем блоки
            var blockWrappers = unitOfWork.Repository<ProductBlock>()
                //.Find(x => string.IsNullOrWhiteSpace(x.StructureCostNumber))
                .Find(x => true).OrderBy(x => x.Designation)
                .Select(x => new ProductBlockWrapper(x));
            ProductBlocks = new ValidatableChangeTrackingCollection<ProductBlockWrapper>(blockWrappers);

            //сохранение
            SaveCommand = new DelegateCommand(async () =>
            {
                ProductBlocks.AcceptChanges();
                await unitOfWork.SaveChangesAsync();
            },
            () => ProductBlocks != null && ProductBlocks.IsValid && ProductBlocks.IsChanged);

            //отмена изменений
            CancelCommand = new DelegateCommand(() =>
            {
                ProductBlocks.RejectChanges();
            },
            () => ProductBlocks != null && ProductBlocks.IsChanged);

            //печать блока в контексте оборудования
            PrintProductBlockCommand = new DelegateCommand(async () =>
            {
                var block = SelectedProductBlock;
                var products = await unitOfWork.Repository<Product>().GetAllAsync();
                products = products.Where(x => x.GetBlocks().Contains(block.Model)).Distinct().ToList();
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