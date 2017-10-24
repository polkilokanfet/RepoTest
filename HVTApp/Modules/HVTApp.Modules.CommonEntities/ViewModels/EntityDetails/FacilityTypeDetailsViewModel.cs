using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class FacilityTypeDetailsViewModel : BaseDetailsViewModel<FacilityTypeWrapper>
    {
        public FacilityTypeDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}