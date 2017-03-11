using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Wrapper;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompaniesViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private CompanyWrapper _selectedCompany;

        public CompaniesViewModel(IUnitOfWork unitOfWork, IDialogService dialogService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;

            Companies = new ObservableCollection<CompanyWrapper>(_unitOfWork.Companies.GetAll().Select(x => new CompanyWrapper(x)));

            NewCompanyCommand = new DelegateCommand(NewCompanyCommand_Execute, NewCompanyCommand_CanExecute);
            EditCompanyCommand = new DelegateCommand(EditCompanyCommand_Execute, EditCompanyCommand_CanExecute);
        }

        private void EditCompanyCommand_Execute()
        {
            CompanyDetailsWindowModel companyDetailsWindowModel = new CompanyDetailsWindowModel(SelectedCompany, _unitOfWork);
            _dialogService.ShowDialog(companyDetailsWindowModel);
        }

        private bool EditCompanyCommand_CanExecute()
        {
            return SelectedCompany != null;
        }

        private void NewCompanyCommand_Execute()
        {
            throw new NotImplementedException();
        }

        private bool NewCompanyCommand_CanExecute()
        {
            return true;
        }

        public ICommand NewCompanyCommand { get; set; }
        public ICommand EditCompanyCommand { get; set; }
        public ICommand DeleteCompanyCommand { get; set; }

        public ObservableCollection<CompanyWrapper> Companies { get; }

        public CompanyWrapper SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                InvalidateCommands();
            }
        }

        private void InvalidateCommands()
        {
            ((DelegateCommand)NewCompanyCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditCompanyCommand).RaiseCanExecuteChanged();
            //((DelegateCommand)DeleteCompanyCommand).RaiseCanExecuteChanged();
        }
    }
}
