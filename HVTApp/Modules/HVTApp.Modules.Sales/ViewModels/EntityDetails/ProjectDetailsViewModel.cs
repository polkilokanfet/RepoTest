using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Sales.Converter;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectDetailsViewModel : BaseDetailsViewModel<ProjectWrapper>
    {
        private readonly IDialogService _dialogService;
        private readonly IUnityContainer _unityContainer;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectDetailsViewModel(IDialogService dialogService, IUnityContainer unityContainer, IUnitOfWork unitOfWork, ProjectWrapper item) : base(item)
        {
            _dialogService = dialogService;
            _unityContainer = unityContainer;
            _unitOfWork = unitOfWork;
            AddProjectUnitsCommand = new DelegateCommand(AddProjectUnitsCommand_Execute);
        }

        private void AddProjectUnitsCommand_Execute()
        {
            var projectUnit = _unitOfWork.ProjectUnits.GetWrapper();
            var viewModel = _unityContainer.Resolve<ProductUnitsDetailsViewModel>(new ParameterOverride("item", projectUnit));
            var dialogResult = _dialogService.ShowDialog(viewModel);
            if(dialogResult != null && dialogResult.Value)
                Item.ProjectUnits.Add(projectUnit);
        }

        public ProductUnitsGroup ProductGroup { get; set; }

        public ICommand AddProjectUnitsCommand { get; }
    }
}