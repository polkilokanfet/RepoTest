using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Project1;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : ProjectLookupListViewModel
    {
        public ProjectsViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void InitSpecialCommands()
        {
            NewItemCommand = new DelegateLogCommand(NewItemCommandExecute, NewItemCommandCanExecute);
            EditItemCommand = new DelegateLogCommand(EditItemCommandExecute, EditItemCommandCanExecute);
            RemoveItemCommand = new DelegateLogCommand(RemoveItemCommand_Execute, () => SelectedItem != null);
        }

        private void EditItemCommandExecute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters { { "prj", SelectedItem } });
        }

        private bool EditItemCommandCanExecute()
        {
            return SelectedItem != null;
        }

        private void NewItemCommandExecute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters());
        }

        private bool NewItemCommandCanExecute()
        {
            return true;
        }
    }
}
