using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class PaymentsActualViewModel : ViewModelBaseCanExportToExcel
    {
        private SalesUnitPayment _selectedPayment;

        public ObservableCollection<SalesUnitPayment> Payments { get; } = new ObservableCollection<SalesUnitPayment>();

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
        public ICommand ReloadCommand { get; }

        public PaymentsActualViewModel(IUnityContainer container) : base(container)
        {
            Load();

            NewCommand = new DelegateCommand(() => RequestNavigate(new PaymentDocument()));
            EditCommand = new DelegateCommand(
                () => RequestNavigate(SelectedPayment.PaymentDocument), 
                () => SelectedPayment != null);
            ReloadCommand = new DelegateCommand(Load);
        }

        private void Load()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.PaymentsActual.Any());
            var documents = UnitOfWork.Repository<PaymentDocument>().GetAll();

            var payments = new List<SalesUnitPayment>();
            foreach (var salesUnit in salesUnits)
            {
                salesUnit.PaymentsActual.ForEach(payment => 
                    payments.Add(new SalesUnitPayment(salesUnit, payment, documents.Single(x => x.Payments.Contains(payment)))));
            }

            Payments.Clear();
            Payments.AddRange(payments.OrderBy(x => x.Payment.Date));
        }

        private void RequestNavigate(PaymentDocument paymentDocument)
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<PaymentDocumentView>(new NavigationParameters { { "", paymentDocument } });
        }
    }
}