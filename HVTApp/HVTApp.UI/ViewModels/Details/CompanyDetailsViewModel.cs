using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
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
                    var exceptCompanies = companies.Where(company => company.ParentCompanies().Select(c => c.Id).Contains(Item.Id)).Concat(new[] {Item.Model});
                    //возможные головные компании
                    return companies.Except(exceptCompanies).ToList();
                };

            //потенциальные сферы деятельности
            _getEntitiesForAddInActivityFildsCommand = () =>
            {
                var fields = UnitOfWork.Repository<ActivityField>().GetAll().ToList();

                //удаляем возможность выбора "производитель ВВА" у всех, кроме администратора
                if (GlobalAppProperties.User.Roles.All(userRole => userRole.Role != Role.Admin))
                {
                    var producerField = fields.Single(activityField => activityField.ActivityFieldEnum == ActivityFieldEnum.ProducerOfHighVoltageEquipment);
                    fields.Remove(producerField);
                }

                var fieldsTarget = fields.Where(activityField => !Item.ActivityFilds.Select(a => a.Id).Contains(activityField.Id)).ToList();

                return fieldsTarget;

            };
        }
    }
}
