﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PaymentsViewModel : LoadableBindableBase
    {
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitWrappers;
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
            SaveCommand = new DelegateCommand(SaveCommand_Execute, () => _salesUnitWrappers != null && _salesUnitWrappers.IsChanged && _salesUnitWrappers.IsValid);
            ReloadCommand = new DelegateCommand(ReloadCommand_Execute);
            RefreshCommand = new DelegateCommand(RefreshPayments);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => SelectedGroup != null && SelectedGroup.WillSave);
        }

        //необходимо для отслеживания изначально сохраненных платежей
        private List<PaymentPlanned> _originPaymentPlanneds;

        protected override async Task LoadedAsyncMethod()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //загружаем все юниты
            var salesUnitWrappers = (await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync()).Select(x => new SalesUnitWrapper(x));
            
            //фиксируем их в коллекции для отслеживания изменений
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnitWrappers);

            //подписка на изменение
            _salesUnitWrappers.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            _salesUnitWrappers.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            _originPaymentPlanneds = _salesUnitWrappers.SelectMany(x => x.PaymentsPlanned).Select(x => x.Model).ToList();

            //актуализация плановых поступлений
            _salesUnitWrappers.ForEach(Actualization);

            RefreshPayments();
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
                ConditionId = x.PaymentPlannedWrapper.Condition.Id,
                WillSave = x.WillSave
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
            SelectedGroup = PaymentsGroups.SingleOrDefault(x => x.Ids.AllMembersAreSame(paymentsGroup.Ids));
        }

        private IEnumerable<PaymentWrapper> GetPayments(SalesUnitWrapper salesUnitWrapper)
        {
            //платежи, находящиеся в юните
            foreach (var ppw in salesUnitWrapper.PaymentsPlanned)
            {
                yield return new PaymentWrapper(ppw, salesUnitWrapper, _originPaymentPlanneds.Contains(ppw.Model), true);
            }

            //платежи сгенерированные
            //необходимо брать именно из Model (актуально)
            foreach (var ppg in salesUnitWrapper.Model.PaymentsPlannedGenerated)
            {
                yield return new PaymentWrapper(new PaymentPlannedWrapper(ppg), salesUnitWrapper, _originPaymentPlanneds.Contains(ppg), false);
            }
        }


        #region Commands

        private void RemoveCommand_Execute()
        {
            SelectedGroup.RemovePayments(UnitOfWork);
            RefreshPayments();
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

        #endregion
    }
}