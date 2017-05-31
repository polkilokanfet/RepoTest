using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ParameterDetailsViewModel : BaseDetailsViewModel<ParameterWrapper, Parameter>
    {
        public ParameterDetailsViewModel(ParameterWrapper item) : base(item)
        {
        }
    }
}