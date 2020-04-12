using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.Modules.Directum;

namespace HVTApp.Modules.BookRegistration.Menus
{
    public class BookRegistrationMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Исходящие", typeof(DirectumTasksOutgoingView)));
            Items.Add(new NavigationItem("Журнал переписки", typeof(BookRegistrationView)));

            if (GlobalAppProperties.User.RoleCurrent == Role.Admin
                || GlobalAppProperties.User.RoleCurrent == Role.Director
                || GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                Items.Add(new NavigationItem("Входящие запросы", typeof(IncomingRequestsView)));
            }
        }
    }
}
