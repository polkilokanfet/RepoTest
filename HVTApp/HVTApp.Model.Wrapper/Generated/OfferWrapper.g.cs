using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferWrapper : WrapperBase<Offer>
  {
    public OfferWrapper(Offer model) : base(model) { }
    public OfferWrapper(Offer model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


    #region SimpleProperties

    public System.DateTime ValidityDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime ValidityDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(ValidityDate));
    public bool ValidityDateIsChanged => GetIsChanged(nameof(ValidityDate));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public DocumentWrapper Document
	{
		get { return GetComplexProperty<Document, DocumentWrapper>(nameof(Document)); }
		set { SetComplexProperty<Document, DocumentWrapper>(value, nameof(Document)); }
	}


	public ProjectWrapper Project
	{
		get { return GetComplexProperty<Project, ProjectWrapper>(nameof(Project)); }
		set { SetComplexProperty<Project, ProjectWrapper>(value, nameof(Project)); }
	}


	public TenderWrapper Tender
	{
		get { return GetComplexProperty<Tender, TenderWrapper>(nameof(Tender)); }
		set { SetComplexProperty<Tender, TenderWrapper>(value, nameof(Tender)); }
	}


	public PlannedTermProductionWrapper PlannedTermProduction
	{
		get { return GetComplexProperty<PlannedTermProduction, PlannedTermProductionWrapper>(nameof(PlannedTermProduction)); }
		set { SetComplexProperty<PlannedTermProduction, PlannedTermProductionWrapper>(value, nameof(PlannedTermProduction)); }
	}


    #endregion


    #region CollectionComplexProperties

    public ValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Offer model)
    {

        Document = GetWrapper<Document, DocumentWrapper>(model.Document);

        Project = GetWrapper<Project, ProjectWrapper>(model.Project);

        Tender = GetWrapper<Tender, TenderWrapper>(model.Tender);

        PlannedTermProduction = GetWrapper<PlannedTermProduction, PlannedTermProductionWrapper>(model.PlannedTermProduction);

    }

  
    protected override void InitializeCollectionComplexProperties(Offer model)
    {

      if (model.OfferUnits == null) throw new ArgumentException("OfferUnits cannot be null");
      OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(model.OfferUnits.Select(e => new OfferUnitWrapper(e, ExistsWrappers)));
      RegisterCollection(OfferUnits, model.OfferUnits);


    }

  }
}
