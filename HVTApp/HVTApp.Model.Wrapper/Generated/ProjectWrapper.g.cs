using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProjectWrapper : WrapperBase<Project>
  {
    public ProjectWrapper() : base(new Project()) { }
    public ProjectWrapper(Project model) : base(model) { }

//	public static ProjectWrapper GetWrapper()
//	{
//		return GetWrapper(new Project());
//	}
//
//	public static ProjectWrapper GetWrapper(Project model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (ProjectWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new ProjectWrapper(model);
//	}


    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.DateTime EstimatedDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime EstimatedDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(EstimatedDate));
    public bool EstimatedDateIsChanged => GetIsChanged(nameof(EstimatedDate));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private UserWrapper _fieldManager;
	public UserWrapper Manager 
    {
        get { return _fieldManager; }
        set
        {
			SetComplexProperty<UserWrapper, User>(_fieldManager, value);
			_fieldManager = value;
        }
    }
    public UserWrapper ManagerOriginalValue { get; private set; }
    public bool ManagerIsChanged => GetIsChanged(nameof(Manager));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; private set; }

    public IValidatableChangeTrackingCollection<TenderWrapper> Tenders { get; private set; }

    public IValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(Project model)
    {
        Manager = GetWrapper<UserWrapper, User>(model.Manager);
    }
  
    protected override void InitializeCollectionComplexProperties(Project model)
    {
      if (model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
      SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(model.SalesUnits.Select(e => GetWrapper<SalesUnitWrapper, SalesUnit>(e)));
      RegisterCollection(SalesUnits, model.SalesUnits);

      if (model.Tenders == null) throw new ArgumentException("Tenders cannot be null");
      Tenders = new ValidatableChangeTrackingCollection<TenderWrapper>(model.Tenders.Select(e => GetWrapper<TenderWrapper, Tender>(e)));
      RegisterCollection(Tenders, model.Tenders);

      if (model.Offers == null) throw new ArgumentException("Offers cannot be null");
      Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(model.Offers.Select(e => GetWrapper<OfferWrapper, Offer>(e)));
      RegisterCollection(Offers, model.Offers);

    }
  }
}
