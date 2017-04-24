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
    public OfferUnitWrapper(OfferUnit model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }



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
        get { return GetComplexProperty<OfferWrapper, Offer>(Model.Offer); }
        set { SetComplexProperty<OfferWrapper, Offer>(Offer, value); }
    }

    public OfferWrapper OfferOriginalValue { get; private set; }
    public bool OfferIsChanged => GetIsChanged(nameof(Offer));


	public SalesUnitWrapper ParentSalesUnit 
    {
        get { return GetComplexProperty<SalesUnitWrapper, SalesUnit>(Model.ParentSalesUnit); }
        set { SetComplexProperty<SalesUnitWrapper, SalesUnit>(ParentSalesUnit, value); }
    }

    public SalesUnitWrapper ParentSalesUnitOriginalValue { get; private set; }
    public bool ParentSalesUnitIsChanged => GetIsChanged(nameof(ParentSalesUnit));


	public SalesUnitWrapper ChildSalesUnit 
    {
        get { return GetComplexProperty<SalesUnitWrapper, SalesUnit>(Model.ChildSalesUnit); }
        set { SetComplexProperty<SalesUnitWrapper, SalesUnit>(ChildSalesUnit, value); }
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
