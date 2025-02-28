using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class EditProjectUnitCommand : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISelectService _selectService;
        private readonly IDialogService _dialogService;
        private readonly ProjectViewModel1 _viewModel;
        private readonly IGetProductService _getProductService;

        #region CanExecute

        public bool CanExecute(object parameter)
        {
            return _viewModel.ProjectWrapper.Units.SelectedUnit != null;
        }

        public event EventHandler CanExecuteChanged;

        #endregion

        public EditProjectUnitCommand(IUnitOfWork unitOfWork, ISelectService selectService, IDialogService dialogService, ProjectViewModel1 viewModel, IGetProductService getProductService)
        {
            _unitOfWork = unitOfWork;
            _selectService = selectService;
            _dialogService = dialogService;
            _viewModel = viewModel;
            _getProductService = getProductService;
        }

        public void Execute(object parameter)
        {
            var unit = _viewModel.ProjectWrapper.Units.SelectedUnit;
            var projectUnitViewModel = new ProjectUnitEditViewModel(unit, _unitOfWork, _selectService, _getProductService, _dialogService);
            _dialogService.Show(projectUnitViewModel, "Редактирование единицы продаж");
        }
    }
}