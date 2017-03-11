using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductBaseWrapper : WrapperBase<ProductBase>
  {
    public ProductBaseWrapper(ProductBase model) : base(model) { }
    public ProductBaseWrapper(ProductBase model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public ProductsMainGroupWrapper ProductsMainGroup { get; private set; }

    public EquipmentWrapper Equipment { get; private set; }

    public TenderInfoWrapper TenderInfo { get; private set; }

    public OrderInfoWrapper OrderInfo { get; private set; }

    public DateInfoWrapper DateInfo { get; private set; }

    public TermsInfoWrapper TermsInfo { get; private set; }

    public CostInfoWrapper CostInfo { get; private set; }

    public PaymentsInfoWrapper PaymentsInfo { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<OfferProductWrapper> OfferProducts { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(ProductBase model)
    {
      if (model.ProductsMainGroup == null) throw new ArgumentException("ProductsMainGroup cannot be null");
      if (ExistsWrappers.ContainsKey(model.ProductsMainGroup))
      {
          ProductsMainGroup = (ProductsMainGroupWrapper)ExistsWrappers[model.ProductsMainGroup];
      }
      else
      {
          ProductsMainGroup = new ProductsMainGroupWrapper(model.ProductsMainGroup, ExistsWrappers);
          RegisterComplexProperty(ProductsMainGroup);
      }

      if (model.Equipment == null) throw new ArgumentException("Equipment cannot be null");
      if (ExistsWrappers.ContainsKey(model.Equipment))
      {
          Equipment = (EquipmentWrapper)ExistsWrappers[model.Equipment];
      }
      else
      {
          Equipment = new EquipmentWrapper(model.Equipment, ExistsWrappers);
          RegisterComplexProperty(Equipment);
      }

      if (model.TenderInfo == null) throw new ArgumentException("TenderInfo cannot be null");
      if (ExistsWrappers.ContainsKey(model.TenderInfo))
      {
          TenderInfo = (TenderInfoWrapper)ExistsWrappers[model.TenderInfo];
      }
      else
      {
          TenderInfo = new TenderInfoWrapper(model.TenderInfo, ExistsWrappers);
          RegisterComplexProperty(TenderInfo);
      }

      if (model.OrderInfo == null) throw new ArgumentException("OrderInfo cannot be null");
      if (ExistsWrappers.ContainsKey(model.OrderInfo))
      {
          OrderInfo = (OrderInfoWrapper)ExistsWrappers[model.OrderInfo];
      }
      else
      {
          OrderInfo = new OrderInfoWrapper(model.OrderInfo, ExistsWrappers);
          RegisterComplexProperty(OrderInfo);
      }

      if (model.DateInfo == null) throw new ArgumentException("DateInfo cannot be null");
      if (ExistsWrappers.ContainsKey(model.DateInfo))
      {
          DateInfo = (DateInfoWrapper)ExistsWrappers[model.DateInfo];
      }
      else
      {
          DateInfo = new DateInfoWrapper(model.DateInfo, ExistsWrappers);
          RegisterComplexProperty(DateInfo);
      }

      if (model.TermsInfo == null) throw new ArgumentException("TermsInfo cannot be null");
      if (ExistsWrappers.ContainsKey(model.TermsInfo))
      {
          TermsInfo = (TermsInfoWrapper)ExistsWrappers[model.TermsInfo];
      }
      else
      {
          TermsInfo = new TermsInfoWrapper(model.TermsInfo, ExistsWrappers);
          RegisterComplexProperty(TermsInfo);
      }

      if (model.CostInfo == null) throw new ArgumentException("CostInfo cannot be null");
      if (ExistsWrappers.ContainsKey(model.CostInfo))
      {
          CostInfo = (CostInfoWrapper)ExistsWrappers[model.CostInfo];
      }
      else
      {
          CostInfo = new CostInfoWrapper(model.CostInfo, ExistsWrappers);
          RegisterComplexProperty(CostInfo);
      }

      if (model.PaymentsInfo == null) throw new ArgumentException("PaymentsInfo cannot be null");
      if (ExistsWrappers.ContainsKey(model.PaymentsInfo))
      {
          PaymentsInfo = (PaymentsInfoWrapper)ExistsWrappers[model.PaymentsInfo];
      }
      else
      {
          PaymentsInfo = new PaymentsInfoWrapper(model.PaymentsInfo, ExistsWrappers);
          RegisterComplexProperty(PaymentsInfo);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(ProductBase model)
    {
      if (model.OfferProducts == null) throw new ArgumentException("OfferProducts cannot be null");
      OfferProducts = new ValidatableChangeTrackingCollection<OfferProductWrapper>(model.OfferProducts.Select(e => new OfferProductWrapper(e, ExistsWrappers)));
      RegisterCollection(OfferProducts, model.OfferProducts);

    }
  }
}
