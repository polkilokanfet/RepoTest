using System;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class EquipmentsViewModel : BaseListViewModel<EquipmentWrapper, EquipmentDetailsViewModel, Equipment>
    {
        public EquipmentsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : 
            base(unitOfWork, container, dialogService)
        {
           unitOfWork.Equipments.GetAll().ForEach(Items.Add);
        }
    }
}
