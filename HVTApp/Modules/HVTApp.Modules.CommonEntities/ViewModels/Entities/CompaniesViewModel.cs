using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class CompaniesViewModel : BaseListViewModel<CompanyLookup, Company, CompanyDetailsViewModel, AfterSaveCompanyEvent>
    {
        public CompaniesViewModel(IUnityContainer container, ICompanyLookupDataService companyLookupDataService) : base(container, companyLookupDataService)
        {
        }
    }
}
