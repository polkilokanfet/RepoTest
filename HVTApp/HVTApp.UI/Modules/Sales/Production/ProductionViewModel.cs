using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Production
{
    public class ProductionViewModel : LoadableExportableExpandCollapseViewModel
    {
        private ProductionGroup _selectedInProduction;
        private IEnumerable<ProductionGroup> _groupsInProduction;

        public ObservableCollection<ProductionGroup> GroupsInProduction { get; } = new ObservableCollection<ProductionGroup>();

        public ICommandRaiseCanExecuteChanged OpenOrderTaskCommand { get; }

        public ProductionGroup SelectedInProduction
        {
            get => _selectedInProduction;
            set
            {
                SetProperty(ref _selectedInProduction, value, () => OpenOrderTaskCommand.RaiseCanExecuteChanged());
            }
        }

        public ProductionViewModel(IUnityContainer container) : base(container)
        {
            OpenOrderTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var salesUnits = SelectedInProduction.ProductionItems.Select(item => item.Model);
                    var priceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>()
                        .Find(task =>
                            task.ParentPriceEngineeringTasksId.HasValue &&
                            task.SalesUnits.Any() &&
                            task.SalesUnits.AllContainsInById(salesUnits))
                        .FirstOrDefault();

                    if (priceEngineeringTask != null)
                        container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceEngineeringTasksViewManager>(new NavigationParameters{{nameof(PriceEngineeringTask), priceEngineeringTask}});
                    else
                        container.Resolve<IMessageService>().Message("Информация", "Задача не найдена...");
                },
                () => SelectedInProduction != null);
        }

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //оборудование, которое уже размещено в производстве
            _groupsInProduction = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetAllOfCurrentUser()
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
                .Select(x => new ProductionGroup(x));
        }

        protected override void BeforeGetData()
        {
            GroupsInProduction.Clear();
            SelectedInProduction = null;
        }

        protected override void AfterGetData()
        {
            GroupsInProduction.AddRange(_groupsInProduction);
        }
    }
}