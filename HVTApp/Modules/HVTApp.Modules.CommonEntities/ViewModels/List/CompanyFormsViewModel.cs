using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class CompanyFormsViewModel : BaseLookupListViewModel<CompanyFormLookup, CompanyForm, CompanyFormDetailsViewModel, AfterSaveCompanyFormEvent>
    {
        public CompanyFormsViewModel(IUnityContainer container, ICompanyFormLookupDataService lookupDataService) : base(container, lookupDataService)
        {
        }
    }
}
