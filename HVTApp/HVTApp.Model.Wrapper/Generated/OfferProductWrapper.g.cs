using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferProductWrapper : WrapperBase<OfferProduct>
  {
    protected OfferProductWrapper(OfferProduct model) : base(model) { }

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
        get { return ProductMainWrapper.GetWrapper(Model.ProductMain); }
        set
        {
            UnRegisterComplexProperty(ProductMain);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public OfferUnitWrapper OfferUnit 
    {
        get { return OfferUnitWrapper.GetWrapper(Model.OfferUnit); }
        set
        {
            UnRegisterComplexProperty(OfferUnit);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public CostInfoWrapper CostInfo 
    {
        get { return CostInfoWrapper.GetWrapper(Model.CostInfo); }
        set
        {
            UnRegisterComplexProperty(CostInfo);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public PlannedTermProductionWrapper PlannedTermProduction 
    {
        get { return PlannedTermProductionWrapper.GetWrapper(Model.PlannedTermProduction); }
        set
        {
            UnRegisterComplexProperty(PlannedTermProduction);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion


    #region CollectionProperties

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
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentsConditionWrapper>(model.PaymentsConditions.Select(e => PaymentsConditionWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);


    }

  }
}
