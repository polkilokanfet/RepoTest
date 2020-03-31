using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.Modules.Settings.Views;

namespace HVTApp.Modules.Settings.Menus
{
    public class SettingsMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var root = new NavigationItem("Смена пароля", typeof(PasswordView));
            Items.Add(root);

            if (GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("Admin", typeof(AdminView)));
                Items.Add(new NavigationItem("DB Backup", typeof(DataBaseBackupView)));
            }
        }
    }
}
