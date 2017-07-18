using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class PaymentConditionStandartWrapper : WrapperBase<PaymentConditionStandart>
  {
    private PaymentConditionStandartWrapper() : base(new PaymentConditionStandart()) { }
    private PaymentConditionStandartWrapper(PaymentConditionStandart model) : base(model) { }



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);


    }

  }
}
