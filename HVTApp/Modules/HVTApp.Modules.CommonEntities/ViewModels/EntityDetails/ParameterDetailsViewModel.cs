using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ParameterDetailsViewModel : BaseDetailsViewModel<ParameterWrapper>
    {
        public ParameterDetailsViewModel(ParameterWrapper item) : base(item)
        {
        }
    }
}