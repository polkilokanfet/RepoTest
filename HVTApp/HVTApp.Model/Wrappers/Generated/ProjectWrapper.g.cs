using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProjectWrapper : WrapperBase<Project>
  {
    private ProjectWrapper() : base(new Project()) { }
    private ProjectWrapper(Project model) : base(model) { }



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public UserWrapper Manager 
    {
        get { return GetComplexProperty<UserWrapper, User>(Model.Manager); }
        set { SetComplexProperty<UserWrapper, User>(Manager, value); }
    }

    public UserWrapper ManagerOriginalValue { get; private set; }
    public bool ManagerIsChanged => GetIsChanged(nameof(Manager));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProjectUnitWrapper> ProjectUnits { get; private set; }


    public IValidatableChangeTrackingCollection<TenderWrapper> Tenders { get; private set; }


    public IValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Manager = GetWrapper<UserWrapper, User>(Model.Manager);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.ProjectUnits == null) throw new ArgumentException("ProjectUnits cannot be null");
      ProjectUnits = new ValidatableChangeTrackingCollection<ProjectUnitWrapper>(Model.ProjectUnits.Select(e => GetWrapper<ProjectUnitWrapper, ProjectUnit>(e)));
      RegisterCollection(ProjectUnits, Model.ProjectUnits);


      if (Model.Tenders == null) throw new ArgumentException("Tenders cannot be null");
      Tenders = new ValidatableChangeTrackingCollection<TenderWrapper>(Model.Tenders.Select(e => GetWrapper<TenderWrapper, Tender>(e)));
      RegisterCollection(Tenders, Model.Tenders);


      if (Model.Offers == null) throw new ArgumentException("Offers cannot be null");
      Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(Model.Offers.Select(e => GetWrapper<OfferWrapper, Offer>(e)));
      RegisterCollection(Offers, Model.Offers);


    }

  }
}
