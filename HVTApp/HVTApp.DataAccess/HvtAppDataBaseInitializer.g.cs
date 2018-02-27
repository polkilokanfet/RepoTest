using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using HVTApp.Model.POCOs;
using HVTApp.TestDataGenerator;

namespace HVTApp.DataAccess
{
    public partial class HvtAppDataBaseInitializer
    {
        protected void AddData(HvtAppContext context)
        {
            TestData testData = new TestData();

            context.CommonOptionDbSet.AddRange(testData.GetAll<CommonOption>());
            context.AddressDbSet.AddRange(testData.GetAll<Address>());
            context.CountryDbSet.AddRange(testData.GetAll<Country>());
            context.DistrictDbSet.AddRange(testData.GetAll<District>());
            context.LocalityDbSet.AddRange(testData.GetAll<Locality>());
            context.LocalityTypeDbSet.AddRange(testData.GetAll<LocalityType>());
            context.RegionDbSet.AddRange(testData.GetAll<Region>());
            context.CalculatePriceTaskDbSet.AddRange(testData.GetAll<CalculatePriceTask>());
            context.CostDbSet.AddRange(testData.GetAll<Cost>());
            context.CurrencyDbSet.AddRange(testData.GetAll<Currency>());
            context.CurrencyExchangeRateDbSet.AddRange(testData.GetAll<CurrencyExchangeRate>());
            context.DescribeProductBlockTaskDbSet.AddRange(testData.GetAll<DescribeProductBlockTask>());
            context.PaymentConditionSetDbSet.AddRange(testData.GetAll<PaymentConditionSet>());
            context.ProductBlockDbSet.AddRange(testData.GetAll<ProductBlock>());
            context.SalesBlockDbSet.AddRange(testData.GetAll<SalesBlock>());
            context.BankDetailsDbSet.AddRange(testData.GetAll<BankDetails>());
            context.CompanyDbSet.AddRange(testData.GetAll<Company>());
            context.CompanyFormDbSet.AddRange(testData.GetAll<CompanyForm>());
            context.DocumentsRegistrationDetailsDbSet.AddRange(testData.GetAll<DocumentsRegistrationDetails>());
            context.EmployeesPositionDbSet.AddRange(testData.GetAll<EmployeesPosition>());
            context.FacilityTypeDbSet.AddRange(testData.GetAll<FacilityType>());
            context.ActivityFieldDbSet.AddRange(testData.GetAll<ActivityField>());
            context.ContractDbSet.AddRange(testData.GetAll<Contract>());
            context.MeasureDbSet.AddRange(testData.GetAll<Measure>());
            context.ParameterDbSet.AddRange(testData.GetAll<Parameter>());
            context.ParameterGroupDbSet.AddRange(testData.GetAll<ParameterGroup>());
            context.ProductRelationDbSet.AddRange(testData.GetAll<ProductRelation>());
            context.PersonDbSet.AddRange(testData.GetAll<Person>());
            context.PaymentPlannedDbSet.AddRange(testData.GetAll<PaymentPlannedList>());
            context.PaymentPlannedPartDbSet.AddRange(testData.GetAll<PaymentPlanned>());
            context.PaymentActualDbSet.AddRange(testData.GetAll<PaymentActual>());
            context.ParameterRelationDbSet.AddRange(testData.GetAll<ParameterRelation>());
            context.SalesUnitDbSet.AddRange(testData.GetAll<SalesUnit>());
            context.TestFriendAddressDbSet.AddRange(testData.GetAll<TestFriendAddress>());
            context.TestFriendDbSet.AddRange(testData.GetAll<TestFriend>());
            context.TestFriendEmailDbSet.AddRange(testData.GetAll<TestFriendEmail>());
            context.TestFriendGroupDbSet.AddRange(testData.GetAll<TestFriendGroup>());
            context.DocumentDbSet.AddRange(testData.GetAll<Document>());
            context.TestEntityDbSet.AddRange(testData.GetAll<TestEntity>());
            context.TestHusbandDbSet.AddRange(testData.GetAll<TestHusband>());
            context.TestWifeDbSet.AddRange(testData.GetAll<TestWife>());
            context.TestChildDbSet.AddRange(testData.GetAll<TestChild>());
            context.CostOnDateDbSet.AddRange(testData.GetAll<CostOnDate>());
            context.ProductDbSet.AddRange(testData.GetAll<Product>());
            context.OfferDbSet.AddRange(testData.GetAll<Offer>());
            context.EmployeeDbSet.AddRange(testData.GetAll<Employee>());
            context.OrderDbSet.AddRange(testData.GetAll<Order>());
            context.PaymentConditionDbSet.AddRange(testData.GetAll<PaymentCondition>());
            context.PaymentDocumentDbSet.AddRange(testData.GetAll<PaymentDocument>());
            context.FacilityDbSet.AddRange(testData.GetAll<Facility>());
            context.ProjectDbSet.AddRange(testData.GetAll<Project>());
            context.UserRoleDbSet.AddRange(testData.GetAll<UserRole>());
            context.SpecificationDbSet.AddRange(testData.GetAll<Specification>());
            context.TenderDbSet.AddRange(testData.GetAll<Tender>());
            context.TenderTypeDbSet.AddRange(testData.GetAll<TenderType>());
            context.UserDbSet.AddRange(testData.GetAll<User>());
        }
    }
}
