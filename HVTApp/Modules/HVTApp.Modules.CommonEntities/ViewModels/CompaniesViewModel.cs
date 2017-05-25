using Prism.Commands;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Wrappers;
using HVTApp.Services.ChooseProductService;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompaniesViewModel : EditableSelectableBindableBase<CompanyWrapper>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private readonly IUnityContainer _container;
        private readonly IChooseProductService _chooseProductService;

        public CompaniesViewModel(IUnitOfWork unitOfWork, IDialogService dialogService, IUnityContainer container, IChooseProductService chooseProductService)
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

        protected override void EditItemCommand_Execute()
        {
            var companyDetailsWindowModel = _container.Resolve<CompanyDetailsWindowModel>();
            companyDetailsWindowModel.Company = SelectedItem;
            var dialogResult = _dialogService.ShowDialog(companyDetailsWindowModel);

            if (dialogResult.HasValue && dialogResult.Value)
                return;

            if (companyDetailsWindowModel.Company.IsChanged)
                companyDetailsWindowModel.Company.RejectChanges();

        }

        protected override void NewItemCommand_Execute()
        {
            CompanyDetailsWindowModel companyDetailsWindowModel = _container.Resolve<CompanyDetailsWindowModel>();
            var dialogResult = _dialogService.ShowDialog(companyDetailsWindowModel);

            if (!dialogResult.HasValue || !dialogResult.Value)
                return;

            //добавляем новую компанию
            //в базу данных
            _unitOfWork.Companies.Add(companyDetailsWindowModel.Company);
            _unitOfWork.Complete();
            //в коллекцию этого окна
            Items.Add(companyDetailsWindowModel.Company);
            //выделяем вновь добавленную компанию
            SelectedItem = companyDetailsWindowModel.Company;
        }

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
