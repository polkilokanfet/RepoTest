using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class UnitWrapper : WrapperBase<Unit>
  {
    public UnitWrapper() : base(new Unit(), new Dictionary<IBaseEntity, object>()) { }
    public UnitWrapper(Unit model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public UnitWrapper(Unit model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }



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


	public ProductionsUnitWrapper ProductionsUnit 
    {
        get { return GetComplexProperty<ProductionsUnitWrapper, ProductionsUnit>(Model.ProductionsUnit); }
        set { SetComplexProperty<ProductionsUnitWrapper, ProductionsUnit>(ProductionsUnit, value); }
    }

    public ProductionsUnitWrapper ProductionsUnitOriginalValue { get; private set; }
    public bool ProductionsUnitIsChanged => GetIsChanged(nameof(ProductionsUnit));


	public SalesUnitWrapper SalesUnit 
    {
        get { return GetComplexProperty<SalesUnitWrapper, SalesUnit>(Model.SalesUnit); }
        set { SetComplexProperty<SalesUnitWrapper, SalesUnit>(SalesUnit, value); }
    }

    public SalesUnitWrapper SalesUnitOriginalValue { get; private set; }
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));


	public ShipmentsUnitWrapper ShipmentsUnit 
    {
        get { return GetComplexProperty<ShipmentsUnitWrapper, ShipmentsUnit>(Model.ShipmentsUnit); }
        set { SetComplexProperty<ShipmentsUnitWrapper, ShipmentsUnit>(ShipmentsUnit, value); }
    }

    public ShipmentsUnitWrapper ShipmentsUnitOriginalValue { get; private set; }
    public bool ShipmentsUnitIsChanged => GetIsChanged(nameof(ShipmentsUnit));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<TendersUnitWrapper> TendersUnits { get; private set; }


    public IValidatableChangeTrackingCollection<OffersUnitWrapper> OffersUnits { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Unit model)
    {

        Project = GetWrapper<ProjectWrapper, Project>(model.Project);

        Facility = GetWrapper<FacilityWrapper, Facility>(model.Facility);

        ProductionsUnit = GetWrapper<ProductionsUnitWrapper, ProductionsUnit>(model.ProductionsUnit);

        SalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.SalesUnit);

        ShipmentsUnit = GetWrapper<ShipmentsUnitWrapper, ShipmentsUnit>(model.ShipmentsUnit);

    }

  
    protected override void InitializeCollectionComplexProperties(Unit model)
    {

      if (model.TendersUnits == null) throw new ArgumentException("TendersUnits cannot be null");
      TendersUnits = new ValidatableChangeTrackingCollection<TendersUnitWrapper>(model.TendersUnits.Select(e => GetWrapper<TendersUnitWrapper, TendersUnit>(e)));
      RegisterCollection(TendersUnits, model.TendersUnits);


      if (model.OffersUnits == null) throw new ArgumentException("OffersUnits cannot be null");
      OffersUnits = new ValidatableChangeTrackingCollection<OffersUnitWrapper>(model.OffersUnits.Select(e => GetWrapper<OffersUnitWrapper, OffersUnit>(e)));
      RegisterCollection(OffersUnits, model.OffersUnits);


    }

  }
}
