using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyFormDetailsViewModel : BaseDetailsViewModel<CompanyFormWrapper>
    {
        public CompanyFormDetailsViewModel(CompanyFormWrapper item) : base(item)
        {
        }
    }
}
