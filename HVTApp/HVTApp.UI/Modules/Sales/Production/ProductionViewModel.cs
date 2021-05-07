using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Production
{
    public class ProductionViewModel : LoadableExportableExpandCollapseViewModel
    {
        private object _selectedToProduction;
        private object _selectedInProduction;
        private List<ProductionGroup> _groupsInProduction;
        private List<ProductionGroup> _groupsToProduction;

        public ObservableCollection<ProductionGroup> GroupsInProduction { get; } = new ObservableCollection<ProductionGroup>();
        public ObservableCollection<ProductionGroup> GroupsToProduction { get; } = new ObservableCollection<ProductionGroup>();

        public object SelectedInProduction
        {
            get => _selectedInProduction;
            set
            {
                _selectedInProduction = value;
                RemoveFromProductionCommand?.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public object SelectedToProduction
        {
            get => _selectedToProduction;
            set
            {
                _selectedToProduction = value;
                (ProductUnitCommand)?.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Разместить оборудование в производстве
        /// </summary>
        public DelegateLogCommand ProductUnitCommand { get; }

        /// <summary>
        /// Отозвать оборудование из производства
        /// </summary>
        public DelegateLogCommand RemoveFromProductionCommand { get; }

        public ProductionViewModel(IUnityContainer container) : base(container)
        {

            ProductUnitCommand = new DelegateLogCommand(
                () =>
                {
                    //подтверждение
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Размещение в производстве", "Разместить оборудование в производстве?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    //размещение в производстве
                    if (SelectedToProduction is ProductionGroup productionGroup)
                    {
                        productionGroup.SignalToStartProduction = DateTime.Today;
                        GroupsToProduction.Remove(productionGroup);
                        GroupsInProduction.Add(productionGroup);
                        productionGroup.AcceptChanges();
                    }
                    else if (SelectedToProduction is ProductionItem productionItem)
                    {
                        productionItem.SignalToStartProduction = DateTime.Today;
                        var group = GroupsToProduction.Single(productionGroup1 => productionGroup1.ProductionItems.Contains(productionItem));
                        if (group.Amount == 1)
                        {
                            GroupsToProduction.Remove(group);
                            GroupsInProduction.Add(group);
                        }
                        else
                        {
                            group.ProductionItems.Remove(productionItem);
                            GroupsInProduction.Add(new ProductionGroup(new List<ProductionItem> {productionItem}));
                        }

                        productionItem.AcceptChanges();
                    }

                    UnitOfWork.SaveChanges();

                    SelectedToProduction = null;
                }, 
                () => SelectedToProduction != null);

            RemoveFromProductionCommand = new DelegateLogCommand(
                () =>
                {
                    //подтверждение
                    var messageService = Container.Resolve<IMessageService>();
                    var dr = messageService.ShowYesNoMessageDialog("Отзыв из производства", "Отозвать оборудование из производства?", defaultNo: true);
                    if (dr != MessageDialogResult.Yes) return;

                    if (SelectedInProduction is ProductionGroup productionGroup)
                    {
                        if (productionGroup.SalesUnit.Order != null)
                        {
                            messageService.ShowOkMessageDialog("Отзыв из производства", "Отзыв из производства невозможен.\nСначала удалите заводской заказ.");
                            return;
                        }

                        productionGroup.CleanDatesOnRemoveFromProduction();

                        GroupsInProduction.Remove(productionGroup);
                        GroupsToProduction.Add(productionGroup);

                        productionGroup.AcceptChanges();
                    }
                    else if (SelectedInProduction is ProductionItem productionItem)
                    {
                        if (productionItem.Model.Order != null)
                        {
                            messageService.ShowOkMessageDialog("Отзыв из производства", "Отзыв из производства невозможен. \nСначала удалите заводской заказ.");
                            return;
                        }
                        productionItem.SignalToStartProduction = null;
                        var group = GroupsInProduction.Single(productionGroup1 => productionGroup1.ProductionItems.Contains(productionItem));
                        if (group.Amount == 1)
                        {
                            GroupsInProduction.Remove(group);
                            GroupsToProduction.Add(group);
                        }
                        else
                        {
                            group.ProductionItems.Remove(productionItem);
                            GroupsToProduction.Add(new ProductionGroup(new List<ProductionItem> {productionItem}));
                        }

                        productionItem.AcceptChanges();
                    }

                    //сохранение изменений
                    UnitOfWork.SaveChanges();

                    SelectedInProduction = null;
                },
                () => SelectedInProduction != null);
        }

        private List<SalesUnit> _salesUnitsAll;

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //все единицы текущего пользователя
            _salesUnitsAll = ((ISalesUnitRepository) UnitOfWork.Repository<SalesUnit>()).GetAllOfCurrentUser().ToList();

            //оборудование, которое уже размещено в производстве
            _groupsInProduction = _salesUnitsAll
                .Where(salesUnit => salesUnit.SignalToStartProduction.HasValue)
                .Select(salesUnit => new ProductionItem(salesUnit, UnitOfWork))
                .GroupBy(productionItem => new
                {
                    ProductId = productionItem.Model.Product.Id,
                    FacilityId = productionItem.Model.Facility.Id,
                    OrderId = productionItem.Model.Order?.Id,
                    productionItem.Model.Comment,
                    productionItem.Model.EndProductionDateCalculated
                })
                .OrderBy(x => x.Key.EndProductionDateCalculated)
                .ThenBy(x => x.Key.FacilityId)
                .Select(x => new ProductionGroup(x))
                .ToList();

            // Загрузка юнитов, которые могут быть включены в производство
            //задачи из ТСЕ, которые менеджер запустил
            List<TechnicalRequrementsTask> requrementsTasks = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(technicalRequrementsTask => technicalRequrementsTask.Start.HasValue);

            //пересечение множеств
            var salesUnits = _salesUnitsAll
                .Where(salesUnit => !salesUnit.IsRemoved)                           //не удалены
                .Where(salesUnit => !salesUnit.IsLoosen)                            //не проиграны
                .Where(salesUnit => !salesUnit.SignalToStartProduction.HasValue)    //не включены в производство
                .Intersect(requrementsTasks.SelectMany(technicalRequrementsTask => technicalRequrementsTask.Requrements.SelectMany(technicalRequrements => technicalRequrements.SalesUnits)).Distinct());

            var productionItems = salesUnits.Select(salesUnit => new ProductionItem(salesUnit, UnitOfWork)).ToList();

            _groupsToProduction = productionItems
                .Where(productionItem => !productionItem.SignalToStartProduction.HasValue)
                .GroupBy(productionItem => new
                {
                    ProductId = productionItem.Model.Product.Id,
                    FacilityId = productionItem.Model.Facility.Id,
                    productionItem.Model.Comment,
                    productionItem.Model.EndProductionDateCalculated
                })
                .OrderBy(x => x.Key.EndProductionDateCalculated)
                .ThenBy(x => x.Key.FacilityId)
                .Select(x => new ProductionGroup(x))
                .ToList();

        }

        protected override void BeforeGetData()
        {
            GroupsInProduction.Clear();
            GroupsToProduction.Clear();

            SelectedToProduction = null;
            SelectedInProduction = null;
        }

        protected override void AfterGetData()
        {
            GroupsInProduction.AddRange(_groupsInProduction);
            GroupsToProduction.AddRange(_groupsToProduction);
        }
    }
}