using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class EmployeeDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            //потенциальные персоны
            _getEntitiesForSelectPersonCommand = async () =>
            {
                var persons = await UnitOfWork.Repository<Person>().GetAllAsync();
                var except = (await UnitOfWork.Repository<Employee>().GetAllAsync()).Select(x => x.Person).Distinct();
                return persons.Except(except).ToList();
            };
        }
    }
}