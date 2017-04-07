using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferWrapper : WrapperBase<Offer>
  {
    protected OfferWrapper(Offer model) : base(model) { }

	public static OfferWrapper GetWrapper()
	{
		return GetWrapper(new Offer());
	}

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
			var oldPropVal = Document;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public DocumentWrapper DocumentOriginalValue => DocumentWrapper.GetWrapper(GetOriginalValue<Document>(nameof(Document)));
    public bool DocumentIsChanged => GetIsChanged(nameof(Document));


	public ProjectWrapper Project 
    {
        get { return ProjectWrapper.GetWrapper(Model.Project); }
        set
        {
			var oldPropVal = Project;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProjectWrapper ProjectOriginalValue => ProjectWrapper.GetWrapper(GetOriginalValue<Project>(nameof(Project)));
    public bool ProjectIsChanged => GetIsChanged(nameof(Project));


	public TenderWrapper Tender 
    {
        get { return TenderWrapper.GetWrapper(Model.Tender); }
        set
        {
			var oldPropVal = Tender;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public TenderWrapper TenderOriginalValue => TenderWrapper.GetWrapper(GetOriginalValue<Tender>(nameof(Tender)));
    public bool TenderIsChanged => GetIsChanged(nameof(Tender));


    #endregion

    protected override void InitializeComplexProperties(Offer model)
    {

        Document = DocumentWrapper.GetWrapper(model.Document);

        Project = ProjectWrapper.GetWrapper(model.Project);

        Tender = TenderWrapper.GetWrapper(model.Tender);

    }

  }
}
