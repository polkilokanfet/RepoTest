using System;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class EquipmentsViewModel : BaseListViewModel<ProductWrapper, EquipmentDetailsViewModel>
    {
        public EquipmentsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : 
            base(unitOfWork, container, dialogService)
        {
           unitOfWork.Products.GetAll().ForEach(Items.Add);
        }
    }
}
