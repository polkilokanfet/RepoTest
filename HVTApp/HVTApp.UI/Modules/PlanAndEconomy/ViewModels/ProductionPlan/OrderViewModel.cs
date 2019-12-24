using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels.Groups;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class OrderViewModel : OrderDetailsViewModel
    {
        private IValidatableChangeTrackingCollection<SalesUnitOrderItem> _unitsWrappers;

        public SalesUnitOrderGroupsCollection GroupsInOrder { get; } = new SalesUnitOrderGroupsCollection();
        public SalesUnitOrderGroupsCollection GroupsPotential { get; } = new SalesUnitOrderGroupsCollection();

        public ICommand SaveOrderCommand { get; }
        public ICommand AddGroupCommand { get; }
        public ICommand RemoveGroupCommand { get; }

        public ICommand ShowProductStructureCommand { get; }

        public OrderViewModel(IUnityContainer container) : base(container)
        {
            //��������� �����
            SaveOrderCommand = new DelegateCommand(
                () =>
                {
                    var changed = _unitsWrappers.ModifiedItems.ToList();

                    _unitsWrappers.AcceptChanges();
                    UnitOfWork.SaveChanges();

                    if(changed.Any())
                        Container.Resolve<IEventAggregator>().GetEvent<AfterSaveOrderItemsEvent>().Publish(changed.Select(x => x.Model));

                    ((DelegateCommand) SaveOrderCommand).RaiseCanExecuteChanged();
                },
                () =>
                {
                    //����� �������
                    var unitsIsValid = _unitsWrappers != null && _unitsWrappers.IsValid;
                    if (!unitsIsValid) return false;

                    //����� �������
                    var orderIsValid = Item != null && Item.IsValid && GroupsInOrder.Any();
                    if (!orderIsValid) return false;

                    //���� ��� �������� ��� ������������
                    if (GroupsInOrder.SelectMany(x => x.Units).Any(x => x.EndProductionPlanDate == null))
                        return false;

                    //���� ��� ������� ������
                    if (GroupsInOrder.SelectMany(x => x.Units).Any(x => string.IsNullOrEmpty(x.OrderPosition)))
                        return false;

                    //���-�� ����������
                    return _unitsWrappers.IsChanged || Item.IsChanged;
                });

            AddGroupCommand = new DelegateCommand(AddGroupCommand_Execute, () => GroupsPotential.SelectedItem != null);

            RemoveGroupCommand = new DelegateCommand(RemoveGroupCommand_Execute, () => GroupsInOrder.SelectedItem != null);

            ShowProductStructureCommand = new DelegateCommand(
                () =>
                {
                    var salesUnit = GroupsPotential.SelectedUnit?.Model ??
                                    GroupsPotential.SelectedGroup.Unit;
                    var productStructureViewModel = new ProductStructureViewModel(salesUnit);
                    Container.Resolve<IDialogService>().Show(productStructureViewModel);
                }, 
                () => GroupsPotential.SelectedItem != null);
        }

        protected override async Task AfterLoading()
        {
            //��������� ����� ��� ������� ���� � ������������� �������
            //��������� ����� ��� ������� � ������������
            var salesUnits = (await UnitOfWork.Repository<SalesUnit>().GetAllAsync())
                .Where(x => x.SignalToStartProduction.HasValue)
                .Where(x => x.Order == null || x.Order.Id == Item.Id);
            //��� ������ �� ������ � �������� �������������
            var priceCalculationItems = UnitOfWork.Repository<PriceCalculationItem>().Find(x => x.StructureCosts.All(sc => !string.IsNullOrEmpty(sc.Number)));

            _unitsWrappers = new ValidatableChangeTrackingCollection<SalesUnitOrderItem>(salesUnits
                .Select(x => new SalesUnitOrderItem(x,
                    priceCalculationItems
                        .Where(p => p.SalesUnits.Contains(x))
                        .OrderBy(p => p.OrderInTakeDate)
                        .LastOrDefault())));

            //����� � ������
            var unitsInOrder = _unitsWrappers.Where(x => x.Order != null).ToList();
            var groupsInOrder = unitsInOrder.GroupBy(x => new
            {
                FacilityId = x.Model.Facility.Id,
                ProductId = x.Model.Product.Id,
                OrderId = x.Order?.Id,
                ProjectId = x.Model.Project.Id,
                SpecificationId = x.Model.Specification?.Id,
                x.Model.EndProductionPlanDate
            }).OrderBy(x => x.Key.EndProductionPlanDate);
            GroupsInOrder.AddRange(groupsInOrder.Select(x => new SalesUnitOrderGroup(x)));

            //����� ��� ���������� � ������������
            var unitsToProduct = _unitsWrappers.Except(unitsInOrder).ToList();
            var groupsToProduct = unitsToProduct.GroupBy(x => new
            {
                FacilityId = x.Model.Facility.Id,
                ProductId = x.Model.Product.Id,
                OrderId = x.Order?.Id,
                ProjectId = x.Model.Project.Id,
                SpecificationId = x.Model.Specification?.Id,
                x.Model.EndProductionDateCalculated
            }).OrderBy(x => x.Key.EndProductionDateCalculated);
            GroupsPotential.AddRange(groupsToProduct.Select(x => new SalesUnitOrderGroup(x)));

            //�������� �� ����� ��������� ������������� ������ � ������������
            GroupsPotential.SelectedGroupChanged += group =>
            {
                ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)ShowProductStructureCommand).RaiseCanExecuteChanged();
            };

            //�������� �� ����� ��������� ������ � ������������
            GroupsInOrder.SelectedGroupChanged += group =>
            {
                ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
            };

            //�������� �� ��������� ������� ������ � ������
            Item.PropertyChanged += OnPropertyChanged;
            _unitsWrappers?.ForEach(x => x.PropertyChanged += OnPropertyChanged);

            await base.AfterLoading();
        }

        private void AddGroupCommand_Execute()
        {
            if (GroupsPotential.IsGroupSelected)
                AddGroup(GroupsPotential.SelectedGroup);

            if (GroupsPotential.IsUnitSelected)
                AddUnit(GroupsPotential.SelectedUnit);
        }

        private void AddGroup(SalesUnitOrderGroup unitsGroup)
        {
            //��������� �����
            unitsGroup.Order = Item;
            //��������� ���� �������� � �����
            unitsGroup.SignalToStartProductionDone = DateTime.Today;
            //������ �������������� ���� ������������
            unitsGroup.EndProductionPlanDate = unitsGroup.Units.First().EndProductionDateExpected;
            unitsGroup.Units.ForEach(x => x.EndProductionPlanDate = x.EndProductionDateExpected);
            //��������� ������� ������
            int orderPosition = 1;
            unitsGroup.Units.ForEach(x => x.OrderPosition = orderPosition++.ToString());
            //��������� ������ � ���� ������������
            GroupsInOrder.Add(unitsGroup);
            GroupsPotential.Remove(unitsGroup);
        }

        private void AddUnit(SalesUnitOrderItem unit)
        {
            //��������� �����
            unit.Order = Item;
            //��������� ���� �������� � �����
            unit.SignalToStartProductionDone = DateTime.Today;
            //������ �������������� ���� ������������
            unit.EndProductionPlanDate = unit.EndProductionDateExpected;
            //��������� ������� ������
            unit.OrderPosition = "1";
            //��������� ������ � ���� ������������
            GroupsInOrder.Add(new SalesUnitOrderGroup(new List<SalesUnitOrderItem> {unit}));
            //������� � ����������
            RemoveUnitFromGroup(GroupsPotential, unit);
        }

        private void RemoveUnitFromGroup(SalesUnitOrderGroupsCollection collection, SalesUnitOrderItem unit)
        {
            //������ �� ������� ���������� ������� ����
            var group = collection.Single(x => x.Units.Contains(unit));
            //��������
            group.Units.Remove(unit);
            //���� � ������ �� �������� ������, ������� ������ �� ���������
            if (!group.Units.Any())
                collection.Remove(group);
        }

        private void RemoveGroupCommand_Execute()
        {
            var salesUnitOrder = GroupsInOrder.SelectedUnit ?? (ISalesUnitOrder) GroupsInOrder.SelectedGroup;

            salesUnitOrder.Order = null;
            salesUnitOrder.SignalToStartProductionDone = null;
            salesUnitOrder.EndProductionPlanDate = null;

            if (GroupsInOrder.IsGroupSelected)
            {
                var salesUnitOrderGroup = GroupsInOrder.SelectedGroup;
                GroupsPotential.Add(salesUnitOrderGroup);
                GroupsInOrder.Remove(salesUnitOrderGroup);
            }

            if (GroupsInOrder.IsUnitSelected)
            {
                var unit = GroupsInOrder.SelectedUnit;
                GroupsPotential.Add(new SalesUnitOrderGroup(new List<SalesUnitOrderItem> { unit }));
                RemoveUnitFromGroup(GroupsInOrder, unit);
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ((DelegateCommand)SaveOrderCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
        }

        protected override void GoBackCommand_Execute()
        {
            //���� ���� �����-�� ���������
            if (((DelegateCommand)SaveOrderCommand).CanExecute())
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("����������", "��������� ���������?") == MessageDialogResult.Yes)
                {
                    ((DelegateCommand)SaveOrderCommand).Execute();
                }
            }

            RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.GoBack();
        }

    }
}