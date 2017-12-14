using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using HVTApp.Model.POCOs;
using HVTApp.TestDataGenerator;

namespace HVTApp.DataAccess
{
    public partial class HvtAppDataBaseInitializer
    {
        protected override void Seed(HvtAppContext context)
        {
            TestData testData = new TestData();

            context.AddressDbSet.AddRange(testData.GetAll<Address>());
            context.CountryDbSet.AddRange(testData.GetAll<Country>());
            context.DistrictDbSet.AddRange(testData.GetAll<District>());
            context.LocalityDbSet.AddRange(testData.GetAll<Locality>());
            context.LocalityTypeDbSet.AddRange(testData.GetAll<LocalityType>());
            context.RegionDbSet.AddRange(testData.GetAll<Region>());
            context.AdditionalSalesUnitsDbSet.AddRange(testData.GetAll<AdditionalSalesUnits>());
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
            context.StandartPaymentConditionsDbSet.AddRange(testData.GetAll<StandartPaymentConditions>());
            context.PersonDbSet.AddRange(testData.GetAll<Person>());
            context.PaymentPlannedDbSet.AddRange(testData.GetAll<PaymentPlanned>());
            context.PaymentActualDbSet.AddRange(testData.GetAll<PaymentActual>());
            context.ParameterRelationDbSet.AddRange(testData.GetAll<ParameterRelation>());
            context.ProjectUnitDbSet.AddRange(testData.GetAll<ProjectUnit>());
            context.TenderUnitDbSet.AddRange(testData.GetAll<TenderUnit>());
            context.ShipmentUnitDbSet.AddRange(testData.GetAll<ShipmentUnit>());
            context.ProductionUnitDbSet.AddRange(testData.GetAll<ProductionUnit>());
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
            context.CostDbSet.AddRange(testData.GetAll<Cost>());
            context.CurrencyDbSet.AddRange(testData.GetAll<Currency>());
            context.ExchangeCurrencyRateDbSet.AddRange(testData.GetAll<ExchangeCurrencyRate>());
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
            context.OfferUnitDbSet.AddRange(testData.GetAll<OfferUnit>());

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    Trace.TraceInformation("Entry: {0}", validationErrors.Entry.Entity);
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0}; Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }

            base.Seed(context);
        }
    }
}
