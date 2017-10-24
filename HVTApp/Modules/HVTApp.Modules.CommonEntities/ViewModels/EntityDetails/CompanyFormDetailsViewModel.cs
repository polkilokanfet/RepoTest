using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyFormDetailsViewModel : BaseDetailsViewModel<CompanyFormWrapper>
    {
        public CompanyFormDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}
