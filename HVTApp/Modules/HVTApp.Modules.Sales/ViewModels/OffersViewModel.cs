using HVTApp.Infrastructure.Extansions;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OffersViewModel : OfferLookupListViewModel
    {
        public OffersViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void InitSpecialCommands()
        {
            EditItemCommand = new DelegateCommand(EditItemCommandExecute, () => SelectedItem != null);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_ExecuteAsync, () => SelectedItem != null);
        }

        private void EditItemCommandExecute()
        {
            var navigationParameters = new NavigationParameters { { "offer", SelectedItem }, { "edit", true } };
            RegionManager.RequestNavigateContentRegion<OfferView>(navigationParameters);
        }
    }
}
