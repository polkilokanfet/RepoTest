using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Production
{
    public class ProductionViewModel : LoadableExportableExpandCollapseViewModel
    {
        private object _selectedToProduction;
        private object _selectedInProduction;

        public ObservableCollection<ProductionGroup> GroupsInProduction { get; } = new ObservableCollection<ProductionGroup>();
        public ObservableCollection<ProductionGroup> GroupsToProduction { get; } = new ObservableCollection<ProductionGroup>();

        public object SelectedInProduction
        {
            get { return _selectedInProduction; }
            set
            {
                _selectedInProduction = value;
                ((DelegateCommand)RemoveFromProductionCommand).RaiseCanExecuteChanged();
            }
        }

        public object SelectedToProduction
        {
            get { return _selectedToProduction; }
            set
            {
                _selectedToProduction = value;
                ((DelegateCommand)ProductUnitCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand ProductUnitCommand { get; }
        public ICommand RemoveFromProductionCommand { get; }

        public ProductionViewModel(IUnityContainer container) : base(container)
        {

            ProductUnitCommand = new DelegateCommand(
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

            RemoveFromProductionCommand = new DelegateCommand(
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
                            messageService.ShowOkMessageDialog("Отзыв из производства", "Отзыв из производства невозможен. \nСначала удалите заводской заказ.");
                            return;
                        }

                        productionGroup.SignalToStartProduction = null;
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

        private IEnumerable<ProductionGroup> _groupsToProduction;
        private IEnumerable<ProductionGroup> _groupsInProduction;

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //все единицы текущего пользователя
            var salesUnitsAll = UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsRemoved && !x.IsLoosen && x.Project.Manager.IsAppCurrentUser());
            //задачи из ТСЕ
            var requrementsTasks = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(x => x.Start.HasValue);

            //пересечение этих множеств
            var salesUnits = salesUnitsAll.Intersect(requrementsTasks.SelectMany(x => x.Requrements.SelectMany(r => r.SalesUnits)).Distinct()).ToList();
            var productionItems = salesUnits.Select(x => new ProductionItem(x, x.ActualPriceCalculationItem(UnitOfWork))).ToList();

            _groupsToProduction = productionItems
                .Where(x => !x.SignalToStartProduction.HasValue)
                .GroupBy(x => new
                {
                    ProductId = x.Model.Product.Id,
                    FacilityId = x.Model.Facility.Id,
                    x.Model.Comment,
                    x.Model.EndProductionDateCalculated
                })
                .OrderBy(x => x.Key.EndProductionDateCalculated)
                .ThenBy(x => x.Key.FacilityId)
                .Select(x => new ProductionGroup(x));


            _groupsInProduction = salesUnitsAll
                .Where(x => x.SignalToStartProduction.HasValue)
                .Select(x => new ProductionItem(x, x.ActualPriceCalculationItem(UnitOfWork)))
                .GroupBy(x => new
                {
                    ProductId = x.Model.Product.Id,
                    FacilityId = x.Model.Facility.Id,
                    OrderId = x.Model.Order?.Id,
                    x.Model.Comment,
                    x.Model.EndProductionDateCalculated
                })
                .OrderBy(x => x.Key.EndProductionDateCalculated)
                .ThenBy(x => x.Key.FacilityId)
                .Select(x => new ProductionGroup(x));
        }

        protected override void AfterGetData()
        {
            GroupsToProduction.Clear();
            GroupsToProduction.AddRange(_groupsToProduction);

            GroupsInProduction.Clear();
            GroupsInProduction.AddRange(_groupsInProduction);
        }
    }
}