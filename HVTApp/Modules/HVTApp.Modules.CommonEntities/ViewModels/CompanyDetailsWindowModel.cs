using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.Wrapper;
using Prism.Commands;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyDetailsWindowModel : IDialogRequestClose
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChooseService _chooseService;

        public CompanyDetailsWindowModel(CompanyWrapper companyWrapper, IUnitOfWork unitOfWork, IChooseService chooseService)
        {
            CompanyWrapper = companyWrapper;
            _unitOfWork = unitOfWork;
            _chooseService = chooseService;

            Forms = new ObservableCollection<CompanyFormWrapper>(_unitOfWork.CompanyForms.GetAll().Select(x => new CompanyFormWrapper(x)));

            OkCommand = new DelegateCommand(OkCommand_Execute, OkCommand_CanExecute);
            SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute);

            CompanyWrapper.PropertyChanged += CompanyWrapperOnPropertyChanged;
        }

        private void SelectParentCompanyCommand_Execute()
        {
            List<Company> companies = _unitOfWork.Companies.GetAll();
            Company parentCompany = _chooseService.ChooseDialog(companies, CompanyWrapper.ParentCompany.Model);
            if (parentCompany !=null && !Equals(parentCompany, CompanyWrapper.ParentCompany.Model))
            {
                CompanyWrapper.ParentCompany = new CompanyWrapper(parentCompany);
            }
        }

        private void CompanyWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OkCommand.RaiseCanExecuteChanged();
        }

        private void OkCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
            CompanyWrapper.AcceptChanges();
            _unitOfWork.Complete();
        }

        private bool OkCommand_CanExecute()
        {
            return CompanyWrapper.IsChanged && CompanyWrapper.IsValid;
        }

        public DelegateCommand OkCommand { get; }
        public DelegateCommand SelectParentCompanyCommand { get; }

        public CompanyWrapper CompanyWrapper { get; set; }

        public ObservableCollection<CompanyFormWrapper> Forms { get; }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
