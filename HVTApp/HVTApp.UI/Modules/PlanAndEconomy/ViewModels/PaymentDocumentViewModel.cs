using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class PaymentDocumentViewModel : PaymentDocumentDetailsViewModel
    {
        #region Fields

        //коллекци€ дл€ отслеживани€ элементов
        private IValidatableChangeTrackingCollection<SalesUnitPaymentWrapper> _salesUnitWrappers;
        private Payment _selectedPayment;
        private object[] _selectedPotentialUnits;

        #endregion

        #region Props

        private PaymentDocumentWrapper PaymentDocument => this.Item;
        public ObservableCollection<Payment> Payments { get; } = new ObservableCollection<Payment>();
        public ObservableCollection<SalesUnitPaymentWrapper> Potential { get; } = new ObservableCollection<SalesUnitPaymentWrapper>();

        /// <summary>
        /// ¬ыбранный потенциальный юнит
        /// </summary>
        public object[] SelectedPotentialUnits
        {
            get { return _selectedPotentialUnits; }
            set
            {
                _selectedPotentialUnits = value;
                ((DelegateCommand)AddPaymentCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ¬ыбранный платеж
        /// </summary>
        public Payment SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                ((DelegateCommand)RemovePaymentCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        private DateTime _dockDate;

        /// <summary>
        /// ƒата платежей
        /// </summary>
        public DateTime DockDate
        {
            get { return _dockDate; }
            set
            {
                Payments.ForEach(x => x.PaymentActual.Date = value);
                _dockDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// —умма платежного документа
        /// </summary>
        public double DockSum
        {
            get { return Payments.Any() ? Payments.Sum(x => x.PaymentActual.Sum) : 0; }
            set
            {
                if (value < 0) return;
                if (!Payments.Any()) return;

                //неоплаченное без учета текущего платежа
                var notPaid = Payments.Sum(x => x.SalesUnit.Model.SumNotPaid) + Payments.Sum(x => x.PaymentActual.Sum);

                if (value > notPaid) return;

                Payments.ForEach(x => x.PaymentActual.Sum = value * ((x.SalesUnit.Model.SumNotPaid + x.PaymentActual.Sum) / notPaid));
            }
        }

        public ICommand AddPaymentCommand { get; }
        public ICommand RemovePaymentCommand { get; }
        public ICommand SaveDocumentCommand { get; }

        #endregion

        public PaymentDocumentViewModel(IUnityContainer container) : base(container)
        {
            SaveDocumentCommand = new DelegateCommand(
                () =>
                {
                    PaymentDocument.PropertyChanged -= PaymentDocumentOnPropertyChanged;
                    _salesUnitWrappers.PropertyChanged -= SalesUnitWrappersOnPropertyChanged;

                    _salesUnitWrappers.AcceptChanges();
                    SaveItem();
                    ((DelegateCommand)SaveDocumentCommand).RaiseCanExecuteChanged();

                    PaymentDocument.PropertyChanged += PaymentDocumentOnPropertyChanged;
                    _salesUnitWrappers.PropertyChanged += SalesUnitWrappersOnPropertyChanged;
                },

                () =>
                {
                    if (PaymentDocument == null) return false;
                    if (!PaymentDocument.IsValid) return false;
                    if (_salesUnitWrappers == null) return false;
                    if (!_salesUnitWrappers.IsValid) return false;
                    return PaymentDocument.IsChanged || _salesUnitWrappers.IsChanged;
                });


            AddPaymentCommand = new DelegateCommand(
                () =>
                {
                    var sum = DockSum;
                    var date = DockDate;

                    var selectedUnits = SelectedPotentialUnits.Cast<SalesUnitPaymentWrapper>().ToList();

                    foreach (var selectedUnit in selectedUnits)
                    {
                        var payment = new Payment(selectedUnit);
                        PaymentDocument.Payments.Add(payment.PaymentActual);
                        Payments.Add(payment);
                        Potential.Remove(selectedUnit);
                    }
                    SelectedPotentialUnits = null;

                    DockSum = sum;
                    DockDate = date;
                },

                () => SelectedPotentialUnits != null);

            RemovePaymentCommand = new DelegateCommand(
                () =>
                {
                    UnitOfWork.Repository<PaymentActual>().Delete(SelectedPayment.PaymentActual.Model);

                    //удаление платежа из документа
                    var payment = PaymentDocument.Payments.Single(x => x.Id == SelectedPayment.PaymentActual.Id);
                    PaymentDocument.Payments.Remove(payment);

                    //добавление потенциального платежа в список
                    var potential = _salesUnitWrappers.Single(x => x.PaymentsActual.Select(pa => pa.Id).Contains(payment.Id));
                    Potential.Add(potential);

                    //удаление платежа из юнита
                    var paymentToRemove = potential.PaymentsActual.Single(x => x.Id == payment.Id);
                    potential.PaymentsActual.Remove(paymentToRemove);

                    Payments.Remove(SelectedPayment);

                    OnPropertyChanged(nameof(DockSum));
                },

                () => SelectedPayment != null);

        }

        protected override void AfterLoading()
        {
            //получаем коллекцию единниц продаж
            var salesUnitWrappers = UnitOfWork.Repository<SalesUnit>().GetAll()
                .Select(salesUnit => new SalesUnitPaymentWrapper(salesUnit))
                .ToList();
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitPaymentWrapper>(salesUnitWrappers);

            //заполн€ем платежи
            foreach (var paymentActual in PaymentDocument.Payments)
            {
                var paymentActualWrapper = _salesUnitWrappers.SelectMany(x => x.PaymentsActual).Single(x => x.Id == paymentActual.Id);
                var salesUnitWrapper = _salesUnitWrappers.Single(x => x.PaymentsActual.Contains(paymentActualWrapper));
                Payments.Add(new Payment(salesUnitWrapper, paymentActualWrapper));
            }

            //формируем список потенциального оборудовани€ 
            //(исключа€ то, что в выбранном платеже и полностью оплачено)
            var potentialSalesUnits = _salesUnitWrappers
                .Where(x => !x.Model.IsPaid && !x.Model.IsLoosen)
                .Except(Payments.Select(payment => payment.SalesUnit))
                .OrderBy(x => x.Model.OrderInTakeDate);
            Potential.AddRange(potentialSalesUnits);

            _dockDate = Payments.Any() ? Payments.First().PaymentActual.Date : DateTime.Today;

            OnPropertyChanged(nameof(DockSum));
            OnPropertyChanged(nameof(DockDate));

            //событие изменени€ в платежном документе
            PaymentDocument.PropertyChanged += PaymentDocumentOnPropertyChanged;

            //событие изменени€ в юните
            _salesUnitWrappers.PropertyChanged += SalesUnitWrappersOnPropertyChanged;

            base.AfterLoading();
        }

        private void SalesUnitWrappersOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveDocumentCommand).RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(DockSum));
        }

        private void PaymentDocumentOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ((DelegateCommand) SaveDocumentCommand).RaiseCanExecuteChanged();
        }

        protected override void GoBackCommand_Execute()
        {
            //если были какие-то изменени€
            if (((DelegateCommand)SaveDocumentCommand).CanExecute())
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("—охранение", "—охранить изменени€?") == MessageDialogResult.Yes)
                {
                    ((DelegateCommand)SaveDocumentCommand).Execute();
                }
            }

            RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.GoBack();
        }

    }

}