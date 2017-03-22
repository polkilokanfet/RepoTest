using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferWrapper : WrapperBase<Offer>
  {
    public OfferWrapper(Offer model) : base(model) { }
    public OfferWrapper(Offer model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


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

		if (model.Document != null)
		{
            Document = GetWrapper<Document, DocumentWrapper>(model.Document);
			//if (ExistsWrappers.ContainsKey(model.Document))
			//{
			//	Document = (DocumentWrapper)ExistsWrappers[model.Document];
			//}
			//else
			//{
			//	Document = new DocumentWrapper(model.Document, ExistsWrappers);
			//	RegisterComplexProperty(Document);
			//}
		}


		if (model.Project != null)
		{
            Project = GetWrapper<Project, ProjectWrapper>(model.Project);
			//if (ExistsWrappers.ContainsKey(model.Project))
			//{
			//	Project = (ProjectWrapper)ExistsWrappers[model.Project];
			//}
			//else
			//{
			//	Project = new ProjectWrapper(model.Project, ExistsWrappers);
			//	RegisterComplexProperty(Project);
			//}
		}


		if (model.Tender != null)
		{
            Tender = GetWrapper<Tender, TenderWrapper>(model.Tender);
			//if (ExistsWrappers.ContainsKey(model.Tender))
			//{
			//	Tender = (TenderWrapper)ExistsWrappers[model.Tender];
			//}
			//else
			//{
			//	Tender = new TenderWrapper(model.Tender, ExistsWrappers);
			//	RegisterComplexProperty(Tender);
			//}
		}


		if (model.PlannedTermProduction != null)
		{
            PlannedTermProduction = GetWrapper<PlannedTermProduction, PlannedTermProductionWrapper>(model.PlannedTermProduction);
			//if (ExistsWrappers.ContainsKey(model.PlannedTermProduction))
			//{
			//	PlannedTermProduction = (PlannedTermProductionWrapper)ExistsWrappers[model.PlannedTermProduction];
			//}
			//else
			//{
			//	PlannedTermProduction = new PlannedTermProductionWrapper(model.PlannedTermProduction, ExistsWrappers);
			//	RegisterComplexProperty(PlannedTermProduction);
			//}
		}


    }

  
    protected override void InitializeCollectionComplexProperties(Offer model)
    {

      if (model.OfferUnits == null) throw new ArgumentException("OfferUnits cannot be null");
      OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(model.OfferUnits.Select(e => new OfferUnitWrapper(e, ExistsWrappers)));
      RegisterCollection(OfferUnits, model.OfferUnits);


    }

  }
}
