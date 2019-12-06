using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OffersViewModel : OfferLookupListViewModel
    {
        public ICommand PrintOfferCommand { get; }

        public OffersViewModel(IUnityContainer container) : base(container)
        {
            PrintOfferCommand = new DelegateCommand(
                async () =>
                {
                    await Container.Resolve<IPrintOfferService>().PrintOfferAsync(SelectedItem.Id);
                },
                () => SelectedItem != null);

            this.SelectedLookupChanged += lookup => { ((DelegateCommand)PrintOfferCommand).RaiseCanExecuteChanged(); };
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
