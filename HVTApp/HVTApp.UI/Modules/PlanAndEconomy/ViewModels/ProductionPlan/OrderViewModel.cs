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
        private IValidatableChangeTrackingCollection<SalesUnitOrder> _unitsWrappers;

        public SalesUnitOrderGroupsCollection GroupsInOrder { get; } = new SalesUnitOrderGroupsCollection();
        public SalesUnitOrderGroupsCollection GroupsPotential { get; } = new SalesUnitOrderGroupsCollection();

        public ICommand SaveOrderCommand { get; }
        public ICommand AddGroupCommand { get; }
        public ICommand RemoveGroupCommand { get; }

        public ICommand ShowProductStructureCommand { get; }

        public OrderViewModel(IUnityContainer container) : base(container)
        {
            //сохранить заказ
            SaveOrderCommand = new DelegateCommand(
                () =>
                {
                    _unitsWrappers.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveOrderEvent>().Publish(Item.Model);
                    ((DelegateCommand) SaveOrderCommand).RaiseCanExecuteChanged();
                },
                () =>
                {
                    var unitsIsValid = _unitsWrappers != null && _unitsWrappers.IsValid;
                    if (!unitsIsValid) return false;

                    var orderIsValid = Item != null && Item.IsValid && GroupsInOrder.Any();
                    if (!orderIsValid) return false;

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
            //оставляем юниты без заказов либо с редактируемым заказом
            //исключаем юниты без сигнала к производству
            var salesUnits = (await UnitOfWork.Repository<SalesUnit>().GetAllAsync())
                .Where(x => x.SignalToStartProduction.HasValue)
                .Where(x => x.Order == null || x.Order.Id == Item.Id);
            _unitsWrappers = new ValidatableChangeTrackingCollection<SalesUnitOrder>(salesUnits.Select(x => new SalesUnitOrder(x)));

            //юниты в заказе
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

            //юниты для размещения в производстве
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

            //подписка на смену выбранной потенциальной группы в производстве
            GroupsPotential.SelectedGroupChanged += group =>
            {
                ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)ShowProductStructureCommand).RaiseCanExecuteChanged();
            };

            //подписка на смену выбранной группы в производстве
            GroupsInOrder.SelectedGroupChanged += group =>
            {
                ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
            };

            //подписка на изменение свойств заказа и юнитов
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
            //фиксируем заказ
            unitsGroup.Order = Item;
            //фиксируем дату действия и заказ
            unitsGroup.SignalToStartProductionDone = DateTime.Today;
            //ставим предполагаемую дату производства
            unitsGroup.Units.ForEach(x => x.EndProductionPlanDate = x.Model.DeliveryDateExpected);
            //заполняем позиции заказа
            int orderPosition = 1;
            unitsGroup.Units.ForEach(x => x.OrderPosition = orderPosition++.ToString());
            //переносим группу в план производства
            GroupsInOrder.Add(unitsGroup);
            GroupsPotential.Remove(unitsGroup);
        }

        private void AddUnit(SalesUnitOrder unit)
        {
            //фиксируем заказ
            unit.Order = Item;
            //фиксируем дату действия и заказ
            unit.SignalToStartProductionDone = DateTime.Today;
            //ставим предполагаемую дату производства
            unit.EndProductionPlanDate = unit.Model.DeliveryDateExpected;
            //заполняем позиции заказа
            unit.OrderPosition = "1";
            //добавляем группу в план производства
            GroupsInOrder.Add(new SalesUnitOrderGroup(new List<SalesUnitOrder> {unit}));
            //удаляем в подгруппах
            RemoveUnitFromGroup(GroupsPotential, unit);
        }

        private void RemoveUnitFromGroup(SalesUnitOrderGroupsCollection collection, SalesUnitOrder unit)
        {
            //группа из которой необходимо удалить юнит
            var group = collection.Single(x => x.Units.Contains(unit));
            //удаление
            group.Units.Remove(unit);
            //если в группе не осталось юнитов, удаляем группу из коллекции
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
                GroupsPotential.Add(new SalesUnitOrderGroup(new List<SalesUnitOrder> { unit }));
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
            //если были какие-то изменения
            if (((DelegateCommand)SaveOrderCommand).CanExecute())
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Сохранение", "Сохранить изменения?") == MessageDialogResult.Yes)
                {
                    ((DelegateCommand)SaveOrderCommand).Execute();
                }
            }

            RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.GoBack();
        }

    }
}