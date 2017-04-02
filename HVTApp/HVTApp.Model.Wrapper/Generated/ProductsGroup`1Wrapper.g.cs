using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductsGroup`1Wrapper : WrapperBase<ProductsGroup`1>
  {
    protected ProductsGroup`1Wrapper(ProductsGroup`1 model) : base(model) { }
    //public ProductsGroup`1Wrapper(ProductsGroup`1 model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static ProductsGroup`1Wrapper GetWrapper(ProductsGroup`1 model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductsGroup`1Wrapper)Repository.ModelWrapperDictionary[model];

		return new ProductsGroup`1Wrapper(model);
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


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<TProductWrapper> Products { get; private set; }


    #endregion


    #region GetProperties

    public System.Double Sum => GetValue<System.Double>(); 


    public System.Double SumWithVat => GetValue<System.Double>(); 


    public System.DateTime OrderInTakeDate => GetValue<System.DateTime>(); 


    #endregion

  
    protected override void InitializeCollectionComplexProperties(ProductsGroup`1 model)
    {

      if (model.Products == null) throw new ArgumentException("Products cannot be null");
      Products = new ValidatableChangeTrackingCollection<TProductWrapper>(model.Products.Select(e => TProductWrapper.GetWrapper(e)));
      RegisterCollection(Products, model.Products);


    }

  }
}
