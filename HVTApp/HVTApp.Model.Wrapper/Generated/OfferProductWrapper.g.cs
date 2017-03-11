using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferProductWrapper : WrapperBase<OfferProduct>
  {
    public OfferProductWrapper(OfferProduct model) : base(model) { }
    public OfferProductWrapper(OfferProduct model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public ProductMainWrapper ProductMain { get; private set; }

    public OfferUnitWrapper OfferUnit { get; private set; }

    public CostInfoWrapper CostInfo { get; private set; }

    public PlannedTermProductionWrapper PlannedTermProduction { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<PaymentsConditionWrapper> PaymentsConditions { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(OfferProduct model)
    {
      if (model.ProductMain == null) throw new ArgumentException("ProductMain cannot be null");
      if (ExistsWrappers.ContainsKey(model.ProductMain))
      {
          ProductMain = (ProductMainWrapper)ExistsWrappers[model.ProductMain];
      }
      else
      {
          ProductMain = new ProductMainWrapper(model.ProductMain, ExistsWrappers);
          RegisterComplexProperty(ProductMain);
      }

      if (model.OfferUnit == null) throw new ArgumentException("OfferUnit cannot be null");
      if (ExistsWrappers.ContainsKey(model.OfferUnit))
      {
          OfferUnit = (OfferUnitWrapper)ExistsWrappers[model.OfferUnit];
      }
      else
      {
          OfferUnit = new OfferUnitWrapper(model.OfferUnit, ExistsWrappers);
          RegisterComplexProperty(OfferUnit);
      }

      if (model.CostInfo == null) throw new ArgumentException("CostInfo cannot be null");
      if (ExistsWrappers.ContainsKey(model.CostInfo))
      {
          CostInfo = (CostInfoWrapper)ExistsWrappers[model.CostInfo];
      }
      else
      {
          CostInfo = new CostInfoWrapper(model.CostInfo, ExistsWrappers);
          RegisterComplexProperty(CostInfo);
      }

      if (model.PlannedTermProduction == null) throw new ArgumentException("PlannedTermProduction cannot be null");
      if (ExistsWrappers.ContainsKey(model.PlannedTermProduction))
      {
          PlannedTermProduction = (PlannedTermProductionWrapper)ExistsWrappers[model.PlannedTermProduction];
      }
      else
      {
          PlannedTermProduction = new PlannedTermProductionWrapper(model.PlannedTermProduction, ExistsWrappers);
          RegisterComplexProperty(PlannedTermProduction);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(OfferProduct model)
    {
      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentsConditionWrapper>(model.PaymentsConditions.Select(e => new PaymentsConditionWrapper(e, ExistsWrappers)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);

    }
  }
}
