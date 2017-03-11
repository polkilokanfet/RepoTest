using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductsOptionalGroupWrapper : WrapperBase<ProductsOptionalGroup>
  {
    public ProductsOptionalGroupWrapper(ProductsOptionalGroup model) : base(model) { }
    public ProductsOptionalGroupWrapper(ProductsOptionalGroup model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<ProductOptionalWrapper> Products { get; private set; }

    #endregion

    #region GetProperties
    public System.Double Sum => GetValue<System.Double>(); 

    public System.Double SumWithVat => GetValue<System.Double>(); 

    public System.DateTime OrderInTakeDate => GetValue<System.DateTime>(); 

    #endregion
  
    protected override void InitializeCollectionComplexProperties(ProductsOptionalGroup model)
    {
      if (model.Products == null) throw new ArgumentException("Products cannot be null");
      Products = new ValidatableChangeTrackingCollection<ProductOptionalWrapper>(model.Products.Select(e => new ProductOptionalWrapper(e, ExistsWrappers)));
      RegisterCollection(Products, model.Products);

    }
  }
}
