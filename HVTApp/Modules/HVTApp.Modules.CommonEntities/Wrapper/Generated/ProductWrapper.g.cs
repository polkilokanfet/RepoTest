using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class ProductWrapper : WrapperBase<Product>
	{
	public ProductWrapper(Product model) : base(model) { }

	
    #region SimpleProperties
    public System.String Designation
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
    public bool DesignationIsChanged => GetIsChanged(nameof(Designation));

    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	public PartWrapper Part 
    {
        get { return GetWrapper<PartWrapper>(); }
        set { SetComplexValue<Part, PartWrapper>(Part, value); }
    }

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<ProductWrapper> DependentProducts { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<PartWrapper>(nameof(Part), Model.Part == null ? null : new PartWrapper(Model.Part));

    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.DependentProducts == null) throw new ArgumentException("DependentProducts cannot be null");
      DependentProducts = new ValidatableChangeTrackingCollection<ProductWrapper>(Model.DependentProducts.Select(e => new ProductWrapper(e)));
      RegisterCollection(DependentProducts, Model.DependentProducts);

    }
	}
}
	