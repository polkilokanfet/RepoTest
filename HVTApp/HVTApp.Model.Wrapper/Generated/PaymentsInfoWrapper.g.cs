using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentsInfoWrapper : WrapperBase<PaymentsInfo>
  {
    public PaymentsInfoWrapper(PaymentsInfo model) : base(model) { }
    public PaymentsInfoWrapper(PaymentsInfo model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
    public ProductBaseWrapper Product { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<PaymentsConditionWrapper> PaymentsConditions { get; private set; }

    public ValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }

    public ValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }

    #endregion

    #region GetProperties
    public System.Double PaymentsSumToStartProduction => GetValue<System.Double>(); 

    public System.Double PaymentsSumToShipping => GetValue<System.Double>(); 

    public System.Boolean IsPaid => GetValue<System.Boolean>(); 

    #endregion
    
    protected override void InitializeComplexProperties(PaymentsInfo model)
    {
      if (model.Product == null) throw new ArgumentException("Product cannot be null");
      if (ExistsWrappers.ContainsKey(model.Product))
      {
          Product = (ProductBaseWrapper)ExistsWrappers[model.Product];
      }
      else
      {
          Product = new ProductBaseWrapper(model.Product, ExistsWrappers);
          RegisterComplexProperty(Product);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(PaymentsInfo model)
    {
      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentsConditionWrapper>(model.PaymentsConditions.Select(e => new PaymentsConditionWrapper(e, ExistsWrappers)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);

      if (model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
      PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(model.PaymentsActual.Select(e => new PaymentActualWrapper(e, ExistsWrappers)));
      RegisterCollection(PaymentsActual, model.PaymentsActual);

      if (model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
      PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(model.PaymentsPlanned.Select(e => new PaymentPlannedWrapper(e, ExistsWrappers)));
      RegisterCollection(PaymentsPlanned, model.PaymentsPlanned);

    }
  }
}
