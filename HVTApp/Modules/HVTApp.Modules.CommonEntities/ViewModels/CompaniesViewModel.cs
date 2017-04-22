using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompaniesViewModel : BindableBase, ISelectViewModel<CompanyWrapper>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private readonly IChooseService _chooseService;
        private readonly ISelectService _selectService;
        private CompanyWrapper _selectedCompany;
        private ICommand _selectItemCommand;

        public CompaniesViewModel(IUnitOfWork unitOfWork, IDialogService dialogService, IChooseService chooseService, ISelectService selectService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;
            _chooseService = chooseService;
            _selectService = selectService;

            Companies = new ObservableCollection<CompanyWrapper>(_unitOfWork.Companies.GetAll().Select(x => CompanyWrapper.GetWrapper(x)));

            NewCompanyCommand = new DelegateCommand(NewCompanyCommand_Execute, NewCompanyCommand_CanExecute);
            EditCompanyCommand = new DelegateCommand(EditCompanyCommand_Execute, EditCompanyCommand_CanExecute);
            SelectItemCommand = new DelegateCommand(SelectItemCommand_Execute, SelectItemCommand_CanExecute);
        }

        private void EditCompanyCommand_Execute()
        {
            var companyDetailsWindowModel = new CompanyDetailsWindowModel(SelectedCompany, _unitOfWork, _chooseService, _selectService);
            var dialogResult = _dialogService.ShowDialog(companyDetailsWindowModel);

            if (dialogResult.HasValue && dialogResult.Value)
                return;

            if (companyDetailsWindowModel.CompanyWrapper.IsChanged)
                companyDetailsWindowModel.CompanyWrapper.RejectChanges();
        }

        private bool EditCompanyCommand_CanExecute()
        {
            return SelectedCompany != null;
        }

        private void NewCompanyCommand_Execute()
        {
            var companyDetailsWindowModel = new CompanyDetailsWindowModel(CompanyWrapper.GetWrapper(new Company()), _unitOfWork, _chooseService, _selectService);
            var dialogResult = _dialogService.ShowDialog(companyDetailsWindowModel);

            if (!dialogResult.HasValue || !dialogResult.Value)
                return;

            _unitOfWork.Companies.Add(companyDetailsWindowModel.CompanyWrapper.Model);
            _unitOfWork.Complete();
            Companies.Add(companyDetailsWindowModel.CompanyWrapper);
        }

        private bool NewCompanyCommand_CanExecute()
        {
            return true;
        }

        public DelegateCommand NewCompanyCommand { get; set; }
        public DelegateCommand EditCompanyCommand { get; set; }
        public DelegateCommand DeleteCompanyCommand { get; set; }

        public ObservableCollection<CompanyWrapper> Companies { get; }

        public CompanyWrapper SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                OnPropertyChanged();
                InvalidateCommands();
            }
        }

        private void InvalidateCommands()
        {
            NewCompanyCommand.RaiseCanExecuteChanged();
            EditCompanyCommand.RaiseCanExecuteChanged();
            //DeleteCompanyCommand.RaiseCanExecuteChanged();
            ((DelegateCommand)SelectItemCommand).RaiseCanExecuteChanged();
        }

        public ICommand NewItemCommand => NewCompanyCommand;

        public CompanyWrapper SelectedItem
        {
            get { return SelectedCompany; }
            set { SelectedCompany = value; }
        }

        public ICommand SelectItemCommand { get; }

        private bool SelectItemCommand_CanExecute()
        {
            return SelectedItem != null;
        }

        private void SelectItemCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
