using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.Modules.BookRegistration.Views;

namespace HVTApp.Modules.BookRegistration.Menus
{
    public class BookRegistrationMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Журнал регистрации", typeof(BookRegistrationView)));

            if(GlobalAppProperties.User.RoleCurrent == Role.Admin || GlobalAppProperties.User.RoleCurrent == Role.Director)
                Items.Add(new NavigationItem("Поручение запросов", typeof(IncomingRequestsView)));
        }
    }
}
