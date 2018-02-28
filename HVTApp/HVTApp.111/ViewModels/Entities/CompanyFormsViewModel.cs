using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyFormsViewModel : BaseListViewModel<CompanyFormLookup, CompanyForm, CompanyFormDetailsViewModel, AfterSaveCompanyFormEvent>
    {
        public CompanyFormsViewModel(IUnityContainer container, ICompanyFormLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
