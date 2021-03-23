using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Directum;
using HVTApp.UI.Modules.Directum.ToAccept;
using HVTApp.UI.Modules.Directum.ToPerform;

namespace HVTApp.Modules.DirectumLite.Menus
{
    public class DirectumLiteMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Исходящие", typeof(DirectumTasksOutgoingView)));
            
            NavigationItem navigationItem = new NavigationItem("Входящие", typeof(DirectumTasksIncomingView));
            navigationItem.Items.Add(new NavigationItem("Контроль", typeof(DirectumTasksIncomingToAcceptView)));
            navigationItem.Items.Add(new NavigationItem("Исполнение", typeof(DirectumTasksIncomingToPerformView)));
            Items.Add(navigationItem);
        }
    }
}
