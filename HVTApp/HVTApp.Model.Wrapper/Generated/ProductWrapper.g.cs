using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductWrapper : WrapperBase<Product>
  {
    public ProductWrapper() : base(new Product()) { }
    public ProductWrapper(Product model) : base(model) { }

//	public static ProductWrapper GetWrapper()
//	{
//		return GetWrapper(new Product());
//	}
//
//	public static ProductWrapper GetWrapper(Product model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (ProductWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new ProductWrapper(model);
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
	private ProductWrapper _fieldParentProduct;
	public ProductWrapper ParentProduct 
    {
        get { return _fieldParentProduct; }
        set
        {
			SetComplexProperty<ProductWrapper, Product>(_fieldParentProduct, value);
			_fieldParentProduct = value;
        }
    }
    public ProductWrapper ParentProductOriginalValue { get; private set; }
    public bool ParentProductIsChanged => GetIsChanged(nameof(ParentProduct));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<ProductWrapper> ChildProducts { get; private set; }

    public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }

    public IValidatableChangeTrackingCollection<SumOnDateWrapper> Prices { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(Product model)
    {
        ParentProduct = GetWrapper<ProductWrapper, Product>(model.ParentProduct);
    }
  
    protected override void InitializeCollectionComplexProperties(Product model)
    {
      if (model.ChildProducts == null) throw new ArgumentException("ChildProducts cannot be null");
      ChildProducts = new ValidatableChangeTrackingCollection<ProductWrapper>(model.ChildProducts.Select(e => GetWrapper<ProductWrapper, Product>(e)));
      RegisterCollection(ChildProducts, model.ChildProducts);

      if (model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
      Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(model.Parameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(Parameters, model.Parameters);

      if (model.Prices == null) throw new ArgumentException("Prices cannot be null");
      Prices = new ValidatableChangeTrackingCollection<SumOnDateWrapper>(model.Prices.Select(e => GetWrapper<SumOnDateWrapper, SumOnDate>(e)));
      RegisterCollection(Prices, model.Prices);

    }
  }
}
