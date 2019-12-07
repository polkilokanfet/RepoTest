using HVTApp.Infrastructure;
using HVTApp.UI.Modules.BookRegistration.Views;

namespace HVTApp.Modules.BookRegistration.Menus
{
    public class BookRegistrationMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Книга регистрации", typeof(BookRegistrationView)));
        }
    }
}
