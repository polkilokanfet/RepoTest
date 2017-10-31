using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class OfferWrapper : WrapperBase<Offer>
	{
	public OfferWrapper(Offer model) : base(model) { }

	
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
	private DocumentWrapper _fieldDocument;
	public DocumentWrapper Document 
    {
        get { return _fieldDocument ; }
        set
        {
            SetComplexValue<Document, DocumentWrapper>(_fieldDocument, value);
            _fieldDocument  = value;
        }
    }
    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
		if (Model.Document != null)
        {
            _fieldDocument = new DocumentWrapper(Model.Document);
            RegisterComplex(Document);
        }
    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.OfferUnits == null) throw new ArgumentException("OfferUnits cannot be null");
      OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(Model.OfferUnits.Select(e => new OfferUnitWrapper(e)));
      RegisterCollection(OfferUnits, Model.OfferUnits);

    }
	}
}
	