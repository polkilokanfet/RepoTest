using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompaniesViewModel : BaseListViewModel<CompanyLookup, Company, CompanyDetailsViewModel, AfterSaveCompanyEvent>
    {
        public CompaniesViewModel(IUnityContainer container, ICompanyLookupDataService companyLookupDataService) : base(container, companyLookupDataService)
        {
        }
    }
}
