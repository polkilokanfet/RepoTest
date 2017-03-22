using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentDocumentWrapper : WrapperBase<PaymentDocument>
  {
    public PaymentDocumentWrapper(PaymentDocument model) : base(model) { }
    public PaymentDocumentWrapper(PaymentDocument model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String Number
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
    public bool NumberIsChanged => GetIsChanged(nameof(Number));

    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<PaymentActualWrapper> Payments { get; private set; }

    #endregion
  
    protected override void InitializeCollectionComplexProperties(PaymentDocument model)
    {
      if (model.Payments == null) throw new ArgumentException("Payments cannot be null");
      Payments = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(model.Payments.Select(e => new PaymentActualWrapper(e, ExistsWrappers)));
      RegisterCollection(Payments, model.Payments);

    }
  }
}
