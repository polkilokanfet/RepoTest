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
        private SalesUnitsGroup _selectedGroup;
        private ProductIncludedWrapper _selectedProductIncluded;

        /// <summary>
        /// ������
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitsGroup> Groups { get; }

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
                              .Select(x => new SalesUnitsGroup(x));

            Groups = new ValidatableChangeTrackingCollection<SalesUnitsGroup>(groups);

            AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => SelectedGroup != null);
            ChangeFacilityCommand = new DelegateCommand<SalesUnitsGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<SalesUnitsGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new DelegateCommand<SalesUnitsGroup>(ChangePaymentsCommand_Execute);

            AddProductIncludedCommand = new DelegateCommand(AddProductIncludedCommand_Execute, () => SelectedGroup != null);
            RemoveProductIncludedCommand = new DelegateCommand(RemoveProductIncludedCommand_Execute, () => SelectedProductIncluded != null);
        }

        protected override async Task LoadedAsyncMethod()
        {
            _blocks = await UnitOfWork.Repository<ProductBlock>().GetAllAsync();
            Groups.ForEach(RefreshPrice);
        }

        private List<ProductBlock> _blocks;
        private readonly Dictionary<SalesUnitsGroup, PriceStructures> _priceDictionary = new Dictionary<SalesUnitsGroup, PriceStructures>();

        protected void RefreshPrice(SalesUnitsGroup group)
        {
            if (group == null) return;

            var priceDate = group.OrderInTakeDate < DateTime.Today ? group.OrderInTakeDate : DateTime.Today;
            var priceTerm = CommonOptions.ActualOptions.ActualPriceTerm;

            if (!_priceDictionary.ContainsKey(group)) _priceDictionary.Add(group, null);

            _priceDictionary[group] = new PriceStructures(group.Model, priceDate, priceTerm, _blocks);

            group.Price = _priceDictionary[group].Total;
            OnPropertyChanged(nameof(PriceStructures));

            group.Groups?.ForEach(RefreshPrice);
        }

        /// <summary>
        /// ��������� ������������� ��������� ������
        /// </summary>
        public PriceStructures PriceStructures => SelectedGroup == null ? null : _priceDictionary[SelectedGroup];

        /// <summary>
        /// ��������� ������.
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
                OnPropertyChanged(nameof(PriceStructures));
            }
        }

        //��������� ��������� �������
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
            //������� ����� ���� � ����������� ��� � �������
            var salesUnit = new SalesUnitWrapper(new SalesUnit());
            if(_project != null) salesUnit.Project = new ProjectWrapper(_project);

            //������� ������ ��� �������
            var viewModel = new SalesUnitsViewModel(salesUnit, Container, _unitOfWork);

            //��������� ���� ���������� �������
            if (SelectedGroup != null)
            {
                viewModel.ViewModel.Item.Cost = SelectedGroup.Cost;
                viewModel.ViewModel.Item.Facility = SelectedGroup.Facility;
                viewModel.ViewModel.Item.PaymentConditionSet = SelectedGroup.PaymentConditionSet;
                viewModel.ViewModel.Item.ProductionTerm = SelectedGroup.ProductionTerm;
                viewModel.ViewModel.Item.Product = SelectedGroup.Product;
                viewModel.ViewModel.Item.DeliveryDateExpected = SelectedGroup.DeliveryDateExpected;
                
                //������� ��������� ������������
                foreach (var prodIncl in SelectedGroup.ProductsIncluded)
                {
                    var pi = new ProductIncluded { Product = prodIncl.Product.Model, Amount = prodIncl.Amount };
                    viewModel.ViewModel.Item.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
                }
            }

            //������ � �������������
            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);

            if (!result.HasValue || !result.Value) return;

            //��������� �����
            var units = new List<SalesUnit>();
            for (int i = 0; i < viewModel.Amount; i++)
            {
                var unit = (SalesUnit)viewModel.ViewModel.Item.Model.Clone();
                unit.Id = Guid.NewGuid();
                unit.ProductsIncluded = new List<ProductIncluded>();
                units.Add(unit);
            }

            var group = new SalesUnitsGroup(units);
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
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("��������", "�������?") == MessageDialogResult.No)
                return;

            SelectedGroup.ProductsIncluded.Remove(SelectedProductIncluded);
            RefreshPrice(SelectedGroup);
        }

        private void RemoveCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("��������", "�������?") == MessageDialogResult.No)
                return;

            //�������� �� ������ ��� �� ���������
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

        private async void ChangeProductCommand_Execute(SalesUnitsGroup group)
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(group.Product?.Model);
            if (product == null || product.Id == group.Product.Id) return;
            product = await _unitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            group.Product = new ProductWrapper(product);
            RefreshPrice(group);
        }

        private async void ChangeFacilityCommand_Execute(SalesUnitsGroup group)
        {
            var facilities = await _unitOfWork.Repository<Facility>().GetAllAsNoTrackingAsync();
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities, group.Facility?.Id);
            if (facility == null) return;
            facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(facility.Id);
            group.Facility = new FacilityWrapper(facility);
        }

        private async void ChangePaymentsCommand_Execute(SalesUnitsGroup group)
        {
            var sets = await _unitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTrackingAsync();
            var set = Container.Resolve<ISelectService>().SelectItem(sets, group.PaymentConditionSet?.Id);
            if (set == null) return;
            set = await _unitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(set.Id);
            group.PaymentConditionSet = new PaymentConditionSetWrapper(set);
        }


        #endregion

        public virtual async Task SaveChanges()
        {
            //��������� ���������
            var added = Groups.AddedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups).ToList();
            added = added.Concat(Groups.AddedItems).ToList();
            _unitOfWork.Repository<SalesUnit>().AddRange(added.Select(x => x.Model).Distinct());

            //������� ���������
            var removed = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.RemovedItems).ToList();
            removed = Groups.RemovedItems.Concat(removed).ToList();
            removed = removed.Concat(Groups.RemovedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups)).ToList();
            _unitOfWork.Repository<SalesUnit>().DeleteRange(removed.Select(x => x.Model).Distinct());

            var modified = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.ModifiedItems).ToList();
            modified = Groups.ModifiedItems.Concat(modified).ToList();

            Groups.AcceptChanges();
            await _unitOfWork.SaveChangesAsync();

            var eventAggregator = Container.Resolve<IEventAggregator>();

            //�������� �� ����������
            added.Concat(modified).Select(x => x.Model).Distinct().ForEach(x => eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Publish(x));
            removed.Select(x => x.Model).ForEach(x => eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Publish(x));
        }

        public virtual bool CanSaveChanges()
        {
            return Groups.Any() && Groups.IsValid;
        }
    }
}