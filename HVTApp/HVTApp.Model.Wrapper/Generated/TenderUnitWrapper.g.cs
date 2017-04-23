using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TenderUnitWrapper : WrapperBase<TenderUnit>
  {
    public TenderUnitWrapper() : base(new TenderUnit()) { }
    public TenderUnitWrapper(TenderUnit model) : base(model) { }

//	public static TenderUnitWrapper GetWrapper()
//	{
//		return GetWrapper(new TenderUnit());
//	}
//
//	public static TenderUnitWrapper GetWrapper(TenderUnit model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ExistsWrappers.ContainsKey(model))
//			return (TenderUnitWrapper)Repository.ExistsWrappers[model];
//
//		return new TenderUnitWrapper(model);
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
	private TenderWrapper _fieldTender;
	public TenderWrapper Tender 
    {
        get { return _fieldTender; }
        set
        {
			SetComplexProperty<TenderWrapper, Tender>(_fieldTender, value);
			_fieldTender = value;
        }
    }
    public TenderWrapper TenderOriginalValue { get; private set; }
    public bool TenderIsChanged => GetIsChanged(nameof(Tender));

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
    protected override void InitializeComplexProperties(TenderUnit model)
    {
        Tender = GetWrapper<TenderWrapper, Tender>(model.Tender);
        ParentSalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.ParentSalesUnit);
        ChildSalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.ChildSalesUnit);
    }
  }
}
