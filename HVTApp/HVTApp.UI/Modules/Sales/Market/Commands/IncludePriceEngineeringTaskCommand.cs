using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Market.Items;
using HVTApp.UI.PriceEngineering.View;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class IncludePriceEngineeringTaskCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;
        private readonly ISelectService _selectService;

        MarketSalesUnitsItem MarketSalesUnitsItem => _viewModel.SelectedItem as MarketSalesUnitsItem;

        public IncludePriceEngineeringTaskCommand(Market2ViewModel viewModel, IUnitOfWork unitOfWork, IRegionManager regionManager, ISelectService selectService)
        {
            _viewModel = viewModel;
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;
            _selectService = selectService;
        }

        protected override void ExecuteMethod()
        {
            var engineeringTasksList = _unitOfWork.Repository<PriceEngineeringTasks>()
                .Find(tasks => tasks.UserManager.Id == GlobalAppProperties.User.Id);
            var priceEngineeringTasks = _selectService.SelectItem(engineeringTasksList);
            if (priceEngineeringTasks == null)
                return;

            _regionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewManager>(
                    new NavigationParameters
                    {
                        { nameof(PriceEngineeringTasks), priceEngineeringTasks },
                        { nameof(SalesUnit), MarketSalesUnitsItem.SalesUnits }
                    });
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedItem is MarketSalesUnitsItem;
        }
    }
}