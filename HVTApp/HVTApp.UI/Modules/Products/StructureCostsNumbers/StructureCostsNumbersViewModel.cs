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
                    var kitTarget = productService.GetKit(SelectedItem.DepartmentsKits.First());
                    if (kitTarget == null) return;
                    if (string.IsNullOrWhiteSpace(kitTarget.ProductBlock.StructureCostNumber))
                    {
                        messageService.Message("Выберите комплект со стракчакостом");
                        return;
                    }

                    kitTarget = unitOfWork.Repository<Product>().GetById(kitTarget.Id);
                    var kitTargetBlock = kitTarget.ProductBlock;
                    var kitToReplace = unitOfWork.Repository<Product>().GetById(SelectedItem.ProductKit.Id);
                    var kitToReplaceBlock = kitToReplace.ProductBlock;

                    if (kitTarget.Id == kitToReplace.Id)
                    {
                        messageService.Message("Вы выбрали тот же комплект");
                        return;
                    }

                    if (productService.ReplaceProduct(SelectedItem.ProductKit, kitTarget))
                    {
                        //замена ремкомплекта в задачах
                        unitOfWork.Repository<PriceEngineeringTask>()
                            .Find(x => x.ProductBlockEngineer.Id == kitToReplaceBlock.Id)
                            .ForEach(x => x.ProductBlockEngineer = kitTargetBlock);

                        unitOfWork.Repository<PriceEngineeringTask>()
                            .Find(x => x.ProductBlockManager.Id == kitToReplaceBlock.Id)
                            .ForEach(x => x.ProductBlockManager = kitTargetBlock);

                        unitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>()
                            .Find(x => x.ProductBlock.Id == kitToReplaceBlock.Id)
                            .ForEach(x => x.ProductBlock = kitTargetBlock);

                        unitOfWork.Repository<StructureCost>()
                            .Find(x => x.OriginalStructureCostProductBlock?.Id == kitToReplaceBlock.Id)
                            .ForEach(x => x.OriginalStructureCostProductBlock = kitTargetBlock);

                        //перенос всех прайсов из удаляемого блока
                        foreach (var sumOnDate in kitToReplaceBlock.Prices.ToList())
                        {
                            kitTargetBlock.Prices.Add(sumOnDate);
                            kitToReplaceBlock.Prices.Remove(sumOnDate);
                        }

                        unitOfWork.Repository<Product>().Delete(kitToReplace);
                        unitOfWork.Repository<ProductBlock>().Delete(kitToReplaceBlock);

                        Items.Remove(SelectedItem);
                        SelectedItem = null;

                        unitOfWork.SaveChanges();
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
            var kits = new List<StructureCostsNumber>();
            foreach (var kitDepartment in departments)
            {
                foreach (var product in kitDepartment.Kits)
                {
                    if (kits.Any(structureCostsNumber => structureCostsNumber.ProductKit.Id == product.Id)) continue;
                    kits.Add(new StructureCostsNumber(product, departments.Where(department => department.Kits.Contains(product))));
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