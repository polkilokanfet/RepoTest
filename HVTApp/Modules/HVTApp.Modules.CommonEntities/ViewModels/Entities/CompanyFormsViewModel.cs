using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyFormsViewModel : BaseListViewModel<CompanyFormLookup, CompanyForm, CompanyFormDetailsViewModel>
    {
        public CompanyFormsViewModel(IUnityContainer container, ICompanyFormLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
