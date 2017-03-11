using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductsMainGroupWrapper : WrapperBase<ProductsMainGroup>
  {
    public ProductsMainGroupWrapper(ProductsMainGroup model) : base(model) { }
    public ProductsMainGroupWrapper(ProductsMainGroup model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
		get { return GetComplexProperty<Project, ProjectWrapper>(nameof(Project)); }
		set { SetComplexProperty<Project, ProjectWrapper>(value, this.Project, nameof(Project)); }
	}

	public FacilityWrapper Facility
	{
		get { return GetComplexProperty<Facility, FacilityWrapper>(nameof(Facility)); }
		set { SetComplexProperty<Facility, FacilityWrapper>(value, this.Facility, nameof(Facility)); }
	}

	public SpecificationWrapper Specification
	{
		get { return GetComplexProperty<Specification, SpecificationWrapper>(nameof(Specification)); }
		set { SetComplexProperty<Specification, SpecificationWrapper>(value, this.Specification, nameof(Specification)); }
	}

    #endregion

    #region CollectionComplexProperties
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
		if (model.Project != null)
		{
			if (ExistsWrappers.ContainsKey(model.Project))
			{
				Project = (ProjectWrapper)ExistsWrappers[model.Project];
			}
			else
			{
				Project = new ProjectWrapper(model.Project, ExistsWrappers);
				//ExistsWrappers.Add(model.Project, new ProjectWrapper(model.Project, ExistsWrappers));
				RegisterComplexProperty(Project);
			}
		}

		if (model.Facility != null)
		{
			if (ExistsWrappers.ContainsKey(model.Facility))
			{
				Facility = (FacilityWrapper)ExistsWrappers[model.Facility];
			}
			else
			{
				Facility = new FacilityWrapper(model.Facility, ExistsWrappers);
				//ExistsWrappers.Add(model.Facility, new FacilityWrapper(model.Facility, ExistsWrappers));
				RegisterComplexProperty(Facility);
			}
		}

		if (model.Specification != null)
		{
			if (ExistsWrappers.ContainsKey(model.Specification))
			{
				Specification = (SpecificationWrapper)ExistsWrappers[model.Specification];
			}
			else
			{
				Specification = new SpecificationWrapper(model.Specification, ExistsWrappers);
				//ExistsWrappers.Add(model.Specification, new SpecificationWrapper(model.Specification, ExistsWrappers));
				RegisterComplexProperty(Specification);
			}
		}

    }
  
    protected override void InitializeCollectionComplexProperties(ProductsMainGroup model)
    {
      if (model.ProductsOptionalGroups == null) throw new ArgumentException("ProductsOptionalGroups cannot be null");
      ProductsOptionalGroups = new ValidatableChangeTrackingCollection<ProductsOptionalGroupWrapper>(model.ProductsOptionalGroups.Select(e => new ProductsOptionalGroupWrapper(e, ExistsWrappers)));
      RegisterCollection(ProductsOptionalGroups, model.ProductsOptionalGroups);

      if (model.Products == null) throw new ArgumentException("Products cannot be null");
      Products = new ValidatableChangeTrackingCollection<ProductMainWrapper>(model.Products.Select(e => new ProductMainWrapper(e, ExistsWrappers)));
      RegisterCollection(Products, model.Products);

    }
  }
}
