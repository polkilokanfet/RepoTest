using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class PaymentDocumentViewModel : PaymentDocumentDetailsViewModel
    {
        //��������� ��� ������������ ���������
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitWrappers;
        private SalesUnitWrapper _selectedUnit;
        private Payment _selectedPayment;

        public ObservableCollection<Payment> Payments { get; } = new ObservableCollection<Payment>();
        public ObservableCollection<SalesUnitWrapper> Potential { get; } = new ObservableCollection<SalesUnitWrapper>();

        /// <summary>
        /// ��������� ������������� ����
        /// </summary>
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

        /// <summary>
        /// ��������� ������
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

        /// <summary>
        /// ���� ��������
        /// </summary>
        public DateTime DockDate
        {
            get { return Payments.Any() ? Payments.First().PaymentActual.Date : DateTime.Today; }
            set
            {
                Payments.ForEach(x => x.PaymentActual.Date = value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ����� ���������� ���������
        /// </summary>
        public double DockSum
        {
            get { return Payments.Any() ? Payments.Sum(x => x.PaymentActual.Sum) : 0; }
            set
            {
                if (value < 0) return;
                if (!Payments.Any()) return;

                //������������ ��� ����� �������� �������
                var notPaid = Payments.Sum(x => x.SalesUnit.SumNotPaid) + Payments.Sum(x => x.PaymentActual.Sum);

                if (value > notPaid) return;

                Payments.ForEach(x => x.PaymentActual.Sum = value * ((x.SalesUnit.SumNotPaid + x.PaymentActual.Sum) / notPaid));
            }
        }

        public ICommand AddPaymentCommand { get; }
        public ICommand RemovePaymentCommand { get; }
        public ICommand SaveDocumentCommand { get; }

        public PaymentDocumentViewModel(IUnityContainer container) : base(container)
        {
            SaveDocumentCommand = new DelegateCommand(
                () =>
                {
                    _salesUnitWrappers.AcceptChanges();
                    this.SaveCommand_Execute();
                },

                () =>
                {
                    bool itemIsValid = Item != null && Item.IsValid;
                    bool unitsIsValid = _salesUnitWrappers != null && _salesUnitWrappers.IsValid;
                    return itemIsValid && unitsIsValid && (Item.IsChanged || _salesUnitWrappers.IsChanged);
                });


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
                    SelectedPayment = payment;

                    DockSum = sum;
                    DockDate = date;
                },

                () => SelectedUnit != null);

            RemovePaymentCommand = new DelegateCommand(
                () =>
                {
                    UnitOfWork.Repository<PaymentActual>().Delete(SelectedPayment.PaymentActual.Model);

                    //�������� ������� �� ���������
                    var payment = Item.Payments.Single(x => x.Id == SelectedPayment.PaymentActual.Id);
                    Item.Payments.Remove(payment);

                    //���������� �������������� ������� � ������
                    var potential = _salesUnitWrappers.Single(x => x.PaymentsActual.Select(pa => pa.Id).Contains(payment.Id));
                    Potential.Add(potential);

                    //�������� ������� �� �����
                    var paymentToRemove = potential.PaymentsActual.Single(x => x.Id == payment.Id);
                    potential.PaymentsActual.Remove(paymentToRemove);

                    Payments.Remove(SelectedPayment);

                    OnPropertyChanged(nameof(DockSum));
                },

                () => SelectedPayment != null);

        }

        protected override async Task AfterLoading()
        {
            //�������� ��������� ������� ������
            var salesUnitWrappers = (await UnitOfWork.Repository<SalesUnit>().GetAllAsync())
                .Where(salesUnit => !salesUnit.IsPaid)
                .Select(salesUnit => new SalesUnitWrapper(salesUnit));
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnitWrappers);

            //��������� �������
            foreach (var paymentActual in Item.Payments)
            {
                var paymentActualWrapper = _salesUnitWrappers.SelectMany(x => x.PaymentsActual).Single(x => x.Id == paymentActual.Id);
                var salesUnitWrapper = _salesUnitWrappers.Single(x => x.PaymentsActual.Contains(paymentActualWrapper));
                Payments.Add(new Payment(salesUnitWrapper, paymentActualWrapper));
            }

            //��������� ������ �������������� ������������ 
            //(�������� ��, ��� � ��������� �������)
            var potentialSalesUnits = _salesUnitWrappers
                .Where(x => !x.IsLoosen)
                .Except(Payments.Select(payment => payment.SalesUnit))
                .OrderBy(x => x.OrderInTakeDate);
            Potential.AddRange(potentialSalesUnits);

            OnPropertyChanged(nameof(DockSum));
            OnPropertyChanged(nameof(DockDate));

            this.Item.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand)SaveDocumentCommand).RaiseCanExecuteChanged();
            };

            this._salesUnitWrappers.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand)SaveDocumentCommand).RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(DockSum));
            };

            await base.AfterLoading();
        }

        protected override void GoBackCommand_Execute()
        {
            //���� ���� �����-�� ���������
            if (((DelegateCommand)SaveDocumentCommand).CanExecute())
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("����������", "��������� ���������?") == MessageDialogResult.Yes)
                {
                    ((DelegateCommand)SaveDocumentCommand).Execute();
                }
            }

            RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.GoBack();
        }

    }
}