using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class AddProjectUnitCommand : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private readonly ISelectService _selectService;
        private readonly ProjectViewModel1 _viewModel;

        #region CanExecute

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        #endregion

        public AddProjectUnitCommand(IUnitOfWork unitOfWork, ISelectService selectService, IDialogService dialogService, ProjectViewModel1 viewModel)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;
            _selectService = selectService;
            _viewModel = viewModel;
        }

        public void Execute(object parameter)
        {
            //создаем модель для диалога
            var projectUnitAddViewModel = new ProjectUnitAddViewModel(_unitOfWork, _selectService);

            //заполняем начальные данные
            projectUnitAddViewModel.ProjectUnit.CopyProps(_viewModel.ProjectWrapper.Units.SelectedUnit);

            //диалог с пользователем
            var result = _dialogService.ShowDialog(projectUnitAddViewModel);
            if (result.HasValue == false || 
                result.Value == false) 
                return;

            projectUnitAddViewModel.Result.ForEach(projectUnit => _viewModel.ProjectWrapper.Units.Add(projectUnit));
        }
    }
}