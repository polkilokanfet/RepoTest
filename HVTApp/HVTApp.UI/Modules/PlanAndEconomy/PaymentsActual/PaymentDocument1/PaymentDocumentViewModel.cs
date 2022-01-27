using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class PaymentDocumentViewModel : BaseDetailsViewModel<PaymentDocumentWrapper1, PaymentDocument, AfterSavePaymentDocumentEvent>
    {
        #region Fields

        private Payment _selectedPayment;
        private object[] _selectedPotentialUnits;
        private DateTime _dockDate;
        private IMessageService _messageService;

        #endregion

        #region Props

        //��������� ��� ������������ ���������
        public IValidatableChangeTrackingCollection<SalesUnitPaymentWrapper> SalesUnitWrappers { get; private set; }

        public PaymentDocumentWrapper1 PaymentDocument => this.Item;

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
            get => _selectedPotentialUnits;
            set
            {
                _selectedPotentialUnits = value;
                AddPaymentCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// ��������� ������
        /// </summary>
        public Payment SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                RemovePaymentCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// ���� ��������
        /// </summary>
        public DateTime DockDate
        {
            get => _dockDate;
            set
            {
                if (value > DateTime.Today.AddYears(50))
                {
                    _messageService.ShowOkMessageDialog("��������������", "���� ����� 50 ��� � ������� �����������!");
                    return;
                }
                Payments.ForEach(payment => payment.PaymentActual.Date = value);
                _dockDate = value;
                RaisePropertyChanged();
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
            get
            {
                return Payments.Any()
                    ? Payments.Sum(payment => payment.SumWithVat)
                    : 0;
            }
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
                var notPaidWithVat = Payments.Sum(payment => payment.SalesUnit.Model.SumNotPaidWithVat) + Payments.Sum(x => x.SumWithVat);

                //���� ���������� �����, �����������, �� ����������
                if (value - notPaidWithVat > 0.000001)
                {
                    _messageService.ShowOkMessageDialog("��������������", $"����� �������� ������� ������. ��������� ��������: {notPaidWithVat:C}");
                    return;
                }

                Payments.ForEach(payment => payment.SumWithVat = value * ((payment.SalesUnit.Model.SumNotPaidWithVat + payment.SumWithVat) / notPaidWithVat));
            }
        }

        //�������
        public IUnitOfWork UnitOfWork1 => this.UnitOfWork;

        #endregion

        #region ICommand

        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        public AddPaymentCommand AddPaymentCommand { get; }

        /// <summary>
        /// ������� �������� �������
        /// </summary>
        public RemovePaymentCommand RemovePaymentCommand { get; }

        /// <summary>
        /// ������� ���������� ��������
        /// </summary>
        public DelegateLogCommand SaveDocumentCommand { get; }

        /// <summary>
        /// ������� �������� ��������
        /// </summary>
        public RemoveDocumentCommand RemoveDocumentCommand { get; }

        /// <summary>
        /// ������� ������ �������
        /// </summary>
        public RestPaymentCommand RestPaymentCommand { get; }
        
        #endregion

        public PaymentDocumentViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

            SaveDocumentCommand = new DelegateLogCommand(
                () =>
                {
                    PaymentDocument.PropertyChanged -= PaymentDocumentOnPropertyChanged;
                    SalesUnitWrappers.PropertyChanged -= SalesUnitWrappersOnPropertyChanged;

                    SalesUnitWrappers.AcceptChanges();
                    SaveItem();
                    SaveDocumentCommand.RaiseCanExecuteChanged();

                    PaymentDocument.PropertyChanged += PaymentDocumentOnPropertyChanged;
                    SalesUnitWrappers.PropertyChanged += SalesUnitWrappersOnPropertyChanged;
                },
                () =>
                {
                    if (PaymentDocument == null) return false;
                    if (!PaymentDocument.IsValid) return false;
                    if (SalesUnitWrappers == null) return false;
                    if (!SalesUnitWrappers.IsValid) return false;
                    return PaymentDocument.IsChanged || SalesUnitWrappers.IsChanged;
                });

            AddPaymentCommand = new AddPaymentCommand(this, this.Container);
            RemovePaymentCommand = new RemovePaymentCommand(this, this.Container);
            RemoveDocumentCommand = new RemoveDocumentCommand(this, this.Container);
            RestPaymentCommand = new RestPaymentCommand(this, this.Container);

            Payments.CollectionChanged += (sender, args) =>
            {
                RestPaymentCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(DockSumWithVat));
            };
        }

        protected override void AfterLoading()
        {
            //�������� ��������� ������� ������
            var salesUnitWrappers = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetAllIncludeActualPayments()
                .Select(salesUnit => new SalesUnitPaymentWrapper(salesUnit))
                .ToList();
            SalesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitPaymentWrapper>(salesUnitWrappers);

            //��������� �������
            foreach (var paymentActual in PaymentDocument.Payments)
            {
                var paymentActualWrapper = SalesUnitWrappers.SelectMany(x => x.PaymentsActual).Single(x => x.Id == paymentActual.Id);
                var salesUnitWrapper = SalesUnitWrappers.Single(x => x.PaymentsActual.Contains(paymentActualWrapper));
                Payments.Add(new Payment(salesUnitWrapper, paymentActualWrapper));
            }

            //��������� ������ �������������� ������������ 
            //(�������� ��, ��� � ��������� ������� � ��������� ��������)
            var potentialSalesUnits = SalesUnitWrappers
                .Where(x => !x.Model.IsPaid && !x.Model.IsLoosen)
                .Except(Payments.Select(payment => payment.SalesUnit))
                .OrderBy(x => x.Model.Facility.ToString())
                .ThenBy(x => x.Model.Project.Name)
                .ThenBy(x => x.Model.Product.ToString())
                .ThenBy(x => x.Model.Cost)
                .ToList();
                //.OrderBy(x => x.Model.OrderInTakeDate); - �������� ��� ����� ����� �������� �������
            Potential.AddRange(potentialSalesUnits);

            _dockDate = Payments.Any() 
                ? Payments.First().PaymentActual.Date 
                : DateTime.Today;

            RaisePropertyChanged(nameof(DockSumWithVat));
            RaisePropertyChanged(nameof(DockDate));

            //������� ��������� � ��������� ���������
            PaymentDocument.PropertyChanged += PaymentDocumentOnPropertyChanged;

            //������� ��������� � �����
            SalesUnitWrappers.PropertyChanged += SalesUnitWrappersOnPropertyChanged;

            base.AfterLoading();
        }

        private void SalesUnitWrappersOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            SaveDocumentCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(DockSumWithVat));
        }

        private void PaymentDocumentOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveDocumentCommand.RaiseCanExecuteChanged();
        }

        protected override void GoBackCommand_Execute()
        {
            //���� ���� �����-�� ���������
            if (SaveDocumentCommand.CanExecute())
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("����������", "��������� ���������?", defaultNo:true) == MessageDialogResult.Yes)
                {
                    SaveDocumentCommand.Execute();
                }
            }

            RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.GoBack();
        }
    }

}