using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentsInfoWrapper : WrapperBase<PaymentsInfo>
  {
    protected PaymentsInfoWrapper(PaymentsInfo model) : base(model) { }

	public static PaymentsInfoWrapper GetWrapper(PaymentsInfo model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (PaymentsInfoWrapper)Repository.ModelWrapperDictionary[model];

		return new PaymentsInfoWrapper(model);
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

	private ProductBaseWrapper _fieldProduct;
	public ProductBaseWrapper Product 
    {
        get { return _fieldProduct; }
        set
        {
            if (Equals(_fieldProduct, value))
                return;

            UnRegisterComplexProperty(_fieldProduct);

            _fieldProduct = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<PaymentsConditionWrapper> PaymentsConditions { get; private set; }


    public ValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }


    public ValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }


    #endregion


    #region GetProperties

    public System.Double PaymentsSumToStartProduction => GetValue<System.Double>(); 


    public System.Double PaymentsSumToShipping => GetValue<System.Double>(); 


    public System.Boolean IsPaid => GetValue<System.Boolean>(); 


    #endregion

    protected override void InitializeComplexProperties(PaymentsInfo model)
    {

        Product = ProductBaseWrapper.GetWrapper(model.Product);

    }

  
    protected override void InitializeCollectionComplexProperties(PaymentsInfo model)
    {

      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentsConditionWrapper>(model.PaymentsConditions.Select(e => PaymentsConditionWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);


      if (model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
      PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(model.PaymentsActual.Select(e => PaymentActualWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsActual, model.PaymentsActual);


      if (model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
      PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(model.PaymentsPlanned.Select(e => PaymentPlannedWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsPlanned, model.PaymentsPlanned);


    }

  }
}
