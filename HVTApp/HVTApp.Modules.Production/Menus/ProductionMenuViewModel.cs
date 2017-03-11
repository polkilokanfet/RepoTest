using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Modules.Production.Views;

namespace HVTApp.Modules.Production.Menus
{
    public class ProductionMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            NavigationItem root = new NavigationItem("Production plan", typeof(ProductionPlanView));
            Items.Add(root);
        }
    }
}
