using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class CompanyDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            _getEntitiesForSelectParentCompanyCommand = async () =>
            {
                var companies = await WrapperDataService.GetRepository<Company>().GetAllAsync();
                //компании, которые не могут быть головной (дочерние и т.д.)
                var exceptCompanies = companies.Where(x => Equals(x.ParentCompany?.Id, Item.Id)).Concat(new[] {Item.Model});
                //возможные головные компании
                return companies.Except(exceptCompanies).ToList();
            };

            _getEntitiesForAddInActivityFildsCommand = async () =>
            {
                var exceptIds = Item.ActivityFilds.Select(x => x.Id);
                return (await WrapperDataService.GetRepository<ActivityField>().GetAllAsync()).Where(x => !exceptIds.Contains(x.Id)).ToList();
            };
        }
    }
}
