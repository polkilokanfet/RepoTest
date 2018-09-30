using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
    public class SalesUnitsGroupsViewModel : ViewModelBase, IGroupsViewModel<SalesUnit, ProjectWrapper>
    {
        private ProjectWrapper _projectWrapper;

        //блоки, необходимые для поиска аналогов
        private static List<ProductBlock> _blocks;
        private readonly Dictionary<SalesUnitsWrappersGroup, PriceStructures> _priceDictionary = new Dictionary<SalesUnitsWrappersGroup, PriceStructures>();

        private SalesUnitsWrappersGroup _selectedGroup;
        private ProductIncludedWrapper _selectedProductIncluded;

        /// <summary>
        /// Группы
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitsWrappersGroup> Groups { get; protected set; }

        #region ICommand

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; }

        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentsCommand { get; }

        public ICommand AddProductIncludedCommand { get; }
        public ICommand RemoveProductIncludedCommand { get; }

        #endregion

        public SalesUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
            AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => SelectedGroup != null);
            ChangeFacilityCommand = new DelegateCommand<SalesUnitsWrappersGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<SalesUnitsWrappersGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new DelegateCommand<SalesUnitsWrappersGroup>(ChangePaymentsCommand_Execute);

            AddProductIncludedCommand = new DelegateCommand(AddProductIncludedCommand_Execute, () => SelectedGroup != null);
            RemoveProductIncludedCommand = new DelegateCommand(RemoveProductIncludedCommand_Execute, () => SelectedProductIncluded != null);

            if(_blocks == null)
                _blocks = UnitOfWork.Repository<ProductBlock>().Find(x => true);

        }

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
        public PriceStructures PriceStructures => SelectedGroup == null ? null : _priceDictionary[SelectedGroup];

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public SalesUnitsWrappersGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (Equals(_selectedGroup, value)) return;
                _selectedGroup = value;
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

        #region AddCommand

        protected virtual void AddCommand_Execute()
        {
            //создаем новый юнит и привязываем его к объекту
            var salesUnit = new SalesUnitWrapper(new SalesUnit());
            if(_projectWrapper != null) salesUnit.Project = _projectWrapper;

            //создаем модель для диалога
            var viewModel = new SalesUnitsViewModel(salesUnit, Container, UnitOfWork);

            //заполняем юнит начальными данными
            FillingSalesUnit(viewModel.ViewModel.Item);

            //диалог с пользователем
            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);
            if (!result.HasValue || !result.Value) return;

            //клонируем юниты
            var units = CloneSalesUnits(viewModel.ViewModel.Item.Model, viewModel.Amount);

            var group = new SalesUnitsWrappersGroup(units);
            Groups.Add(group);
            RefreshPrice(group);
            SelectedGroup = group;
        }

        /// <summary>
        /// Заполнение юнита по выбранной группе
        /// </summary>
        /// <param name="salesUnitWrapper"></param>
        private void FillingSalesUnit(SalesUnitWrapper salesUnitWrapper)
        {
            if (SelectedGroup == null) return;

            salesUnitWrapper.Cost = SelectedGroup.Cost;
            salesUnitWrapper.Facility = SelectedGroup.Facility;
            salesUnitWrapper.PaymentConditionSet = SelectedGroup.PaymentConditionSet;
            salesUnitWrapper.ProductionTerm = SelectedGroup.ProductionTerm;
            salesUnitWrapper.Product = SelectedGroup.Product;
            salesUnitWrapper.DeliveryDateExpected = SelectedGroup.DeliveryDateExpected;
                
            //создаем зависимое оборудование
            foreach (var prodIncl in SelectedGroup.ProductsIncluded)
            {
                var pi = new ProductIncluded { Product = prodIncl.Product.Model, Amount = prodIncl.Amount };
                salesUnitWrapper.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
            }
        }

        /// <summary>
        /// Клонирование юнитов по образцу.
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private IEnumerable<SalesUnit> CloneSalesUnits(SalesUnit salesUnit, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var unit = (SalesUnit)salesUnit.Clone();
                unit.Id = Guid.NewGuid();
                unit.ProductsIncluded = new List<ProductIncluded>();
                yield return unit;
            }
            
        }

        #endregion

        private async void AddProductIncludedCommand_Execute()
        {
            var productIncluded = new ProductIncluded();
            productIncluded = await Container.Resolve<IUpdateDetailsService>().UpdateDetailsWithoutSaving(productIncluded);
            if (productIncluded == null) return;
            productIncluded.Product = await UnitOfWork.Repository<Product>().GetByIdAsync(productIncluded.Product.Id);
            SelectedGroup.AddProductIncluded(productIncluded);
            RefreshPrice(SelectedGroup);
        }

        private void RemoveProductIncludedCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            SelectedGroup.RemoveProductIncluded(SelectedProductIncluded);
            RefreshPrice(SelectedGroup);
        }

        private void RemoveCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            //удаление из группы или из подгруппы
            if (Groups.Contains(SelectedGroup))
            {
                Groups.Remove(SelectedGroup);
            }
            else
            {
                var group = Groups.Single(x => x.Groups != null && x.Groups.Contains(SelectedGroup));
                group.Groups.Remove(SelectedGroup);

                if (!group.Groups.Any())
                {
                    Groups.Remove(group);
                }
            }

            SelectedGroup = null;
        }

        private async void ChangeProductCommand_Execute(SalesUnitsWrappersGroup wrappersGroup)
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(wrappersGroup.Product?.Model);
            if (product == null || product.Id == wrappersGroup.Product.Id) return;
            product = await UnitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            wrappersGroup.Product = new ProductWrapper(product);
            RefreshPrice(wrappersGroup);
        }

        private async void ChangeFacilityCommand_Execute(SalesUnitsWrappersGroup wrappersGroup)
        {
            var facilities = await UnitOfWork.Repository<Facility>().GetAllAsNoTrackingAsync();
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities, wrappersGroup.Facility?.Id);
            if (facility == null) return;
            facility = await UnitOfWork.Repository<Facility>().GetByIdAsync(facility.Id);
            wrappersGroup.Facility = new FacilityWrapper(facility);
        }

        private async void ChangePaymentsCommand_Execute(SalesUnitsWrappersGroup wrappersGroup)
        {
            var sets = await UnitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTrackingAsync();
            var set = Container.Resolve<ISelectService>().SelectItem(sets, wrappersGroup.PaymentConditionSet?.Id);
            if (set == null) return;
            set = await UnitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(set.Id);
            wrappersGroup.PaymentConditionSet = new PaymentConditionSetWrapper(set);
        }


        #endregion

        public void AcceptChanges()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();

            //добавляем созданные
            var added = GetAddedUnits().ToList();
            UnitOfWork.Repository<SalesUnit>().AddRange(added);

            //удаляем удаленные
            var removedModels = GetRemovedUnits().ToList();
            //сообщаем об удалении (так высоко, т.к. после удаления объект рушится)
            removedModels.ForEach(x => eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Publish(x));
            UnitOfWork.Repository<SalesUnit>().DeleteRange(removedModels);

            var modified = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.ModifiedItems).ToList();
            modified = Groups.ModifiedItems.Concat(modified).ToList();

            Groups.AcceptChanges();

            added.Concat(modified.Select(x => x.Model)).Distinct().ForEach(x => eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Publish(x));
        }

        private IEnumerable<SalesUnit> GetAddedUnits()
        {
            var added = Groups.AddedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups);
            added = added.Concat(Groups.AddedItems);
            return added.Select(x => x.Model).Distinct();
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

        public void Load(IEnumerable<SalesUnit> units, ProjectWrapper parentWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            _projectWrapper = parentWrapper;
            UnitOfWork = unitOfWork;

            var groups = units.GroupBy(x => x, new SalesUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new SalesUnitsWrappersGroup(x)).ToList();

            if (isNew)
            {
                Groups = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(new List<SalesUnitsWrappersGroup>());
                groups.ForEach(x => Groups.Add(x));
            }
            else
            {
                Groups = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(groups);
            }

            OnPropertyChanged(nameof(Groups));

            Groups.PropertyChanged += GroupsOnPropertyChanged;
            Groups.CollectionChanged += GroupsOnCollectionChanged;

            Groups.ForEach(RefreshPrice);
        }

        public bool IsValid => Groups != null && Groups.Any() && Groups.IsValid;

        public bool IsChanged => Groups != null && Groups.IsChanged;

        private void GroupsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            GroupChanged?.Invoke();
        }

        private void GroupsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            GroupChanged?.Invoke();
        }

        public event Action GroupChanged;
    }
}