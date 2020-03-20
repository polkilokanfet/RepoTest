using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        //��������� ��� ������������ ���������
        private IValidatableChangeTrackingCollection<SalesUnitPaymentWrapper> _salesUnitWrappers;
        private Payment _selectedPayment;
        private object[] _selectedPotentialUnits;
        private DateTime _dockDate;
        private IMessageService _messageService;

        #endregion

        #region Props

        private PaymentDocumentWrapper PaymentDocument => this.Item;

        /// <summary>
        /// ������� � ���� ��������
        /// </summary>
        public ObservableCollection<Payment> Payments { get; } = new ObservableCollection<Payment>();

        /// <summary>
        /// ������������� �������
        /// </summary>
        public ObservableCollection<SalesUnitPaymentWrapper> Potential { get; } = new ObservableCollection<SalesUnitPaymentWrapper>();

        /// <summary>
        /// ��������� ������������� �����
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
            get { return _dockDate; }
            set
            {
                Payments.ForEach(x => x.PaymentActual.Date = value);
                _dockDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ����� ���������� ��������� ��� ���
        /// </summary>
        //public double DockSum
        //{
        //    get { return Payments.Any() ? Payments.Sum(x => x.PaymentActual.Sum) : 0; }
        //    set
        //    {
        //        if (value < 0) return;
        //        if (!Payments.Any()) return;

        //        //������������ ��� ����� �������� �������
        //        var notPaid = Payments.Sum(x => x.SalesUnit.Model.SumNotPaid) + Payments.Sum(x => x.PaymentActual.Sum);

        //        //���� ���������� �����, �����������, �� ����������
        //        if (value > notPaid) return;

        //        Payments.ForEach(x => x.PaymentActual.Sum = value * ((x.SalesUnit.Model.SumNotPaid + x.PaymentActual.Sum) / notPaid));
        //    }
        //}

        /// <summary>
        /// ����� ���������� ��������� � ���
        /// </summary>
        public double DockSumWithVat
        {
            get { return Payments.Any() ? Payments.Sum(x => x.SumWithVat) : 0; }
            set
            {
                if (value < 0)
                {
                    _messageService.ShowOkMessageDialog("��������������", "������������� ������� �����������!");
                    return;
                }

                if (!Payments.Any())
                {
                    _messageService.ShowOkMessageDialog("��������������", "������� � �������� ������������.");
                    return;
                }

                //������������ ��� ����� �������� ������� (c ���)
                var notPaidWithVat = Payments.Sum(x => x.SalesUnit.Model.SumNotPaidWithVat) + Payments.Sum(x => x.SumWithVat);

                //���� ���������� �����, �����������, �� ����������
                if (value - notPaidWithVat > 0.000001)
                {
                    _messageService.ShowOkMessageDialog("��������������", $"����� �������� ������� ������. ��������� ��������: {notPaidWithVat:C}");
                    return;
                }

                Payments.ForEach(x => x.SumWithVat = value * ((x.SalesUnit.Model.SumNotPaidWithVat + x.SumWithVat) / notPaidWithVat));
            }
        }


        public ICommand AddPaymentCommand { get; }
        public ICommand RemovePaymentCommand { get; }
        public ICommand SaveDocumentCommand { get; }
        public ICommand RemoveDocumentCommand { get; }

        #endregion

        public PaymentDocumentViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

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
                    var sum = DockSumWithVat;
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

                    DockSumWithVat = sum;
                    DockDate = date;
                },
                () => SelectedPotentialUnits != null);

            RemovePaymentCommand = new DelegateCommand(
                () =>
                {
                    UnitOfWork.Repository<PaymentActual>().Delete(SelectedPayment.PaymentActual.Model);

                    //�������� ������� �� ���������
                    var payment = PaymentDocument.Payments.Single(x => x.Id == SelectedPayment.PaymentActual.Id);
                    PaymentDocument.Payments.Remove(payment);

                    //���������� �������������� ������� � ������
                    var potential = _salesUnitWrappers.Single(x => x.PaymentsActual.Select(pa => pa.Id).Contains(payment.Id));
                    Potential.Add(potential);

                    //�������� ������� �� �����
                    var paymentToRemove = potential.PaymentsActual.Single(x => x.Id == payment.Id);
                    potential.PaymentsActual.Remove(paymentToRemove);

                    Payments.Remove(SelectedPayment);

                    OnPropertyChanged(nameof(DockSumWithVat));
                },
                () => SelectedPayment != null);

            RemoveDocumentCommand = new DelegateCommand(
                () =>
                {
                    var dr = _messageService.ShowYesNoMessageDialog("��������", "�� �������, ��� ������ ������� ���� ��������� ��������?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    UnitOfWork.Repository<PaymentActual>().DeleteRange(Payments.Select(x => x.PaymentActual.Model));
                    UnitOfWork.Repository<PaymentDocument>().Delete(this.PaymentDocument.Model);
                    UnitOfWork.SaveChanges();

                    GoBackCommand.Execute(null);
                },
                () => UnitOfWork.Repository<PaymentDocument>().GetById(PaymentDocument.Id) != null
            );

        }

        protected override void AfterLoading()
        {
            //�������� ��������� ������� ������
            var salesUnitWrappers = UnitOfWork.Repository<SalesUnit>().GetAll()
                .Select(salesUnit => new SalesUnitPaymentWrapper(salesUnit))
                .ToList();
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitPaymentWrapper>(salesUnitWrappers);

            //��������� �������
            foreach (var paymentActual in PaymentDocument.Payments)
            {
                var paymentActualWrapper = _salesUnitWrappers.SelectMany(x => x.PaymentsActual).Single(x => x.Id == paymentActual.Id);
                var salesUnitWrapper = _salesUnitWrappers.Single(x => x.PaymentsActual.Contains(paymentActualWrapper));
                Payments.Add(new Payment(salesUnitWrapper, paymentActualWrapper));
            }

            //��������� ������ �������������� ������������ 
            //(�������� ��, ��� � ��������� ������� � ��������� ��������)
            var potentialSalesUnits = _salesUnitWrappers
                .Where(x => !x.Model.IsPaid && !x.Model.IsLoosen)
                .Except(Payments.Select(payment => payment.SalesUnit))
                .OrderBy(x => x.Model.OrderInTakeDate);
            Potential.AddRange(potentialSalesUnits);

            _dockDate = Payments.Any() ? Payments.First().PaymentActual.Date : DateTime.Today;

            OnPropertyChanged(nameof(DockSumWithVat));
            OnPropertyChanged(nameof(DockDate));

            //������� ��������� � ��������� ���������
            PaymentDocument.PropertyChanged += PaymentDocumentOnPropertyChanged;

            //������� ��������� � �����
            _salesUnitWrappers.PropertyChanged += SalesUnitWrappersOnPropertyChanged;

            base.AfterLoading();
        }

        private void SalesUnitWrappersOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveDocumentCommand).RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(DockSumWithVat));
        }

        private void PaymentDocumentOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ((DelegateCommand) SaveDocumentCommand).RaiseCanExecuteChanged();
        }

        protected override void GoBackCommand_Execute()
        {
            //���� ���� �����-�� ���������
            if (((DelegateCommand)SaveDocumentCommand).CanExecute())
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("����������", "��������� ���������?", defaultNo:true) == MessageDialogResult.Yes)
                {
                    ((DelegateCommand)SaveDocumentCommand).Execute();
                }
            }

            RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.GoBack();
        }

    }

}