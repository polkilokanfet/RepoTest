using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferWrapper : WrapperBase<Offer>
  {
    public OfferWrapper() : base(new Offer()) { }
    public OfferWrapper(Offer model) : base(model) { }
    public OfferWrapper(Offer model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public OfferWrapper(Offer model, IDictionary<IBaseEntity, object> dictionary) : base(model, new ExistsWrappers(dictionary)) { }



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
        get { return GetComplexProperty<DocumentWrapper, Document>(Model.Document); }
        set { SetComplexProperty<DocumentWrapper, Document>(Document, value); }
    }

    public DocumentWrapper DocumentOriginalValue { get; private set; }
    public bool DocumentIsChanged => GetIsChanged(nameof(Document));


	public ProjectWrapper Project 
    {
        get { return GetComplexProperty<ProjectWrapper, Project>(Model.Project); }
        set { SetComplexProperty<ProjectWrapper, Project>(Project, value); }
    }

    public ProjectWrapper ProjectOriginalValue { get; private set; }
    public bool ProjectIsChanged => GetIsChanged(nameof(Project));


	public TenderWrapper Tender 
    {
        get { return GetComplexProperty<TenderWrapper, Tender>(Model.Tender); }
        set { SetComplexProperty<TenderWrapper, Tender>(Tender, value); }
    }

    public TenderWrapper TenderOriginalValue { get; private set; }
    public bool TenderIsChanged => GetIsChanged(nameof(Tender));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Offer model)
    {

        Document = GetWrapper<DocumentWrapper, Document>(model.Document);

        Project = GetWrapper<ProjectWrapper, Project>(model.Project);

        Tender = GetWrapper<TenderWrapper, Tender>(model.Tender);

    }

  
    protected override void InitializeCollectionComplexProperties(Offer model)
    {

      if (model.OfferUnits == null) throw new ArgumentException("OfferUnits cannot be null");
      OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(model.OfferUnits.Select(e => GetWrapper<OfferUnitWrapper, OfferUnit>(e)));
      RegisterCollection(OfferUnits, model.OfferUnits);


    }

  }
}
