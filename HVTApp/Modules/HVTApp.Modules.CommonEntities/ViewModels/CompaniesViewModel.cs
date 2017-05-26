using Prism.Commands;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Services.ChooseProductService;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompaniesViewModel : EditableBase<CompanyWrapper, CompanyDetailsWindowModel, Company>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private readonly IUnityContainer _container;
        private readonly IChooseProductService _chooseProductService;

        public CompaniesViewModel(IUnitOfWork unitOfWork, IDialogService dialogService, IUnityContainer container, IChooseProductService chooseProductService) : base(unitOfWork, container, dialogService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;
            _container = container;
            _chooseProductService = chooseProductService;

            _unitOfWork.Companies.GetAll().ForEach(Items.Add);

            RefreshCommand = new DelegateCommand(RefreshCommand_Execute);
        }

        #region Commands

        public DelegateCommand RefreshCommand { get; set; }

        protected override void RemoveItemCommand_Execute()
        {
            _chooseProductService.ChooseProduct();
        }

        protected override bool RemoveItemCommand_CanExecute()
        {
            return true;
        }

        private void RefreshCommand_Execute()
        {
            Items.Clear();
            _unitOfWork.Companies.GetAll().ForEach(Items.Add);
        }

        #endregion
    }
}
