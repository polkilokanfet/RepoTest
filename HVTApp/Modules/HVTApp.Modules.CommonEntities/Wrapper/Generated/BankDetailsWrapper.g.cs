using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class BankDetailsWrapper : WrapperBase<BankDetails>
	{
	public BankDetailsWrapper(BankDetails model) : base(model) { }

	

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
	