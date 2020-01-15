using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class UserDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            _getEntitiesForSelectEmployeeCommand = () =>
            {
                var employes = UnitOfWork.Repository<Employee>().GetAll();
                var users = UnitOfWork.Repository<User>().GetAll();
                return employes.Except(users.Select(x => x.Employee)).ToList();
            };

            _getEntitiesForAddInRolesCommand = () =>
            {
                var roles = UnitOfWork.Repository<UserRole>().GetAll();
                return roles.Except(Item.Model.Roles).ToList();
            };
        }
    }
}