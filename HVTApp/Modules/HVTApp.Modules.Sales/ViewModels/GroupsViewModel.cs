using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.Services.PriceService;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class GroupsViewModel : LoadableBindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private SalesUnitsGroup _selectedGroup;
        private ProductIncludedWrapper _selectedProductIncluded;

        #region ICommand

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; }
        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentsCommand { get; }

        public ICommand AddProductIncludedCommand { get; }
        public ICommand RemoveProductIncludedCommand { get; }

        #endregion

        public GroupsViewModel(IUnityContainer container, IEnumerable<SalesUnit> units, IUnitOfWork unitOfWork) : base(container)
        {
            _unitOfWork = unitOfWork;
            var groups = units.GroupBy(x => new
            {
                Product = x.Product.Id,
                Facility = x.Facility.Id,
                PaymentConditionSet = x.PaymentConditionSet.Id,
                Cost = x.Cost,
                ProductionTerm = x.ProductionTerm
            }).OrderBy(x => x.Key.Cost)
              .Select(x => new SalesUnitsGroup(x));

            Groups = new ValidatableChangeTrackingCollection<SalesUnitsGroup>(groups);

            //AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => SelectedGroup != null);
            ChangeFacilityCommand = new DelegateCommand<SalesUnitsGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<SalesUnitsGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new DelegateCommand<SalesUnitsGroup>(ChangePaymentsCommand_Execute);

            AddProductIncludedCommand = new DelegateCommand(AddProductIncludedCommand_Execute, () => SelectedGroup != null);
            RemoveProductIncludedCommand = new DelegateCommand(RemoveProductIncludedCommand_Execute, () => SelectedProductIncluded != null);
        }

        protected override async Task LoadedAsyncMethod()
        {
            foreach (var group in Groups)
                await RefreshPrice(group);
        }


        private async Task RefreshPrice(SalesUnitsGroup group)
        {
            if (group == null) return;

            var priceService = Container.Resolve<IPriceService>();

            //прайс для основного оборудования
            var price = await priceService.GetPrice(group.Product.Model, GetDate(), CommonOptions.ActualPriceTerm, _priceErrors);

            //добавляем прайсы дополнительного оборудования
            foreach (var productIncluded in group.ProductsIncluded)
            {
                price += productIncluded.Amount * await priceService.GetPrice(productIncluded.Product.Model, DateTime.Today, CommonOptions.ActualPriceTerm, _priceErrors);
            }

            group.Price = price;
            OnPropertyChanged(nameof(PriceErrors));
        }
        

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public SalesUnitsGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (Equals(_selectedGroup, value)) return;
                _selectedGroup = value;
                ((DelegateCommand)RemoveCommand)?.RaiseCanExecuteChanged();
                ((DelegateCommand)AddProductIncludedCommand)?.RaiseCanExecuteChanged();
                OnPropertyChanged();
                OnPropertyChanged(nameof(PriceErrors));
            }
        }

        //выбранный зависимый продукт
        public ProductIncludedWrapper SelectedProductIncluded
        {
            get { return _selectedProductIncluded; }
            set
            {
                if (Equals(_selectedProductIncluded, value)) return;
                _selectedProductIncluded = value;
                ((DelegateCommand)RemoveProductIncludedCommand)?.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Группы
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitsGroup> Groups { get; }

        #region Commands

        //protected async void AddCommand_Execute()
        //{
        //    var salesUnit = new SalesUnitWrapper(new SalesUnit { Project = Model });
        //    var viewModel = new SalesUnitsViewModel(salesUnit, Container, _unitOfWork);
        //    if (SelectedGroup != null)
        //    {
        //        viewModel.ViewModel.Item.Cost = SelectedGroup.Cost;
        //        viewModel.ViewModel.Item.Facility = SelectedGroup.Facility;
        //        viewModel.ViewModel.Item.PaymentConditionSet = SelectedGroup.PaymentConditionSet;
        //        viewModel.ViewModel.Item.ProductionTerm = SelectedGroup.ProductionTerm;
        //        viewModel.ViewModel.Item.Product = SelectedGroup.Product;
        //        viewModel.ViewModel.Item.DeliveryDateExpected = SelectedGroup.DeliveryDateExpected;
        //        foreach (var prodIncl in SelectedGroup.ProductsIncluded)
        //        {
        //            var pi = new ProductIncluded { Product = prodIncl.Product.Model, Amount = prodIncl.Amount };
        //            viewModel.ViewModel.Item.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
        //        }
        //    }

        //    var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);
        //    if (!result.HasValue || !result.Value)
        //        return;

        //    var wrappers = new List<SalesUnitWrapper>();
        //    for (int i = 0; i < viewModel.Amount; i++)
        //    {
        //        var unit = (SalesUnit)viewModel.ViewModel.Item.Model.Clone();
        //        unit.Id = Guid.NewGuid();
        //        unit.ProductsIncluded = new List<ProductIncluded>();
        //        var unitWrapper = new SalesUnitWrapper(unit);
        //        this.Item.Units.Add(unitWrapper);
        //        wrappers.Add(unitWrapper);
        //    }

        //    //var group = new UnitsDatedGroup(wrappers);
        //    //Groups.Add(group);
        //    await RefreshPrices();
        //    //SelectedGroup = group;
        //}


        private async void AddProductIncludedCommand_Execute()
        {
            var productIncluded = new ProductIncluded();
            productIncluded = await Container.Resolve<IUpdateDetailsService>().UpdateDetailsWithoutSaving(productIncluded);
            if (productIncluded == null) return;
            productIncluded.Product = await _unitOfWork.Repository<Product>().GetByIdAsync(productIncluded.Product.Id);
            SelectedGroup.ProductsIncluded.Add(new ProductIncludedWrapper(productIncluded));
            await RefreshPrice(SelectedGroup);
        }

        private async void RemoveProductIncludedCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;
            SelectedGroup.ProductsIncluded.Remove(SelectedProductIncluded);
            await RefreshPrice(SelectedGroup);
        }

        private void RemoveCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            Groups.Remove(SelectedGroup);
            SelectedGroup = null;
        }

        private async void ChangeProductCommand_Execute(SalesUnitsGroup group)
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(group.Product?.Model);
            if (product == null || product.Id == group.Product.Id) return;
            product = await _unitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            group.Product = new ProductWrapper(product);
            await RefreshPrice(group);
        }

        private async void ChangeFacilityCommand_Execute(SalesUnitsGroup group)
        {
            var facilities = await _unitOfWork.Repository<Facility>().GetAllAsNoTrackingAsync();
            var facility = await Container.Resolve<ISelectService>().SelectItem(facilities, group.Facility?.Id);
            if (facility == null) return;
            facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(facility.Id);
            group.Facility = new FacilityWrapper(facility);
        }

        private async void ChangePaymentsCommand_Execute(SalesUnitsGroup group)
        {
            var sets = await _unitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTrackingAsync();
            var set = await Container.Resolve<ISelectService>().SelectItem(sets, group.PaymentConditionSet?.Id);
            if (set == null) return;
            set = await _unitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(set.Id);
            group.PaymentConditionSet = new PaymentConditionSetWrapper(set);
        }


        #endregion

        private readonly PriceErrors _priceErrors = new PriceErrors();

        public string PriceErrors
        {
            get
            {
                var blocks = new List<ProductBlock>();
                if (SelectedGroup == null)
                {
                    foreach (var unitsGroup in Groups)
                    {
                        blocks.AddRange(unitsGroup.Product.Model.GetBlocks());
                        foreach (var pi in unitsGroup.ProductsIncluded)
                        {
                            blocks.AddRange(pi.Product.Model.GetBlocks());
                        }
                    }
                }
                else
                {
                    blocks.AddRange(SelectedGroup.Product.Model.GetBlocks());
                    foreach (var pi in SelectedGroup.ProductsIncluded)
                    {
                        blocks.AddRange(pi.Product.Model.GetBlocks());
                    }
                }

                return _priceErrors.Print(blocks);
            }
        }

        /// <summary>
        /// Возвращает дату для расчета прайса.
        /// </summary>
        /// <returns></returns>
        protected DateTime GetDate() { return DateTime.Today; }

        public async Task SaveChanges()
        {
            //добавляем созданные юниты и удаляем удаленные
            var added = GetAllUnits(Groups.AddedItems).ToList();
            _unitOfWork.Repository<SalesUnit>().AddRange(added);

            //добавляем созданные юниты и удаляем удаленные
            var removed = GetAllUnits(Groups.RemovedItems).ToList();
            _unitOfWork.Repository<SalesUnit>().DeleteRange(removed);

            var modified = GetAllUnits(Groups.ModifiedItems).ToList();

            Groups.AcceptChanges();
            await _unitOfWork.SaveChangesAsync();

            var eventAggregator = Container.Resolve<IEventAggregator>();

            //сообщаем об изменениях
            added.Concat(modified).ForEach(x => eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Publish(x));
            removed.ForEach(x => eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Publish(x));

        }

        public bool CanSaveChanges()
        {
            return Groups.Any() && Groups.IsValid;
        }

        private IEnumerable<SalesUnit> GetAllUnits(IEnumerable<SalesUnitsGroup> groups)
        {
            return groups.Select(x => x.Model)
                         .Concat(Groups.AddedItems.Where(x => x.Groups != null)
                         .SelectMany(x => x.Groups.Select(g => g.Model)))
                         .Distinct();
        }
    }
}