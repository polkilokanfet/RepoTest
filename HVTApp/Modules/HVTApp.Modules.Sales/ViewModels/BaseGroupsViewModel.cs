using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Structures;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public abstract class BaseGroupsViewModel<TGroup, TSubGroup, TModel> : ViewModelBase
        where TModel : IUnitPoco
        where TSubGroup : class, IGroupValidatableChangeTracking<TModel>
        where TGroup : class, IGroupValidatableChangeTrackingWithCollection<TSubGroup, TModel>
    {
        //блоки, необходимые для поиска аналогов
        protected static List<ProductBlock> _blocks;
        protected readonly Dictionary<TGroup, PriceStructures> _priceDictionary = new Dictionary<TGroup, PriceStructures>();

        private TGroup _selectedGroup;
        private ProductIncludedWrapper _selectedProductIncluded;

        public IValidatableChangeTrackingCollection<TGroup> Groups { get; protected set; }

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public TGroup SelectedGroup
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

        protected abstract DateTime GetPriceDate(TGroup grp);

        protected void RefreshPrice(TGroup grp)
        {
            if (grp == null) return;

            //var priceDate = grp.OrderInTakeDate < DateTime.Today ? grp.OrderInTakeDate : DateTime.Today;
            var priceTerm = CommonOptions.ActualOptions.ActualPriceTerm;

            if (!_priceDictionary.ContainsKey(grp)) _priceDictionary.Add(grp, null);

            _priceDictionary[grp] = new PriceStructures(grp.Model, GetPriceDate(grp), priceTerm, _blocks);

            grp.Price = _priceDictionary[grp].Total;
            OnPropertyChanged(nameof(PriceStructures));

            grp.Groups?.ForEach(x => RefreshPrice(x as TGroup));
        }

        /// <summary>
        /// Структура себестоимости выбранной группы
        /// </summary>
        public PriceStructures PriceStructures => SelectedGroup == null ? null : _priceDictionary[SelectedGroup];


        #region ICommand

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; }

        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentsCommand { get; }

        public ICommand AddProductIncludedCommand { get; }
        public ICommand RemoveProductIncludedCommand { get; }

        #endregion

        protected BaseGroupsViewModel(IUnityContainer container) : base(container)
        {
            AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => SelectedGroup != null);

            ChangeFacilityCommand = new DelegateCommand<TGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<TGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new DelegateCommand<TGroup>(ChangePaymentsCommand_Execute);

            AddProductIncludedCommand = new DelegateCommand(AddProductIncludedCommand_Execute, () => SelectedGroup != null);
            RemoveProductIncludedCommand = new DelegateCommand(RemoveProductIncludedCommand_Execute, () => SelectedProductIncluded != null);

            if(_blocks == null)
                _blocks = UnitOfWork.Repository<ProductBlock>().Find(x => true);
        }

        #region Commands

        #region AddCommand

        protected abstract void AddCommand_Execute();

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

            //удаление из группы
            if (Groups.Contains(SelectedGroup))
            {
                Groups.Remove(SelectedGroup);
            }
            //удаление из подгруппы
            else
            {
                var group = Groups.Single(x => x.Groups != null && x.Groups.Contains(SelectedGroup as TSubGroup));
                group.Groups.Remove(SelectedGroup as TSubGroup);

                //если группа стала пустая - удалить
                if (!group.Groups.Any())
                {
                    Groups.Remove(group);
                }
            }

            SelectedGroup = default(TGroup);
        }

        private async void ChangeProductCommand_Execute(TGroup wrappersGroup)
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(wrappersGroup.Product?.Model);
            if (product == null || product.Id == wrappersGroup.Product.Id) return;
            product = await UnitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            wrappersGroup.Product = new ProductWrapper(product);
            RefreshPrice(wrappersGroup);
        }

        private async void ChangeFacilityCommand_Execute(TGroup wrappersGroup)
        {
            var facilities = await UnitOfWork.Repository<Facility>().GetAllAsNoTrackingAsync();
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities, wrappersGroup.Facility?.Id);
            if (facility == null) return;
            facility = await UnitOfWork.Repository<Facility>().GetByIdAsync(facility.Id);
            wrappersGroup.Facility = new FacilityWrapper(facility);
        }

        private async void ChangePaymentsCommand_Execute(TGroup wrappersGroup)
        {
            var sets = await UnitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTrackingAsync();
            var set = Container.Resolve<ISelectService>().SelectItem(sets, wrappersGroup.PaymentConditionSet?.Id);
            if (set == null) return;
            set = await UnitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(set.Id);
            wrappersGroup.PaymentConditionSet = new PaymentConditionSetWrapper(set);
        }


        #endregion
    }
}