using System;
using System.Collections;
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
            RemoveParentCompanyCommand = new DelegateCommand(RemoveParentCompanyCommand_Execute);
            AddActivityFieldCommand = new DelegateCommand(AddActivityFieldCommand_Execute);

            CompanyWrapper.PropertyChanged += CompanyWrapperOnPropertyChanged;
        }

        private void AddActivityFieldCommand_Execute()
        {
            IEnumerable<ActivityField> fields = _unitOfWork.ActivityFields.GetAll();
            fields = fields.Except(CompanyWrapper.ActivityFilds.Select(x => x.Model));
            _chooseService.ChooseDialog(fields);
        }

        private void RemoveParentCompanyCommand_Execute()
        {
            if (CompanyWrapper.ParentCompany == null)
                return;

            CompanyWrapper.ParentCompany.ChildCompanies.Remove(CompanyWrapper);
            CompanyWrapper.ParentCompany = null;
        }

        private void SelectParentCompanyCommand_Execute()
        {
            List<Company> exceptCompanies = CompanyWrapper.Model.GetAllChilds().ToList();
            exceptCompanies.Add(CompanyWrapper.Model);

            IEnumerable<Company> possibleParents = _unitOfWork.Companies.GetAll().Except(exceptCompanies);

            Company possibleParent = _chooseService.ChooseDialog(possibleParents, CompanyWrapper.ParentCompany?.Model);

            if (possibleParent != null && !Equals(possibleParent, CompanyWrapper.ParentCompany?.Model))
            {
                RemoveParentCompanyCommand_Execute();
                CompanyWrapper.ParentCompany = new CompanyWrapper(possibleParent);
                //CompanyWrapper.ParentCompany.ChildCompanies.Add(CompanyWrapper);
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
        public DelegateCommand RemoveParentCompanyCommand { get; }
        public DelegateCommand AddActivityFieldCommand { get; }

        public CompanyWrapper CompanyWrapper { get; set; }

        public ObservableCollection<CompanyFormWrapper> Forms { get; }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
