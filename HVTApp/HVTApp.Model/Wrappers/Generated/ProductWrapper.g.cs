using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProductWrapper : WrapperBase<Product>
  {
    public ProductWrapper() : base(new Product(), new Dictionary<IBaseEntity, object>()) { }
    public ProductWrapper(Product model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public ProductWrapper(Product model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }



    #region SimpleProperties

    public System.String Designation
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
    public bool DesignationIsChanged => GetIsChanged(nameof(Designation));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ProductItemWrapper ProductItem 
    {
        get { return GetComplexProperty<ProductItemWrapper, ProductItem>(Model.ProductItem); }
        set { SetComplexProperty<ProductItemWrapper, ProductItem>(ProductItem, value); }
    }

    public ProductItemWrapper ProductItemOriginalValue { get; private set; }
    public bool ProductItemIsChanged => GetIsChanged(nameof(ProductItem));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProductWrapper> ChildProducts { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Product model)
    {

        ProductItem = GetWrapper<ProductItemWrapper, ProductItem>(model.ProductItem);

    }

  
    protected override void InitializeCollectionComplexProperties(Product model)
    {

      if (model.ChildProducts == null) throw new ArgumentException("ChildProducts cannot be null");
      ChildProducts = new ValidatableChangeTrackingCollection<ProductWrapper>(model.ChildProducts.Select(e => GetWrapper<ProductWrapper, Product>(e)));
      RegisterCollection(ChildProducts, model.ChildProducts);


    }

  }
}
