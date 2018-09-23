using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class CompanyDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            //потенциальные головные компании
            _getEntitiesForSelectParentCompanyCommand = async () =>
            {
                var companies = await UnitOfWork.Repository<Company>().GetAllAsync();
                //компании, которые не могут быть головной (дочерние и т.д.)
                var exceptCompanies = companies.Where(x => x.ParentCompanies().Select(c => c.Id).Contains(Item.Id)).Concat(new[] {Item.Model});
                //возможные головные компании
                return companies.Except(exceptCompanies).ToList();
            };

            //потенциальные сферы деятельности
            _getEntitiesForAddInActivityFildsCommand = async () =>
            {
                return (await UnitOfWork.Repository<ActivityField>().GetAllAsync())
                                        .Where(x => !Item.ActivityFilds.Select(a => a.Id).Contains(x.Id)).ToList();
            };
        }
    }
}
