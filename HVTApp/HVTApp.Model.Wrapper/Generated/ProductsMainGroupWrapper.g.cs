using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductsMainGroupWrapper : WrapperBase<ProductsMainGroup>
  {
    protected ProductsMainGroupWrapper(ProductsMainGroup model) : base(model) { }

	public static ProductsMainGroupWrapper GetWrapper(ProductsMainGroup model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductsMainGroupWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductsMainGroupWrapper(model);
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

	public ProjectWrapper Project 
    {
        get { return ProjectWrapper.GetWrapper(Model.Project); }
        set
        {
			var oldPropVal = Project;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }


	public FacilityWrapper Facility 
    {
        get { return FacilityWrapper.GetWrapper(Model.Facility); }
        set
        {
			var oldPropVal = Facility;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }


	public SpecificationWrapper Specification 
    {
        get { return SpecificationWrapper.GetWrapper(Model.Specification); }
        set
        {
			var oldPropVal = Specification;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<ProductsOptionalGroupWrapper> ProductsOptionalGroups { get; private set; }


    public ValidatableChangeTrackingCollection<ProductMainWrapper> Products { get; private set; }


    #endregion


    #region GetProperties

    public System.Double Sum => GetValue<System.Double>(); 


    public System.Double SumWithVat => GetValue<System.Double>(); 


    public System.DateTime OrderInTakeDate => GetValue<System.DateTime>(); 


    #endregion

    protected override void InitializeComplexProperties(ProductsMainGroup model)
    {

        Project = ProjectWrapper.GetWrapper(model.Project);

        Facility = FacilityWrapper.GetWrapper(model.Facility);

        Specification = SpecificationWrapper.GetWrapper(model.Specification);

    }

  
    protected override void InitializeCollectionComplexProperties(ProductsMainGroup model)
    {

      if (model.ProductsOptionalGroups == null) throw new ArgumentException("ProductsOptionalGroups cannot be null");
      ProductsOptionalGroups = new ValidatableChangeTrackingCollection<ProductsOptionalGroupWrapper>(model.ProductsOptionalGroups.Select(e => ProductsOptionalGroupWrapper.GetWrapper(e)));
      RegisterCollection(ProductsOptionalGroups, model.ProductsOptionalGroups);


      if (model.Products == null) throw new ArgumentException("Products cannot be null");
      Products = new ValidatableChangeTrackingCollection<ProductMainWrapper>(model.Products.Select(e => ProductMainWrapper.GetWrapper(e)));
      RegisterCollection(Products, model.Products);


    }

  }
}
