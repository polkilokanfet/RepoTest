using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
  {
    protected OfferUnitWrapper(OfferUnit model) : base(model) { }

	public static OfferUnitWrapper GetWrapper(OfferUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (OfferUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new OfferUnitWrapper(model);
	}



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
        get { return EquipmentWrapper.GetWrapper(Model.Equipment); }
        set
        {
			var oldPropVal = Equipment;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }


	public OfferWrapper Offer 
    {
        get { return OfferWrapper.GetWrapper(Model.Offer); }
        set
        {
			var oldPropVal = Offer;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }


	public PlannedTermProductionWrapper PlannedTermProduction 
    {
        get { return PlannedTermProductionWrapper.GetWrapper(Model.PlannedTermProduction); }
        set
        {
			var oldPropVal = PlannedTermProduction;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }


    #endregion


    #region CollectionProperties

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

        Equipment = EquipmentWrapper.GetWrapper(model.Equipment);

        Offer = OfferWrapper.GetWrapper(model.Offer);

        PlannedTermProduction = PlannedTermProductionWrapper.GetWrapper(model.PlannedTermProduction);

    }

  
    protected override void InitializeCollectionComplexProperties(OfferUnit model)
    {

      if (model.Facilities == null) throw new ArgumentException("Facilities cannot be null");
      Facilities = new ValidatableChangeTrackingCollection<FacilityWrapper>(model.Facilities.Select(e => FacilityWrapper.GetWrapper(e)));
      RegisterCollection(Facilities, model.Facilities);


      if (model.OfferProducts == null) throw new ArgumentException("OfferProducts cannot be null");
      OfferProducts = new ValidatableChangeTrackingCollection<OfferProductWrapper>(model.OfferProducts.Select(e => OfferProductWrapper.GetWrapper(e)));
      RegisterCollection(OfferProducts, model.OfferProducts);


    }

  }
}
