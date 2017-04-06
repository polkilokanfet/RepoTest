using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class EmployeesPositionWrapper : WrapperBase<EmployeesPosition>
  {
    protected EmployeesPositionWrapper(EmployeesPosition model) : base(model) { }

	public static EmployeesPositionWrapper GetWrapper()
	{
		return GetWrapper(new EmployeesPosition());
	}

	public static EmployeesPositionWrapper GetWrapper(EmployeesPosition model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (EmployeesPositionWrapper)Repository.ModelWrapperDictionary[model];

		return new EmployeesPositionWrapper(model);
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

  }
}
