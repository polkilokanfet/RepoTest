using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class EmployeeDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            //потенциальные персоны
            _getEntitiesForSelectPersonCommand = () =>
            {
                var persons = UnitOfWork.Repository<Person>().GetAll();
                var except = UnitOfWork.Repository<Employee>().GetAll().Select(employee => employee.Person).Distinct();
                return persons.Except(except).ToList();
            };
        }
    }
}