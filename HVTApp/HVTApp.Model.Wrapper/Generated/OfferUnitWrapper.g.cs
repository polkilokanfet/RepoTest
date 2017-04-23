using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
  {
    public OfferUnitWrapper() : base(new OfferUnit()) { }
    public OfferUnitWrapper(OfferUnit model) : base(model) { }

//	public static OfferUnitWrapper GetWrapper()
//	{
//		return GetWrapper(new OfferUnit());
//	}
//
//	public static OfferUnitWrapper GetWrapper(OfferUnit model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (OfferUnitWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new OfferUnitWrapper(model);
//	}


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
	private OfferWrapper _fieldOffer;
	public OfferWrapper Offer 
    {
        get { return _fieldOffer; }
        set
        {
			SetComplexProperty<OfferWrapper, Offer>(_fieldOffer, value);
			_fieldOffer = value;
        }
    }
    public OfferWrapper OfferOriginalValue { get; private set; }
    public bool OfferIsChanged => GetIsChanged(nameof(Offer));

	private SalesUnitWrapper _fieldParentSalesUnit;
	public SalesUnitWrapper ParentSalesUnit 
    {
        get { return _fieldParentSalesUnit; }
        set
        {
			SetComplexProperty<SalesUnitWrapper, SalesUnit>(_fieldParentSalesUnit, value);
			_fieldParentSalesUnit = value;
        }
    }
    public SalesUnitWrapper ParentSalesUnitOriginalValue { get; private set; }
    public bool ParentSalesUnitIsChanged => GetIsChanged(nameof(ParentSalesUnit));

	private SalesUnitWrapper _fieldChildSalesUnit;
	public SalesUnitWrapper ChildSalesUnit 
    {
        get { return _fieldChildSalesUnit; }
        set
        {
			SetComplexProperty<SalesUnitWrapper, SalesUnit>(_fieldChildSalesUnit, value);
			_fieldChildSalesUnit = value;
        }
    }
    public SalesUnitWrapper ChildSalesUnitOriginalValue { get; private set; }
    public bool ChildSalesUnitIsChanged => GetIsChanged(nameof(ChildSalesUnit));

    #endregion
    protected override void InitializeComplexProperties(OfferUnit model)
    {
        Offer = GetWrapper<OfferWrapper, Offer>(model.Offer);
        ParentSalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.ParentSalesUnit);
        ChildSalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.ChildSalesUnit);
    }
  }
}
