using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DocumentWrapper : WrapperBase<Document>
  {
    protected DocumentWrapper(Document model) : base(model) { }

	public static DocumentWrapper GetWrapper(Document model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (DocumentWrapper)Repository.ModelWrapperDictionary[model];

		return new DocumentWrapper(model);
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

	public DocumentWrapper RequestDocument 
    {
        get { return DocumentWrapper.GetWrapper(Model.RequestDocument); }
        set
        {
			var oldPropVal = RequestDocument;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public DocumentWrapper RequestDocumentOriginalValue => DocumentWrapper.GetWrapper(GetOriginalValue<Document>(nameof(RequestDocument)));
    public bool RequestDocumentIsChanged => GetIsChanged(nameof(RequestDocument));


	public CompanyWrapper Sender 
    {
        get { return CompanyWrapper.GetWrapper(Model.Sender); }
        set
        {
			var oldPropVal = Sender;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CompanyWrapper SenderOriginalValue => CompanyWrapper.GetWrapper(GetOriginalValue<Company>(nameof(Sender)));
    public bool SenderIsChanged => GetIsChanged(nameof(Sender));


	public EmployeeWrapper SenderEmployee 
    {
        get { return EmployeeWrapper.GetWrapper(Model.SenderEmployee); }
        set
        {
			var oldPropVal = SenderEmployee;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public EmployeeWrapper SenderEmployeeOriginalValue => EmployeeWrapper.GetWrapper(GetOriginalValue<Employee>(nameof(SenderEmployee)));
    public bool SenderEmployeeIsChanged => GetIsChanged(nameof(SenderEmployee));


	public EmployeeWrapper RecipientEmployee 
    {
        get { return EmployeeWrapper.GetWrapper(Model.RecipientEmployee); }
        set
        {
			var oldPropVal = RecipientEmployee;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public EmployeeWrapper RecipientEmployeeOriginalValue => EmployeeWrapper.GetWrapper(GetOriginalValue<Employee>(nameof(RecipientEmployee)));
    public bool RecipientEmployeeIsChanged => GetIsChanged(nameof(RecipientEmployee));


	public RegistrationDetailsWrapper RegistrationDetailsOfSender 
    {
        get { return RegistrationDetailsWrapper.GetWrapper(Model.RegistrationDetailsOfSender); }
        set
        {
			var oldPropVal = RegistrationDetailsOfSender;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public RegistrationDetailsWrapper RegistrationDetailsOfSenderOriginalValue => RegistrationDetailsWrapper.GetWrapper(GetOriginalValue<RegistrationDetails>(nameof(RegistrationDetailsOfSender)));
    public bool RegistrationDetailsOfSenderIsChanged => GetIsChanged(nameof(RegistrationDetailsOfSender));


	public RegistrationDetailsWrapper RegistrationDetailsOfRecipient 
    {
        get { return RegistrationDetailsWrapper.GetWrapper(Model.RegistrationDetailsOfRecipient); }
        set
        {
			var oldPropVal = RegistrationDetailsOfRecipient;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public RegistrationDetailsWrapper RegistrationDetailsOfRecipientOriginalValue => RegistrationDetailsWrapper.GetWrapper(GetOriginalValue<RegistrationDetails>(nameof(RegistrationDetailsOfRecipient)));
    public bool RegistrationDetailsOfRecipientIsChanged => GetIsChanged(nameof(RegistrationDetailsOfRecipient));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }


    #endregion


    #region GetProperties

    public HVTApp.Model.Company RecipientCompany => GetValue<HVTApp.Model.Company>(); 


    #endregion

    protected override void InitializeComplexProperties(Document model)
    {

        RequestDocument = DocumentWrapper.GetWrapper(model.RequestDocument);

        Sender = CompanyWrapper.GetWrapper(model.Sender);

        SenderEmployee = EmployeeWrapper.GetWrapper(model.SenderEmployee);

        RecipientEmployee = EmployeeWrapper.GetWrapper(model.RecipientEmployee);

        RegistrationDetailsOfSender = RegistrationDetailsWrapper.GetWrapper(model.RegistrationDetailsOfSender);

        RegistrationDetailsOfRecipient = RegistrationDetailsWrapper.GetWrapper(model.RegistrationDetailsOfRecipient);

    }

  
    protected override void InitializeCollectionComplexProperties(Document model)
    {

      if (model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
      CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(model.CopyToRecipients.Select(e => EmployeeWrapper.GetWrapper(e)));
      RegisterCollection(CopyToRecipients, model.CopyToRecipients);


    }

  }
}
