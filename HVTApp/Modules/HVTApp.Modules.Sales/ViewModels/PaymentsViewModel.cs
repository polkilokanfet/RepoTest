using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PaymentsViewModel
    {
        private readonly IUnityContainer _container;
        private IUnitOfWork UnitOfWork { get; }
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitWrappers;
        public ObservableCollection<PaymentWrapper> Payments { get; } = new ObservableCollection<PaymentWrapper>();

        public PaymentsViewModel(IUnityContainer container)
        {
            _container = container;
            UnitOfWork = container.Resolve<IUnitOfWork>();
        }

        public async Task LoadAsync()
        {
            //загружаем все юниты
            var salesUnits = await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync();

            //фиксируем их в коллекции для отслеживания изменений
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnits.Select(x => new SalesUnitWrapper(x)));

            //вормируем лукапы
            var lookups = salesUnits.Select(x => new SalesUnitLookup(x));
            foreach (var lookup in lookups)
            {
                await lookup.LoadOther(UnitOfWork);
            }


            foreach (var wrapper in _salesUnitWrappers)
            {
                //платежи, находящиеся в юните
                foreach (var paymentPlannedWrapper in wrapper.PaymentsPlanned)
                {
                    Payments.Add(new PaymentWrapper(paymentPlannedWrapper, wrapper, true));
                }

                //сгенерированные платежи
                var lookup = lookups.Single(x => x.Id == wrapper.Id);
                var paymentLookupsToAdd = lookup.PaymentPlannedWithSaved.Except(lookup.PaymentsPlanned);
                foreach (var paymentPlannedLookup in paymentLookupsToAdd)
                {
                    Payments.Add(new PaymentWrapper(new PaymentPlannedWrapper(paymentPlannedLookup.Entity), wrapper, false));
                }
            }
        }
    }

    public class PaymentWrapper
    {
        public PaymentPlannedWrapper PaymentPlannedWrapper { get; }
        private SalesUnitWrapper SalesUnit { get; }
        private bool InUnit { get; }

        public PaymentWrapper(PaymentPlannedWrapper paymentPlannedWrapper, SalesUnitWrapper salesUnit, bool InUnit)
        {
            SalesUnit = salesUnit;
            this.InUnit = InUnit;
            PaymentPlannedWrapper = paymentPlannedWrapper;
            PaymentPlannedWrapper.PropertyChanged += PaymentPlannedWrapperOnPropertyChanged;
        }

        private void PaymentPlannedWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (PaymentPlannedWrapper.IsChanged)
            {
                if (SalesUnit.PaymentsPlanned.Contains(PaymentPlannedWrapper))
                    return;
                SalesUnit.PaymentsPlanned.Add(PaymentPlannedWrapper);
            }
            else
            {
                if (!SalesUnit.PaymentsPlanned.Contains(PaymentPlannedWrapper))
                    return;
                SalesUnit.PaymentsPlanned.Remove(PaymentPlannedWrapper);
            }
        }
    }

}
