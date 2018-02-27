using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public class PaymentPlannedListGeneratorViewModel : BaseListViewModel<PaymentPlanned, PaymentPlannedLookup, AfterSavePaymentPlannedEvent, AfterSelectPaymentPlannedEvent, AfterRemovePaymentPlannedEvent>
    {
        private PaymentPlannedWrapper _selectedPayment;
        public ObservableCollection<PaymentPlannedWrapper> Payments { get; } = new ObservableCollection<PaymentPlannedWrapper>();

        public PaymentPlannedWrapper SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                ((DelegateCommand)DevidePaymentCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemovePaymentCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand GeneratePaymentsCommand { get; }
        public ICommand SaveChangesCommand { get; }
        public ICommand DevidePaymentCommand { get; }
        public ICommand RemovePaymentCommand { get; }

        public PaymentPlannedListGeneratorViewModel(IUnityContainer container) : base(container)
        {
            GeneratePaymentsCommand = new DelegateCommand(GeneratePaymentsCommand_Execute);
            SaveChangesCommand = new DelegateCommand(SaveChangesCommand_Execute);
            DevidePaymentCommand = new DelegateCommand(DevidePaymentCommand_Execute, DevidePaymentCommand_CanExecute);
            RemovePaymentCommand = new DelegateCommand(RemovePaymentCommand_Execute, RemovePaymentCommand_CanExecute);
        }

        private void RemovePaymentCommand_Execute()
        {
            SelectedPayment.PaymentPlannedList.Payments.Remove(SelectedPayment);
            Payments.Remove(SelectedPayment);
            SelectedPayment = null;
        }

        private bool RemovePaymentCommand_CanExecute()
        {
            return SelectedPayment != null && SelectedPayment.PaymentPlannedList.Payments.Count > 1;
        }

        private bool DevidePaymentCommand_CanExecute()
        {
            return SelectedPayment != null;
        }

        private void DevidePaymentCommand_Execute()
        {
            var newPayment = new PaymentPlanned {Date = SelectedPayment.Date};
            var newPaymentWrapper = new PaymentPlannedWrapper(newPayment) {PaymentPlannedList = SelectedPayment.PaymentPlannedList};
            SelectedPayment.PaymentPlannedList.Payments.Add(newPaymentWrapper);
            Payments.Insert(Payments.IndexOf(SelectedPayment) + 1, newPaymentWrapper);
        }

        private async void GeneratePaymentsCommand_Execute()
        {
            var salesUnits = await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync();
            var salesUnitWrappers = salesUnits.Select(x => new SalesUnitWrapper(x)).ToList();

            Payments.Clear();
            Payments.AddRange(salesUnitWrappers.SelectMany(x => x.PaymentPlannedWrappers).OrderBy(x => x.Date));
        }

        private async void SaveChangesCommand_Execute()
        {
            var paymentPlannedListWrappers = Payments.Select(x => x.PaymentPlannedList).Distinct().Where(x => x.IsChanged).ToList();
            if (!paymentPlannedListWrappers.Any()) return;

            foreach (var paymentPlannedListWrapper in paymentPlannedListWrappers)
            {
                if (!paymentPlannedListWrapper.SalesUnit.PaymentsPlannedSaved.Contains(paymentPlannedListWrapper))
                {
                    paymentPlannedListWrapper.SalesUnit.PaymentsPlannedSaved.Add(paymentPlannedListWrapper);
                }
            }

            await UnitOfWork.SaveChangesAsync();
        }
    }
}
