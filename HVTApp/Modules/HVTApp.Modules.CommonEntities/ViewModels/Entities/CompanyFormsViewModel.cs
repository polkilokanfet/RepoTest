using System.Linq;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyFormsViewModel : BaseListViewModel<CompanyFormWrapper, CompanyFormDetailsViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;

        public CompanyFormsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : 
            base(unitOfWork, container, dialogService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;

            _unitOfWork.CompanyForms.GetAll().Select(x => new CompanyFormWrapper(x)).ForEach(Items.Add);
        }

        #region CRUD Commands

        //protected override void NewItemCommand_Execute()
        //{
        //    CompanyForm companyForm = new CompanyForm();
        //    CompanyFormDetailsViewModel companyFormDetailsViewModel = new CompanyFormDetailsViewModel(new CompanyFormWrapper(companyForm));
        //    bool? dialogResult = _dialogService.ShowDialog(companyFormDetailsViewModel);

        //    if (dialogResult.HasValue && dialogResult.Value)
        //    {
        //        companyFormDetailsViewModel.CompanyFormWrapper.AcceptChanges();
        //        Items.Add(companyFormDetailsViewModel.CompanyFormWrapper);

        //        _unitOfWork.CompanyForms.Add(new CompanyFormWrapper(companyForm));
        //        _unitOfWork.Complete();
        //    }
        //}

        protected override void RemoveItemCommand_Execute()
        {
            Items.Remove(SelectedItem);
            _unitOfWork.CompanyForms.Delete(SelectedItem.Model);
            _unitOfWork.Complete();
        }

        #endregion

    }
}
