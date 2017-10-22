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
    public class FacilityTypesViewModel : BaseListViewModel<FacilityTypeWrapper, FacilityTypeDetailsViewModel>
    {
        public FacilityTypesViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
            unitOfWork.FacilityTypes.GetAll().Select(x => new FacilityTypeWrapper(x)).ForEach(Items.Add);
        }
    }
}
