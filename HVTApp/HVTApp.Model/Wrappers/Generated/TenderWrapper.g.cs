using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TenderWrapper : WrapperBase<Tender>
  {
    public TenderWrapper(Tender model) : base(model) { }



    #region SimpleProperties

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


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public TenderTypeWrapper Type { get; set; }

	public ProjectWrapper Project { get; set; }

	public CompanyWrapper Winner { get; set; }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<CompanyWrapper> Participants { get; private set; }


    public IValidatableChangeTrackingCollection<TenderUnitWrapper> TenderUnits { get; private set; }


    public IValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Type = new TenderTypeWrapper(Model.Type);
		RegisterComplex(Type);

        Project = new ProjectWrapper(Model.Project);
		RegisterComplex(Project);

        Winner = new CompanyWrapper(Model.Winner);
		RegisterComplex(Winner);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Participants == null) throw new ArgumentException("Participants cannot be null");
      Participants = new ValidatableChangeTrackingCollection<CompanyWrapper>(Model.Participants.Select(e => new CompanyWrapper(e)));
      RegisterCollection(Participants, Model.Participants);


      if (Model.TenderUnits == null) throw new ArgumentException("TenderUnits cannot be null");
      TenderUnits = new ValidatableChangeTrackingCollection<TenderUnitWrapper>(Model.TenderUnits.Select(e => new TenderUnitWrapper(e)));
      RegisterCollection(TenderUnits, Model.TenderUnits);


      if (Model.Offers == null) throw new ArgumentException("Offers cannot be null");
      Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(Model.Offers.Select(e => new OfferWrapper(e)));
      RegisterCollection(Offers, Model.Offers);


    }

  }
}
