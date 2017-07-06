using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TenderWrapper : WrapperBase<Tender>
  {
    private TenderWrapper() : base(new Tender()) { }
    private TenderWrapper(Tender model) : base(model) { }



    #region SimpleProperties

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

	public TenderTypeWrapper Type 
    {
        get { return GetComplexProperty<TenderTypeWrapper, TenderType>(Model.Type); }
        set { SetComplexProperty<TenderTypeWrapper, TenderType>(Type, value); }
    }

    public TenderTypeWrapper TypeOriginalValue { get; private set; }
    public bool TypeIsChanged => GetIsChanged(nameof(Type));


	public ProjectWrapper Project 
    {
        get { return GetComplexProperty<ProjectWrapper, Project>(Model.Project); }
        set { SetComplexProperty<ProjectWrapper, Project>(Project, value); }
    }

    public ProjectWrapper ProjectOriginalValue { get; private set; }
    public bool ProjectIsChanged => GetIsChanged(nameof(Project));


	public CostWrapper Sum 
    {
        get { return GetComplexProperty<CostWrapper, Cost>(Model.Sum); }
        set { SetComplexProperty<CostWrapper, Cost>(Sum, value); }
    }

    public CostWrapper SumOriginalValue { get; private set; }
    public bool SumIsChanged => GetIsChanged(nameof(Sum));


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


    public IValidatableChangeTrackingCollection<ProductTenderUnitWrapper> ProductTenderUnits { get; private set; }


    public IValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Type = GetWrapper<TenderTypeWrapper, TenderType>(Model.Type);

        Project = GetWrapper<ProjectWrapper, Project>(Model.Project);

        Sum = GetWrapper<CostWrapper, Cost>(Model.Sum);

        Winner = GetWrapper<CompanyWrapper, Company>(Model.Winner);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Participants == null) throw new ArgumentException("Participants cannot be null");
      Participants = new ValidatableChangeTrackingCollection<CompanyWrapper>(Model.Participants.Select(e => GetWrapper<CompanyWrapper, Company>(e)));
      RegisterCollection(Participants, Model.Participants);


      if (Model.TenderUnits == null) throw new ArgumentException("TenderUnits cannot be null");
      ProductTenderUnits = new ValidatableChangeTrackingCollection<ProductTenderUnitWrapper>(Model.TenderUnits.Select(e => GetWrapper<ProductTenderUnitWrapper, TenderUnit>(e)));
      RegisterCollection(ProductTenderUnits, Model.TenderUnits);


      if (Model.Offers == null) throw new ArgumentException("Offers cannot be null");
      Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(Model.Offers.Select(e => GetWrapper<OfferWrapper, Offer>(e)));
      RegisterCollection(Offers, Model.Offers);


    }

  }
}
