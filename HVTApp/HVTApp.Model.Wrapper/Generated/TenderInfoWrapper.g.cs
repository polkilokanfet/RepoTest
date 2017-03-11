using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TenderInfoWrapper : WrapperBase<TenderInfo>
  {
    public TenderInfoWrapper(TenderInfo model) : base(model) { }
    public TenderInfoWrapper(TenderInfo model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public ProductMainWrapper ProductMain { get; private set; }

    public CompanyWrapper ProducerWinner { get; private set; }

    public CostInfoWrapper CostInfo { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(TenderInfo model)
    {
      if (model.ProductMain == null) throw new ArgumentException("ProductMain cannot be null");
      if (ExistsWrappers.ContainsKey(model.ProductMain))
      {
          ProductMain = (ProductMainWrapper)ExistsWrappers[model.ProductMain];
      }
      else
      {
          ProductMain = new ProductMainWrapper(model.ProductMain, ExistsWrappers);
          RegisterComplexProperty(ProductMain);
      }

      if (model.ProducerWinner == null) throw new ArgumentException("ProducerWinner cannot be null");
      if (ExistsWrappers.ContainsKey(model.ProducerWinner))
      {
          ProducerWinner = (CompanyWrapper)ExistsWrappers[model.ProducerWinner];
      }
      else
      {
          ProducerWinner = new CompanyWrapper(model.ProducerWinner, ExistsWrappers);
          RegisterComplexProperty(ProducerWinner);
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

    }
  }
}
