using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using PaymentsGroup = HVTApp.UI.Groups.PaymentsGroup;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class PaymentsViewModel : ViewModelBaseCanExportToExcelSaveCustomization
    {
        private IValidatableChangeTrackingCollection<SalesUnitPaymentsPlannedWrapper> _salesUnitWrappers;
        private PaymentsGroup _selectedGroup;

        public PaymentsGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PaymentsGroup> PaymentsGroups { get; } = new ObservableCollection<PaymentsGroup>();

        public ICommand SaveCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand ReloadCommand { get; set; }

        public PaymentsViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                () =>
                {
                    _salesUnitWrappers.AcceptChanges();
                    UnitOfWork.SaveChanges();
                }, 
                () => _salesUnitWrappers != null && _salesUnitWrappers.IsChanged && _salesUnitWrappers.IsValid);

            ReloadCommand = new DelegateCommand(Load);

            RefreshCommand = new DelegateCommand(RefreshPayments);

            RemoveCommand = new DelegateCommand(
                () =>
                {
                    SelectedGroup.RemovePayments(UnitOfWork);
                    RefreshPayments();
                }, 
                () => SelectedGroup?.WillSave != null);

            Load();
        }

        protected void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //загружаем все юниты и фиксируем их в коллекции для отслеживания изменений
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitPaymentsPlannedWrapper>(UnitOfWork.Repository<SalesUnit>()
                .Find(x => !x.IsLoosen && !x.IsPaid && x.Project.ForReport && x.Project.Manager.IsAppCurrentUser())
                .Select(x => new SalesUnitPaymentsPlannedWrapper(x)));

            //подписка на изменение
            _salesUnitWrappers.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            _salesUnitWrappers.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            _salesUnitWrappers.ForEach(Actualization);

            RefreshPayments();
        }

        /// <summary>
        /// Актуализация плановых поступлений
        /// </summary>
        /// <param name="salesUnitWrapper"></param>
        private void Actualization(SalesUnitPaymentsPlannedWrapper salesUnitWrapper)
        {
            //коллекция актуальных плановых платежей (может отличаться от сохраненных)
            var paymentPlannedActual = salesUnitWrapper.Model.PaymentsPlannedActual;

            foreach (var paymentPlannedWrapper in salesUnitWrapper.PaymentsPlanned.ToList())
            {
                //сопоставляем актуальный плановый платеж с сохраненным
                var paymentActual = paymentPlannedActual.SingleOrDefault(x => x.Id == paymentPlannedWrapper.Id);
                //удаляем неактуальный платеж
                if (paymentActual == null)
                {
                    salesUnitWrapper.PaymentsPlanned.Remove(paymentPlannedWrapper);
                    UnitOfWork.Repository<PaymentPlanned>().Delete(paymentPlannedWrapper.Model);
                    continue;
                }

                //актуализируем параметры платежа
                paymentPlannedWrapper.Date = paymentActual.Date;
                paymentPlannedWrapper.Part = paymentActual.Part;
            }
        }

        private void RefreshPayments()
        {
            var payments = new List<PaymentWrapper>();
            foreach (var salesUnitWrapper in _salesUnitWrappers)
            {
                payments.AddRange(GetPayments(salesUnitWrapper));
            }

            payments = payments.OrderBy(x => x.PaymentPlanned.Date).ToList();
            var groups = payments.GroupBy(x => new
            {
                ProjectId = x.SalesUnit.Model.Project.Id,
                ProductId = x.SalesUnit.Model.Product.Id,
                FacilityId = x.SalesUnit.Model.Facility.Id,
                Cost = x.SalesUnit.Model.Cost,
                Part = x.PaymentPlanned.Part,
                Date = x.PaymentPlanned.Date,
                ConditionId = x.PaymentPlanned.Condition.Id,
                WillSave = x.IsInPlanPayments
            });

            PaymentsGroups.ForEach(x => x.DateChanged -= OnGroupDateChanged);
            PaymentsGroups.ForEach(x => x.RemoveSubscribes());
            PaymentsGroups.Clear();
            PaymentsGroups.AddRange(groups.Select(x => new PaymentsGroup(x)));
            PaymentsGroups.ForEach(x => x.DateChanged += OnGroupDateChanged);
        }

        private void OnGroupDateChanged(PaymentsGroup paymentsGroup)
        {
            RefreshPayments();
            SelectedGroup = PaymentsGroups.SingleOrDefault(x => x.Ids.MembersAreSame(paymentsGroup.Ids));
        }

        private IEnumerable<PaymentWrapper> GetPayments(SalesUnitPaymentsPlannedWrapper salesUnitWrapper)
        {
            //платежи, находящиеся в юните
            foreach (var paymentPlannedWrapper in salesUnitWrapper.PaymentsPlanned)
            {
                yield return new PaymentWrapper(salesUnitWrapper, paymentPlannedWrapper.Id);
            }

            //платежи сгенерированные
            //необходимо брать именно из Model (актуально)
            foreach (var paymentPlanned in salesUnitWrapper.Model.PaymentsPlannedGenerated)
            {
                yield return new PaymentWrapper(salesUnitWrapper, paymentPlanned);
            }
        }
    }
}
