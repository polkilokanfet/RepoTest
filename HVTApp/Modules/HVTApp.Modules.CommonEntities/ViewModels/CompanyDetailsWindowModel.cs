using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Wrapper;
using Prism.Commands;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyDetailsWindowModel : IDialogRequestClose
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISelectService _selectService;
        private CompanyWrapper _companyWrapper;

        public CompanyDetailsWindowModel(IUnitOfWork unitOfWork, ISelectService selectService)
        {
            _unitOfWork = unitOfWork;
            _selectService = selectService;


            Forms = new ObservableCollection<CompanyFormWrapper>(_unitOfWork.CompanyForms.GetAll());

            OkCommand = new DelegateCommand(OkCommand_Execute, OkCommand_CanExecute);
            SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute);
            RemoveParentCompanyCommand = new DelegateCommand(RemoveParentCompanyCommand_Execute);
            AddActivityFieldCommand = new DelegateCommand(AddActivityFieldCommand_Execute);

            CompanyWrapper = new CompanyWrapper();
        }

        public DelegateCommand OkCommand { get; }
        public DelegateCommand SelectParentCompanyCommand { get; }
        public DelegateCommand RemoveParentCompanyCommand { get; }
        public DelegateCommand AddActivityFieldCommand { get; }

        public CompanyWrapper CompanyWrapper
        {
            get { return _companyWrapper; }
            set
            {
                if (_companyWrapper != null)
                    _companyWrapper.PropertyChanged -= CompanyWrapperOnPropertyChanged;
                _companyWrapper = value;
                _companyWrapper.PropertyChanged += CompanyWrapperOnPropertyChanged;
            }
        }

        public ObservableCollection<CompanyFormWrapper> Forms { get; }

        private void AddActivityFieldCommand_Execute()
        {
            IEnumerable<ActivityField> fields = _unitOfWork.ActivityFields.GetAll().Select(x => x.Model);
            fields = fields.Except(CompanyWrapper.ActivityFilds.Select(x => x.Model));
            _selectService.SelectItem(fields);
        }

        private void RemoveParentCompanyCommand_Execute()
        {
            //если головная компания не назначена
            if (CompanyWrapper.ParentCompany == null) return;
            //удаляем из списка дочерних компаний бывшей головной компании текущую компанию
            CompanyWrapper.ParentCompany.ChildCompanies.Remove(CompanyWrapper);
            //удалаем головную компанию текущей компании
            CompanyWrapper.ParentCompany = null;
        }

        private void SelectParentCompanyCommand_Execute()
        {
            List<Company> exceptCompanies = CompanyWrapper.GetAllChilds().Select(x => x.Model).ToList();
            exceptCompanies.Add(CompanyWrapper.Model);

            IEnumerable<Company> possibleParents = _unitOfWork.Companies.GetAll().Select(x => x.Model).Except(exceptCompanies);

            Company possibleParent = _selectService.SelectItem(possibleParents.Select(x => new CompanyWrapper(x)), CompanyWrapper.ParentCompany)?.Model;

            if (possibleParent != null && !Equals(possibleParent, CompanyWrapper.ParentCompany?.Model))
            {
                RemoveParentCompanyCommand_Execute();
                CompanyWrapper.ParentCompany = new CompanyWrapper(possibleParent);
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


        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
