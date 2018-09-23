using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Modules.Settings.Views;

namespace HVTApp.Modules.Settings.Menus
{
    public class ProductionMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var root = new NavigationItem("Смена пароля", typeof(PasswordView));
            Items.Add(root);
        }
    }
}
