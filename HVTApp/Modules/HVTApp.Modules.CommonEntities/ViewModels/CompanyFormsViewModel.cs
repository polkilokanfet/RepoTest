using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyFormsViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private CompanyFormWrapper _selectedCompanyForm;

        public CompanyFormsViewModel(IUnitOfWork unitOfWork, IDialogService dialogService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;

            CompanyForms =
                new ObservableCollection<CompanyFormWrapper>(
                    _unitOfWork.CompanyForms.GetAll().Select(x => new CompanyFormWrapper(x)));

            NewCompanyFormCommand = new DelegateCommand(NewCompanyFormCommand_Execute, NewCompanyFormCommand_CanExecute);
            EditCompanyFormCommand = new DelegateCommand(EditCompanyFormCommand_Execute, EditCompanyFormCommand_CanExecute);
            DeleteCompanyFormCommand = new DelegateCommand(DeleteCompanyFormCommand_Execute, DeleteCompanyFormCommand_CanExecute);
        }

        public ObservableCollection<CompanyFormWrapper> CompanyForms { get; }

        public CompanyFormWrapper SelectedCompanyForm
        {
            get { return _selectedCompanyForm; }
            set
            {
                _selectedCompanyForm = value;
                ((DelegateCommand)EditCompanyFormCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)DeleteCompanyFormCommand).RaiseCanExecuteChanged();
            }
        }

        #region CRUD Commands

        public ICommand NewCompanyFormCommand { get; }
        public ICommand EditCompanyFormCommand { get; }
        public ICommand DeleteCompanyFormCommand { get; }

        private void NewCompanyFormCommand_Execute()
        {
            CompanyForm companyForm = new CompanyForm();
            CompanyFormDetailsViewModel companyFormDetailsViewModel = new CompanyFormDetailsViewModel(new CompanyFormWrapper(companyForm));
            bool? dialogResult = _dialogService.ShowDialog(companyFormDetailsViewModel);

            if (dialogResult.HasValue && dialogResult.Value)
            {
                companyFormDetailsViewModel.CompanyFormWrapper.AcceptChanges();
                CompanyForms.Add(companyFormDetailsViewModel.CompanyFormWrapper);

                _unitOfWork.CompanyForms.Add(companyForm);
                _unitOfWork.Complete();
            }
        }

        private bool NewCompanyFormCommand_CanExecute()
        {
            return true;
        }

        private void EditCompanyFormCommand_Execute()
        {
            CompanyFormDetailsViewModel companyFormDetailsViewModel = new CompanyFormDetailsViewModel(SelectedCompanyForm);
            bool? dialogResult = _dialogService.ShowDialog(companyFormDetailsViewModel);

            if (dialogResult.HasValue && dialogResult.Value)
            {
                SelectedCompanyForm.AcceptChanges();
                _unitOfWork.Complete();
            }
            else
            {
                SelectedCompanyForm.RejectChanges();
            }
        }

        private bool EditCompanyFormCommand_CanExecute()
        {
            return SelectedCompanyForm != null;
        }

        private void DeleteCompanyFormCommand_Execute()
        {
            CompanyForms.Remove(SelectedCompanyForm);
            _unitOfWork.CompanyForms.Delete(SelectedCompanyForm.Model);
            _unitOfWork.Complete();
        }

        private bool DeleteCompanyFormCommand_CanExecute()
        {
            return SelectedCompanyForm != null;
        }

        #endregion

    }
}
