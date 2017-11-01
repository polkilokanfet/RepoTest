using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class ProjectWrapper : WrapperBase<Project>
	{
	public ProjectWrapper(Project model) : base(model) { }

	
    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	public UserWrapper Manager 
    {
        get { return GetWrapper<UserWrapper>(); }
        set { SetComplexValue<User, UserWrapper>(Manager, value); }
    }

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<ProjectUnitWrapper> ProjectUnits { get; private set; }

    public IValidatableChangeTrackingCollection<TenderWrapper> Tenders { get; private set; }

    public IValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<UserWrapper>(nameof(Manager), Model.Manager == null ? null : new UserWrapper(Model.Manager));

    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.ProjectUnits == null) throw new ArgumentException("ProjectUnits cannot be null");
      ProjectUnits = new ValidatableChangeTrackingCollection<ProjectUnitWrapper>(Model.ProjectUnits.Select(e => new ProjectUnitWrapper(e)));
      RegisterCollection(ProjectUnits, Model.ProjectUnits);

      if (Model.Tenders == null) throw new ArgumentException("Tenders cannot be null");
      Tenders = new ValidatableChangeTrackingCollection<TenderWrapper>(Model.Tenders.Select(e => new TenderWrapper(e)));
      RegisterCollection(Tenders, Model.Tenders);

      if (Model.Offers == null) throw new ArgumentException("Offers cannot be null");
      Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(Model.Offers.Select(e => new OfferWrapper(e)));
      RegisterCollection(Offers, Model.Offers);

    }
	}
}
	