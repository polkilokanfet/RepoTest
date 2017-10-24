using System;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class EquipmentsViewModel : BaseListViewModel<ProductWrapper, EquipmentDetailsViewModel>
    {
        public EquipmentsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : 
            base(container)
        {
           unitOfWork.Products.GetAll().Select(x => new ProductWrapper(x)).ForEach(Items.Add);
        }
    }
}
