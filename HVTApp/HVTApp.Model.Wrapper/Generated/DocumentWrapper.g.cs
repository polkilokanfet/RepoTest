using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DocumentWrapper : WrapperBase<Document>
  {
    public DocumentWrapper(Document model) : base(model) { }
    public DocumentWrapper(Document model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


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
		get { return GetComplexProperty<Document, DocumentWrapper>(nameof(RequestDocument)); }
		set { SetComplexProperty<Document, DocumentWrapper>(value, nameof(RequestDocument)); }
	}


	public CompanyWrapper Sender
	{
		get { return GetComplexProperty<Company, CompanyWrapper>(nameof(Sender)); }
		set { SetComplexProperty<Company, CompanyWrapper>(value, nameof(Sender)); }
	}


	public EmployeeWrapper SenderEmployee
	{
		get { return GetComplexProperty<Employee, EmployeeWrapper>(nameof(SenderEmployee)); }
		set { SetComplexProperty<Employee, EmployeeWrapper>(value, nameof(SenderEmployee)); }
	}


	public EmployeeWrapper RecipientEmployee
	{
		get { return GetComplexProperty<Employee, EmployeeWrapper>(nameof(RecipientEmployee)); }
		set { SetComplexProperty<Employee, EmployeeWrapper>(value, nameof(RecipientEmployee)); }
	}


	public RegistrationDetailsWrapper RegistrationDetailsOfSender
	{
		get { return GetComplexProperty<RegistrationDetails, RegistrationDetailsWrapper>(nameof(RegistrationDetailsOfSender)); }
		set { SetComplexProperty<RegistrationDetails, RegistrationDetailsWrapper>(value, nameof(RegistrationDetailsOfSender)); }
	}


	public RegistrationDetailsWrapper RegistrationDetailsOfRecipient
	{
		get { return GetComplexProperty<RegistrationDetails, RegistrationDetailsWrapper>(nameof(RegistrationDetailsOfRecipient)); }
		set { SetComplexProperty<RegistrationDetails, RegistrationDetailsWrapper>(value, nameof(RegistrationDetailsOfRecipient)); }
	}


    #endregion


    #region CollectionComplexProperties

    public ValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }


    #endregion


    #region GetProperties

    public HVTApp.Model.Company RecipientCompany => GetValue<HVTApp.Model.Company>(); 


    #endregion

    protected override void InitializeComplexProperties(Document model)
    {

        RequestDocument = GetWrapper<Document, DocumentWrapper>(model.RequestDocument);

        Sender = GetWrapper<Company, CompanyWrapper>(model.Sender);

        SenderEmployee = GetWrapper<Employee, EmployeeWrapper>(model.SenderEmployee);

        RecipientEmployee = GetWrapper<Employee, EmployeeWrapper>(model.RecipientEmployee);

        RegistrationDetailsOfSender = GetWrapper<RegistrationDetails, RegistrationDetailsWrapper>(model.RegistrationDetailsOfSender);

        RegistrationDetailsOfRecipient = GetWrapper<RegistrationDetails, RegistrationDetailsWrapper>(model.RegistrationDetailsOfRecipient);

    }

  
    protected override void InitializeCollectionComplexProperties(Document model)
    {

      if (model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
      CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(model.CopyToRecipients.Select(e => new EmployeeWrapper(e, ExistsWrappers)));
      RegisterCollection(CopyToRecipients, model.CopyToRecipients);


    }

  }
}
