using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
{
  public partial class TenderTypeWrapper : WrapperBase<TenderType>
  {
    public TenderTypeWrapper(TenderType model) : base(model) { }



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public HVTApp.Model.POCOs.TenderTypeEnum Type
    {
      get { return GetValue<HVTApp.Model.POCOs.TenderTypeEnum>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.POCOs.TenderTypeEnum TypeOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.TenderTypeEnum>(nameof(Type));
    public bool TypeIsChanged => GetIsChanged(nameof(Type));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion

  }
}
