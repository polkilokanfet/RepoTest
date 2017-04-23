using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferWrapper : WrapperBase<Offer>
  {
    public OfferWrapper() : base(new Offer()) { }
    public OfferWrapper(Offer model) : base(model) { }

//	public static OfferWrapper GetWrapper()
//	{
//		return GetWrapper(new Offer());
//	}
//
//	public static OfferWrapper GetWrapper(Offer model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ExistsWrappers.ContainsKey(model))
//			return (OfferWrapper)Repository.ExistsWrappers[model];
//
//		return new OfferWrapper(model);
//	}


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
	private DocumentWrapper _fieldDocument;
	public DocumentWrapper Document 
    {
        get { return _fieldDocument; }
        set
        {
			SetComplexProperty<DocumentWrapper, Document>(_fieldDocument, value);
			_fieldDocument = value;
        }
    }
    public DocumentWrapper DocumentOriginalValue { get; private set; }
    public bool DocumentIsChanged => GetIsChanged(nameof(Document));

	private ProjectWrapper _fieldProject;
	public ProjectWrapper Project 
    {
        get { return _fieldProject; }
        set
        {
			SetComplexProperty<ProjectWrapper, Project>(_fieldProject, value);
			_fieldProject = value;
        }
    }
    public ProjectWrapper ProjectOriginalValue { get; private set; }
    public bool ProjectIsChanged => GetIsChanged(nameof(Project));

	private TenderWrapper _fieldTender;
	public TenderWrapper Tender 
    {
        get { return _fieldTender; }
        set
        {
			SetComplexProperty<TenderWrapper, Tender>(_fieldTender, value);
			_fieldTender = value;
        }
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
