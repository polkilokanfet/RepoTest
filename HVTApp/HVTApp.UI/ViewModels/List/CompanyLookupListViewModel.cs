using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class CompanyLookupListViewModel
    {
        protected override void OnAfterSaveEntity(Company company)
        {
            base.OnAfterSaveEntity(company);
            var companies = Lookups.Where(x => x.ParentCompany != null && x.ParentCompany.Id == company.Id);
            foreach (var companyLookup in companies)
            {
                companyLookup.Entity.ParentCompany = company;
                companyLookup.Refresh();
            }
        }
    }
}