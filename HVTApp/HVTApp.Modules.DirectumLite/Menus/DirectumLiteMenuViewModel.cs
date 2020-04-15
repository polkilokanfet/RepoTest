using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Directum;

namespace HVTApp.Modules.DirectumLite.Menus
{
    public class DirectumLiteMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Исходящие", typeof(DirectumTasksOutgoingView)));
            Items.Add(new NavigationItem("Входящие", typeof(DirectumTasksIncomingView)));
        }
    }
}
