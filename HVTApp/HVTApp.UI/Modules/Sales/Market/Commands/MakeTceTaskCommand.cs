using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
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
        private readonly IMessageService _messageService;

        public MakeTceTaskCommand(Market2ViewModel viewModel, IUnitOfWork unitOfWork, IRegionManager regionManager, IMessageService messageService)
        {
            _viewModel = viewModel;
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;
            _messageService = messageService;
        }

        protected override void ExecuteMethod()
        {
            if (_viewModel.SelectedItem != null)
            {
                var salesUnits = new List<SalesUnit>();

                //если выбран проект целиком
                if (_viewModel.SelectedItem is ProjectItem projectItem)
                {
                    salesUnits = ((ISalesUnitRepository)_unitOfWork.Repository<SalesUnit>())
                        .GetByProject(projectItem.Project.Id)
                        .Where(salesUnit => !salesUnit.IsRemoved)
                        .ToList();
                }

                //если выбрано конкретное оборудование
                else if (_viewModel.SelectedItem is ProjectUnitsGroup projectUnitsGroup)
                {
                    salesUnits = projectUnitsGroup.SalesUnits.ToList();
                }

                //то, что может идти через ТСП должно идти там
                var departments = _unitOfWork.Repository<DesignDepartment>().GetAll();
                var salesUnits1 = salesUnits.Where(salesUnit => salesUnit.Product.GetBlocks().All(block => this.HasDesignDepartment(departments, block))).ToList();
                if (salesUnits1.Any())
                {
                    _messageService.Message("ТСП", $"Проработайте следующее оборудование через ТСП:\n{salesUnits1.ToStringEnum()}");
                    salesUnits = salesUnits.Except(salesUnits1).ToList();
                }

                if (salesUnits.Any())
                {
                    _regionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(
                        new NavigationParameters
                        {
                            { nameof(SalesUnit), salesUnits }
                        });
                }
            }
        }

        private bool HasDesignDepartment(IEnumerable<DesignDepartment> departments, ProductBlock block)
        {
            return departments.Any(department => department.ProductBlockIsSuitable(block));
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedProjectItem != null;
        }
    }
}