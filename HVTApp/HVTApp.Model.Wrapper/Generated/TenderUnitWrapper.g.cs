using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TenderUnitWrapper : WrapperBase<TenderUnit>
  {
    protected TenderUnitWrapper(TenderUnit model) : base(model) { }

	public static TenderUnitWrapper GetWrapper()
	{
		return GetWrapper(new TenderUnit());
	}

	public static TenderUnitWrapper GetWrapper(TenderUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TenderUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new TenderUnitWrapper(model);
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

	public TenderWrapper Tender 
    {
        get { return TenderWrapper.GetWrapper(Model.Tender); }
        set
        {
			var oldPropVal = Tender;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public TenderWrapper TenderOriginalValue => TenderWrapper.GetWrapper(GetOriginalValue<Tender>(nameof(Tender)));
    public bool TenderIsChanged => GetIsChanged(nameof(Tender));


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

    protected override void InitializeComplexProperties(TenderUnit model)
    {

        Tender = TenderWrapper.GetWrapper(model.Tender);

        ParentSalesUnit = SalesUnitWrapper.GetWrapper(model.ParentSalesUnit);

        ChildSalesUnit = SalesUnitWrapper.GetWrapper(model.ChildSalesUnit);

    }

  }
}
