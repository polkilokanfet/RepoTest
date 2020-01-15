using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class CompanyDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            //потенциальные головные компании
            _getEntitiesForSelectParentCompanyCommand =
                () =>
                {
                    var companies = UnitOfWork.Repository<Company>().GetAll();
                    //компании, которые не могут быть головной (дочерние и т.д.)
                    var exceptCompanies = companies.Where(x => x.ParentCompanies().Select(c => c.Id).Contains(Item.Id)).Concat(new[] {Item.Model});
                    //возможные головные компании
                    return companies.Except(exceptCompanies).ToList();
                };

            //потенциальные сферы деятельности
            _getEntitiesForAddInActivityFildsCommand = () =>
            {
                return
                    UnitOfWork.Repository<ActivityField>()
                        .GetAll()
                        .Where(x => !Item.ActivityFilds.Select(a => a.Id).Contains(x.Id))
                        .ToList();
            };
        }
    }
}
