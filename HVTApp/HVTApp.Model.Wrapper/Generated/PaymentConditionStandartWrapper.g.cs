using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentConditionStandartWrapper : WrapperBase<PaymentConditionStandart>
  {
    public PaymentConditionStandartWrapper() : base(new PaymentConditionStandart(), new Dictionary<IBaseEntity, object>()) { }
    public PaymentConditionStandartWrapper(PaymentConditionStandart model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public PaymentConditionStandartWrapper(PaymentConditionStandart model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public PaymentConditionStandartWrapper(PaymentConditionStandart model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }

    #endregion
  
    protected override void InitializeCollectionComplexProperties(PaymentConditionStandart model)
    {
      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);

    }
  }
}
