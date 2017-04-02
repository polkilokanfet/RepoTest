using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DocumentWrapper : WrapperBase<Document>
  {
    protected DocumentWrapper(Document model) : base(model) { }
    //public DocumentWrapper(Document model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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

	private DocumentWrapper _fieldRequestDocument;
	public DocumentWrapper RequestDocument 
    {
        get { return _fieldRequestDocument; }
        set
        {
            if (Equals(_fieldRequestDocument, value))
                return;

            UnRegisterComplexProperty(_fieldRequestDocument);

            _fieldRequestDocument = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private CompanyWrapper _fieldSender;
	public CompanyWrapper Sender 
    {
        get { return _fieldSender; }
        set
        {
            if (Equals(_fieldSender, value))
                return;

            UnRegisterComplexProperty(_fieldSender);

            _fieldSender = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private EmployeeWrapper _fieldSenderEmployee;
	public EmployeeWrapper SenderEmployee 
    {
        get { return _fieldSenderEmployee; }
        set
        {
            if (Equals(_fieldSenderEmployee, value))
                return;

            UnRegisterComplexProperty(_fieldSenderEmployee);

            _fieldSenderEmployee = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private EmployeeWrapper _fieldRecipientEmployee;
	public EmployeeWrapper RecipientEmployee 
    {
        get { return _fieldRecipientEmployee; }
        set
        {
            if (Equals(_fieldRecipientEmployee, value))
                return;

            UnRegisterComplexProperty(_fieldRecipientEmployee);

            _fieldRecipientEmployee = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private RegistrationDetailsWrapper _fieldRegistrationDetailsOfSender;
	public RegistrationDetailsWrapper RegistrationDetailsOfSender 
    {
        get { return _fieldRegistrationDetailsOfSender; }
        set
        {
            if (Equals(_fieldRegistrationDetailsOfSender, value))
                return;

            UnRegisterComplexProperty(_fieldRegistrationDetailsOfSender);

            _fieldRegistrationDetailsOfSender = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private RegistrationDetailsWrapper _fieldRegistrationDetailsOfRecipient;
	public RegistrationDetailsWrapper RegistrationDetailsOfRecipient 
    {
        get { return _fieldRegistrationDetailsOfRecipient; }
        set
        {
            if (Equals(_fieldRegistrationDetailsOfRecipient, value))
                return;

            UnRegisterComplexProperty(_fieldRegistrationDetailsOfRecipient);

            _fieldRegistrationDetailsOfRecipient = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }


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
