using System.Linq;
using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompaniesViewModel : BaseListViewModel<CompanyLookup, Company, CompanyDetailsViewModel>
    {
        public CompaniesViewModel(IUnityContainer container, ICompanyLookupDataService companyLookupDataService) : base(container, companyLookupDataService)
        {
            EventAggregator.GetEvent<AfterSaveCompanyEvent>().Subscribe(OnAfterSaveCompany);
        }

        private void OnAfterSaveCompany(Company company)
        {
            var companyLookup = Items.SingleOrDefault(x => x.Id == company.Id);
            if (companyLookup != null)
            {
                companyLookup.DisplayMember = company.FullName;
            }
            else
            {
                companyLookup = new CompanyLookup {Id = company.Id, DisplayMember = company.FullName};
                Items.Add(companyLookup);
            }
        }
    }
}
