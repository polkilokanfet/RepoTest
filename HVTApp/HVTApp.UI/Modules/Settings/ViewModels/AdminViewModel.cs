using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Settings.ViewModels
{
    public class AdminViewModel
    {
        public ICommand Command { get; }

        public AdminViewModel(IUnityContainer container)
        {
            var unitOfWork = container.Resolve<IUnitOfWork>();
            Command = new DelegateCommand(
                () =>
                {
                    var roles = unitOfWork.Repository<UserRole>().GetAll();

                    if (roles.Any(x => x.Role == Role.Supplier))
                        return;

                    var role = new UserRole()
                    {
                        Name = "Снабженец",
                        Role = Role.Supplier
                    };
                    unitOfWork.Repository<UserRole>().Add(role);
                    unitOfWork.SaveChanges();

                    container.Resolve<IMessageService>().ShowOkMessageDialog("Ok", "Ok!");
                });
        }
    }
}