using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public class CompanyDetailsViewModel : BaseDetailsViewModel<CompanyWrapper, Company, AfterSaveCompanyEvent>
    {
        private readonly ISelectService _selectService;
        private ActivityFieldWrapper _selectedActivityField;

        public CompanyDetailsViewModel(IUnityContainer container, ISelectService selectService) : base(container)
        {
            _selectService = selectService;

            Forms = new ObservableCollection<CompanyFormWrapper>(UnitOfWork.CompanyForms.GetAll().Select(x => new CompanyFormWrapper(x)));

            SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute);
            RemoveParentCompanyCommand = new DelegateCommand(RemoveParentCompanyCommand_Execute);
            AddActivityFieldCommand = new DelegateCommand(AddActivityFieldCommand_Execute);
            RemoveActivityFieldCommand = new DelegateCommand(RemoveActivityFieldCommand_Execute, RemoveActivityFieldCommand_CanExecute);
        }

        #region Commands

        public ICommand SelectParentCompanyCommand { get; }
        public ICommand RemoveParentCompanyCommand { get; }
        public ICommand AddActivityFieldCommand { get; }
        public ICommand RemoveActivityFieldCommand { get; }

        public CompanyWrapper Company => Item;

        public ObservableCollection<CompanyFormWrapper> Forms { get; }

        public ActivityFieldWrapper SelectedActivityField
        {
            get { return _selectedActivityField; }
            set
            {
                _selectedActivityField = value;
                ((DelegateCommand)RemoveActivityFieldCommand).RaiseCanExecuteChanged();
            }
        }

        private void AddActivityFieldCommand_Execute()
        {
            var fields = UnitOfWork.ActivityFields.GetAll().Select(x => new ActivityFieldWrapper(x)).Except(Company.ActivityFilds);
            var field = _selectService.SelectItem(fields);
            if (field != null && !Company.ActivityFilds.Contains(field))
                Company.ActivityFilds.Add(field);
        }

        private void RemoveActivityFieldCommand_Execute()
        {
            Company.ActivityFilds.Remove(SelectedActivityField);
        }

        private bool RemoveActivityFieldCommand_CanExecute()
        {
            return SelectedActivityField != null;
        }

        private void RemoveParentCompanyCommand_Execute()
        {
            //если головная компания не назначена
            if (Company.ParentCompany == null) return;
            //удалаем головную компанию текущей компании
            Company.ParentCompany = null;
        }

        private void SelectParentCompanyCommand_Execute()
        {
            //компании, которые не могут быть головной (дочернии и т.д.)
            IEnumerable<CompanyWrapper> exceptCompanies = Company.GetAllChilds().Concat(new[] {this.Company});
            //возможные головные компании
            IEnumerable<CompanyWrapper> possibleParents = UnitOfWork.Companies.GetAll().Select(x => new CompanyWrapper(x)).Except(exceptCompanies);
            //выбор одной из компаний
            CompanyWrapper possibleParent = _selectService.SelectItem(possibleParents, Company.ParentCompany);

            if (possibleParent != null && !Equals(possibleParent, Company.ParentCompany))
            {
                RemoveParentCompanyCommand_Execute();
                Company.ParentCompany = possibleParent;
            }
        }

        #endregion
    }
}
