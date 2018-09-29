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
using HVTApp.Model.Services;
using HVTApp.Model.Structures;
using HVTApp.UI.Converter;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SalesUnitsGroupsViewModel : LoadableBindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Project _project;
        private SalesUnitsWrappersGroup _selectedWrappersGroup;
        private ProductIncludedWrapper _selectedProductIncluded;

        /// <summary>
        /// Группы
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitsWrappersGroup> Groups { get; }

        #region ICommand

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; }

        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentsCommand { get; }

        public ICommand AddProductIncludedCommand { get; }
        public ICommand RemoveProductIncludedCommand { get; }

        #endregion

        public SalesUnitsGroupsViewModel(IUnityContainer container, 
                                         IEnumerable<SalesUnit> units, 
                                         IUnitOfWork unitOfWork, 
                                         Project project = null) : base(container)
        {
            _unitOfWork = unitOfWork;
            _project = project;
            var groups = units.GroupBy(x => x, new SalesUnitsGroupsComparer())
                              .OrderByDescending(x => x.Key.Cost)
                              .Select(x => new SalesUnitsWrappersGroup(x));

            Groups = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(groups);

            AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => SelectedWrappersGroup != null);
            ChangeFacilityCommand = new DelegateCommand<SalesUnitsWrappersGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<SalesUnitsWrappersGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new DelegateCommand<SalesUnitsWrappersGroup>(ChangePaymentsCommand_Execute);

            AddProductIncludedCommand = new DelegateCommand(AddProductIncludedCommand_Execute, () => SelectedWrappersGroup != null);
            RemoveProductIncludedCommand = new DelegateCommand(RemoveProductIncludedCommand_Execute, () => SelectedProductIncluded != null);
        }

        protected override async Task LoadedAsyncMethod()
        {
            _blocks = await UnitOfWork.Repository<ProductBlock>().GetAllAsync();
            Groups.ForEach(RefreshPrice);
        }

        //блоки, необходимые для поиска аналогов
        private List<ProductBlock> _blocks;
        private readonly Dictionary<SalesUnitsWrappersGroup, PriceStructures> _priceDictionary = new Dictionary<SalesUnitsWrappersGroup, PriceStructures>();

        protected void RefreshPrice(SalesUnitsWrappersGroup wrappersGroup)
        {
            if (wrappersGroup == null) return;

            var priceDate = wrappersGroup.OrderInTakeDate < DateTime.Today ? wrappersGroup.OrderInTakeDate : DateTime.Today;
            var priceTerm = CommonOptions.ActualOptions.ActualPriceTerm;

            if (!_priceDictionary.ContainsKey(wrappersGroup)) _priceDictionary.Add(wrappersGroup, null);

            _priceDictionary[wrappersGroup] = new PriceStructures(wrappersGroup.Model, priceDate, priceTerm, _blocks);

            wrappersGroup.Price = _priceDictionary[wrappersGroup].Total;
            OnPropertyChanged(nameof(PriceStructures));

            wrappersGroup.Groups?.ForEach(RefreshPrice);
        }

        /// <summary>
        /// Структура себестоимости выбранной группы
        /// </summary>
        public PriceStructures PriceStructures => SelectedWrappersGroup == null ? null : _priceDictionary[SelectedWrappersGroup];

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public SalesUnitsWrappersGroup SelectedWrappersGroup
        {
            get { return _selectedWrappersGroup; }
            set
            {
                if (Equals(_selectedWrappersGroup, value)) return;
                _selectedWrappersGroup = value;
                ((DelegateCommand)RemoveCommand)?.RaiseCanExecuteChanged();
                ((DelegateCommand)AddProductIncludedCommand)?.RaiseCanExecuteChanged();
                OnPropertyChanged();
                OnPropertyChanged(nameof(PriceStructures));
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

        #region Commands

        protected virtual void AddCommand_Execute()
        {
            //создаем новый юнит и привязываем его к объекту
            var salesUnit = new SalesUnitWrapper(new SalesUnit());
            if(_project != null) salesUnit.Project = new ProjectWrapper(_project);

            //создаем модель для диалога
            var viewModel = new SalesUnitsViewModel(salesUnit, Container, _unitOfWork);

            //заполняем юнит начальными данными
            if (SelectedWrappersGroup != null)
            {
                viewModel.ViewModel.Item.Cost = SelectedWrappersGroup.Cost;
                viewModel.ViewModel.Item.Facility = SelectedWrappersGroup.Facility;
                viewModel.ViewModel.Item.PaymentConditionSet = SelectedWrappersGroup.PaymentConditionSet;
                viewModel.ViewModel.Item.ProductionTerm = SelectedWrappersGroup.ProductionTerm;
                viewModel.ViewModel.Item.Product = SelectedWrappersGroup.Product;
                viewModel.ViewModel.Item.DeliveryDateExpected = SelectedWrappersGroup.DeliveryDateExpected;
                
                //создаем зависимое оборудование
                foreach (var prodIncl in SelectedWrappersGroup.ProductsIncluded)
                {
                    var pi = new ProductIncluded { Product = prodIncl.Product.Model, Amount = prodIncl.Amount };
                    viewModel.ViewModel.Item.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
                }
            }

            //диалог с пользователем
            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);

            if (!result.HasValue || !result.Value) return;

            //клонируем юниты
            var units = new List<SalesUnit>();
            for (int i = 0; i < viewModel.Amount; i++)
            {
                var unit = (SalesUnit)viewModel.ViewModel.Item.Model.Clone();
                unit.Id = Guid.NewGuid();
                unit.ProductsIncluded = new List<ProductIncluded>();
                units.Add(unit);
            }

            var group = new SalesUnitsWrappersGroup(units);
            Groups.Add(group);
            RefreshPrice(group);
            SelectedWrappersGroup = group;
        }

        private async void AddProductIncludedCommand_Execute()
        {
            var productIncluded = new ProductIncluded();
            productIncluded = await Container.Resolve<IUpdateDetailsService>().UpdateDetailsWithoutSaving(productIncluded);
            if (productIncluded == null) return;
            productIncluded.Product = await _unitOfWork.Repository<Product>().GetByIdAsync(productIncluded.Product.Id);
            SelectedWrappersGroup.AddProductIncluded(productIncluded);
            RefreshPrice(SelectedWrappersGroup);
        }

        private void RemoveProductIncludedCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            SelectedWrappersGroup.RemoveProductIncluded(SelectedProductIncluded);
            RefreshPrice(SelectedWrappersGroup);
        }

        private void RemoveCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            //удаление из группы или из подгруппы
            if (Groups.Contains(SelectedWrappersGroup))
            {
                Groups.Remove(SelectedWrappersGroup);
            }
            else
            {
                var group = Groups.Single(x => x.Groups != null && x.Groups.Contains(SelectedWrappersGroup));
                group.Groups.Remove(SelectedWrappersGroup);

                if (!group.Groups.Any())
                {
                    Groups.Remove(group);
                }
            }

            SelectedWrappersGroup = null;
        }

        private async void ChangeProductCommand_Execute(SalesUnitsWrappersGroup wrappersGroup)
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(wrappersGroup.Product?.Model);
            if (product == null || product.Id == wrappersGroup.Product.Id) return;
            product = await _unitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            wrappersGroup.Product = new ProductWrapper(product);
            RefreshPrice(wrappersGroup);
        }

        private async void ChangeFacilityCommand_Execute(SalesUnitsWrappersGroup wrappersGroup)
        {
            var facilities = await _unitOfWork.Repository<Facility>().GetAllAsNoTrackingAsync();
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities, wrappersGroup.Facility?.Id);
            if (facility == null) return;
            facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(facility.Id);
            wrappersGroup.Facility = new FacilityWrapper(facility);
        }

        private async void ChangePaymentsCommand_Execute(SalesUnitsWrappersGroup wrappersGroup)
        {
            var sets = await _unitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTrackingAsync();
            var set = Container.Resolve<ISelectService>().SelectItem(sets, wrappersGroup.PaymentConditionSet?.Id);
            if (set == null) return;
            set = await _unitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(set.Id);
            wrappersGroup.PaymentConditionSet = new PaymentConditionSetWrapper(set);
        }


        #endregion

        public virtual async Task SaveChanges()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();

            //добавляем созданные
            var added = GetAddedUnits().ToList();
            _unitOfWork.Repository<SalesUnit>().AddRange(added);

            //удаляем удаленные
            var removedModels = GetRemovedUnits().ToList();
            //сообщаем об удалении (так высоко, т.к. после удаления объект рушится)
            removedModels.ForEach(x => eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Publish(x));
            _unitOfWork.Repository<SalesUnit>().DeleteRange(removedModels);

            var modified = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.ModifiedItems).ToList();
            modified = Groups.ModifiedItems.Concat(modified).ToList();

            Groups.AcceptChanges();
            await _unitOfWork.SaveChangesAsync();

            added.Concat(modified.Select(x => x.Model)).Distinct().ForEach(x => eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Publish(x));
        }

        private IEnumerable<SalesUnit> GetAddedUnits()
        {
            var added = Groups.AddedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups);
            added = added.Concat(Groups.AddedItems);
            return added.Select(x => x.Model);
        }

        private IEnumerable<SalesUnit> GetRemovedUnits()
        {
            var added = Groups.AddedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups).ToList();
            added = added.Concat(Groups.AddedItems).ToList();

            //удаляем удаленные
            var removed = Groups.Except(added).Where(x => x.Groups != null).SelectMany(x => x.Groups.RemovedItems).ToList();
            removed = Groups.RemovedItems.Concat(removed).ToList();
            removed = removed.Concat(Groups.RemovedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups)).ToList();
            return removed.Select(x => x.Model).Distinct().ToList();
        }

        public virtual bool CanSaveChanges()
        {
            return Groups.Any() && Groups.IsValid;
        }
    }
}