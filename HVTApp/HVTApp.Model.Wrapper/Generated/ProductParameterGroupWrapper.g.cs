using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductParameterGroupWrapper : WrapperBase<ProductParameterGroup>
  {
    protected ProductParameterGroupWrapper(ProductParameterGroup model) : base(model) { }

	public static ProductParameterGroupWrapper GetWrapper()
	{
		return GetWrapper(new ProductParameterGroup());
	}

	public static ProductParameterGroupWrapper GetWrapper(ProductParameterGroup model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductParameterGroupWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductParameterGroupWrapper(model);
	}



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.Boolean IsOntyChoice
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean IsOntyChoiceOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsOntyChoice));
    public bool IsOntyChoiceIsChanged => GetIsChanged(nameof(IsOntyChoice));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

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

    protected override void InitializeComplexProperties(ProductParameterGroup model)
    {

        Measure = ProductParameterMeasureWrapper.GetWrapper(model.Measure);

    }

  }
}
