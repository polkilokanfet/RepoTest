using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class BankDetailsWrapper : WrapperBase<BankDetails>
  {
    public BankDetailsWrapper() : base(new BankDetails(), new Dictionary<IBaseEntity, object>()) { }
    public BankDetailsWrapper(BankDetails model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public BankDetailsWrapper(BankDetails model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public BankDetailsWrapper(BankDetails model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


    #region SimpleProperties
    public System.String BankName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String BankNameOriginalValue => GetOriginalValue<System.String>(nameof(BankName));
    public bool BankNameIsChanged => GetIsChanged(nameof(BankName));

    public System.String BankIdentificationCode
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String BankIdentificationCodeOriginalValue => GetOriginalValue<System.String>(nameof(BankIdentificationCode));
    public bool BankIdentificationCodeIsChanged => GetIsChanged(nameof(BankIdentificationCode));

    public System.String CorrespondentAccount
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String CorrespondentAccountOriginalValue => GetOriginalValue<System.String>(nameof(CorrespondentAccount));
    public bool CorrespondentAccountIsChanged => GetIsChanged(nameof(CorrespondentAccount));

    public System.String CheckingAccount
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String CheckingAccountOriginalValue => GetOriginalValue<System.String>(nameof(CheckingAccount));
    public bool CheckingAccountIsChanged => GetIsChanged(nameof(CheckingAccount));

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
