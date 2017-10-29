using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class CompanyFormDetailsViewModel : BaseDetailsViewModel<CompanyFormWrapper, CompanyForm, AfterSaveCompanyFormEvent>
    {
        public CompanyFormDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}
