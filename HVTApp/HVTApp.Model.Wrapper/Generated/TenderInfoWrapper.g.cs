using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TenderInfoWrapper : WrapperBase<TenderInfo>
  {
    protected TenderInfoWrapper(TenderInfo model) : base(model) { }
    //public TenderInfoWrapper(TenderInfo model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static TenderInfoWrapper GetWrapper(TenderInfo model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TenderInfoWrapper)Repository.ModelWrapperDictionary[model];

		return new TenderInfoWrapper(model);
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

	private ProductMainWrapper _fieldProductMain;
	public ProductMainWrapper ProductMain 
    {
        get { return _fieldProductMain; }
        set
        {
            if (Equals(_fieldProductMain, value))
                return;

            UnRegisterComplexProperty(_fieldProductMain);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldProductMain = value;
        }
    }


	private CompanyWrapper _fieldProducerWinner;
	public CompanyWrapper ProducerWinner 
    {
        get { return _fieldProducerWinner; }
        set
        {
            if (Equals(_fieldProducerWinner, value))
                return;

            UnRegisterComplexProperty(_fieldProducerWinner);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldProducerWinner = value;
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

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldCostInfo = value;
        }
    }


    #endregion

    protected override void InitializeComplexProperties(TenderInfo model)
    {

        ProductMain = ProductMainWrapper.GetWrapper(model.ProductMain);

        ProducerWinner = CompanyWrapper.GetWrapper(model.ProducerWinner);

        CostInfo = CostInfoWrapper.GetWrapper(model.CostInfo);

    }

  }
}
