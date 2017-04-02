using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductsMainGroupWrapper : WrapperBase<ProductsMainGroup>
  {
    protected ProductsMainGroupWrapper(ProductsMainGroup model) : base(model) { }
    //public ProductsMainGroupWrapper(ProductsMainGroup model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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

	private ProjectWrapper _fieldProject;
	public ProjectWrapper Project 
    {
        get { return _fieldProject; }
        set
        {
            if (Equals(_fieldProject, value))
                return;

            UnRegisterComplexProperty(_fieldProject);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldProject = value;
        }
    }


	private FacilityWrapper _fieldFacility;
	public FacilityWrapper Facility 
    {
        get { return _fieldFacility; }
        set
        {
            if (Equals(_fieldFacility, value))
                return;

            UnRegisterComplexProperty(_fieldFacility);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldFacility = value;
        }
    }


	private SpecificationWrapper _fieldSpecification;
	public SpecificationWrapper Specification 
    {
        get { return _fieldSpecification; }
        set
        {
            if (Equals(_fieldSpecification, value))
                return;

            UnRegisterComplexProperty(_fieldSpecification);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldSpecification = value;
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
