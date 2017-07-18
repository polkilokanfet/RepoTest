using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProjectUnitWrapper : WrapperBase<ProjectUnit>
  {
    private ProjectUnitWrapper() : base(new ProjectUnit()) { }
    private ProjectUnitWrapper(ProjectUnit model) : base(model) { }



    #region SimpleProperties

    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    public System.DateTime RequiredDeliveryDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime RequiredDeliveryDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(RequiredDeliveryDate));
    public bool RequiredDeliveryDateIsChanged => GetIsChanged(nameof(RequiredDeliveryDate));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ProjectWrapper Project 
    {
        get { return GetComplexProperty<ProjectWrapper, Project>(Model.Project); }
        set { SetComplexProperty<ProjectWrapper, Project>(Project, value); }
    }

    public ProjectWrapper ProjectOriginalValue { get; private set; }
    public bool ProjectIsChanged => GetIsChanged(nameof(Project));


	public FacilityWrapper Facility 
    {
        get { return GetComplexProperty<FacilityWrapper, Facility>(Model.Facility); }
        set { SetComplexProperty<FacilityWrapper, Facility>(Facility, value); }
    }

    public FacilityWrapper FacilityOriginalValue { get; private set; }
    public bool FacilityIsChanged => GetIsChanged(nameof(Facility));


	public ProductWrapper Product 
    {
        get { return GetComplexProperty<ProductWrapper, Product>(Model.Product); }
        set { SetComplexProperty<ProductWrapper, Product>(Product, value); }
    }

    public ProductWrapper ProductOriginalValue { get; private set; }
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<TenderUnitWrapper> TenderUnits { get; private set; }


    public IValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Project = GetWrapper<ProjectWrapper, Project>(Model.Project);

        Facility = GetWrapper<FacilityWrapper, Facility>(Model.Facility);

        Product = GetWrapper<ProductWrapper, Product>(Model.Product);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.TenderUnits == null) throw new ArgumentException("TenderUnits cannot be null");
      TenderUnits = new ValidatableChangeTrackingCollection<TenderUnitWrapper>(Model.TenderUnits.Select(e => GetWrapper<TenderUnitWrapper, TenderUnit>(e)));
      RegisterCollection(TenderUnits, Model.TenderUnits);


      if (Model.OfferUnits == null) throw new ArgumentException("OfferUnits cannot be null");
      OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(Model.OfferUnits.Select(e => GetWrapper<OfferUnitWrapper, OfferUnit>(e)));
      RegisterCollection(OfferUnits, Model.OfferUnits);


    }

  }
}
