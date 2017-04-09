using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProjectWrapper : WrapperBase<Project>
  {
    protected ProjectWrapper(Project model) : base(model) { }

	public static ProjectWrapper GetWrapper()
	{
		return GetWrapper(new Project());
	}

	public static ProjectWrapper GetWrapper(Project model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProjectWrapper)Repository.ModelWrapperDictionary[model];

		return new ProjectWrapper(model);
	}



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

	public UserWrapper Manager 
    {
        get { return UserWrapper.GetWrapper(Model.Manager); }
        set
        {
			var oldPropVal = Manager;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public UserWrapper ManagerOriginalValue => UserWrapper.GetWrapper(GetOriginalValue<User>(nameof(Manager)));
    public bool ManagerIsChanged => GetIsChanged(nameof(Manager));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesProductUnits { get; private set; }


    public IValidatableChangeTrackingCollection<TenderWrapper> Tenders { get; private set; }


    public IValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Project model)
    {

        Manager = UserWrapper.GetWrapper(model.Manager);

    }

  
    protected override void InitializeCollectionComplexProperties(Project model)
    {

      if (model.SalesProductUnits == null) throw new ArgumentException("SalesProductUnits cannot be null");
      SalesProductUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(model.SalesProductUnits.Select(e => SalesUnitWrapper.GetWrapper(e)));
      RegisterCollection(SalesProductUnits, model.SalesProductUnits);


      if (model.Tenders == null) throw new ArgumentException("Tenders cannot be null");
      Tenders = new ValidatableChangeTrackingCollection<TenderWrapper>(model.Tenders.Select(e => TenderWrapper.GetWrapper(e)));
      RegisterCollection(Tenders, model.Tenders);


      if (model.Offers == null) throw new ArgumentException("Offers cannot be null");
      Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(model.Offers.Select(e => OfferWrapper.GetWrapper(e)));
      RegisterCollection(Offers, model.Offers);


    }

  }
}
