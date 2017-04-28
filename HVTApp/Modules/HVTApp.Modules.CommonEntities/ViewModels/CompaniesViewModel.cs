using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Wrappers;
using HVTApp.Services.ChooseProductService;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompaniesViewModel : BindableBase, ISelectViewModel<CompanyWrapper>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private readonly IUnityContainer _container;
        private readonly IChooseProductService _chooseProductService;
        private CompanyWrapper _selectedCompany;
        private ICommand _selectItemCommand;
        private ICollection<CompanyWrapper> _items;

        public CompaniesViewModel(IUnitOfWork unitOfWork, IDialogService dialogService, IUnityContainer container, IChooseProductService chooseProductService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;
            _container = container;
            _chooseProductService = chooseProductService;

            Companies = new ObservableCollection<CompanyWrapper>(_unitOfWork.Companies.GetAll());

            NewCompanyCommand = new DelegateCommand(NewCompanyCommand_Execute, NewCompanyCommand_CanExecute);
            EditCompanyCommand = new DelegateCommand(EditCompanyCommand_Execute, EditCompanyCommand_CanExecute);
            DeleteCompanyCommand = new DelegateCommand(DeleteCompanyCommand_Execute);
            SelectItemCommand = new DelegateCommand(SelectItemCommand_Execute, SelectItemCommand_CanExecute);
            RefreshCommand = new DelegateCommand(RefreshCommand_Execute);
        }

        private void DeleteCompanyCommand_Execute()
        {
            _chooseProductService.ChooseProduct();
        }

        public ObservableCollection<CompanyWrapper> Companies { get; }

        public DelegateCommand NewCompanyCommand { get; set; }
        public DelegateCommand EditCompanyCommand { get; set; }
        public DelegateCommand DeleteCompanyCommand { get; set; }

        public DelegateCommand RefreshCommand { get; set; }

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

        private void RefreshCommand_Execute()
        {
            Companies.Clear();
            Companies.AddRange(_unitOfWork.Companies.GetAll());
        }

        private void EditCompanyCommand_Execute()
        {
            var companyDetailsWindowModel = _container.Resolve<CompanyDetailsWindowModel>();
            companyDetailsWindowModel.CompanyWrapper = SelectedCompany;
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
            CompanyDetailsWindowModel companyDetailsWindowModel = _container.Resolve<CompanyDetailsWindowModel>();
            var dialogResult = _dialogService.ShowDialog(companyDetailsWindowModel);

            if (!dialogResult.HasValue || !dialogResult.Value)
                return;

            //добавляем новую компанию
            //в базу данных
            _unitOfWork.Companies.Add(companyDetailsWindowModel.CompanyWrapper);
            _unitOfWork.Complete();
            //в коллекцию этого окна
            Companies.Add(companyDetailsWindowModel.CompanyWrapper);
            //выделяем вновь добавленную компанию
            SelectedCompany = companyDetailsWindowModel.CompanyWrapper;
        }

        private bool NewCompanyCommand_CanExecute()
        {
            return true;
        }


        private void InvalidateCommands()
        {
            NewCompanyCommand.RaiseCanExecuteChanged();
            EditCompanyCommand.RaiseCanExecuteChanged();
            DeleteCompanyCommand.RaiseCanExecuteChanged();
            ((DelegateCommand)SelectItemCommand).RaiseCanExecuteChanged();
        }

        #region ISelectViewModel

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

        public ICollection<CompanyWrapper> Items => Companies;

        #endregion

    }
}
