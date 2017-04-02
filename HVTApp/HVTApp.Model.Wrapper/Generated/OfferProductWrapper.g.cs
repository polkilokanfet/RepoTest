using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferProductWrapper : WrapperBase<OfferProduct>
  {
    protected OfferProductWrapper(OfferProduct model) : base(model) { }
    //public OfferProductWrapper(OfferProduct model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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

	private ProductMainWrapper _fieldProductMain;
	public ProductMainWrapper ProductMain 
    {
        get { return _fieldProductMain; }
        set
        {
            if (Equals(_fieldProductMain, value))
                return;

            UnRegisterComplexProperty(_fieldProductMain);

            _fieldProductMain = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private OfferUnitWrapper _fieldOfferUnit;
	public OfferUnitWrapper OfferUnit 
    {
        get { return _fieldOfferUnit; }
        set
        {
            if (Equals(_fieldOfferUnit, value))
                return;

            UnRegisterComplexProperty(_fieldOfferUnit);

            _fieldOfferUnit = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private CostInfoWrapper _fieldCostInfo;
	public CostInfoWrapper CostInfo 
    {
        get { return _fieldCostInfo; }
        set
        {
            if (Equals(_fieldCostInfo, value))
                return;

            UnRegisterComplexProperty(_fieldCostInfo);

            _fieldCostInfo = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private PlannedTermProductionWrapper _fieldPlannedTermProduction;
	public PlannedTermProductionWrapper PlannedTermProduction 
    {
        get { return _fieldPlannedTermProduction; }
        set
        {
            if (Equals(_fieldPlannedTermProduction, value))
                return;

            UnRegisterComplexProperty(_fieldPlannedTermProduction);

            _fieldPlannedTermProduction = value;
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
