using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class UnitWrapper : WrapperBase<ProductComplexUnit>
  {
    private UnitWrapper() : base(new ProductComplexUnit()) { }
    private UnitWrapper(ProductComplexUnit model) : base(model) { }



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


	public SalesUnitWrapper SalesUnit 
    {
        get { return GetComplexProperty<SalesUnitWrapper, ProductSalesUnit>(Model.ProductSalesUnit); }
        set { SetComplexProperty<SalesUnitWrapper, ProductSalesUnit>(SalesUnit, value); }
    }

    public SalesUnitWrapper SalesUnitOriginalValue { get; private set; }
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));


	public ProductionsUnitWrapper ProductionsUnit 
    {
        get { return GetComplexProperty<ProductionsUnitWrapper, ProductProductionUnit>(Model.ProductProductionUnit); }
        set { SetComplexProperty<ProductionsUnitWrapper, ProductProductionUnit>(ProductionsUnit, value); }
    }

    public ProductionsUnitWrapper ProductionsUnitOriginalValue { get; private set; }
    public bool ProductionsUnitIsChanged => GetIsChanged(nameof(ProductionsUnit));


	public ShipmentsUnitWrapper ShipmentsUnit 
    {
        get { return GetComplexProperty<ShipmentsUnitWrapper, ProductShipmentUnit>(Model.ProductShipmentUnit); }
        set { SetComplexProperty<ShipmentsUnitWrapper, ProductShipmentUnit>(ShipmentsUnit, value); }
    }

    public ShipmentsUnitWrapper ShipmentsUnitOriginalValue { get; private set; }
    public bool ShipmentsUnitIsChanged => GetIsChanged(nameof(ShipmentsUnit));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<TendersUnitWrapper> TendersUnits { get; private set; }


    public IValidatableChangeTrackingCollection<OffersUnitWrapper> OffersUnits { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Project = GetWrapper<ProjectWrapper, Project>(Model.Project);

        Facility = GetWrapper<FacilityWrapper, Facility>(Model.Facility);

        SalesUnit = GetWrapper<SalesUnitWrapper, ProductSalesUnit>(Model.ProductSalesUnit);

        ProductionsUnit = GetWrapper<ProductionsUnitWrapper, ProductProductionUnit>(Model.ProductProductionUnit);

        ShipmentsUnit = GetWrapper<ShipmentsUnitWrapper, ProductShipmentUnit>(Model.ProductShipmentUnit);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.TendersUnits == null) throw new ArgumentException("TendersUnits cannot be null");
      TendersUnits = new ValidatableChangeTrackingCollection<TendersUnitWrapper>(Model.TendersUnits.Select(e => GetWrapper<TendersUnitWrapper, ProductTenderUnit>(e)));
      RegisterCollection(TendersUnits, Model.TendersUnits);


      if (Model.OffersUnits == null) throw new ArgumentException("OffersUnits cannot be null");
      OffersUnits = new ValidatableChangeTrackingCollection<OffersUnitWrapper>(Model.OffersUnits.Select(e => GetWrapper<OffersUnitWrapper, ProductOfferUnit>(e)));
      RegisterCollection(OffersUnits, Model.OffersUnits);


    }

  }
}
