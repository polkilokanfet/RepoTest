using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.Wrapper;
using Prism.Regions;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyViewModel : BindableBase, INavigationAware
    {
        private IUnitOfWork _unitOfWork;
        private CompanyWrapper _companyWrapper;

        public CompanyViewModel()
        {
            AddTypeCommand = new DelegateCommand(OnAddTypeExecute);
            RemoveTypeCommand = new DelegateCommand(OnRemoveTypeExecute, CanRemoveTypeExecute);

            SaveCommand = new DelegateCommand(OnSaveExecute, CanOnSaveExecute);
            ResetCommand = new DelegateCommand(OnResetExecute, CanOnResetExecute);
        }

        public void Load(CompanyWrapper companyWrapper)
        {
            var newCompany = new Company
            {
                FullName = "NewCompany",
                Form = new CompanyForm(),
                ChildCompanies = new List<Company>(),
                Employees = new List<Employee>()
            };

            CompanyWrapper = companyWrapper ?? new CompanyWrapper(newCompany);

            CompanyWrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(CompanyWrapper.IsChanged) ||
                e.PropertyName == nameof(CompanyWrapper.IsValid))
                {
                    InvalidateCommands();
                }
            };

            InvalidateCommands();
        }

        #region Commands

        public DelegateCommand AddTypeCommand { get; }
        public DelegateCommand RemoveTypeCommand { get; }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand ResetCommand { get; }

        private void InvalidateCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
        }

        private bool CanOnResetExecute()
        {
            return CompanyWrapper != null && CompanyWrapper.IsChanged;
        }

        private void OnResetExecute()
        {
            CompanyWrapper.RejectChanges();
        }

        private bool CanOnSaveExecute()
        {
            return CompanyWrapper != null && CompanyWrapper.IsChanged && CompanyWrapper.IsValid;
        }

        private void OnSaveExecute()
        {
            CompanyWrapper.AcceptChanges();
            _unitOfWork.Complete();
        }

        private bool CanRemoveTypeExecute()
        {
            return true;
        }

        private void OnRemoveTypeExecute()
        {
        }

        private void OnAddTypeExecute()
        {
            //CompanyType companyType = new CompanyType();
            //CompanyTypeWrapper companyTypeWrapper = new CompanyTypeWrapper(companyType);
            //CompanyWrapper.CompanyTypes.Add(companyTypeWrapper);
        }

        #endregion

        public CompanyWrapper CompanyWrapper
        {
            get { return _companyWrapper; }
            private set
            {
                _companyWrapper = value;
                OnPropertyChanged();
            }
        }

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var companyWrapper = navigationContext.Parameters[nameof(Company)] as CompanyWrapper;
            _unitOfWork = navigationContext.Parameters["uow"] as IUnitOfWork;
            Load(companyWrapper);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var companyWrapper = navigationContext.Parameters[nameof(Company)] as CompanyWrapper;
            return companyWrapper != null && companyWrapper.Id == CompanyWrapper.Id;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            CompanyWrapper.RejectChanges();
        }
        #endregion
    }
}
