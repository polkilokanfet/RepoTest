using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
  {
    public OfferUnitWrapper(OfferUnit model) : base(model) { }
    public OfferUnitWrapper(OfferUnit model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public EquipmentWrapper Equipment { get; private set; }

    public OfferWrapper Offer { get; private set; }

    public PlannedTermProductionWrapper PlannedTermProduction { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<FacilityWrapper> Facilities { get; private set; }

    public ValidatableChangeTrackingCollection<OfferProductWrapper> OfferProducts { get; private set; }

    #endregion

    #region GetProperties
    public System.Int32 Count => GetValue<System.Int32>(); 

    public System.Double Sum => GetValue<System.Double>(); 

    public System.Double SumWithVat => GetValue<System.Double>(); 

    #endregion
    
    protected override void InitializeComplexProperties(OfferUnit model)
    {
      if (model.Equipment == null) throw new ArgumentException("Equipment cannot be null");
      if (ExistsWrappers.ContainsKey(model.Equipment))
      {
          Equipment = (EquipmentWrapper)ExistsWrappers[model.Equipment];
      }
      else
      {
          Equipment = new EquipmentWrapper(model.Equipment, ExistsWrappers);
          RegisterComplexProperty(Equipment);
      }

      if (model.Offer == null) throw new ArgumentException("Offer cannot be null");
      if (ExistsWrappers.ContainsKey(model.Offer))
      {
          Offer = (OfferWrapper)ExistsWrappers[model.Offer];
      }
      else
      {
          Offer = new OfferWrapper(model.Offer, ExistsWrappers);
          RegisterComplexProperty(Offer);
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
  
    protected override void InitializeCollectionComplexProperties(OfferUnit model)
    {
      if (model.Facilities == null) throw new ArgumentException("Facilities cannot be null");
      Facilities = new ValidatableChangeTrackingCollection<FacilityWrapper>(model.Facilities.Select(e => new FacilityWrapper(e, ExistsWrappers)));
      RegisterCollection(Facilities, model.Facilities);

      if (model.OfferProducts == null) throw new ArgumentException("OfferProducts cannot be null");
      OfferProducts = new ValidatableChangeTrackingCollection<OfferProductWrapper>(model.OfferProducts.Select(e => new OfferProductWrapper(e, ExistsWrappers)));
      RegisterCollection(OfferProducts, model.OfferProducts);

    }
  }
}
