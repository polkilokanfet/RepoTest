using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentsWrapper : WrapperBase<Payments>
  {
    protected PaymentsWrapper(Payments model) : base(model) { }

	public static PaymentsWrapper GetWrapper()
	{
		return GetWrapper(new Payments());
	}

	public static PaymentsWrapper GetWrapper(Payments model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (PaymentsWrapper)Repository.ModelWrapperDictionary[model];

		return new PaymentsWrapper(model);
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


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentWrapper> PaymentsPlanned { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentWrapper> PaymentsActual { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties(Payments model)
    {

      if (model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
      PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentWrapper>(model.PaymentsPlanned.Select(e => PaymentWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsPlanned, model.PaymentsPlanned);


      if (model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
      PaymentsActual = new ValidatableChangeTrackingCollection<PaymentWrapper>(model.PaymentsActual.Select(e => PaymentWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsActual, model.PaymentsActual);


    }

  }
}
