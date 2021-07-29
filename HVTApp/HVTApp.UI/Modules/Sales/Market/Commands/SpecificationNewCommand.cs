using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class SpecificationNewCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public SpecificationNewCommand(Market2ViewModel viewModel, IUnityContainer container, IRegionManager regionManager)
        {
            _viewModel = viewModel;
            _container = container;
            _regionManager = regionManager;
        }

        protected override void ExecuteMethod()
        {
            Project project = _viewModel.SelectedProjectItem.Project;

            IUnitOfWork unitOfWork = _container.Resolve<IUnitOfWork>();
            List<SalesUnit> salesUnits = ((SalesUnitRepository)unitOfWork.Repository<SalesUnit>()).GetByProject(project.Id)
                .Where(salesUnit => salesUnit.Specification == null)
                .Where(salesUnit => salesUnit.IsRemoved == false)
                .Where(salesUnit => salesUnit.IsLoosen == false)
                .ToList();

            if (salesUnits.Any())
            {
                _regionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { nameof(Project), project } });
            }
            else
            {
                _container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Всё оборудование из этого проекта или уже в спецификациях или проиграно.");
            }
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedProjectItem != null;
        }
    }
}