using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProductWrapper : WrapperBase<Product>
  {
    private ProductWrapper(IGetWrapper getWrapper) : base(new Product(), getWrapper) { }
    private ProductWrapper(Product model, IGetWrapper getWrapper) : base(model, getWrapper) { }



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
        get { return GetComplexProperty<PartWrapper, Part>(Model.Part); }
        set { SetComplexProperty<PartWrapper, Part>(Part, value); }
    }

    public PartWrapper PartOriginalValue { get; private set; }
    public bool PartIsChanged => GetIsChanged(nameof(Part));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProductWrapper> DependentProducts { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Part = GetWrapper<PartWrapper, Part>(Model.Part);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.DependentProducts == null) throw new ArgumentException("DependentProducts cannot be null");
      DependentProducts = new ValidatableChangeTrackingCollection<ProductWrapper>(Model.DependentProducts.Select(e => GetWrapper<ProductWrapper, Product>(e)));
      RegisterCollection(DependentProducts, Model.DependentProducts);


    }

  }
}
