using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TenderWrapper : WrapperBase<Tender>
  {
    protected TenderWrapper(Tender model) : base(model) { }

	public static TenderWrapper GetWrapper()
	{
		return GetWrapper(new Tender());
	}

	public static TenderWrapper GetWrapper(Tender model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TenderWrapper)Repository.ModelWrapperDictionary[model];

		return new TenderWrapper(model);
	}



    #region SimpleProperties

    public HVTApp.Model.TenderType Type
    {
      get { return GetValue<HVTApp.Model.TenderType>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.TenderType TypeOriginalValue => GetOriginalValue<HVTApp.Model.TenderType>(nameof(Type));
    public bool TypeIsChanged => GetIsChanged(nameof(Type));


    public System.Double Sum
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
    public bool SumIsChanged => GetIsChanged(nameof(Sum));


    public System.DateTime DateOpen
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOpenOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateOpen));
    public bool DateOpenIsChanged => GetIsChanged(nameof(DateOpen));


    public System.DateTime DateClose
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateCloseOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateClose));
    public bool DateCloseIsChanged => GetIsChanged(nameof(DateClose));


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
        get { return ProjectWrapper.GetWrapper(Model.Project); }
        set
        {
			var oldPropVal = Project;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProjectWrapper ProjectOriginalValue => ProjectWrapper.GetWrapper(GetOriginalValue<Project>(nameof(Project)));
    public bool ProjectIsChanged => GetIsChanged(nameof(Project));


	public CompanyWrapper Winner 
    {
        get { return CompanyWrapper.GetWrapper(Model.Winner); }
        set
        {
			var oldPropVal = Winner;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CompanyWrapper WinnerOriginalValue => CompanyWrapper.GetWrapper(GetOriginalValue<Company>(nameof(Winner)));
    public bool WinnerIsChanged => GetIsChanged(nameof(Winner));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<CompanyWrapper> Participants { get; private set; }


    public IValidatableChangeTrackingCollection<TenderInfoWrapper> TenderUnits { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Tender model)
    {

        Project = ProjectWrapper.GetWrapper(model.Project);

        Winner = CompanyWrapper.GetWrapper(model.Winner);

    }

  
    protected override void InitializeCollectionComplexProperties(Tender model)
    {

      if (model.Participants == null) throw new ArgumentException("Participants cannot be null");
      Participants = new ValidatableChangeTrackingCollection<CompanyWrapper>(model.Participants.Select(e => CompanyWrapper.GetWrapper(e)));
      RegisterCollection(Participants, model.Participants);


      if (model.TenderUnits == null) throw new ArgumentException("TenderUnits cannot be null");
      TenderUnits = new ValidatableChangeTrackingCollection<TenderInfoWrapper>(model.TenderUnits.Select(e => TenderInfoWrapper.GetWrapper(e)));
      RegisterCollection(TenderUnits, model.TenderUnits);


    }

  }
}
