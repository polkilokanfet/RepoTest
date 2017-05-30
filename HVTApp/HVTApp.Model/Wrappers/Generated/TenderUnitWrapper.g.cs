using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TenderUnitWrapper : WrapperBase<TenderUnit>
  {
    public TenderUnitWrapper() : base(new TenderUnit(), new Dictionary<IBaseEntity, object>()) { }
    public TenderUnitWrapper(TenderUnit model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public TenderUnitWrapper(TenderUnit model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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
	public TenderWrapper Tender 
    {
        get { return GetComplexProperty<TenderWrapper, Tender>(Model.Tender); }
        set { SetComplexProperty<TenderWrapper, Tender>(Tender, value); }
    }

    public TenderWrapper TenderOriginalValue { get; private set; }
    public bool TenderIsChanged => GetIsChanged(nameof(Tender));

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
    protected override void InitializeComplexProperties(TenderUnit model)
    {
        Tender = GetWrapper<TenderWrapper, Tender>(model.Tender);
        ParentSalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.ParentSalesUnit);
        ChildSalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.ChildSalesUnit);
    }
  }
}
