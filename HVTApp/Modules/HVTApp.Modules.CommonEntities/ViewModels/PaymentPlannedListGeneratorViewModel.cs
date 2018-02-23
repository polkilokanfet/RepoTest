using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess.Annotations;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public class PaymentPlannedListGeneratorViewModel : BaseListViewModel<PaymentPlanned, PaymentPlannedLookup, AfterSavePaymentPlannedEvent, AfterSelectPaymentPlannedEvent, AfterRemovePaymentPlannedEvent>
    {
        public ObservableCollection<PaymentPlannedGroup> Payments { get; } = new ObservableCollection<PaymentPlannedGroup>();

        public ICommand GeneratePaymentsCommand { get; }
        public new ICommand NewItemCommand { get; }

        public PaymentPlannedListGeneratorViewModel(IUnityContainer container) : base(container)
        {
            GeneratePaymentsCommand = new DelegateCommand(GeneratePaymentsCommand_Execute);
            NewItemCommand = new DelegateCommand(GeneratePaymentsCommand_Execute);
        }

        private async void GeneratePaymentsCommand_Execute()
        {
            var currentPayments = await UnitOfWork.GetRepository<PaymentPlanned>().GetAllAsync();
            UnitOfWork.GetRepository<PaymentPlanned>().DeleteRange(currentPayments);

            var salesUnitWrappers = (await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync()).
                Select(x => new SalesUnitWrapper(x)).ToList();
            salesUnitWrappers.ForEach(x => x.ReloadPaymentsPlannedFull());

            var salesUnits = await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync();

            Payments.Clear();
            var groups = salesUnitWrappers.SelectMany(x => x.PaymentsPlanned).
                Select(x => new {salesUnit = salesUnits.Single(su => su.Id == x.SalesUnitId), payment = x.Model}).
                GroupBy(x => new
                {
                    productId = x.salesUnit.Product.Id,
                    facilityId = x.salesUnit.Facility.Id,
                    cost = x.salesUnit.Cost,
                    date = x.payment.Date,
                    sum = x.payment.Sum,
                    conditionId = x.payment.Condition.Id
                }).Select(x => new PaymentPlannedGroup(x.Select(y => y.payment), x.Select(y => y.salesUnit).First()));
            Payments.AddRange(groups);
        }
    }

    public class PaymentPlannedGroup : INotifyPropertyChanged
    {
        private readonly List<PaymentPlanned> _payments;

        public PaymentPlannedGroup(IEnumerable<PaymentPlanned> payments, SalesUnit salesUnit)
        {
            SalesUnit = salesUnit;
            _payments = new List<PaymentPlanned>(payments);
        }

        public SalesUnit SalesUnit { get; }

        public double Sum
        {
            get { return _payments.Sum(x => x.Sum); }
            set
            {
                _payments.ForEach(x => x.Sum = value / _payments.Count);
                OnPropertyChanged();
            }
        }

        public int Amount => _payments.Count;

        public DateTime DateTime
        {
            get { return _payments.First().Date; }
            set
            {
                _payments.ForEach(x => x.Date = value);
                OnPropertyChanged();
            }
        }

        public PaymentCondition Condition => _payments.First().Condition;


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
