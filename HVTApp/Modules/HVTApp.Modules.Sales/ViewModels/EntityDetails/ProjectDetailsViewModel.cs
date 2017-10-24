using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
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

        private ProductUnitsGroup _productGroup;

        public ProjectDetailsViewModel(IDialogService dialogService, IUnityContainer unityContainer, IUnitOfWork unitOfWork, ProjectWrapper item, IUnityContainer container) : base(container)
        {
            _dialogService = dialogService;
            _unityContainer = unityContainer;
            _unitOfWork = unitOfWork;

            AddProjectUnitsCommand = new DelegateCommand(AddProjectUnitsCommand_Execute);
            ChangeProjectUnitsCommand = new DelegateCommand(ChangeProjectUnitsCommand_Execute, ChangeProjectUnitsCommand_CanExecute);
        }

        public ProductUnitsGroup ProductGroup
        {
            get { return _productGroup; }
            set
            {
                if (Equals(_productGroup, value)) return;
                _productGroup = value;
                ((DelegateCommand)ChangeProjectUnitsCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddProjectUnitsCommand { get; }
        public ICommand ChangeProjectUnitsCommand { get; }


        private void AddProjectUnitsCommand_Execute()
        {
            var projectUnit = new ProjectUnitWrapper(new ProjectUnit());
            var viewModel = _unityContainer.Resolve<ProductUnitsDetailsViewModel>(new ParameterOverride("item", projectUnit));
            var dialogResult = _dialogService.ShowDialog(viewModel);
            if(dialogResult.HasValue && dialogResult.Value)
                Item.ProjectUnits.Add(projectUnit);
        }

        private void ChangeProjectUnitsCommand_Execute()
        {
            var projectUnit = Item.ProjectUnits.First(x => (x.Product.Equals(ProductGroup.Product) && x.Facility.Equals(ProductGroup.Facility)));
            var viewModel = _unityContainer.Resolve<ProductUnitsDetailsViewModel>(new ParameterOverride("item", projectUnit));
            var dialogResult = _dialogService.ShowDialog(viewModel);
            if(dialogResult.HasValue && dialogResult.Value)
                Item.ProjectUnits.Add(projectUnit);
        }

        private bool ChangeProjectUnitsCommand_CanExecute()
        {
            return ProductGroup != null;
        }
    }
}