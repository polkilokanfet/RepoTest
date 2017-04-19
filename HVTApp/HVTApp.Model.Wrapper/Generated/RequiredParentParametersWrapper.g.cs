using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class RequiredParentParametersWrapper : WrapperBase<RequiredParentParameters>
  {
    protected RequiredParentParametersWrapper(RequiredParentParameters model) : base(model) { }

	public static RequiredParentParametersWrapper GetWrapper()
	{
		return GetWrapper(new RequiredParentParameters());
	}

	public static RequiredParentParametersWrapper GetWrapper(RequiredParentParameters model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (RequiredParentParametersWrapper)Repository.ModelWrapperDictionary[model];

		return new RequiredParentParametersWrapper(model);
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

    public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties(RequiredParentParameters model)
    {

      if (model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
      Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(model.Parameters.Select(e => ParameterWrapper.GetWrapper(e)));
      RegisterCollection(Parameters, model.Parameters);


    }

  }
}
