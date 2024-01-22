using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.View;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class StructureCostsCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IRegionManager _regionManager;
        private readonly IUnitOfWork _unitOfWork;

        public StructureCostsCommand(Market2ViewModel viewModel, IRegionManager regionManager, IUnitOfWork unitOfWork)
        {
            _viewModel = viewModel;
            _regionManager = regionManager;
            _unitOfWork = unitOfWork;
        }

        protected override void ExecuteMethod()
        {
            var salesUnits = _unitOfWork.Repository<SalesUnit>().Find(salesUnit => salesUnit.Project.Id == _viewModel.SelectedProjectItem.Project.Id);
            _regionManager.RequestNavigateContentRegion<PriceCalculationView>(
                new NavigationParameters
                {
                    { nameof(SalesUnit), salesUnits }
                });
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedProjectItem != null;
        }
    }
}