using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferWrapper : WrapperBase<Offer>
  {
    protected OfferWrapper(Offer model) : base(model) { }

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

	public DocumentWrapper Document 
    {
        get { return DocumentWrapper.GetWrapper(Model.Document); }
        set
        {
            UnRegisterComplexProperty(Document);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public ProjectWrapper Project 
    {
        get { return ProjectWrapper.GetWrapper(Model.Project); }
        set
        {
            UnRegisterComplexProperty(Project);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public TenderWrapper Tender 
    {
        get { return TenderWrapper.GetWrapper(Model.Tender); }
        set
        {
            UnRegisterComplexProperty(Tender);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public PlannedTermProductionWrapper PlannedTermProduction 
    {
        get { return PlannedTermProductionWrapper.GetWrapper(Model.PlannedTermProduction); }
        set
        {
            UnRegisterComplexProperty(PlannedTermProduction);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
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
