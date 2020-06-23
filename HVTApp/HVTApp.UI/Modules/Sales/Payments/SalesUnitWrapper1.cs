using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.Modules.Sales.Payments
{
    public class SalesUnitWrapper1 : WrapperBase<SalesUnit>
    {
        public SalesUnitWrapper1(SalesUnit model) : base(model)
        {
            // Актуализация плановых поступлений (удаление оплаченных, и актуализация дат/сумм)
            
            //коллекция актуальных плановых платежей (может отличаться от сохраненных)
            var paymentPlannedActual = this.Model.PaymentsPlannedActual;

            foreach (var paymentPlannedWrapper in this.PaymentsPlanned.ToList())
            {
                //сопоставляем актуальный плановый платеж с сохраненным
                var paymentActual = paymentPlannedActual.SingleOrDefault(x => x.Id == paymentPlannedWrapper.Id);
                if (paymentActual == null)
                {
                    //удаляем неактуальный платеж
                    this.PaymentsPlanned.Remove(paymentPlannedWrapper);
                }
                else
                {
                    //актуализируем параметры актуального платежа
                    paymentPlannedWrapper.Date = paymentActual.Date;
                    paymentPlannedWrapper.Part = paymentActual.Part;
                }
            }
        }

        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }

        protected override void InitializeCollectionProperties()
        {
            if (Model.PaymentsPlanned == null) throw new ArgumentException($"{nameof(PaymentsPlanned)} can not be null");
            PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlanned.Select(e => new PaymentPlannedWrapper(e)));
            RegisterCollection(PaymentsPlanned, Model.PaymentsPlanned);
        }
    }
}