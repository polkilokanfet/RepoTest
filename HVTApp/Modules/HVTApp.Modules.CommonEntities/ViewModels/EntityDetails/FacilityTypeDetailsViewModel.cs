using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class FacilityTypeDetailsViewModel : BaseDetailsViewModel<FacilityTypeWrapper>
    {
        public FacilityTypeDetailsViewModel(FacilityTypeWrapper item) : base(item)
        {
        }
    }
}