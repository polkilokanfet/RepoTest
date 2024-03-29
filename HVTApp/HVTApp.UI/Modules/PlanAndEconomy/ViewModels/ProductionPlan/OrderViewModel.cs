using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels.Groups;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class OrderViewModel : OrderDetailsViewModel
    {
        private IValidatableChangeTrackingCollection<SalesUnitOrderItem> _unitsWrappers;

        public SalesUnitOrderGroupsCollection GroupsInOrder { get; } = new SalesUnitOrderGroupsCollection();
        //public SalesUnitOrderGroupsCollection GroupsPotential { get; } = new SalesUnitOrderGroupsCollection();

        public DelegateLogCommand SaveOrderCommand { get; }
        //public DelegateLogCommand RemoveOrderCommand { get; }
        //public DelegateLogCommand AddGroupCommand { get; }
        //public DelegateLogCommand RemoveGroupCommand { get; }

        //public DelegateLogCommand ShowProductStructureCommand { get; }

        public OrderViewModel(IUnityContainer container) : base(container)
        {
            //��������� �����
            SaveOrderCommand = new DelegateLogCommand(
                () =>
                {
                    if (UnitOfWork.SaveChanges().OperationCompletedSuccessfully)
                    {
                        Item.AcceptChanges();
                        _unitsWrappers.AcceptChanges();
                        
                        var changed = _unitsWrappers.ModifiedItems.ToList();
                        if (changed.Any())
                        {
                            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveOrderItemsEvent>().Publish(changed.Select(salesUnitOrderItem => salesUnitOrderItem.Model));
                        }
                    }
                    
                    SaveOrderCommand.RaiseCanExecuteChanged();
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

            //RemoveOrderCommand = new DelegateLogCommand(
            //    () =>
            //    {
            //        var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("��������", "�� �������, ��� ������ ������� ���� �����?");
            //        if (dr == false) return;

            //        var order = Item.Model;

            //        _unitsWrappers.ForEach(x => x.RejectChanges());

            //        foreach (var groupInOrder in GroupsInOrder)
            //        {
            //            groupInOrder.EndProductionPlanDate = null;
            //            groupInOrder.SignalToStartProductionDone = null;
            //            groupInOrder.Order = null;
            //            groupInOrder.Units.ForEach(x => x.OrderPosition = string.Empty);
            //        }

            //        _unitsWrappers.ForEach(salesUnitOrderItem => salesUnitOrderItem.AcceptChanges());

            //        if (UnitOfWork.RemoveEntity(order).OperationCompletedSuccessfully)
            //        {
            //            Container.Resolve<IEventAggregator>().GetEvent<AfterRemoveOrderEvent>().Publish(order);
            //            GoBackCommand.Execute(null);
            //        }
            //    });

            //AddGroupCommand = new DelegateLogCommand(AddGroupCommand_Execute, () => GroupsPotential.SelectedItem != null);

            //RemoveGroupCommand = new DelegateLogCommand(RemoveGroupCommand_Execute, () => GroupsInOrder.SelectedItem != null);

            //ShowProductStructureCommand = new DelegateLogCommand(
            //    () =>
            //    {
            //        var salesUnit = GroupsPotential.SelectedUnit?.Model ??
            //                        GroupsPotential.SelectedGroup.Unit;
            //        var productStructureViewModel = new ProductStructureViewModel(salesUnit);
            //        Container.Resolve<IDialogService>().Show(productStructureViewModel, "��������� ��������");
            //    }, 
            //    () => GroupsPotential.SelectedItem != null);
        }

        protected override void AfterLoading()
        {
            //��������� ����� ��� ������� ���� � ������������� �������
            //��������� ����� ��� ������� � ������������
            var salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetAllForOrderView().ToList();

            //����� �� ������
            var salesUnitsInOrder = ((ISalesUnitRepository) UnitOfWork.Repository<SalesUnit>()).GetByOrder(Item.Id).ToList();
            salesUnits.AddRange(salesUnitsInOrder);

            //List<PriceCalculation> calculations = UnitOfWork.Repository<PriceCalculation>().GetAll();
            var salesUnitOrderItems = salesUnits
                //.Select(salesUnit => new SalesUnitOrderItem(salesUnit, salesUnit.ActualPriceCalculationItem(calculations)))
                .Select(salesUnit => new SalesUnitOrderItem(salesUnit, null))
                .ToList();
            _unitsWrappers = new ValidatableChangeTrackingCollection<SalesUnitOrderItem>(salesUnitOrderItems);

            //����� � ������
            var unitsInOrder = _unitsWrappers.Where(item => item.Order != null).ToList();
            var groupsInOrder = unitsInOrder.GroupBy(item => new
            {
                FacilityId = item.Model.Facility.Id,
                ProductId = item.Model.Product.Id,
                OrderId = item.Order?.Id,
                ProjectId = item.Model.Project.Id,
                SpecificationId = item.Model.Specification?.Id,
                item.Model.EndProductionPlanDate,
                item.Model.Cost
            }).OrderBy(x => x.Key.EndProductionPlanDate);
            GroupsInOrder.AddRange(groupsInOrder.Select(x => new SalesUnitOrderGroup(x)));

            ////����� ��� ���������� � ������������
            //var unitsToProduct = _unitsWrappers.Where(item => !item.Model.IsLoosen).Except(unitsInOrder).ToList();
            //var groupsToProduct = unitsToProduct.GroupBy(item => new
            //{
            //    FacilityId = item.Model.Facility.Id,
            //    ProductId = item.Model.Product.Id,
            //    OrderId = item.Order?.Id,
            //    ProjectId = item.Model.Project.Id,
            //    SpecificationId = item.Model.Specification?.Id,
            //    item.Model.EndProductionDateCalculated,
            //    item.Model.Cost
            //}).OrderBy(x => x.Key.EndProductionDateCalculated);
            //GroupsPotential.AddRange(groupsToProduct.Select(x => new SalesUnitOrderGroup(x)));

            ////�������� �� ����� ��������� ������������� ������ � ������������
            //GroupsPotential.SelectedGroupChanged += group =>
            //{
            //    (AddGroupCommand).RaiseCanExecuteChanged();
            //    (ShowProductStructureCommand).RaiseCanExecuteChanged();
            //};

            ////�������� �� ����� ��������� ������ � ������������
            //GroupsInOrder.SelectedGroupChanged += group =>
            //{
            //    (RemoveGroupCommand).RaiseCanExecuteChanged();
            //};

            //�������� �� ��������� ������� ������ � ������
            Item.PropertyChanged += OnPropertyChanged;
            _unitsWrappers?.ForEach(x => x.PropertyChanged += OnPropertyChanged);

            base.AfterLoading();
        }

        //private void AddGroupCommand_Execute()
        //{
        //    if (GroupsPotential.IsGroupSelected)
        //        AddGroup(GroupsPotential.SelectedGroup);

        //    if (GroupsPotential.IsUnitSelected)
        //        AddUnit(GroupsPotential.SelectedUnit);
        //}

        //private void AddGroup(SalesUnitOrderGroup unitsGroup)
        //{
        //    //��������� �����
        //    unitsGroup.Order = Item;
        //    //��������� ���� �������� � �����
        //    unitsGroup.SignalToStartProductionDone = DateTime.Today;
        //    //������ �������������� ���� ������������
        //    unitsGroup.EndProductionPlanDate = unitsGroup.Units.First().EndProductionDateExpected;
        //    unitsGroup.Units.ForEach(x => x.EndProductionPlanDate = x.EndProductionDateExpected);
        //    //��������� ������� ������
        //    int orderPosition = 1;
        //    unitsGroup.Units.ForEach(x => x.OrderPosition = orderPosition++.ToString());
        //    //��������� ������ � ���� ������������
        //    GroupsInOrder.Add(unitsGroup);
        //    GroupsPotential.Remove(unitsGroup);
        //    SaveOrderCommand.RaiseCanExecuteChanged();
        //}

        //private void AddUnit(SalesUnitOrderItem unit)
        //{
        //    //��������� �����
        //    unit.Order = Item;
        //    //��������� ���� �������� � �����
        //    unit.SignalToStartProductionDone = DateTime.Today;
        //    //������ �������������� ���� ������������
        //    unit.EndProductionPlanDate = unit.EndProductionDateExpected;
        //    //��������� ������� ������
        //    unit.OrderPosition = "1";
        //    //��������� ������ � ���� ������������
        //    GroupsInOrder.Add(new SalesUnitOrderGroup(new List<SalesUnitOrderItem> {unit}));
        //    //������� � ����������
        //    RemoveUnitFromGroup(GroupsPotential, unit);
        //}

        //private void RemoveUnitFromGroup(SalesUnitOrderGroupsCollection collection, SalesUnitOrderItem unit)
        //{
        //    //������ �� ������� ���������� ������� ����
        //    var group = collection.Single(x => x.Units.Contains(unit));
        //    //��������
        //    group.Units.Remove(unit);
        //    //���� � ������ �� �������� ������, ������� ������ �� ���������
        //    if (!group.Units.Any())
        //        collection.Remove(group);
        //}

        //private void RemoveGroupCommand_Execute()
        //{
        //    var salesUnitOrder = GroupsInOrder.SelectedUnit ?? (ISalesUnitOrder) GroupsInOrder.SelectedGroup;

        //    salesUnitOrder.Order = null;
        //    salesUnitOrder.SignalToStartProductionDone = null;
        //    salesUnitOrder.EndProductionPlanDate = null;

        //    if (GroupsInOrder.IsGroupSelected)
        //    {
        //        var salesUnitOrderGroup = GroupsInOrder.SelectedGroup;
        //        GroupsPotential.Add(salesUnitOrderGroup);
        //        GroupsInOrder.Remove(salesUnitOrderGroup);
        //    }

        //    if (GroupsInOrder.IsUnitSelected)
        //    {
        //        var unit = GroupsInOrder.SelectedUnit;
        //        GroupsPotential.Add(new SalesUnitOrderGroup(new List<SalesUnitOrderItem> { unit }));
        //        RemoveUnitFromGroup(GroupsInOrder, unit);
        //    }
        //}

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveOrderCommand.RaiseCanExecuteChanged();
            //AddGroupCommand.RaiseCanExecuteChanged();
        }

        protected override void GoBackCommand_Execute()
        {
            //���� ���� �����-�� ���������
            if (SaveOrderCommand.CanExecute())
            {
                if (Container.Resolve<IMessageService>().ConfirmationDialog("����������", "��������� ���������?", defaultNo:true))
                {
                    SaveOrderCommand.Execute();
                }
            }

            RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.GoBack();
        }
    }
}