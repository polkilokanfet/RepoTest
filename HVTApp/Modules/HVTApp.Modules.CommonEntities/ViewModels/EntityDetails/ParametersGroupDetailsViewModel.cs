using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ParametersGroupDetailsViewModel : BaseDetailsViewModel<ParameterGroupWrapper>
    {
        public ParametersGroupDetailsViewModel(ParameterGroupWrapper item) : base(item)
        {
        }
    }
}