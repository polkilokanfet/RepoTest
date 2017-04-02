using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ActivityFieldWrapper : WrapperBase<ActivityField>
  {
    protected ActivityFieldWrapper(ActivityField model) : base(model) { }
    //public ActivityFieldWrapper(ActivityField model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static ActivityFieldWrapper GetWrapper(ActivityField model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ActivityFieldWrapper)Repository.ModelWrapperDictionary[model];

		return new ActivityFieldWrapper(model);
	}



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public HVTApp.Model.FieldOfActivity FieldOfActivity
    {
      get { return GetValue<HVTApp.Model.FieldOfActivity>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.FieldOfActivity FieldOfActivityOriginalValue => GetOriginalValue<HVTApp.Model.FieldOfActivity>(nameof(FieldOfActivity));
    public bool FieldOfActivityIsChanged => GetIsChanged(nameof(FieldOfActivity));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion

  }
}
