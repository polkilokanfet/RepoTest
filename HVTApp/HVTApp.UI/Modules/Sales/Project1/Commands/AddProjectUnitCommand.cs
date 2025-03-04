using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.Project1.ViewModels;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class AddProjectUnitCommand : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private readonly ISelectService _selectService;
        private readonly ProjectViewModel _viewModel;
        private readonly IGetProductService _getProductService;

        #region CanExecute

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        #endregion

        public AddProjectUnitCommand(IUnitOfWork unitOfWork, ISelectService selectService, IDialogService dialogService, ProjectViewModel viewModel, IGetProductService getProductService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;
            _selectService = selectService;
            _viewModel = viewModel;
            _getProductService = getProductService;
        }

        public void Execute(object parameter)
        {
            //создаем модель для диалога
            var projectUnitAddViewModel = new ProjectUnitAddViewModel(_viewModel.ProjectWrapper, _viewModel.SelectedUnit, _unitOfWork, _selectService, _getProductService, _dialogService);

            //диалог с пользователем
            _dialogService.ShowDialog(projectUnitAddViewModel, "Добавление оборудования в проект");
        }
    }
}