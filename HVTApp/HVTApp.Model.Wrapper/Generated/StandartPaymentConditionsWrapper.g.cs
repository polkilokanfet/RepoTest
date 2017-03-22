using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class StandartPaymentConditionsWrapper : WrapperBase<StandartPaymentConditions>
  {
    public StandartPaymentConditionsWrapper(StandartPaymentConditions model) : base(model) { }
    public StandartPaymentConditionsWrapper(StandartPaymentConditions model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<PaymentsConditionWrapper> PaymentsConditionsCollection { get; private set; }

    #endregion
  
    protected override void InitializeCollectionComplexProperties(StandartPaymentConditions model)
    {
      if (model.PaymentsConditionsCollection == null) throw new ArgumentException("PaymentsConditionsCollection cannot be null");
      PaymentsConditionsCollection = new ValidatableChangeTrackingCollection<PaymentsConditionWrapper>(model.PaymentsConditionsCollection.Select(e => new PaymentsConditionWrapper(e, ExistsWrappers)));
      RegisterCollection(PaymentsConditionsCollection, model.PaymentsConditionsCollection);

    }
  }
}
