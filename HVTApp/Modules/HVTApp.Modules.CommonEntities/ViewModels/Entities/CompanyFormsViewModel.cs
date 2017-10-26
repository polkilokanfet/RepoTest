using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.Modules.Infrastructure;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
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
