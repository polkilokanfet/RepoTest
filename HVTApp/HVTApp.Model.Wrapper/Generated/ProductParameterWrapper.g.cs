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

	public ProductParameterGroupWrapper Group 
    {
        get { return ProductParameterGroupWrapper.GetWrapper(Model.Group); }
        set
        {
			var oldPropVal = Group;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductParameterGroupWrapper GroupOriginalValue => ProductParameterGroupWrapper.GetWrapper(GetOriginalValue<ProductParameterGroup>(nameof(Group)));
    public bool GroupIsChanged => GetIsChanged(nameof(Group));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProductParameterSetWrapper> ProductParameterSets { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(ProductParameter model)
    {

        Group = ProductParameterGroupWrapper.GetWrapper(model.Group);

    }

  
    protected override void InitializeCollectionComplexProperties(ProductParameter model)
    {

      if (model.ProductParameterSets == null) throw new ArgumentException("ProductParameterSets cannot be null");
      ProductParameterSets = new ValidatableChangeTrackingCollection<ProductParameterSetWrapper>(model.ProductParameterSets.Select(e => ProductParameterSetWrapper.GetWrapper(e)));
      RegisterCollection(ProductParameterSets, model.ProductParameterSets);


    }

  }
}
