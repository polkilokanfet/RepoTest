using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Settings.Views;

namespace HVTApp.Modules.Settings.Menus
{
    public class SettingsMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var root = new NavigationItem("Смена пароля", typeof(PasswordView));
            Items.Add(root);
        }
    }
}
