using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class PaymentsActualViewModel : PaymentDocumentLookupListViewModel
    {
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitWrappers;
        private IUnitOfWork _unitOfWork;
        private SalesUnitWrapper _selectedUnit;

        public PaymentDocumentDetailsViewModel PaymentDocumentDetailsViewModel { get; }

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

        public ICommand SaveCommand { get; }
        public ICommand AddPaymentCommand { get; }
        public ICommand ReloadCommand { get; }

        public PaymentsActualViewModel(IUnityContainer container) : base(container)
        {
            PaymentDocumentDetailsViewModel = new PaymentDocumentDetailsViewModel(container);

            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
            SaveCommand = new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);
            AddPaymentCommand = new DelegateCommand(AddPaymentCommand_Execute, AddPaymentCommand_CanExecute);

            this.SelectedLookupChanged += Refresh;
        }

        private async void AddPaymentCommand_Execute()
        {
            var sum = DockSum;
            var date = DockDate;

            var payment = new Payment(SelectedUnit);
            var doc = await _unitOfWork.Repository<PaymentDocument>().GetByIdAsync(SelectedItem.Id);
            doc.Payments.Add(payment.PaymentActual.Model);
            SelectedItem.Payments.Add(payment.PaymentActual.Model);
            Payments.Add(payment);
            Potential.Remove(SelectedUnit);
            SelectedUnit = null;

            DockSum = sum;
            DockDate = date;
        }

        private bool AddPaymentCommand_CanExecute()
        {
            return SelectedItem != null && SelectedUnit != null;
        }

        private async void SaveCommand_Execute()
        {
            var flag = PaymentDocumentDetailsViewModel.Item.IsChanged;
            var savedPaymentsActual = _salesUnitWrappers.ModifiedItems.SelectMany(x => x.PaymentsActual);

            PaymentDocumentDetailsViewModel.Item.AcceptChanges();
            _salesUnitWrappers.AcceptChanges();

            await _unitOfWork.SaveChangesAsync();

            //публикация событий
            if (flag)
            {
                Container.Resolve<IEventAggregator>().GetEvent<AfterSavePaymentDocumentEvent>().Publish(PaymentDocumentDetailsViewModel.Entity);
            }
            foreach (var savedSalesUnit in savedPaymentsActual)
            {
                Container.Resolve<IEventAggregator>().GetEvent<AfterSavePaymentActualEvent>().Publish(savedSalesUnit.Model);
            }
        }

        private bool SaveCommand_CanExecute()
        {
            return PaymentDocumentDetailsViewModel.Item!= null &&
                   PaymentDocumentDetailsViewModel.Item.IsValid &&
                   _salesUnitWrappers != null && 
                   (_salesUnitWrappers.IsChanged || PaymentDocumentDetailsViewModel.Item.IsChanged) && 
                   _salesUnitWrappers.IsValid &&
                   _unitOfWork != null;
        }

        public override async Task LoadAsync()
        {
            await base.LoadAsync();

            Payments.Clear();
            Potential.Clear();

            //загружаем необходимые данные
            _unitOfWork = Container.Resolve<IUnitOfWork>();
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(
                (await _unitOfWork.Repository<SalesUnit>().GetAllAsync()).Select(x => new SalesUnitWrapper(x)));

            //отслеживаем их изменения
            _salesUnitWrappers.PropertyChanged += SalesUnitWrappersOnPropertyChanged;
        }

        private void SalesUnitWrappersOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(DockSum));
            OnPropertyChanged(nameof(DockDate));
        }

        private void Refresh(PaymentDocumentLookup paymentDocumentLookup)
        {
            //отмена изменений в предыдущем выборе
            _salesUnitWrappers.ModifiedItems.ForEach(x => x.RejectChanges());

            //загрузка деталей платежного документа
            PaymentDocumentDetailsViewModel.Load(new PaymentDocumentWrapper(paymentDocumentLookup.Entity), _unitOfWork);
            PaymentDocumentDetailsViewModel.Item.PropertyChanged += SalesUnitWrappersOnPropertyChanged;

            //очищаем платежи
            Payments.Clear();
            Potential.Clear();

            //если ничего не выбрано - выходим
            if (SelectedItem == null) return;

            //заполняем платежи
            foreach (var paymentActual in SelectedItem.Payments)
            {
                var paymentActualWrapper = _salesUnitWrappers.SelectMany(x => x.PaymentsActual).Single(x => x.Id == paymentActual.Id);
                var salesUnitWrapper = _salesUnitWrappers.Single(x => x.PaymentsActual.Contains(paymentActualWrapper));
                Payments.Add(new Payment(salesUnitWrapper, paymentActualWrapper));
            }

            //формируем список потенциального оборудования (исключая то, что в выбранном платеже)
            var units = _salesUnitWrappers.Where(x => !x.IsPaid && !Payments.Select(p => p.SalesUnit).Contains(x)).OrderBy(x => x.OrderInTakeDate);
            Potential.AddRange(units);

            OnPropertyChanged(nameof(DockSum));
            OnPropertyChanged(nameof(DockDate));
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