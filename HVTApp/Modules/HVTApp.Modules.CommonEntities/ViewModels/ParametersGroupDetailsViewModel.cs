using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ParametersGroupDetailsViewModel : BaseDetailsViewModel<ParameterGroupWrapper, ParameterGroup>
    {
        public ParametersGroupDetailsViewModel(ParameterGroupWrapper item) : base(item)
        {
        }
    }
}