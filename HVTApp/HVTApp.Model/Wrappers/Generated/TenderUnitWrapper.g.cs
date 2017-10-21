using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TenderUnitWrapper : WrapperBase<TenderUnit>
  {
    public TenderUnitWrapper(TenderUnit model) : base(model) { }



    #region SimpleProperties

    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    public System.DateTime DeliveryDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DeliveryDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(DeliveryDate));
    public bool DeliveryDateIsChanged => GetIsChanged(nameof(DeliveryDate));


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

	private TenderWrapper _fieldTender;
	public TenderWrapper Tender 
    {
        get { return _fieldTender ; }
        set
        {
            SetComplexValue<Tender, TenderWrapper>(_fieldTender, value);
            _fieldTender  = value;
        }
    }

	private CompanyWrapper _fieldProducerWinner;
	public CompanyWrapper ProducerWinner 
    {
        get { return _fieldProducerWinner ; }
        set
        {
            SetComplexValue<Company, CompanyWrapper>(_fieldProducerWinner, value);
            _fieldProducerWinner  = value;
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

		if (Model.Tender != null)
        {
            _fieldTender = new TenderWrapper(Model.Tender);
            RegisterComplex(Tender);
        }

		if (Model.ProducerWinner != null)
        {
            _fieldProducerWinner = new CompanyWrapper(Model.ProducerWinner);
            RegisterComplex(ProducerWinner);
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
