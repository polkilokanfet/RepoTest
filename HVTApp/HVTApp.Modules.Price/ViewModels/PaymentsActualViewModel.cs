using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Modules.PlanAndEconomy.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class PaymentsActualViewModel : PaymentDocumentLookupListViewModel
    {

        public PaymentsActualViewModel(IUnityContainer container) : base(container)
        {
            NewItemCommand = new DelegateCommand(() => RequestNavigate(new PaymentDocument()));
            EditItemCommand = new DelegateCommand(() => RequestNavigate(SelectedItem), () => SelectedItem != null);
        }

        private void RequestNavigate(PaymentDocument paymentDocument)
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<PaymentDocumentView>(new NavigationParameters { { "", paymentDocument } });
        }
    }
}