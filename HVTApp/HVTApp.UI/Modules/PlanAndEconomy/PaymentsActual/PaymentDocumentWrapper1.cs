using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class PaymentDocumentWrapper1 : WrapperBase<PaymentDocument>
    {
        public PaymentDocumentWrapper1(PaymentDocument model) : base(model) { }

        #region SimpleProperties

        //Number
        public string Number
        {
            get => Model.Number;
            set => SetValue(value);
        }
        public string NumberOriginalValue => GetOriginalValue<string>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));

        //Vat
        public double Vat
        {
            get => Model.Vat;
            set => SetValue(value);
        }
        public double VatOriginalValue => GetOriginalValue<double>(nameof(Vat));
        public bool VatIsChanged => GetIsChanged(nameof(Vat));

        //Id
        public System.Guid Id => Model.Id;

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<PaymentActualWrapper1> Payments { get; private set; }

        #endregion

        #region GetProperties

        public DateTime Date => Model.Date;

        #endregion

        protected override void InitializeCollectionProperties()
        {

            if (Model.Payments == null) throw new ArgumentException("Payments cannot be null");
            Payments = new ValidatableChangeTrackingCollection<PaymentActualWrapper1>(Model.Payments.Select(paymentActual => new PaymentActualWrapper1(paymentActual)));
            RegisterCollection(Payments, Model.Payments);
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (Vat < 0)
            {
                yield return new ValidationResult("НДС не может быть отрицательным", new[] { nameof(Vat) });
            }

        }
    }
}