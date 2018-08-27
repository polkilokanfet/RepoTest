using System;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.UI.ViewModels
{
    public class OfferUnitsDetailsViewModel : IDialogRequestClose
    {
        public OfferUnitDetailsViewModel OfferUnitDetailsViewModel { get; }

        public OfferUnitsDetailsViewModel(OfferUnitDetailsViewModel offerUnitDetailsViewModel)
        {
            OfferUnitDetailsViewModel = offerUnitDetailsViewModel;
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
