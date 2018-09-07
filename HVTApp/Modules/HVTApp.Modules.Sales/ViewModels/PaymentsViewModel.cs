using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PaymentsViewModel : BindableBase
    {
        private readonly IUnityContainer _container;
        private IUnitOfWork UnitOfWork;
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitWrappers;
        private bool _isLoaded = false;
        public ObservableCollection<PaymentsGroup> PaymentsGroups { get; } = new ObservableCollection<PaymentsGroup>();

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand ReloadCommand { get; set; }

        public PaymentsViewModel(IUnityContainer container)
        {
            _container = container;

            SaveCommand = new DelegateCommand(SaveCommand_Execute, () => _salesUnitWrappers != null && _salesUnitWrappers.IsChanged && _salesUnitWrappers.IsValid);
            ReloadCommand = new DelegateCommand(ReloadCommand_Execute);
            RefreshCommand = new DelegateCommand(RefreshPayments);
        }

        private async void ReloadCommand_Execute()
        {
            await LoadAsync();
        }

        private async void SaveCommand_Execute()
        {
            _salesUnitWrappers.AcceptChanges();
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task LoadAsync()
        {
            IsLoaded = false;
            UnitOfWork = _container.Resolve<IUnitOfWork>();

            //загружаем все юниты
            var salesUnitWrappers = (await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync()).Select(x => new SalesUnitWrapper(x));
            
            //фиксируем их в коллекции для отслеживания изменений
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnitWrappers);

            //подписка на изменение
            _salesUnitWrappers.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            _salesUnitWrappers.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();


            //актуализация плановых поступлений
            _salesUnitWrappers.ForEach(Actualization);

            RefreshPayments();

            IsLoaded = true;
        }

        private void RefreshPayments()
        {
            var payments = new List<PaymentWrapper>();
            foreach (var salesUnitWrapper in _salesUnitWrappers)
            {
                payments.AddRange(GetPayments(salesUnitWrapper));
            }

            payments = payments.OrderBy(x => x.PaymentPlannedWrapper.Date).ToList();

            var groups = payments.GroupBy(x => new
            {
                ProjectId = x.SalesUnit.Project.Id,
                ProductId = x.SalesUnit.Product.Id,
                FacilityId = x.SalesUnit.Facility.Id,
                Cost = x.SalesUnit.Cost,
                Part = x.PaymentPlannedWrapper.Part,
                Date = x.PaymentPlannedWrapper.Date,
                ConditionId = x.PaymentPlannedWrapper.Condition.Id
            });

            PaymentsGroups.Clear();
            PaymentsGroups.AddRange(groups.Select(x => new PaymentsGroup(x)));
        }

        private void Actualization(SalesUnitWrapper salesUnitWrapper)
        {
            var paymentsWrappers = salesUnitWrapper.PaymentsPlanned;
            var paymentsActual = salesUnitWrapper.Model.PaymentsPlannedActual;
            var remove = new List<PaymentPlannedWrapper>();
            foreach (var paymentWrapper in paymentsWrappers)
            {
                var paymentActual = paymentsActual.SingleOrDefault(x => x.Id == paymentWrapper.Id);
                if (paymentActual == null)
                {
                    remove.Add(paymentWrapper);
                    UnitOfWork.GetRepository<PaymentPlanned>().Delete(paymentWrapper.Model);
                    continue;
                }

                paymentWrapper.Date = paymentActual.Date;
                paymentWrapper.Part = paymentActual.Part;
            }
            remove.ForEach(x => salesUnitWrapper.PaymentsPlanned.Remove(x));
        }

        private IEnumerable<PaymentWrapper> GetPayments(SalesUnitWrapper salesUnitWrapper)
        {
            //платежи, находящиеся в юните
            foreach (var paymentPlannedWrapper in salesUnitWrapper.PaymentsPlanned)
            {
                yield return new PaymentWrapper(paymentPlannedWrapper, salesUnitWrapper, true);
            }

            //платежи сгенерированные
            foreach (var ppg in salesUnitWrapper.PaymentsPlannedGenerated)
            {
                yield return new PaymentWrapper(ppg, salesUnitWrapper, false);
            }
        }
    }

    public class PaymentsGroup : BindableBase
    {
        private readonly List<PaymentWrapper> _payments;
        private DateTime _date;

        public int Amount => _payments.Count;

        public double Sum => Amount * SalesUnit.Cost;

        public SalesUnitWrapper SalesUnit { get; set; }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (Equals(_date, value)) return;
                if (value < DateTime.Today) return;
                _date = value;
                if(Groups.Any())
                    Groups.ForEach(x => x.Date = value);
                else
                    _payments.ForEach(x => x.PaymentPlannedWrapper.Date = value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PaymentsGroup> Groups { get; } = new ObservableCollection<PaymentsGroup>();

        public PaymentsGroup(IEnumerable<PaymentWrapper> payments)
        {
            _payments = payments.ToList();
            _date = payments.First().PaymentPlannedWrapper.Date;
            SalesUnit = payments.First().SalesUnit;


            if(payments.Count() > 1)
                Groups.AddRange(payments.Select(x => new PaymentsGroup(new List<PaymentWrapper> {x})));
        }
    }

    public class PaymentWrapper : BindableBase
    {
        private bool _willSave;

        public PaymentPlannedWrapper PaymentPlannedWrapper { get; }
        public SalesUnitWrapper SalesUnit { get; }

        /// <summary>
        /// был ли платеж изначально сохранен в юните
        /// </summary>
        private bool InUnit { get; }

        /// <summary>
        /// будет ли платеж сохранен
        /// </summary>
        public bool WillSave
        {
            get { return _willSave; }
            private set
            {
                _willSave = value;
                OnPropertyChanged();
            }
        }

        public PaymentWrapper(PaymentPlannedWrapper paymentPlannedWrapper, SalesUnitWrapper salesUnit, bool inUnit)
        {
            SalesUnit = salesUnit;
            this.InUnit = inUnit;
            WillSave = inUnit;
            PaymentPlannedWrapper = paymentPlannedWrapper;

            PaymentPlannedWrapper.Sum = paymentPlannedWrapper.Part * paymentPlannedWrapper.Condition.Part * SalesUnit.Cost;

            //подписка на событие изменения свойств
            PaymentPlannedWrapper.PropertyChanged += PaymentPlannedWrapperOnPropertyChanged;
        }

        private void PaymentPlannedWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!Equals(args.PropertyName, nameof(PaymentPlannedWrapper.Date)))
                return;

            //если дата изменилась, платеж нужно запомнить
            if (PaymentPlannedWrapper.IsChanged)
            {
                //если он уже не добавлен
                if (SalesUnit.PaymentsPlanned.Contains(PaymentPlannedWrapper))
                    return;
                SalesUnit.PaymentsPlanned.Add(PaymentPlannedWrapper);
                WillSave = true;
            }
            else
            {
                //если изменения убрали - забыть платеж, если он не был запомнен изначально
                if (SalesUnit.PaymentsPlanned.Contains(PaymentPlannedWrapper) && !InUnit)
                {
                    SalesUnit.PaymentsPlanned.Remove(PaymentPlannedWrapper);
                    WillSave = false;
                }
            }
        }
    }
}
