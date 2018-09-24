using HVTApp.Infrastructure.Extansions;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : ProjectLookupListViewModel
    {
        public ProjectsViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void InitSpecialCommands()
        {
            NewItemCommand = new DelegateCommand(NewItemCommandExecute, NewItemCommandCanExecute);
            EditItemCommand = new DelegateCommand(EditItemCommandExecute, EditItemCommandCanExecute);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_ExecuteAsync, () => SelectedItem != null);
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
