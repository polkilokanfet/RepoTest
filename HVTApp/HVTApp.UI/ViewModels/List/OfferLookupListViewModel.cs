using System;
using System.Windows.Input;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferLookupListViewModel
    {
        public ICommand PrintOfferCommand { get; set; }

        protected override Offer GetNewItem()
        {
            return new Offer {ValidityDate = DateTime.Today.AddDays(60)};
        }

        protected override void InitSpecialCommands()
        {
            PrintOfferCommand = new DelegateCommand(async () => await Container.Resolve<IPrintOfferService>().PrintOfferAsync(SelectedItem.Id), () => SelectedItem != null);
            this.SelectedLookupChanged += lookup => { ((DelegateCommand)PrintOfferCommand).RaiseCanExecuteChanged(); };
        }
    }
}