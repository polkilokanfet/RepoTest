using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class TenderWrapper : WrapperBase<Tender>
  {
    public TenderWrapper() : base(new Tender(), new Dictionary<IBaseEntity, object>()) { }
    public TenderWrapper(Tender model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public TenderWrapper(Tender model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public TenderWrapper(Tender model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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

    public System.Nullable<System.DateTime> DateNotice
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateNoticeOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateNotice));
    public bool DateNoticeIsChanged => GetIsChanged(nameof(DateNotice));

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

	public CompanyWrapper Winner 
    {
        get { return GetComplexProperty<CompanyWrapper, Company>(Model.Winner); }
        set { SetComplexProperty<CompanyWrapper, Company>(Winner, value); }
    }

    public CompanyWrapper WinnerOriginalValue { get; private set; }
    public bool WinnerIsChanged => GetIsChanged(nameof(Winner));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<CompanyWrapper> Participants { get; private set; }

    public IValidatableChangeTrackingCollection<TenderUnitWrapper> TenderUnits { get; private set; }

    public IValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(Tender model)
    {
        Project = GetWrapper<ProjectWrapper, Project>(model.Project);
        Winner = GetWrapper<CompanyWrapper, Company>(model.Winner);
    }
  
    protected override void InitializeCollectionComplexProperties(Tender model)
    {
      if (model.Participants == null) throw new ArgumentException("Participants cannot be null");
      Participants = new ValidatableChangeTrackingCollection<CompanyWrapper>(model.Participants.Select(e => GetWrapper<CompanyWrapper, Company>(e)));
      RegisterCollection(Participants, model.Participants);

      if (model.TenderUnits == null) throw new ArgumentException("TenderUnits cannot be null");
      TenderUnits = new ValidatableChangeTrackingCollection<TenderUnitWrapper>(model.TenderUnits.Select(e => GetWrapper<TenderUnitWrapper, TenderUnit>(e)));
      RegisterCollection(TenderUnits, model.TenderUnits);

      if (model.Offers == null) throw new ArgumentException("Offers cannot be null");
      Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(model.Offers.Select(e => GetWrapper<OfferWrapper, Offer>(e)));
      RegisterCollection(Offers, model.Offers);

    }
  }
}
