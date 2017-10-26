using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
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
