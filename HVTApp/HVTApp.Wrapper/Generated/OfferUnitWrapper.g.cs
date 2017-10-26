using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
{
	public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
	{
	public OfferUnitWrapper(OfferUnit model) : base(model) { }

	

    #region SimpleProperties

    public System.Guid OfferId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid OfferIdOriginalValue => GetOriginalValue<System.Guid>(nameof(OfferId));
    public bool OfferIdIsChanged => GetIsChanged(nameof(OfferId));


    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    public System.Int32 ProductionTerm
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 ProductionTermOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductionTerm));
    public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private ProjectUnitWrapper _fieldProjectUnit;
	public ProjectUnitWrapper ProjectUnit 
    {
        get { return _fieldProjectUnit ; }
        set
        {
            SetComplexValue<ProjectUnit, ProjectUnitWrapper>(_fieldProjectUnit, value);
            _fieldProjectUnit  = value;
        }
    }

	private ProductWrapper _fieldProduct;
	public ProductWrapper Product 
    {
        get { return _fieldProduct ; }
        set
        {
            SetComplexValue<Product, ProductWrapper>(_fieldProduct, value);
            _fieldProduct  = value;
        }
    }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.ProjectUnit != null)
        {
            _fieldProjectUnit = new ProjectUnitWrapper(Model.ProjectUnit);
            RegisterComplex(ProjectUnit);
        }

		if (Model.Product != null)
        {
            _fieldProduct = new ProductWrapper(Model.Product);
            RegisterComplex(Product);
        }

    }

  
    protected override void InitializeCollectionProperties()
    {

      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => new PaymentConditionWrapper(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);


    }

	}
}
	