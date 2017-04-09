using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductParameterWrapper : WrapperBase<ProductParameter>
  {
    protected ProductParameterWrapper(ProductParameter model) : base(model) { }

	public static ProductParameterWrapper GetWrapper()
	{
		return GetWrapper(new ProductParameter());
	}

	public static ProductParameterWrapper GetWrapper(ProductParameter model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductParameterWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductParameterWrapper(model);
	}



    #region SimpleProperties

    public System.String Value
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String ValueOriginalValue => GetOriginalValue<System.String>(nameof(Value));
    public bool ValueIsChanged => GetIsChanged(nameof(Value));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ProductParameterTypeWrapper Type 
    {
        get { return ProductParameterTypeWrapper.GetWrapper(Model.Type); }
        set
        {
			var oldPropVal = Type;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductParameterTypeWrapper TypeOriginalValue => ProductParameterTypeWrapper.GetWrapper(GetOriginalValue<ProductParameterType>(nameof(Type)));
    public bool TypeIsChanged => GetIsChanged(nameof(Type));


	public ProductParameterMeasureWrapper Measure 
    {
        get { return ProductParameterMeasureWrapper.GetWrapper(Model.Measure); }
        set
        {
			var oldPropVal = Measure;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductParameterMeasureWrapper MeasureOriginalValue => ProductParameterMeasureWrapper.GetWrapper(GetOriginalValue<ProductParameterMeasure>(nameof(Measure)));
    public bool MeasureIsChanged => GetIsChanged(nameof(Measure));


    #endregion

    protected override void InitializeComplexProperties(ProductParameter model)
    {

        Type = ProductParameterTypeWrapper.GetWrapper(model.Type);

        Measure = ProductParameterMeasureWrapper.GetWrapper(model.Measure);

    }

  }
}
