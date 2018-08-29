using System.Windows.Input;
using HVTApp.Services.OfferToDocService;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OffersViewModel : OfferLookupListViewModel
    {
        public ICommand PrintOfferCommand { get; }
        public OffersViewModel(IUnityContainer container) : base(container)
        {
            PrintOfferCommand = new DelegateCommand(PrintOfferCommand_Execute);
        }

        private async void PrintOfferCommand_Execute()
        {
            await Container.Resolve<IOfferToDoc>().PrintOfferAsync(SelectedLookup.Id);
        }
    }
}
