using System.Windows.Input;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.ViewModels;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Specifications
{
    public class SpecificationsViewModelBase : SpecificationLookupListViewModel
    {
        public ICommand OpenScanCommand { get; }

        public SpecificationsViewModelBase(IUnityContainer container) : base(container)
        {
            OpenScanCommand = new OpenSpecificationScanCommand(
                () => this.SelectedItem, 
                () => this.SelectedItem != null,
                container.Resolve<IFilesStorageService>(), 
                MessageService);

            this.SelectedLookupChanged += lookup =>
            {
                ((OpenSpecificationScanCommand) OpenScanCommand).RaiseCanExecuteChanged();
            };
        }

        protected override void InitSpecialCommands()
        {
            EditItemCommand = null;
        }
    }
}