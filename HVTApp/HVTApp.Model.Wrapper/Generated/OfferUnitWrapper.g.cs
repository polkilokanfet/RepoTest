using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
  {
    public OfferUnitWrapper(OfferUnit model) : base(model) { }
    public OfferUnitWrapper(OfferUnit model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


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

	public EquipmentWrapper Equipment
	{
		get { return GetComplexProperty<Equipment, EquipmentWrapper>(nameof(Equipment)); }
		set { SetComplexProperty<Equipment, EquipmentWrapper>(value, nameof(Equipment)); }
	}


	public OfferWrapper Offer
	{
		get { return GetComplexProperty<Offer, OfferWrapper>(nameof(Offer)); }
		set { SetComplexProperty<Offer, OfferWrapper>(value, nameof(Offer)); }
	}


	public PlannedTermProductionWrapper PlannedTermProduction
	{
		get { return GetComplexProperty<PlannedTermProduction, PlannedTermProductionWrapper>(nameof(PlannedTermProduction)); }
		set { SetComplexProperty<PlannedTermProduction, PlannedTermProductionWrapper>(value, nameof(PlannedTermProduction)); }
	}


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

        Equipment = GetWrapper<Equipment, EquipmentWrapper>(model.Equipment);

        Offer = GetWrapper<Offer, OfferWrapper>(model.Offer);

        PlannedTermProduction = GetWrapper<PlannedTermProduction, PlannedTermProductionWrapper>(model.PlannedTermProduction);

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
