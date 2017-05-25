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
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Prism.Commands;
using CompanyWrapper = HVTApp.Model.Wrappers.CompanyWrapper;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyDetailsWindowModel : IDialogRequestClose
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISelectService _selectService;
        private CompanyWrapper _company;

        public CompanyDetailsWindowModel(IUnitOfWork unitOfWork, ISelectService selectService)
        {
            _unitOfWork = unitOfWork;
            _selectService = selectService;


            Forms = new ObservableCollection<CompanyFormWrapper>(_unitOfWork.CompanyForms.GetAll());

            OkCommand = new DelegateCommand(OkCommand_Execute, OkCommand_CanExecute);
            SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute);
            RemoveParentCompanyCommand = new DelegateCommand(RemoveParentCompanyCommand_Execute);
            AddActivityFieldCommand = new DelegateCommand(AddActivityFieldCommand_Execute);

            Company = new CompanyWrapper();
        }

        public DelegateCommand OkCommand { get; }
        public DelegateCommand SelectParentCompanyCommand { get; }
        public DelegateCommand RemoveParentCompanyCommand { get; }
        public DelegateCommand AddActivityFieldCommand { get; }

        public CompanyWrapper Company
        {
            get { return _company; }
            set
            {
                if (_company != null)
                    _company.PropertyChanged -= CompanyOnPropertyChanged;
                _company = value;
                _company.PropertyChanged += CompanyOnPropertyChanged;
            }
        }

        public ObservableCollection<CompanyFormWrapper> Forms { get; }

        private void AddActivityFieldCommand_Execute()
        {
            IEnumerable<ActivityFieldWrapper> fields = _unitOfWork.ActivityFields.GetAll();
            fields = fields.Except(Company.ActivityFilds);
            _selectService.SelectItem(fields);
        }

        private void RemoveParentCompanyCommand_Execute()
        {
            //если головная компания не назначена
            if (Company.ParentCompany == null) return;
            //удаляем из списка дочерних компаний бывшей головной компании текущую компанию
            Company.ParentCompany.ChildCompanies.Remove(Company);
            //удалаем головную компанию текущей компании
            Company.ParentCompany = null;
        }

        private void SelectParentCompanyCommand_Execute()
        {
            //компании, которые не могут быть головной (дочернии и т.д.)
            IEnumerable<CompanyWrapper> exceptCompanies = Company.GetAllChilds().Concat(new[] {this.Company});
            //возможные головные компании
            IEnumerable<CompanyWrapper> possibleParents = _unitOfWork.Companies.GetAll().Except(exceptCompanies);
            //выбор одной из компаний
            CompanyWrapper possibleParent = _selectService.SelectItem(possibleParents, Company.ParentCompany);

            if (possibleParent != null && !Equals(possibleParent, Company.ParentCompany))
            {
                RemoveParentCompanyCommand_Execute();
                Company.ParentCompany = possibleParent;
            }
        }

        private void CompanyOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OkCommand.RaiseCanExecuteChanged();
        }

        private void OkCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
            Company.AcceptChanges();
            _unitOfWork.Complete();
        }

        private bool OkCommand_CanExecute()
        {
            return Company.IsChanged && Company.IsValid;
        }


        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
