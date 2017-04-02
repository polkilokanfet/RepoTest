using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferProductWrapper : WrapperBase<OfferProduct>
  {
    public OfferProductWrapper(OfferProduct model) : base(model) { }
    public OfferProductWrapper(OfferProduct model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static OfferProductWrapper GetWrapper(OfferProduct model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (OfferProductWrapper)Repository.ModelWrapperDictionary[model];

		return new OfferProductWrapper(model);
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

	public ProductMainWrapper ProductMain
	{
		get { return GetComplexProperty<ProductMain, ProductMainWrapper>(nameof(ProductMain)); }
		set { SetComplexProperty<ProductMain, ProductMainWrapper>(value, nameof(ProductMain)); }
	}


	public OfferUnitWrapper OfferUnit
	{
		get { return GetComplexProperty<OfferUnit, OfferUnitWrapper>(nameof(OfferUnit)); }
		set { SetComplexProperty<OfferUnit, OfferUnitWrapper>(value, nameof(OfferUnit)); }
	}


	public CostInfoWrapper CostInfo
	{
		get { return GetComplexProperty<CostInfo, CostInfoWrapper>(nameof(CostInfo)); }
		set { SetComplexProperty<CostInfo, CostInfoWrapper>(value, nameof(CostInfo)); }
	}


	public PlannedTermProductionWrapper PlannedTermProduction
	{
		get { return GetComplexProperty<PlannedTermProduction, PlannedTermProductionWrapper>(nameof(PlannedTermProduction)); }
		set { SetComplexProperty<PlannedTermProduction, PlannedTermProductionWrapper>(value, nameof(PlannedTermProduction)); }
	}


    #endregion


    #region CollectionComplexProperties

    public ValidatableChangeTrackingCollection<PaymentsConditionWrapper> PaymentsConditions { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(OfferProduct model)
    {

        ProductMain = ProductMainWrapper.GetWrapper(model.ProductMain);

        OfferUnit = OfferUnitWrapper.GetWrapper(model.OfferUnit);

        CostInfo = CostInfoWrapper.GetWrapper(model.CostInfo);

        PlannedTermProduction = PlannedTermProductionWrapper.GetWrapper(model.PlannedTermProduction);

    }

  
    protected override void InitializeCollectionComplexProperties(OfferProduct model)
    {

      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentsConditionWrapper>(model.PaymentsConditions.Select(e => new PaymentsConditionWrapper(e, ExistsWrappers)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);


    }

  }
}
