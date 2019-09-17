using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class PaymentDocumentViewModel : PaymentDocumentDetailsViewModel
    {
        //коллекция для отслеживания элементов
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitWrappers;
        private SalesUnitWrapper _selectedUnit;

        public ObservableCollection<Payment> Payments { get; } = new ObservableCollection<Payment>();
        public ObservableCollection<SalesUnitWrapper> Potential { get; } = new ObservableCollection<SalesUnitWrapper>();

        public SalesUnitWrapper SelectedUnit
        {
            get { return _selectedUnit; }
            set
            {
                if (Equals(_selectedUnit, value)) return;
                _selectedUnit = value;
                ((DelegateCommand)AddPaymentCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public DateTime DockDate
        {
            get { return Payments.Any() ? Payments.First().PaymentActual.Date : DateTime.Today; }
            set
            {
                Payments.ForEach(x => x.PaymentActual.Date = value);
                OnPropertyChanged();
            }
        }

        public double DockSum
        {
            get { return Payments.Any() ? Payments.Sum(x => x.PaymentActual.Sum) : 0; }
            set
            {
                if (value < 0) return;
                if (!Payments.Any()) return;

                //неоплаченное без учета текущего платежа
                var notPaid = Payments.Sum(x => x.SalesUnit.SumNotPaid) + Payments.Sum(x => x.PaymentActual.Sum);

                if (value > notPaid) return;

                Payments.ForEach(x => x.PaymentActual.Sum = value * ((x.SalesUnit.SumNotPaid + x.PaymentActual.Sum) / notPaid));
            }
        }

        public ICommand AddPaymentCommand { get; }

        public PaymentDocumentViewModel(IUnityContainer container) : base(container)
        {
            AddPaymentCommand = new DelegateCommand(
                () =>
                {
                    var sum = DockSum;
                    var date = DockDate;

                    var payment = new Payment(SelectedUnit);
                    Item.Payments.Add(payment.PaymentActual);
                    Payments.Add(payment);
                    Potential.Remove(SelectedUnit);
                    SelectedUnit = null;

                    DockSum = sum;
                    DockDate = date;
                },

                () => SelectedUnit != null);
        }

        protected override async Task AfterLoading()
        {
            //получаем коллекцию единниц продаж
            var salesUnitWrappers = (await UnitOfWork.Repository<SalesUnit>().GetAllAsync())
                .Where(x => !x.IsPaid)
                .Select(x => new SalesUnitWrapper(x));
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnitWrappers);

            //заполняем платежи
            foreach (var paymentActual in Item.Payments)
            {
                var paymentActualWrapper = _salesUnitWrappers.SelectMany(x => x.PaymentsActual).Single(x => x.Id == paymentActual.Id);
                var salesUnitWrapper = _salesUnitWrappers.Single(x => x.PaymentsActual.Contains(paymentActualWrapper));
                Payments.Add(new Payment(salesUnitWrapper, paymentActualWrapper));
            }

            //формируем список потенциального оборудования 
            //(исключая то, что в выбранном платеже)
            var potentialSalesUnits = _salesUnitWrappers
                .Where(salesUnitWrapper => !salesUnitWrapper.IsPaid)
                .Except(Payments.Select(payment => payment.SalesUnit))
                .OrderBy(x => x.OrderInTakeDate);
            Potential.AddRange(potentialSalesUnits);

            OnPropertyChanged(nameof(DockSum));
            OnPropertyChanged(nameof(DockDate));

            await base.AfterLoading();
        }
    }
}