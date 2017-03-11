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
    public DocumentWrapper Document { get; private set; }

    public ProjectWrapper Project { get; private set; }

    public TenderWrapper Tender { get; private set; }

    public PlannedTermProductionWrapper PlannedTermProduction { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(Offer model)
    {
      if (model.Document == null) throw new ArgumentException("Document cannot be null");
      if (ExistsWrappers.ContainsKey(model.Document))
      {
          Document = (DocumentWrapper)ExistsWrappers[model.Document];
      }
      else
      {
          Document = new DocumentWrapper(model.Document, ExistsWrappers);
          RegisterComplexProperty(Document);
      }

      if (model.Project == null) throw new ArgumentException("Project cannot be null");
      if (ExistsWrappers.ContainsKey(model.Project))
      {
          Project = (ProjectWrapper)ExistsWrappers[model.Project];
      }
      else
      {
          Project = new ProjectWrapper(model.Project, ExistsWrappers);
          RegisterComplexProperty(Project);
      }

      if (model.Tender == null) throw new ArgumentException("Tender cannot be null");
      if (ExistsWrappers.ContainsKey(model.Tender))
      {
          Tender = (TenderWrapper)ExistsWrappers[model.Tender];
      }
      else
      {
          Tender = new TenderWrapper(model.Tender, ExistsWrappers);
          RegisterComplexProperty(Tender);
      }

      if (model.PlannedTermProduction == null) throw new ArgumentException("PlannedTermProduction cannot be null");
      if (ExistsWrappers.ContainsKey(model.PlannedTermProduction))
      {
          PlannedTermProduction = (PlannedTermProductionWrapper)ExistsWrappers[model.PlannedTermProduction];
      }
      else
      {
          PlannedTermProduction = new PlannedTermProductionWrapper(model.PlannedTermProduction, ExistsWrappers);
          RegisterComplexProperty(PlannedTermProduction);
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
