using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ProductionViewModel : ViewModelBase
    {
        private object _selectedToProduction;

        public ObservableCollection<ProductionGroup> GroupsInProduction { get; } = new ObservableCollection<ProductionGroup>();
        public ObservableCollection<ProductionGroup> GroupsToProduction { get; } = new ObservableCollection<ProductionGroup>();

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
        public ICommand ReloadCommand { get; }

        public ProductionViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);

            ProductUnitCommand = new DelegateCommand(
                () =>
                {
                    //подтверждение
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Размещение в производстве", "Разместить оборудование в производстве?");
                    if (dr != MessageDialogResult.Yes) return;

                    //размещение в производстве

                    var productionGroup = SelectedToProduction as ProductionGroup;
                    if (productionGroup != null)
                    {
                        productionGroup.SignalToStartProduction = DateTime.Today;
                        GroupsToProduction.Remove(productionGroup);
                        GroupsInProduction.Add(productionGroup);
                    }

                    var productionItem = SelectedToProduction as ProductionItem;
                    if (productionItem != null)
                    {
                        productionItem.SignalToStartProduction = DateTime.Today;
                        var group = GroupsToProduction.Single(x => x.ProductionItems.Contains(productionItem));
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
                    }

                    //сохранение изменений
                    ((IValidatableChangeTracking)SelectedToProduction).AcceptChanges();
                    UnitOfWork.SaveChanges();

                    SelectedToProduction = null;

                }, 
            () => SelectedToProduction != null);

            Load();
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //все единицы текущего пользователя
            var salesUnitsAll = UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsLoosen && x.Project.Manager.IsAppCurrentUser());
            //все задачи на расчет с номерами стракчакостов
            var priceCalculationItems = UnitOfWork.Repository<PriceCalculationItem>().Find(x => x.StructureCosts.All(sc => !string.IsNullOrEmpty(sc.Number)));

            //пересечение этих множеств
            var salesUnits = salesUnitsAll.Intersect(priceCalculationItems.SelectMany(x => x.SalesUnits).Distinct()).ToList();
            var productionItems = salesUnits.Select(x => new ProductionItem(x,
                priceCalculationItems
                    .Where(p => p.SalesUnits.Contains(x))
                    .OrderBy(p => p.OrderInTakeDate)
                    .LastOrDefault())).ToList();

            var groupsToProduction = productionItems
                .Where(x => !x.SignalToStartProduction.HasValue)
                .GroupBy(x => new
                {
                    ProductId = x.Model.Product.Id,
                    FacilityId = x.Model.Facility.Id,
                    x.Model.EndProductionDateCalculated
                })
                .OrderBy(x => x.Key.EndProductionDateCalculated);

            GroupsToProduction.Clear();
            GroupsToProduction.AddRange(groupsToProduction.Select(x => new ProductionGroup(x)));

            var groupsInProduction = productionItems
                .Where(x => x.SignalToStartProduction.HasValue)
                .GroupBy(x => new
                {
                    ProductId = x.Model.Product.Id,
                    FacilityId = x.Model.Facility.Id,
                    OrderId = x.Model.Order?.Id,
                    x.Model.EndProductionDateCalculated
                })
                .OrderBy(x => x.Key.EndProductionDateCalculated);

            GroupsInProduction.Clear();
            GroupsInProduction.AddRange(groupsInProduction.Select(x => new ProductionGroup(x)));
        }
    }
}