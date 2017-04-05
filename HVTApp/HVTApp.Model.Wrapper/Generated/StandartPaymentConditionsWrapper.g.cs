using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class StandartPaymentConditionsWrapper : WrapperBase<StandartPaymentConditions>
  {
    protected StandartPaymentConditionsWrapper(StandartPaymentConditions model) : base(model) { }

	public static StandartPaymentConditionsWrapper GetWrapper(StandartPaymentConditions model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (StandartPaymentConditionsWrapper)Repository.ModelWrapperDictionary[model];

		return new StandartPaymentConditionsWrapper(model);
	}



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentsConditionWrapper> PaymentsConditionsCollection { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties(StandartPaymentConditions model)
    {

      if (model.PaymentsConditionsCollection == null) throw new ArgumentException("PaymentsConditionsCollection cannot be null");
      PaymentsConditionsCollection = new ValidatableChangeTrackingCollection<PaymentsConditionWrapper>(model.PaymentsConditionsCollection.Select(e => PaymentsConditionWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsConditionsCollection, model.PaymentsConditionsCollection);


    }

  }
}
