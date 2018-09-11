using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
	public partial class CreateNewProductTask
	{
		public virtual Guid? ProductId { get; set; }
	}

	public partial class DocumentIncomingNumber
	{
	}

	public partial class DocumentOutgoingNumber
	{
	}

	public partial class PaymentActual
	{
	}

	public partial class PaymentPlanned
	{
		public virtual Guid? ConditionId { get; set; }
	}

	public partial class ProductBlockIsService
	{
	}

	public partial class ProductIncluded
	{
		public virtual Guid? ProductId { get; set; }
	}

	public partial class ProductDesignation
	{
	}

	public partial class ProductType
	{
	}

	public partial class ProductTypeDesignation
	{
		public virtual Guid? ProductTypeId { get; set; }
	}

	public partial class ProjectType
	{
	}

	public partial class CommonOption
	{
	}

	public partial class Address
	{
		public virtual Guid? LocalityId { get; set; }
	}

	public partial class Country
	{
	}

	public partial class District
	{
		public virtual Guid? CountryId { get; set; }
	}

	public partial class Locality
	{
		public virtual Guid? LocalityTypeId { get; set; }
		public virtual Guid? RegionId { get; set; }
	}

	public partial class LocalityType
	{
	}

	public partial class Region
	{
		public virtual Guid? DistrictId { get; set; }
	}

	public partial class CalculatePriceTask
	{
		public virtual Guid? ProductBlockId { get; set; }
	}

	public partial class Sum
	{
	}

	public partial class CurrencyExchangeRate
	{
	}

	public partial class DescribeProductBlockTask
	{
		public virtual Guid? ProductBlockId { get; set; }
		public virtual Guid? ProductId { get; set; }
	}

	public partial class Note
	{
	}

	public partial class OfferUnit
	{
		public virtual Guid? OfferId { get; set; }
		public virtual Guid? ProductId { get; set; }
		public virtual Guid? FacilityId { get; set; }
		public virtual Guid? PaymentConditionSetId { get; set; }
	}

	public partial class PaymentConditionSet
	{
	}

	public partial class ProductBlock
	{
	}

	public partial class ProductDependent
	{
		public virtual Guid? ProductId { get; set; }
	}

	public partial class ProductionTask
	{
	}

	public partial class SalesBlock
	{
	}

	public partial class BankDetails
	{
	}

	public partial class Company
	{
		public virtual Guid? FormId { get; set; }
		public virtual Guid? ParentCompanyId { get; set; }
		public virtual Guid? AddressLegalId { get; set; }
		public virtual Guid? AddressPostId { get; set; }
	}

	public partial class CompanyForm
	{
	}

	public partial class DocumentsRegistrationDetails
	{
	}

	public partial class EmployeesPosition
	{
	}

	public partial class FacilityType
	{
	}

	public partial class ActivityField
	{
	}

	public partial class Contract
	{
		public virtual Guid? ContragentId { get; set; }
	}

	public partial class Measure
	{
	}

	public partial class Parameter
	{
		public virtual Guid? ParameterGroupId { get; set; }
	}

	public partial class ParameterGroup
	{
		public virtual Guid? MeasureId { get; set; }
	}

	public partial class ProductRelation
	{
	}

	public partial class Person
	{
	}

	public partial class ParameterRelation
	{
	}

	public partial class SalesUnit
	{
		public virtual Guid? ProductId { get; set; }
		public virtual Guid? FacilityId { get; set; }
		public virtual Guid? PaymentConditionSetId { get; set; }
		public virtual Guid? ProjectId { get; set; }
		public virtual Guid? ProducerId { get; set; }
		public virtual Guid? OrderId { get; set; }
		public virtual Guid? SpecificationId { get; set; }
		public virtual Guid? AddressId { get; set; }
	}

	public partial class TestFriendAddress
	{
	}

	public partial class TestFriend
	{
		public virtual Guid? TestFriendAddressId { get; set; }
		public virtual Guid? TestFriendGroupId { get; set; }
		public virtual Guid? TestFriendEmailGetId { get; set; }
	}

	public partial class TestFriendEmail
	{
	}

	public partial class TestFriendGroup
	{
	}

	public partial class Document
	{
		public virtual Guid? RequestDocumentId { get; set; }
		public virtual Guid? AuthorId { get; set; }
		public virtual Guid? SenderEmployeeId { get; set; }
		public virtual Guid? RecipientEmployeeId { get; set; }
		public virtual Guid? RegistrationDetailsOfSenderId { get; set; }
		public virtual Guid? RegistrationDetailsOfRecipientId { get; set; }
	}

	public partial class TestEntity
	{
	}

	public partial class TestHusband
	{
		public virtual Guid? WifeId { get; set; }
	}

	public partial class TestWife
	{
		public virtual Guid? HusbandId { get; set; }
	}

	public partial class TestChild
	{
		public virtual Guid? HusbandId { get; set; }
		public virtual Guid? WifeId { get; set; }
	}

	public partial class SumOnDate
	{
	}

	public partial class Product
	{
		public virtual Guid? ProductTypeId { get; set; }
		public virtual Guid? ProductBlockId { get; set; }
	}

	public partial class Offer
	{
		public virtual Guid? ProjectId { get; set; }
		public virtual Guid? RequestDocumentId { get; set; }
		public virtual Guid? AuthorId { get; set; }
		public virtual Guid? SenderEmployeeId { get; set; }
		public virtual Guid? RecipientEmployeeId { get; set; }
		public virtual Guid? RegistrationDetailsOfSenderId { get; set; }
		public virtual Guid? RegistrationDetailsOfRecipientId { get; set; }
	}

	public partial class Employee
	{
		public virtual Guid? PersonId { get; set; }
		public virtual Guid? CompanyId { get; set; }
		public virtual Guid? PositionId { get; set; }
	}

	public partial class Order
	{
	}

	public partial class PaymentCondition
	{
	}

	public partial class PaymentDocument
	{
	}

	public partial class Facility
	{
		public virtual Guid? TypeId { get; set; }
		public virtual Guid? OwnerCompanyId { get; set; }
		public virtual Guid? AddressId { get; set; }
	}

	public partial class Project
	{
		public virtual Guid? ProjectTypeId { get; set; }
		public virtual Guid? ManagerId { get; set; }
	}

	public partial class UserRole
	{
	}

	public partial class Specification
	{
		public virtual Guid? ContractId { get; set; }
	}

	public partial class Tender
	{
		public virtual Guid? ProjectId { get; set; }
		public virtual Guid? WinnerId { get; set; }
	}

	public partial class TenderType
	{
	}

	public partial class User
	{
		public virtual Guid? EmployeeId { get; set; }
	}

}
