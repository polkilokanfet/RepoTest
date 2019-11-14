using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Modules.PlanAndEconomy.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class SalesUnitPayment
    {
        public SalesUnit SalesUnit { get; }
        public PaymentActual Payment { get; }
        public PaymentDocument PaymentDocument { get; }

        public SalesUnitPayment(SalesUnit salesUnit, PaymentActual payment, PaymentDocument paymentDocument)
        {
            SalesUnit = salesUnit;
            Payment = payment;
            PaymentDocument = paymentDocument;
        }
    }

    public class PaymentsActualViewModel : ViewModelBase
    {
        private SalesUnitPayment _selectedPayment;


        public ObservableCollection<SalesUnitPayment> Payments { get; }

        public SalesUnitPayment SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                ((DelegateCommand)EditCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand NewCommand { get; }
        public ICommand EditCommand { get; }

        public PaymentsActualViewModel(IUnityContainer container) : base(container)
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.PaymentsActual.Any());
            var documents = UnitOfWork.Repository<PaymentDocument>().Find(x => true);

            var payments = new List<SalesUnitPayment>();
            foreach (var salesUnit in salesUnits)
            {
                salesUnit.PaymentsActual.ForEach(payment => payments.Add(new SalesUnitPayment(salesUnit, payment, documents.Single(x => x.Payments.Contains(payment)))));
            }

            Payments = new ObservableCollection<SalesUnitPayment>(payments.OrderBy(x => x.Payment.Date));

            NewCommand = new DelegateCommand(() => RequestNavigate(new PaymentDocument()));
            EditCommand = new DelegateCommand(
                () => RequestNavigate(SelectedPayment.PaymentDocument), 
                () => SelectedPayment != null);
        }

        private void RequestNavigate(PaymentDocument paymentDocument)
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<PaymentDocumentView>(new NavigationParameters { { "", paymentDocument } });
        }
    }
}