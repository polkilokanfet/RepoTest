using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Products.StructureCostsNumbers
{
    public class StructureCostsNumbersViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private StructureCostsNumber _selectedItem;

        public ObservableCollection<StructureCostsNumber> Items { get; private set; }

        public StructureCostsNumber SelectedItem
        {
            get => _selectedItem;
            set
            {
                this.SetProperty(ref _selectedItem, value, () =>
                {
                    if (value != null && value.IsChanged)
                        _selectedItem.RejectChanges();

                    this.ReplaceCommand.RaiseCanExecuteChanged();
                });
            }
        }

        public DelegateLogCommand SaveCommand { get; }

        public DelegateLogCommand ReplaceCommand { get; }

        public StructureCostsNumbersViewModel(IUnitOfWork unitOfWork, IMessageService messageService, IGetProductService productService)
        {
            _unitOfWork = unitOfWork;
            Load();

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    _selectedItem.AcceptChanges();
                    _unitOfWork.SaveChanges();
                    SaveCommand.RaiseCanExecuteChanged();
                },
                () => SelectedItem != null && SelectedItem.IsValid && SelectedItem.IsChanged);

            ReplaceCommand = new DelegateLogConfirmationCommand(
                messageService, 
                "Вы уверены, что хотите заменить этот комплект на другой (удалив этот)?",
                () =>
                {
                    var kit = productService.GetKit(SelectedItem.DepartmentsKits.First());
                    if (kit == null) return;
                    if (string.IsNullOrWhiteSpace(kit.ProductBlock.StructureCostNumber))
                    {
                        messageService.Message("Выберите комплект со стракчакостом");
                        return;
                    }

                    if (productService.ReplaceProduct(SelectedItem.ProductKit, kit))
                    {
                        kit = unitOfWork.Repository<Product>().GetById(kit.Id);
                        unitOfWork.Repository<ProductBlock>().Delete(kit.ProductBlock);
                        unitOfWork.Repository<Product>().Delete(kit);

                        Items.Remove(SelectedItem);
                        SelectedItem = null;
                    }
                },
                () => SelectedItem != null && SelectedItem.Model.IsKit);
        }

        private void Load()
        {
            var departments = _unitOfWork.Repository<DesignDepartment>()
                .Find(department => department.Head.Id == GlobalAppProperties.User.Id);

            var blocks = _unitOfWork.Repository<ProductBlock>()
                .Find(block => departments.Any(department => department.ProductBlockIsSuitable(block)))
                .OrderBy(block => block)
                .Select(block => new StructureCostsNumber(block));

            //ремонтные комплекты
            var kitDepartments = departments.Where(department => department.IsKitDepartment).ToList();

            var kits = new List<StructureCostsNumber>();
            foreach (var kitDepartment in kitDepartments)
            {
                foreach (var product in kitDepartment.Kits)
                {
                    if (kits.Any(structureCostsNumber => structureCostsNumber.ProductKit.Id == product.Id)) continue;
                    kits.Add(new StructureCostsNumber(product, kitDepartments.Where(department => department.Kits.Contains(product))));
                }
            }

            Items = new ObservableCollection<StructureCostsNumber>(blocks.Union(kits));

            Items.ForEach(structureCostsNumber => structureCostsNumber.PropertyChanged += ItemOnPropertyChanged);
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.SaveCommand.RaiseCanExecuteChanged();
        }
    }
}