using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferWrapper : WrapperBase<Offer>
  {
    protected OfferWrapper(Offer model) : base(model) { }
    //public OfferWrapper(Offer model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static OfferWrapper GetWrapper(Offer model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (OfferWrapper)Repository.ModelWrapperDictionary[model];

		return new OfferWrapper(model);
	}



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
            if (Equals(_fieldDocument, value))
                return;

            UnRegisterComplexProperty(_fieldDocument);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldDocument = value;
        }
    }


	private ProjectWrapper _fieldProject;
	public ProjectWrapper Project 
    {
        get { return _fieldProject; }
        set
        {
            if (Equals(_fieldProject, value))
                return;

            UnRegisterComplexProperty(_fieldProject);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldProject = value;
        }
    }


	private TenderWrapper _fieldTender;
	public TenderWrapper Tender 
    {
        get { return _fieldTender; }
        set
        {
            if (Equals(_fieldTender, value))
                return;

            UnRegisterComplexProperty(_fieldTender);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldTender = value;
        }
    }


	private PlannedTermProductionWrapper _fieldPlannedTermProduction;
	public PlannedTermProductionWrapper PlannedTermProduction 
    {
        get { return _fieldPlannedTermProduction; }
        set
        {
            if (Equals(_fieldPlannedTermProduction, value))
                return;

            UnRegisterComplexProperty(_fieldPlannedTermProduction);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldPlannedTermProduction = value;
        }
    }


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Offer model)
    {

        Document = DocumentWrapper.GetWrapper(model.Document);

        Project = ProjectWrapper.GetWrapper(model.Project);

        Tender = TenderWrapper.GetWrapper(model.Tender);

        PlannedTermProduction = PlannedTermProductionWrapper.GetWrapper(model.PlannedTermProduction);

    }

  
    protected override void InitializeCollectionComplexProperties(Offer model)
    {

      if (model.OfferUnits == null) throw new ArgumentException("OfferUnits cannot be null");
      OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(model.OfferUnits.Select(e => OfferUnitWrapper.GetWrapper(e)));
      RegisterCollection(OfferUnits, model.OfferUnits);


    }

  }
}
