using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductWrapper : WrapperBase<Product>
  {
    protected ProductWrapper(Product model) : base(model) { }

	public static ProductWrapper GetWrapper()
	{
		return GetWrapper(new Product());
	}

	public static ProductWrapper GetWrapper(Product model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductWrapper(model);
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


    #region ComplexProperties

	public EquipmentWrapper Equipment 
    {
        get { return EquipmentWrapper.GetWrapper(Model.Equipment); }
        set
        {
			var oldPropVal = Equipment;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public EquipmentWrapper EquipmentOriginalValue => EquipmentWrapper.GetWrapper(GetOriginalValue<Equipment>(nameof(Equipment)));
    public bool EquipmentIsChanged => GetIsChanged(nameof(Equipment));


	public ProductWrapper ParentProduct 
    {
        get { return ProductWrapper.GetWrapper(Model.ParentProduct); }
        set
        {
			var oldPropVal = ParentProduct;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductWrapper ParentProductOriginalValue => ProductWrapper.GetWrapper(GetOriginalValue<Product>(nameof(ParentProduct)));
    public bool ParentProductIsChanged => GetIsChanged(nameof(ParentProduct));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProductWrapper> ChildProducts { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Product model)
    {

        Equipment = EquipmentWrapper.GetWrapper(model.Equipment);

        ParentProduct = ProductWrapper.GetWrapper(model.ParentProduct);

    }

  
    protected override void InitializeCollectionComplexProperties(Product model)
    {

      if (model.ChildProducts == null) throw new ArgumentException("ChildProducts cannot be null");
      ChildProducts = new ValidatableChangeTrackingCollection<ProductWrapper>(model.ChildProducts.Select(e => ProductWrapper.GetWrapper(e)));
      RegisterCollection(ChildProducts, model.ChildProducts);


    }

  }
}
