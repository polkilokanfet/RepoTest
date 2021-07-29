using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Market.Items;
using HVTApp.UI.TechnicalRequrementsTasksModule;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class MakeTceTaskCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;

        public MakeTceTaskCommand(Market2ViewModel viewModel, IUnitOfWork unitOfWork, IRegionManager regionManager)
        {
            _viewModel = viewModel;
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;
        }

        protected override void ExecuteMethod()
        {
            if (_viewModel.SelectedItem != null)
            {
                List<SalesUnit> salesUnits = new List<SalesUnit>();

                //если выбран проект целиком
                if (_viewModel.SelectedItem is ProjectItem projectItem)
                {
                    salesUnits = ((ISalesUnitRepository)_unitOfWork.Repository<SalesUnit>()).GetByProject(projectItem.Project.Id).Where(salesUnit => !salesUnit.IsRemoved).ToList();
                }

                //если выбрано конкретное оборудование
                else if (_viewModel.SelectedItem is ProjectUnitsGroup projectUnitsGroup)
                {
                    salesUnits = projectUnitsGroup.SalesUnits.ToList();
                }

                _regionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(
                    new NavigationParameters
                    {
                        { nameof(SalesUnit), salesUnits }
                    });
            }
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedProjectItem != null;
        }
    }
}