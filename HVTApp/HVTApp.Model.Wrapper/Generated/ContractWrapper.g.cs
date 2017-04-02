using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ContractWrapper : WrapperBase<Contract>
  {
    protected ContractWrapper(Contract model) : base(model) { }
    //public ContractWrapper(Contract model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static ContractWrapper GetWrapper(Contract model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ContractWrapper)Repository.ModelWrapperDictionary[model];

		return new ContractWrapper(model);
	}



    #region SimpleProperties

    public System.String Number
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
    public bool NumberIsChanged => GetIsChanged(nameof(Number));


    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private CompanyWrapper _fieldContragent;
	public CompanyWrapper Contragent 
    {
        get { return _fieldContragent; }
        set
        {
            if (Equals(_fieldContragent, value))
                return;

            UnRegisterComplexProperty(_fieldContragent);

            _fieldContragent = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<SpecificationWrapper> Specifications { get; private set; }


    #endregion


    #region GetProperties

    public System.Double Sum => GetValue<System.Double>(); 


    public System.Double SumWithVat => GetValue<System.Double>(); 


    #endregion

    protected override void InitializeComplexProperties(Contract model)
    {

        Contragent = CompanyWrapper.GetWrapper(model.Contragent);

    }

  
    protected override void InitializeCollectionComplexProperties(Contract model)
    {

      if (model.Specifications == null) throw new ArgumentException("Specifications cannot be null");
      Specifications = new ValidatableChangeTrackingCollection<SpecificationWrapper>(model.Specifications.Select(e => SpecificationWrapper.GetWrapper(e)));
      RegisterCollection(Specifications, model.Specifications);


    }

  }
}
