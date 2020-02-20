using HVTApp.Infrastructure;
using HVTApp.UI.Modules.SupplyModule.Views;

namespace HVTApp.Modules.SupplyModule.Menus
{
    public class SupplyMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Факт комплектации", typeof(PickingDatesView)));
            Items.Add(new NavigationItem("План комплектации", typeof(SupplyPlanView)));
        }
    }
}
