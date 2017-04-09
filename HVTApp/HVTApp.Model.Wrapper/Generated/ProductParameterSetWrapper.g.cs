using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductParameterSetWrapper : WrapperBase<ProductParameterSet>
  {
    protected ProductParameterSetWrapper(ProductParameterSet model) : base(model) { }

	public static ProductParameterSetWrapper GetWrapper()
	{
		return GetWrapper(new ProductParameterSet());
	}

	public static ProductParameterSetWrapper GetWrapper(ProductParameterSet model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductParameterSetWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductParameterSetWrapper(model);
	}



    #region SimpleProperties

    public System.Boolean IsRequired
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean IsRequiredOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsRequired));
    public bool IsRequiredIsChanged => GetIsChanged(nameof(IsRequired));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ProductParameterWrapper Parameter 
    {
        get { return ProductParameterWrapper.GetWrapper(Model.Parameter); }
        set
        {
			var oldPropVal = Parameter;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductParameterWrapper ParameterOriginalValue => ProductParameterWrapper.GetWrapper(GetOriginalValue<ProductParameter>(nameof(Parameter)));
    public bool ParameterIsChanged => GetIsChanged(nameof(Parameter));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProductParameterWrapper> RequiredParentParameters { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(ProductParameterSet model)
    {

        Parameter = ProductParameterWrapper.GetWrapper(model.Parameter);

    }

  
    protected override void InitializeCollectionComplexProperties(ProductParameterSet model)
    {

      if (model.RequiredParentParameters == null) throw new ArgumentException("RequiredParentParameters cannot be null");
      RequiredParentParameters = new ValidatableChangeTrackingCollection<ProductParameterWrapper>(model.RequiredParentParameters.Select(e => ProductParameterWrapper.GetWrapper(e)));
      RegisterCollection(RequiredParentParameters, model.RequiredParentParameters);


    }

  }
}
