using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class OffersViewModel : OfferLookupListViewModel
    {
        public DelegateLogCommand PrintOfferCommand { get; }

        public OffersViewModel(IUnityContainer container) : base(container)
        {
            PrintOfferCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IPrintOfferService>().PrintOffer(SelectedItem.Id);
                },
                () => SelectedItem != null);

            this.SelectedLookupChanged += lookup => { PrintOfferCommand.RaiseCanExecuteChanged(); };
        }

        protected override void InitSpecialCommands()
        {
            EditItemCommand = new DelegateLogCommand(EditItemCommandExecute, () => SelectedItem != null);
            RemoveItemCommand = new DelegateLogCommand(RemoveItemCommand_Execute, () => SelectedItem != null);
        }

        private void EditItemCommandExecute()
        {
            var navigationParameters = new NavigationParameters { { "offer", SelectedItem }, { "edit", true } };
            RegionManager.RequestNavigateContentRegion<OfferView>(navigationParameters);
        }
    }
}
