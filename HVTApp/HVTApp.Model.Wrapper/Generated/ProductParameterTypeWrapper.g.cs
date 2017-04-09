using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductParameterTypeWrapper : WrapperBase<ProductParameterType>
  {
    protected ProductParameterTypeWrapper(ProductParameterType model) : base(model) { }

	public static ProductParameterTypeWrapper GetWrapper()
	{
		return GetWrapper(new ProductParameterType());
	}

	public static ProductParameterTypeWrapper GetWrapper(ProductParameterType model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductParameterTypeWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductParameterTypeWrapper(model);
	}



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion

  }
}
