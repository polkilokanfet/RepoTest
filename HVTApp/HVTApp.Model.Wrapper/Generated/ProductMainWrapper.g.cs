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

	public ProductsMainGroupWrapper ProductsMainGroup 
    {
        get { return ProductsMainGroupWrapper.GetWrapper(Model.ProductsMainGroup); }
        set
        {
            UnRegisterComplexProperty(ProductsMainGroup);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public EquipmentWrapper Equipment 
    {
        get { return EquipmentWrapper.GetWrapper(Model.Equipment); }
        set
        {
            UnRegisterComplexProperty(Equipment);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public TenderInfoWrapper TenderInfo 
    {
        get { return TenderInfoWrapper.GetWrapper(Model.TenderInfo); }
        set
        {
            UnRegisterComplexProperty(TenderInfo);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public OrderInfoWrapper OrderInfo 
    {
        get { return OrderInfoWrapper.GetWrapper(Model.OrderInfo); }
        set
        {
            UnRegisterComplexProperty(OrderInfo);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public DateInfoWrapper DateInfo 
    {
        get { return DateInfoWrapper.GetWrapper(Model.DateInfo); }
        set
        {
            UnRegisterComplexProperty(DateInfo);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public TermsInfoWrapper TermsInfo 
    {
        get { return TermsInfoWrapper.GetWrapper(Model.TermsInfo); }
        set
        {
            UnRegisterComplexProperty(TermsInfo);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public CostInfoWrapper CostInfo 
    {
        get { return CostInfoWrapper.GetWrapper(Model.CostInfo); }
        set
        {
            UnRegisterComplexProperty(CostInfo);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public PaymentsInfoWrapper PaymentsInfo 
    {
        get { return PaymentsInfoWrapper.GetWrapper(Model.PaymentsInfo); }
        set
        {
            UnRegisterComplexProperty(PaymentsInfo);
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
