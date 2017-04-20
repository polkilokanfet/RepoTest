using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentPlanWrapper : WrapperBase<PaymentPlan>
  {
    protected PaymentPlanWrapper(PaymentPlan model) : base(model) { }

	public static PaymentPlanWrapper GetWrapper()
	{
		return GetWrapper(new PaymentPlan());
	}

	public static PaymentPlanWrapper GetWrapper(PaymentPlan model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (PaymentPlanWrapper)Repository.ModelWrapperDictionary[model];

		return new PaymentPlanWrapper(model);
	}



    #region SimpleProperties

    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));


    public System.String Comment
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
    public bool CommentIsChanged => GetIsChanged(nameof(Comment));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public SalesUnitWrapper SalesUnit 
    {
        get { return SalesUnitWrapper.GetWrapper(Model.SalesUnit); }
        set
        {
			var oldPropVal = SalesUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SalesUnitWrapper SalesUnitOriginalValue => SalesUnitWrapper.GetWrapper(GetOriginalValue<SalesUnit>(nameof(SalesUnit)));
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));


	public SumAndVatWrapper SumAndVat 
    {
        get { return SumAndVatWrapper.GetWrapper(Model.SumAndVat); }
        set
        {
			var oldPropVal = SumAndVat;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SumAndVatWrapper SumAndVatOriginalValue => SumAndVatWrapper.GetWrapper(GetOriginalValue<SumAndVat>(nameof(SumAndVat)));
    public bool SumAndVatIsChanged => GetIsChanged(nameof(SumAndVat));


    #endregion

    protected override void InitializeComplexProperties(PaymentPlan model)
    {

        SalesUnit = SalesUnitWrapper.GetWrapper(model.SalesUnit);

        SumAndVat = SumAndVatWrapper.GetWrapper(model.SumAndVat);

    }

  }
}
