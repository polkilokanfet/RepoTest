using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class OfferWrapper : WrapperBase<Offer>
  {
    private OfferWrapper() : base(new Offer()) { }
    private OfferWrapper(Offer model) : base(model) { }



    #region SimpleProperties

    public System.DateTime ValidityDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime ValidityDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(ValidityDate));
    public bool ValidityDateIsChanged => GetIsChanged(nameof(ValidityDate));


    public System.Double Vat
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
    public bool VatIsChanged => GetIsChanged(nameof(Vat));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
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

    public override void InitializeComplexProperties()
    {

        Document = GetWrapper<DocumentWrapper, Document>(Model.Document);

        Project = GetWrapper<ProjectWrapper, Project>(Model.Project);

        Tender = GetWrapper<TenderWrapper, Tender>(Model.Tender);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.OfferUnits == null) throw new ArgumentException("OfferUnits cannot be null");
      OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(Model.OfferUnits.Select(e => GetWrapper<OfferUnitWrapper, OfferUnit>(e)));
      RegisterCollection(OfferUnits, Model.OfferUnits);


    }

  }
}
