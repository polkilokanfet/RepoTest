using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Wrapper;
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

            //��������� �����
            var blockWrappers = unitOfWork.Repository<ProductBlock>()
                //.Find(x => string.IsNullOrWhiteSpace(x.StructureCostNumber))
                .Find(x => !x.IsService).OrderBy(x => x.Designation)
                .Select(x => new ProductBlockStructureCostWrapper(x));
            ProductBlocks = new ValidatableChangeTrackingCollection<ProductBlockStructureCostWrapper>(blockWrappers);

            //����������
            SaveCommand = new DelegateCommand(async () =>
            {
                ProductBlocks.AcceptChanges();
                await unitOfWork.SaveChangesAsync();
            },
            () => ProductBlocks != null && ProductBlocks.IsValid && ProductBlocks.IsChanged);

            //������ ���������
            CancelCommand = new DelegateCommand(() =>
            {
                ProductBlocks.RejectChanges();
            },
            () => ProductBlocks != null && ProductBlocks.IsChanged);

            //������ ����� � ��������� ������������
            PrintProductBlockCommand = new DelegateCommand(async () =>
            {
                var block = SelectedProductBlock;
                var products = await unitOfWork.Repository<Product>().GetAllAsync();
                products = products.Where(x => x.GetBlocks().Contains(block.Model)).Distinct().ToList();
                container.Resolve<IPrintProductService>().PrintProducts(products, block.Model);
            },
            () => SelectedProductBlock != null);

            //�������� �� ��������� ����������
            ProductBlocks.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) CancelCommand).RaiseCanExecuteChanged();
            };


        }
    }
}