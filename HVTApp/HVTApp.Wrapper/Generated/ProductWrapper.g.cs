using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
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

	private PartWrapper _fieldPart;
	public PartWrapper Part 
    {
        get { return _fieldPart ; }
        set
        {
            SetComplexValue<Part, PartWrapper>(_fieldPart, value);
            _fieldPart  = value;
        }
    }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProductWrapper> DependentProducts { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Part != null)
        {
            _fieldPart = new PartWrapper(Model.Part);
            RegisterComplex(Part);
        }

    }

  
    protected override void InitializeCollectionProperties()
    {

      if (Model.DependentProducts == null) throw new ArgumentException("DependentProducts cannot be null");
      DependentProducts = new ValidatableChangeTrackingCollection<ProductWrapper>(Model.DependentProducts.Select(e => new ProductWrapper(e)));
      RegisterCollection(DependentProducts, Model.DependentProducts);


    }

	}
}
	