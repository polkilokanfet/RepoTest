using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Price.ViewModels
{
    public class PaymentDocumentsViewModel : PaymentDocumentLookupListViewModel
    {
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitWrappers;
        private IUnitOfWork _unitOfWork;
        private SalesUnitWrapper _selectedUnit;

        public ObservableCollection<Payment> Payments { get; } = new ObservableCollection<Payment>();
        public ObservableCollection<SalesUnitWrapper> Potential { get; } = new ObservableCollection<SalesUnitWrapper>();

        public SalesUnitWrapper SelectedUnit
        {
            get { return _selectedUnit; }
            set
            {
                _selectedUnit = value;
                ((DelegateCommand)AddPaymentCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public DateTime DockDate
        {
            get { return Payments.Any() ? Payments.First().Date : DateTime.Today; }
            set
            {
                Payments.ForEach(x => x.Date = value);
                OnPropertyChanged();
            }
        }

        public double DockSum
        {
            get { return Payments.Any() ? Payments.Sum(x => x.Sum) : 0 ; }
            set
            {
                if (Math.Abs(DockSum - value) < 0.000001) return;
                if (value < 0) return;
                if(!Payments.Any()) return;

                //неоплаченное без учета текущего платежа
                var notPaid = Payments.Sum(x => x.SalesUnit.SumNotPaid) + Payments.Sum(x => x.Sum);

                if (value > notPaid) return;

                //Payments.ForEach(x => x.Sum = value * (x.SalesUnit.SumNotPaid / notPaid) ); 

                foreach (var payment in Payments)
                {
                    var np = payment.SalesUnit.SumNotPaid + payment.Sum;
                    payment.Sum = value * np / notPaid;
                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand AddPaymentCommand { get; }
        public ICommand ReloadCommand { get; }

        public PaymentDocumentsViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
            SaveCommand = new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);
            AddPaymentCommand = new DelegateCommand(AddPaymentCommand_Execute, AddPaymentCommand_CanExecute);
            this.SelectedLookupChanged += lookup => Refresh();
        }

        private bool AddPaymentCommand_CanExecute()
        {
            return SelectedItem != null && SelectedUnit != null;
        }

        private void AddPaymentCommand_Execute()
        {
            double sum = DockSum;
            Payments.Add(new Payment(new PaymentActual(), SelectedUnit));
            Potential.Remove(SelectedUnit);
            SelectedUnit = null;
            DockSum = sum;
        }

        private bool SaveCommand_CanExecute()
        {
            return _salesUnitWrappers != null && 
                   _salesUnitWrappers.IsChanged && 
                   _salesUnitWrappers.IsValid &&
                   _unitOfWork != null;
        }

        private async void SaveCommand_Execute()
        {
            _salesUnitWrappers.AcceptChanges();
            await _unitOfWork.SaveChangesAsync();
        }

        protected override async Task<IEnumerable<PaymentDocumentLookup>> GetLookups()
        {
            Payments.Clear();
            Potential.Clear();

            //загружаем необходимые данные
            _unitOfWork = Container.Resolve<IUnitOfWork>();
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(
                (await _unitOfWork.Repository<SalesUnit>().GetAllAsync()).Select(x => new SalesUnitWrapper(x)));

            //отслеживаем их изменения
            _salesUnitWrappers.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            return await base.GetLookups();
        }

        private void Refresh()
        {
            //очищаем платежи
            Payments.Clear();
            Potential.Clear();

            //если ничего не выбрано - выходим
            if (SelectedItem == null) return;

            //заполняем платежи
            foreach (var paymentActual in SelectedItem.Payments)
            {
                var salesUnitWrapper = _salesUnitWrappers.Single(x => x.PaymentsActual.Select(p => p.Id).Contains(paymentActual.Id));
                var pa = salesUnitWrapper.PaymentsActual.Single(x => x.Id == paymentActual.Id).Model;
                Payments.Add(new Payment(pa, salesUnitWrapper));
            }

            //формируем список потенциального оборудования (исключая то, что в платеже)
            var units = _salesUnitWrappers.Where(x => !x.IsPaid && !Payments.Select(p => p.SalesUnit).Contains(x)).OrderBy(x => x.OrderInTakeDate);
            Potential.AddRange(units);

            OnPropertyChanged(nameof(DockSum));
            OnPropertyChanged(nameof(DockDate));
        }
    }

    public class Payment : PaymentActualWrapper
    {
        public SalesUnitWrapper SalesUnit { get; }

        public Payment(PaymentActual model, SalesUnitWrapper salesUnit) : base(model)
        {
            SalesUnit = salesUnit;
            if(salesUnit.PaymentsActual.Select(x => x.Id).Contains(model.Id))
                this.PropertyChanged += OnPropertyChanged;
            else
                salesUnit.PaymentsActual.Add(this);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            string propertyName = args.PropertyName;
            if (!this.GetType().GetProperty(propertyName).CanWrite) return;
            var other = SalesUnit.PaymentsActual.Single(x => x.Id == this.Id);
            var thisVal = this.GetType().GetProperty(propertyName).GetValue(this);
            var otherVal = other.GetType().GetProperty(propertyName).GetValue(other);
            if(Equals(thisVal, otherVal)) return;
            other.GetType().GetProperty(propertyName).SetValue(other, thisVal);
        }
    }
}