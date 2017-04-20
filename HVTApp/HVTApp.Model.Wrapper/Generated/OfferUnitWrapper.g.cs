using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
  {
    protected OfferUnitWrapper(OfferUnit model) : base(model) { }

	public static OfferUnitWrapper GetWrapper()
	{
		return GetWrapper(new OfferUnit());
	}

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
    public OfferWrapper OfferOriginalValue => OfferWrapper.GetWrapper(GetOriginalValue<Offer>(nameof(Offer)));
    public bool OfferIsChanged => GetIsChanged(nameof(Offer));


	public SalesUnitWrapper ParentSalesUnit 
    {
        get { return SalesUnitWrapper.GetWrapper(Model.ParentSalesUnit); }
        set
        {
			var oldPropVal = ParentSalesUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SalesUnitWrapper ParentSalesUnitOriginalValue => SalesUnitWrapper.GetWrapper(GetOriginalValue<SalesUnit>(nameof(ParentSalesUnit)));
    public bool ParentSalesUnitIsChanged => GetIsChanged(nameof(ParentSalesUnit));


	public SalesUnitWrapper ChildSalesUnit 
    {
        get { return SalesUnitWrapper.GetWrapper(Model.ChildSalesUnit); }
        set
        {
			var oldPropVal = ChildSalesUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SalesUnitWrapper ChildSalesUnitOriginalValue => SalesUnitWrapper.GetWrapper(GetOriginalValue<SalesUnit>(nameof(ChildSalesUnit)));
    public bool ChildSalesUnitIsChanged => GetIsChanged(nameof(ChildSalesUnit));


    #endregion

    protected override void InitializeComplexProperties(OfferUnit model)
    {

        Offer = OfferWrapper.GetWrapper(model.Offer);

        ParentSalesUnit = SalesUnitWrapper.GetWrapper(model.ParentSalesUnit);

        ChildSalesUnit = SalesUnitWrapper.GetWrapper(model.ChildSalesUnit);

    }

  }
}
