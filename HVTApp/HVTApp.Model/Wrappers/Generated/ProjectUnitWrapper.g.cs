using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProjectUnitWrapper : WrapperBase<ProjectUnit>
  {
    private ProjectUnitWrapper(IGetWrapper getWrapper) : base(new ProjectUnit(), getWrapper) { }
    private ProjectUnitWrapper(ProjectUnit model, IGetWrapper getWrapper) : base(model, getWrapper) { }



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


	public EquipmentWrapper Equipment 
    {
        get { return GetComplexProperty<EquipmentWrapper, Equipment>(Model.Equipment); }
        set { SetComplexProperty<EquipmentWrapper, Equipment>(Equipment, value); }
    }

    public EquipmentWrapper EquipmentOriginalValue { get; private set; }
    public bool EquipmentIsChanged => GetIsChanged(nameof(Equipment));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<TenderUnitWrapper> TenderUnits { get; private set; }


    public IValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Project = GetWrapper<ProjectWrapper, Project>(Model.Project);

        Facility = GetWrapper<FacilityWrapper, Facility>(Model.Facility);

        Equipment = GetWrapper<EquipmentWrapper, Equipment>(Model.Equipment);

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
