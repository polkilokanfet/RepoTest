using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DocumentWrapper : WrapperBase<Document>
  {
    public DocumentWrapper(Document model) : base(model) { }
    public DocumentWrapper(Document model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


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
		set { SetComplexProperty<Document, DocumentWrapper>(value, this.RequestDocument, nameof(RequestDocument)); }
	}


	public CompanyWrapper Sender
	{
		get { return GetComplexProperty<Company, CompanyWrapper>(nameof(Sender)); }
		set { SetComplexProperty<Company, CompanyWrapper>(value, this.Sender, nameof(Sender)); }
	}


	public EmployeeWrapper SenderEmployee
	{
		get { return GetComplexProperty<Employee, EmployeeWrapper>(nameof(SenderEmployee)); }
		set { SetComplexProperty<Employee, EmployeeWrapper>(value, this.SenderEmployee, nameof(SenderEmployee)); }
	}


	public EmployeeWrapper RecipientEmployee
	{
		get { return GetComplexProperty<Employee, EmployeeWrapper>(nameof(RecipientEmployee)); }
		set { SetComplexProperty<Employee, EmployeeWrapper>(value, this.RecipientEmployee, nameof(RecipientEmployee)); }
	}


	public RegistrationDetailsWrapper RegistrationDetailsOfSender
	{
		get { return GetComplexProperty<RegistrationDetails, RegistrationDetailsWrapper>(nameof(RegistrationDetailsOfSender)); }
		set { SetComplexProperty<RegistrationDetails, RegistrationDetailsWrapper>(value, this.RegistrationDetailsOfSender, nameof(RegistrationDetailsOfSender)); }
	}


	public RegistrationDetailsWrapper RegistrationDetailsOfRecipient
	{
		get { return GetComplexProperty<RegistrationDetails, RegistrationDetailsWrapper>(nameof(RegistrationDetailsOfRecipient)); }
		set { SetComplexProperty<RegistrationDetails, RegistrationDetailsWrapper>(value, this.RegistrationDetailsOfRecipient, nameof(RegistrationDetailsOfRecipient)); }
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

		if (model.RequestDocument != null)
		{
			if (ExistsWrappers.ContainsKey(model.RequestDocument))
			{
				RequestDocument = (DocumentWrapper)ExistsWrappers[model.RequestDocument];
			}
			else
			{
				RequestDocument = new DocumentWrapper(model.RequestDocument, ExistsWrappers);
				//ExistsWrappers.Add(model.RequestDocument, new DocumentWrapper(model.RequestDocument, ExistsWrappers));
				RegisterComplexProperty(RequestDocument);
			}
		}


		if (model.Sender != null)
		{
			if (ExistsWrappers.ContainsKey(model.Sender))
			{
				Sender = (CompanyWrapper)ExistsWrappers[model.Sender];
			}
			else
			{
				Sender = new CompanyWrapper(model.Sender, ExistsWrappers);
				//ExistsWrappers.Add(model.Sender, new CompanyWrapper(model.Sender, ExistsWrappers));
				RegisterComplexProperty(Sender);
			}
		}


		if (model.SenderEmployee != null)
		{
			if (ExistsWrappers.ContainsKey(model.SenderEmployee))
			{
				SenderEmployee = (EmployeeWrapper)ExistsWrappers[model.SenderEmployee];
			}
			else
			{
				SenderEmployee = new EmployeeWrapper(model.SenderEmployee, ExistsWrappers);
				//ExistsWrappers.Add(model.SenderEmployee, new EmployeeWrapper(model.SenderEmployee, ExistsWrappers));
				RegisterComplexProperty(SenderEmployee);
			}
		}


		if (model.RecipientEmployee != null)
		{
			if (ExistsWrappers.ContainsKey(model.RecipientEmployee))
			{
				RecipientEmployee = (EmployeeWrapper)ExistsWrappers[model.RecipientEmployee];
			}
			else
			{
				RecipientEmployee = new EmployeeWrapper(model.RecipientEmployee, ExistsWrappers);
				//ExistsWrappers.Add(model.RecipientEmployee, new EmployeeWrapper(model.RecipientEmployee, ExistsWrappers));
				RegisterComplexProperty(RecipientEmployee);
			}
		}


		if (model.RegistrationDetailsOfSender != null)
		{
			if (ExistsWrappers.ContainsKey(model.RegistrationDetailsOfSender))
			{
				RegistrationDetailsOfSender = (RegistrationDetailsWrapper)ExistsWrappers[model.RegistrationDetailsOfSender];
			}
			else
			{
				RegistrationDetailsOfSender = new RegistrationDetailsWrapper(model.RegistrationDetailsOfSender, ExistsWrappers);
				//ExistsWrappers.Add(model.RegistrationDetailsOfSender, new RegistrationDetailsWrapper(model.RegistrationDetailsOfSender, ExistsWrappers));
				RegisterComplexProperty(RegistrationDetailsOfSender);
			}
		}


		if (model.RegistrationDetailsOfRecipient != null)
		{
			if (ExistsWrappers.ContainsKey(model.RegistrationDetailsOfRecipient))
			{
				RegistrationDetailsOfRecipient = (RegistrationDetailsWrapper)ExistsWrappers[model.RegistrationDetailsOfRecipient];
			}
			else
			{
				RegistrationDetailsOfRecipient = new RegistrationDetailsWrapper(model.RegistrationDetailsOfRecipient, ExistsWrappers);
				//ExistsWrappers.Add(model.RegistrationDetailsOfRecipient, new RegistrationDetailsWrapper(model.RegistrationDetailsOfRecipient, ExistsWrappers));
				RegisterComplexProperty(RegistrationDetailsOfRecipient);
			}
		}


    }

  
    protected override void InitializeCollectionComplexProperties(Document model)
    {

      if (model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
      CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(model.CopyToRecipients.Select(e => new EmployeeWrapper(e, ExistsWrappers)));
      RegisterCollection(CopyToRecipients, model.CopyToRecipients);


    }

  }
}
