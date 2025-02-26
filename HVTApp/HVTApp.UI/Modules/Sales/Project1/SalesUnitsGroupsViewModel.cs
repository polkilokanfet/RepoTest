using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Comparers;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Price;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using HVTApp.UI.Modules.Sales.ViewModels;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class SalesUnitsGroupsViewModel : BaseGroupsViewModel<ProjectUnitsGroup, ProjectUnitsGroup, SalesUnit, AfterSaveSalesUnitEvent, AfterRemoveSalesUnitEvent>, IGroupsViewModel<SalesUnit, ProjectWrapper1>
    {
        private ProjectWrapper1 _projectWrapper;

        protected override bool CanRemoveGroup(ProjectUnitsGroup targetGroup)
        {
            if (targetGroup.CanRemove == false)
            {
                Container.Resolve<IMessageService>().Message("Удаление невозможно", $"Оборудованию ({targetGroup}) присвоен заводской заказ.");
                return targetGroup.CanRemove;
            }
            return true;
        }

        protected override void RemoveGroup(ProjectUnitsGroup targetGroup)
        {
            var salesUnits = targetGroup.Groups == null
                ? new List<SalesUnit> { targetGroup.SalesUnit }
                : new List<SalesUnit>(targetGroup.Groups.Select(projectUnitsGroup => projectUnitsGroup.SalesUnit));

            //если ни один юнит ещё не сохранен в БД
            if (salesUnits.All(salesUnit => UnitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id) == null))
            {
                base.RemoveGroup(targetGroup);
                return;
            }

            //проверяем не включено ли оборудование в какой-либо бюджет
            var budgetUnits = UnitOfWork.Repository<BudgetUnit>().Find(budgetUnit => budgetUnit.IsRemoved == false);
            var idIntersection = salesUnits
                .Select(salesUnit => salesUnit.Id)
                .Intersect(budgetUnits.Select(budgetUnit => budgetUnit.SalesUnit.Id))
                .ToList();
            if (idIntersection.Any())
            {
                var dr = Container.Resolve<IMessageService>().ConfirmationDialog("Это оборудование включено в бюджет. Вы уверены, что хотите удалить его?");

                if (dr)
                    salesUnits.Where(salesUnit => idIntersection.Contains(salesUnit.Id)).ForEach(salesUnit => salesUnit.IsRemoved = true);
                else
                    return;
            }

            //проверка на включение в задачи ТСП
            var salesUnitsInTasks = UnitOfWork.Repository<PriceEngineeringTask>()
                .Find(priceEngineeringTask => priceEngineeringTask.SalesUnits.Any())
                .SelectMany(priceEngineeringTask => priceEngineeringTask.SalesUnits);
            foreach (var salesUnit in salesUnits.Intersect(salesUnitsInTasks))
            {
                salesUnit.IsRemoved = true;
            }

            //проверка на включение в задачи TCE
            var salesUnitsInTce = UnitOfWork.Repository<TechnicalRequrements>()
                .Find(technicalRequrements => technicalRequrements.SalesUnits.Any())
                .SelectMany(technicalRequrements => technicalRequrements.SalesUnits);
            foreach (var salesUnit in salesUnits.Intersect(salesUnitsInTce))
            {
                salesUnit.IsRemoved = true;
            }

            UnitOfWork.Repository<SalesUnit>().DeleteRange(salesUnits.Where(salesUnit => salesUnit.IsRemoved == false));
            base.RemoveGroup(targetGroup);
        }

        protected override IEnumerable<SalesUnit> GetUnitsForTotalRemove()
        {
            return base.GetUnitsForTotalRemove().Where(salesUnit => !salesUnit.IsRemoved);
        }

        /// <summary>
        /// Изменить производителя
        /// </summary>
        public ICommand ChangeProducerCommand { get; }

        /// <summary>
        /// Удалить производителя
        /// </summary>
        public ICommand RemoveProducerCommand { get; }

        /// <summary>
        /// Перенести оборудование в другой проект
        /// </summary>
        public DelegateLogCommand ChangeProjectCommand { get; }

        public SalesUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
            ChangeProjectCommand = new DelegateLogCommand(
                () =>
                {
                    var projectUnitsGroup = Groups.SelectedGroup;

                    var projects = UnitOfWork.Repository<Project>().Find(project1 => project1.Manager.Id == GlobalAppProperties.User.Id);
                    var project = Container.Resolve<ISelectService>().SelectItem(projects);
                    if (project == null) return;
                    project = UnitOfWork.Repository<Project>().GetById(project.Id);
                    projectUnitsGroup.Project = new ProjectSimpleWrapper(project);
                    base.RemoveGroup(projectUnitsGroup);

                    ChangeProjectCommand.RaiseCanExecuteChanged();
                },
                () => true);

            ChangeProducerCommand = new DelegateCommand<ProjectUnitsGroup>(
                projectUnitsGroup =>
                {
                    var producers = UnitOfWork.Repository<Company>().Find(company => company.ActivityFilds.Select(af => af.ActivityFieldEnum).Contains(ActivityFieldEnum.ProducerOfHighVoltageEquipment));
                    var producer = Container.Resolve<ISelectService>().SelectItem(producers, projectUnitsGroup.Producer?.Model.Id);
                    if (producer == null) return;
                    producer = UnitOfWork.Repository<Company>().GetById(producer.Id);
                    projectUnitsGroup.Producer = new CompanySimpleWrapper(producer);
                    ((DelegateCommand<ProjectUnitsGroup>)RemoveProducerCommand).RaiseCanExecuteChanged();

                    //если групповое действие
                    if (IsGroupActionMode && this.Groups.SelectedGroups != null && this.Groups.SelectedGroups.Length > 1)
                    {
                        foreach (var selectedGroup in Groups.SelectedGroups)
                        {
                            if (selectedGroup is ProjectUnitsGroup grp)
                            {
                                if (Equals(grp, projectUnitsGroup))
                                    continue;

                                if (grp.Specification == null)
                                    grp.Producer = projectUnitsGroup.Producer;
                            }
                        }
                    }

                },
                projectUnitsGroup => projectUnitsGroup?.Specification == null);

            RemoveProducerCommand = new DelegateCommand<ProjectUnitsGroup>(
                projectUnitsGroup =>
                {
                    projectUnitsGroup.Producer = null;
                    ((DelegateCommand<ProjectUnitsGroup>)RemoveProducerCommand).RaiseCanExecuteChanged();
                },
                projectUnitsGroup => projectUnitsGroup?.Specification == null && projectUnitsGroup?.Producer != null);
        }

        protected override List<ProjectUnitsGroup> GetGroups(IEnumerable<SalesUnit> units)
        {
            return units.GroupBy(salesUnit => salesUnit, new SalesUnitsGroupsComparer())
                .OrderBy(x => x.Key, new ProductCostComparer())
                .Select(x => new ProjectUnitsGroup(x.ToList())).ToList();
        }

        public void Load(IEnumerable<SalesUnit> units, ProjectWrapper1 parentWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            Load(units, unitOfWork, isNew);
            _projectWrapper = parentWrapper;
            _projectWrapper.PropertyChanged += (sender, args) => { AddCommand.RaiseCanExecuteChanged(); };
        }

        protected override DateTime GetPriceDate(ProjectUnitsGroup @group)
        {
            return @group.RealizationDateCalculated;
        }

        #region AddCommand

        protected override void AddCommand_Execute()
        {
            //создаем новый юнит и привязываем его к объекту
            var salesUnit = new SalesUnitWrapper(new SalesUnit());
            if (_projectWrapper != null) salesUnit.Project = new ProjectWrapper(_projectWrapper.Model); ;

            //создаем модель для диалога
            var viewModel = new SalesUnitsViewModel(salesUnit, Container, UnitOfWork);

            //заполняем юнит начальными данными
            FillingSalesUnit(viewModel.ViewModel.Item);

            //диалог с пользователем
            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);
            if (!result.HasValue || !result.Value) return;

            //клонируем юниты
            var units = CloneSalesUnits(viewModel.ViewModel.Item.Model, viewModel.Amount);

            var group = new ProjectUnitsGroup(units.ToList());
            Groups.Add(group);
            RefreshPrice(group);
            Groups.SelectedGroup = group;
        }

        protected override bool AddCommand_CanExecute()
        {
            return _projectWrapper != null && _projectWrapper.IsValid;
        }

        /// <summary>
        /// Заполнение юнита по выбранной группе
        /// </summary>
        /// <param name="salesUnitWrapper"></param>
        private void FillingSalesUnit(SalesUnitWrapper salesUnitWrapper)
        {
            if (Groups.SelectedGroup == null)
            {
                var paymentConditionSet = UnitOfWork.Repository<PaymentConditionSet>()
                        .Find(conditionSet => conditionSet.Id == GlobalAppProperties.Actual.PaymentConditionSet.Id)
                        .First();
                salesUnitWrapper.PaymentConditionSet = new PaymentConditionSetWrapper(paymentConditionSet);
                salesUnitWrapper.ProductionTerm = GlobalAppProperties.Actual.StandartTermFromStartToEndProduction;

                return;
            }

            salesUnitWrapper.Cost = Groups.SelectedGroup.Cost;
            salesUnitWrapper.Facility = new FacilityWrapper(Groups.SelectedGroup.Facility.Model);
            salesUnitWrapper.PaymentConditionSet = new PaymentConditionSetWrapper(Groups.SelectedGroup.PaymentConditionSet.Model);
            salesUnitWrapper.ProductionTerm = Groups.SelectedGroup.ProductionTerm;
            salesUnitWrapper.Product = new ProductWrapper(Groups.SelectedGroup.Product.Model);
            salesUnitWrapper.DeliveryDateExpected = Groups.SelectedGroup.DeliveryDateExpected;
            if (Groups.SelectedGroup.CostDelivery.HasValue)
            {
                if (Groups.SelectedGroup.Groups != null &&
                    Groups.SelectedGroup.Groups.Any() &&
                    !Groups.SelectedGroup.Groups.First().CostDelivery.HasValue)
                {
                    salesUnitWrapper.CostDelivery = null;
                }
                else
                {
                    salesUnitWrapper.CostDelivery = Groups.SelectedGroup.CostDelivery / Groups.SelectedGroup.Amount;
                }
            }

            //создаем зависимое оборудование
            foreach (var prodIncl in Groups.SelectedGroup.ProductsIncluded)
            {
                var pi = new ProductIncluded { Product = prodIncl.Model.Product, Amount = prodIncl.Model.Amount };
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
    }
}