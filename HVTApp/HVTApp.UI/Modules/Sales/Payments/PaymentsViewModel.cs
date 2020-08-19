using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Payments
{
    public class PaymentsViewModel : LoadableExportableExpandCollapseViewModel
    {
        private IValidatableChangeTrackingCollection<SalesUnitWrapper1> _salesUnitWrappers;
        private object _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PaymentsGroup> PaymentsGroups { get; } = new ObservableCollection<PaymentsGroup>();

        public ICommand SaveCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand RemoveCommand { get; }

        public PaymentsViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                () =>
                {
                    _salesUnitWrappers.AcceptChanges();
                    UnitOfWork.SaveChanges();
                }, 
                () => _salesUnitWrappers != null && _salesUnitWrappers.IsChanged && _salesUnitWrappers.IsValid);


            RefreshCommand = new DelegateCommand(RefreshPayments);

            RemoveCommand = new DelegateCommand(
                () =>
                {
                    (SelectedItem as PaymentsGroup)?.RemovePayments(UnitOfWork);
                    (SelectedItem as PaymentWrapper)?.Remove(UnitOfWork);
                },
                () =>
                {
                    var paymentsGroup = SelectedItem as PaymentsGroup;
                    if (paymentsGroup != null)
                    {
                        if (paymentsGroup.IsCustom != null)
                            return paymentsGroup.IsCustom.Value;
                        return false;
                    }

                    var item = SelectedItem as PaymentWrapper;
                    if (item != null)
                    {
                        return item.IsInPlanPayments;
                    }

                    return false;
                });

        }

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //загружаем все юниты и фиксируем их в коллекции для отслеживания изменений
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper1>(
                UnitOfWork.Repository<SalesUnit>()
                .Find(x => !x.IsPaid && !x.IsLoosen && x.Project.ForReport && x.Project.Manager.IsAppCurrentUser())
                .Select(x => new SalesUnitWrapper1(x, UnitOfWork)));

            //подписка на изменение
            _salesUnitWrappers.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            _salesUnitWrappers.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override void AfterGetData()
        {
            RefreshPayments();
        }

        private void RefreshPayments()
        {
            var payments = new List<PaymentWrapper>();
            foreach (var salesUnitWrapper in _salesUnitWrappers)
            {
                payments.AddRange(GetPayments(salesUnitWrapper));
            }

            payments = payments.OrderBy(x => x.PaymentPlanned.Date).ThenBy(x => x.SalesUnit.Model.Facility.Id).ToList();
            var groups = payments.GroupBy(x => new
            {
                ProjectId = x.SalesUnit.Model.Project.Id,
                ProductId = x.SalesUnit.Model.Product.Id,
                FacilityId = x.SalesUnit.Model.Facility.Id,
                Cost = x.SalesUnit.Model.Cost,
                Part = x.PaymentPlanned.Part,
                Date = x.PaymentPlanned.Date,
                ConditionId = x.PaymentPlanned.Condition.Id,
                SpecificationId = x.SalesUnit.Model.Specification?.Id,
                WillSave = x.IsInPlanPayments
            }).Where(x => x.Sum(xx => xx.Sum) > 0.00001);

            PaymentsGroups.Clear();
            PaymentsGroups.AddRange(groups.Select(x => new PaymentsGroup(x)));
        }

        private IEnumerable<PaymentWrapper> GetPayments(SalesUnitWrapper1 salesUnitWrapper)
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
