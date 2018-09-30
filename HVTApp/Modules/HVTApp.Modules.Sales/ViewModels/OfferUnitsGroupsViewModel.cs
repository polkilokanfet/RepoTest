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
    public class OfferUnitsGroupsViewModel : LoadableBindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Offer _offer;
        private OfferUnitsGroup _selectedGroup;
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

        public OfferUnitsGroupsViewModel(IUnityContainer container, IEnumerable<OfferUnit> units, IUnitOfWork unitOfWork, Offer offer) : base(container)
        {
            _unitOfWork = unitOfWork;
            _offer = offer;

            var groups = units.GroupBy(x => x, new OfferUnitsGroupsComparer())
                              .OrderByDescending(x => x.Key.Cost)
                              .Select(x => new OfferUnitsGroup(x));

            Groups = new ValidatableChangeTrackingCollection<OfferUnitsGroup>(groups);

            AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => SelectedGroup != null);
            ChangeFacilityCommand = new DelegateCommand<OfferUnitsGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<OfferUnitsGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new DelegateCommand<OfferUnitsGroup>(ChangePaymentsCommand_Execute);

            AddProductIncludedCommand = new DelegateCommand(AddProductIncludedCommand_Execute, () => SelectedGroup != null);
            RemoveProductIncludedCommand = new DelegateCommand(RemoveProductIncludedCommand_Execute, () => SelectedProductIncluded != null);
        }

        protected override async Task LoadedAsyncMethod()
        {
            _blocks = await UnitOfWork.Repository<ProductBlock>().GetAllAsync();
            Groups.ForEach(RefreshPrice);
        }

        private List<ProductBlock> _blocks;
        private readonly Dictionary<OfferUnitsGroup, PriceStructures> _priceDictionary = new Dictionary<OfferUnitsGroup, PriceStructures>();

        protected void RefreshPrice(OfferUnitsGroup group)
        {
            if (group == null) return;

            var priceDate = _offer.Date < DateTime.Today ? _offer.Date : DateTime.Today;
            var priceTerm = CommonOptions.ActualOptions.ActualPriceTerm;

            if (!_priceDictionary.ContainsKey(group)) _priceDictionary.Add(group, null);

            _priceDictionary[group] = new PriceStructures(group.Model, priceDate, priceTerm, _blocks);

            group.Price = _priceDictionary[group].Total;
            OnPropertyChanged(nameof(PriceStructures));

            group.Groups?.ForEach(RefreshPrice);
        }

        /// <summary>
        /// Структура себестоимости выбранной группы
        /// </summary>
        public PriceStructures PriceStructures => SelectedGroup == null ? null : _priceDictionary[SelectedGroup];


        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public OfferUnitsGroup SelectedGroup
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

        /// <summary>
        /// Группы
        /// </summary>
        public IValidatableChangeTrackingCollection<OfferUnitsGroup> Groups { get; }

        #region Commands

        protected void AddCommand_Execute()
        {
            //создаем новый юнит и привязываем его к объекту
            var salesUnit = new OfferUnitWrapper(new OfferUnit());
            if (_offer != null) salesUnit.Offer = new OfferWrapper(_offer);

            //создаем модель для диалога
            var viewModel = new OfferUnitsViewModel(salesUnit, Container, _unitOfWork);

            //заполняем юнит начальными данными
            if (SelectedGroup != null)
            {
                viewModel.ViewModel.Item.Cost = SelectedGroup.Cost;
                viewModel.ViewModel.Item.Facility = SelectedGroup.Facility;
                viewModel.ViewModel.Item.PaymentConditionSet = SelectedGroup.PaymentConditionSet;
                viewModel.ViewModel.Item.ProductionTerm = SelectedGroup.ProductionTerm;
                viewModel.ViewModel.Item.Product = SelectedGroup.Product;

                //создаем зависимое оборудование
                foreach (var prodIncl in SelectedGroup.ProductsIncluded)
                {
                    var pi = new ProductIncluded { Product = prodIncl.Product.Model, Amount = prodIncl.Amount };
                    viewModel.ViewModel.Item.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
                }
            }

            //диалог с пользователем
            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);

            if (!result.HasValue || !result.Value) return;

            //клонируем юниты
            var units = new List<OfferUnit>();
            for (int i = 0; i < viewModel.Amount; i++)
            {
                var unit = (OfferUnit)viewModel.ViewModel.Item.Model.Clone();
                unit.Id = Guid.NewGuid();
                unit.ProductsIncluded = new List<ProductIncluded>();
                units.Add(unit);
            }

            var group = new OfferUnitsGroup(units);
            Groups.Add(group);
            RefreshPrice(group);
            SelectedGroup = group;
        }


        private async void AddProductIncludedCommand_Execute()
        {
            var productIncluded = new ProductIncluded();
            productIncluded = await Container.Resolve<IUpdateDetailsService>().UpdateDetailsWithoutSaving(productIncluded);
            if (productIncluded == null) return;
            productIncluded.Product = await _unitOfWork.Repository<Product>().GetByIdAsync(productIncluded.Product.Id);
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

        private async void ChangeProductCommand_Execute(OfferUnitsGroup group)
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(group.Product?.Model);
            if (product == null || product.Id == group.Product.Id) return;
            product = await _unitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            group.Product = new ProductWrapper(product);
            RefreshPrice(group);
        }

        private async void ChangeFacilityCommand_Execute(OfferUnitsGroup group)
        {
            var facilities = await _unitOfWork.Repository<Facility>().GetAllAsNoTrackingAsync();
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities, group.Facility?.Id);
            if (facility == null) return;
            facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(facility.Id);
            group.Facility = new FacilityWrapper(facility);
        }

        private async void ChangePaymentsCommand_Execute(OfferUnitsGroup group)
        {
            var sets = await _unitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTrackingAsync();
            var set = Container.Resolve<ISelectService>().SelectItem(sets, group.PaymentConditionSet?.Id);
            if (set == null) return;
            set = await _unitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(set.Id);
            group.PaymentConditionSet = new PaymentConditionSetWrapper(set);
        }


        #endregion

        public void AcceptChanges()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();

            //добавляем созданные
            var added = Groups.AddedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups).ToList();
            added = added.Concat(Groups.AddedItems).ToList();
            _unitOfWork.Repository<OfferUnit>().AddRange(added.Select(x => x.Model).Distinct());

            //удаляем удаленные
            var removed = Groups.Except(added).Where(x => x.Groups != null).SelectMany(x => x.Groups.RemovedItems).ToList();
            //removed = removed.Except(added.Where(x => x.Groups != null).SelectMany(x => x.Groups)).ToList(); // исключаем вновь добавленные
            removed = Groups.RemovedItems.Concat(removed).ToList();
            removed = removed.Concat(Groups.RemovedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups)).ToList();
            var removedModels = removed.Select(x => x.Model).Distinct().ToList();
            //сообщаем об изменениях (так высоко, т.к. после удаления объект рушится)
            removedModels.ForEach(x => eventAggregator.GetEvent<AfterRemoveOfferUnitEvent>().Publish(x));
            _unitOfWork.Repository<OfferUnit>().DeleteRange(removedModels);

            var modified = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.ModifiedItems).ToList();
            modified = Groups.ModifiedItems.Concat(modified).ToList();

            Groups.AcceptChanges();

            //сообщаем об изменениях
            added.Concat(modified).Select(x => x.Model).Distinct().ForEach(x => eventAggregator.GetEvent<AfterSaveOfferUnitEvent>().Publish(x));
        }


        public bool CanSaveChanges()
        {
            return Groups.Any() && Groups.IsValid;
        }

        private IEnumerable<OfferUnit> GetAllUnits(IEnumerable<OfferUnitsGroup> groups)
        {
            return groups.Select(x => x.Model)
                         .Concat(Groups.AddedItems.Where(x => x.Groups != null)
                         .SelectMany(x => x.Groups.Select(g => g.Model)))
                         .Distinct();
        }
    }
}