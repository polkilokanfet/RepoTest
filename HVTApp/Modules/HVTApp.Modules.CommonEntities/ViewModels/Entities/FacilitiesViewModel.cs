using System.Linq;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class FacilitiesViewModel : BaseListViewModel<FacilityWrapper, FacilityDetailsViewModel>
    {
        public FacilitiesViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
            unitOfWork.Facilities.GetAll().Select(x => new FacilityWrapper(x)).ForEach(Items.Add);
        }
    }
}
