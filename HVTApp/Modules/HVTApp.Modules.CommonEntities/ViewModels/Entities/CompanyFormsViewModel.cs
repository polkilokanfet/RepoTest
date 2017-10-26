using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class CompanyFormsViewModel : BaseListViewModel<CompanyFormLookup, CompanyForm, CompanyFormDetailsViewModel, AfterSaveCompanyFormEvent>
    {
        public CompanyFormsViewModel(IUnityContainer container, ICompanyFormLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
