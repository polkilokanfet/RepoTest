using System.ComponentModel;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Modules.PlanAndEconomy.Views;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class PaymentsActualViewModel : PaymentDocumentLookupListViewModel
    {
        public ICommand CreatePaymentDocumentCommand { get; }
        public ICommand EditPaymentDocumentCommand { get; }

        public PaymentsActualViewModel(IUnityContainer container) : base(container)
        {
            var regionManager = container.Resolve<IRegionManager>();

            CreatePaymentDocumentCommand = new DelegateCommand(() =>
            {
                regionManager.RequestNavigateContentRegion<PaymentDocumentView>(new NavigationParameters { {"new", new PaymentDocument()} });
            });

            EditPaymentDocumentCommand = new DelegateCommand(() =>
            {
                regionManager.RequestNavigateContentRegion<PaymentDocumentView>(new NavigationParameters { { "edit", SelectedItem } });
            }, () => SelectedItem != null);

            this.SelectedLookupChanged += lookup => ((DelegateCommand)EditPaymentDocumentCommand).RaiseCanExecuteChanged();
        }
    }

    public class Payment : BindableBase
    {
        public PaymentActualWrapper PaymentActual { get; }
        public SalesUnitWrapper SalesUnit { get; }
        public double SumNotPaid => SalesUnit.SumNotPaid;

        public double Sum
        {
            get { return PaymentActual.Sum; }
            set
            {
                if (Equals(value, Sum)) return;
                if (value < 0) return;
                if (value > SalesUnit.SumNotPaid + Sum) return;
                PaymentActual.Sum = value;
                OnPropertyChanged();
            }
        }

        public Payment(SalesUnitWrapper salesUnit)
        {
            var paymentActual = new PaymentActualWrapper(new PaymentActual());
            salesUnit.PaymentsActual.Add(paymentActual);
            SalesUnit = salesUnit;
            PaymentActual = paymentActual;
            PaymentActual.PropertyChanged += PaymentActualOnPropertyChanged;
        }

        public Payment(SalesUnitWrapper salesUnit, PaymentActualWrapper paymentActual)
        {
            SalesUnit = salesUnit;
            PaymentActual = paymentActual;
            PaymentActual.PropertyChanged += PaymentActualOnPropertyChanged;
        }

        private void PaymentActualOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged(nameof(SumNotPaid));
            OnPropertyChanged(nameof(Sum));
        }
    }
}