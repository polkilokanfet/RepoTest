using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class IncomingRequestDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            _getEntitiesForAddInPerformersCommand =
                () =>
                {
                    return UnitOfWork.Repository<Employee>()
                        .Find(x => x.Company.Id == GlobalAppProperties.Actual.OurCompany.Id)
                        .OrderBy(x => x.Person.Surname)
                        .ToList();
                };
        }
    }
}