using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class UserDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            _getEntitiesForSelectEmployeeCommand = async () =>
            {
                var employes = await UnitOfWork.Repository<Employee>().GetAllAsync();
                var users = await UnitOfWork.Repository<User>().GetAllAsync();
                return employes.Except(users.Select(x => x.Employee)).ToList();
            };

            _getEntitiesForAddInRolesCommand = async () =>
            {
                var roles = await UnitOfWork.Repository<UserRole>().GetAllAsync();
                return roles.Except(Item.Model.Roles).ToList();
            };
        }
    }
}