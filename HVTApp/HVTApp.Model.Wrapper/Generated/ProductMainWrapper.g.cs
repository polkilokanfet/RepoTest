using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductMainWrapper : WrapperBase<ProductMain>
  {
    protected ProductMainWrapper(ProductMain model) : base(model) { }

	public static ProductMainWrapper GetWrapper(ProductMain model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductMainWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductMainWrapper(model);
	}



    #region SimpleProperties

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private ProductsMainGroupWrapper _fieldProductsMainGroup;
	public ProductsMainGroupWrapper ProductsMainGroup 
    {
        get { return _fieldProductsMainGroup; }
        set
        {
            if (Equals(_fieldProductsMainGroup, value))
                return;

            UnRegisterComplexProperty(_fieldProductsMainGroup);

            _fieldProductsMainGroup = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private EquipmentWrapper _fieldEquipment;
	public EquipmentWrapper Equipment 
    {
        get { return _fieldEquipment; }
        set
        {
            if (Equals(_fieldEquipment, value))
                return;

            UnRegisterComplexProperty(_fieldEquipment);

            _fieldEquipment = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private TenderInfoWrapper _fieldTenderInfo;
	public TenderInfoWrapper TenderInfo 
    {
        get { return _fieldTenderInfo; }
        set
        {
            if (Equals(_fieldTenderInfo, value))
                return;

            UnRegisterComplexProperty(_fieldTenderInfo);

            _fieldTenderInfo = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private OrderInfoWrapper _fieldOrderInfo;
	public OrderInfoWrapper OrderInfo 
    {
        get { return _fieldOrderInfo; }
        set
        {
            if (Equals(_fieldOrderInfo, value))
                return;

            UnRegisterComplexProperty(_fieldOrderInfo);

            _fieldOrderInfo = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private DateInfoWrapper _fieldDateInfo;
	public DateInfoWrapper DateInfo 
    {
        get { return _fieldDateInfo; }
        set
        {
            if (Equals(_fieldDateInfo, value))
                return;

            UnRegisterComplexProperty(_fieldDateInfo);

            _fieldDateInfo = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private TermsInfoWrapper _fieldTermsInfo;
	public TermsInfoWrapper TermsInfo 
    {
        get { return _fieldTermsInfo; }
        set
        {
            if (Equals(_fieldTermsInfo, value))
                return;

            UnRegisterComplexProperty(_fieldTermsInfo);

            _fieldTermsInfo = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private CostInfoWrapper _fieldCostInfo;
	public CostInfoWrapper CostInfo 
    {
        get { return _fieldCostInfo; }
        set
        {
            if (Equals(_fieldCostInfo, value))
                return;

            UnRegisterComplexProperty(_fieldCostInfo);

            _fieldCostInfo = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private PaymentsInfoWrapper _fieldPaymentsInfo;
	public PaymentsInfoWrapper PaymentsInfo 
    {
        get { return _fieldPaymentsInfo; }
        set
        {
            if (Equals(_fieldPaymentsInfo, value))
                return;

            UnRegisterComplexProperty(_fieldPaymentsInfo);

            _fieldPaymentsInfo = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<ProductsOptionalGroupWrapper> ProductsOptionalGroups { get; private set; }


    public ValidatableChangeTrackingCollection<OfferProductWrapper> OfferProducts { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(ProductMain model)
    {

        ProductsMainGroup = ProductsMainGroupWrapper.GetWrapper(model.ProductsMainGroup);

        Equipment = EquipmentWrapper.GetWrapper(model.Equipment);

        TenderInfo = TenderInfoWrapper.GetWrapper(model.TenderInfo);

        OrderInfo = OrderInfoWrapper.GetWrapper(model.OrderInfo);

        DateInfo = DateInfoWrapper.GetWrapper(model.DateInfo);

        TermsInfo = TermsInfoWrapper.GetWrapper(model.TermsInfo);

        CostInfo = CostInfoWrapper.GetWrapper(model.CostInfo);

        PaymentsInfo = PaymentsInfoWrapper.GetWrapper(model.PaymentsInfo);

    }

  
    protected override void InitializeCollectionComplexProperties(ProductMain model)
    {

      if (model.ProductsOptionalGroups == null) throw new ArgumentException("ProductsOptionalGroups cannot be null");
      ProductsOptionalGroups = new ValidatableChangeTrackingCollection<ProductsOptionalGroupWrapper>(model.ProductsOptionalGroups.Select(e => ProductsOptionalGroupWrapper.GetWrapper(e)));
      RegisterCollection(ProductsOptionalGroups, model.ProductsOptionalGroups);


      if (model.OfferProducts == null) throw new ArgumentException("OfferProducts cannot be null");
      OfferProducts = new ValidatableChangeTrackingCollection<OfferProductWrapper>(model.OfferProducts.Select(e => OfferProductWrapper.GetWrapper(e)));
      RegisterCollection(OfferProducts, model.OfferProducts);


    }

  }
}
