using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentWrapper : WrapperBase<Payment>
  {
    protected PaymentWrapper(Payment model) : base(model) { }

	public static PaymentWrapper GetWrapper()
	{
		return GetWrapper(new Payment());
	}

	public static PaymentWrapper GetWrapper(Payment model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (PaymentWrapper)Repository.ModelWrapperDictionary[model];

		return new PaymentWrapper(model);
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


    #endregion

    protected override void InitializeComplexProperties(Payment model)
    {

        SumAndVat = SumAndVatWrapper.GetWrapper(model.SumAndVat);

        SalesUnit = SalesUnitWrapper.GetWrapper(model.SalesUnit);

    }

  }
}
