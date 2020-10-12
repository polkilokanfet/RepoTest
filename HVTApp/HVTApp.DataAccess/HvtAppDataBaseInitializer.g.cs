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

            context.CountryUnionDbSet.AddRange(testData.GetAll<CountryUnion>());
            context.BankGuaranteeDbSet.AddRange(testData.GetAll<BankGuarantee>());
            context.BankGuaranteeTypeDbSet.AddRange(testData.GetAll<BankGuaranteeType>());
            context.BudgetDbSet.AddRange(testData.GetAll<Budget>());
            context.BudgetUnitDbSet.AddRange(testData.GetAll<BudgetUnit>());
            context.ConstructorParametersListDbSet.AddRange(testData.GetAll<ConstructorParametersList>());
            context.ConstructorsParametersDbSet.AddRange(testData.GetAll<ConstructorsParameters>());
            context.CreateNewProductTaskDbSet.AddRange(testData.GetAll<CreateNewProductTask>());
            context.DirectumTaskDbSet.AddRange(testData.GetAll<DirectumTask>());
            context.DirectumTaskGroupDbSet.AddRange(testData.GetAll<DirectumTaskGroup>());
            context.DirectumTaskMessageDbSet.AddRange(testData.GetAll<DirectumTaskMessage>());
            context.DocumentNumberDbSet.AddRange(testData.GetAll<DocumentNumber>());
            context.IncomingRequestDbSet.AddRange(testData.GetAll<IncomingRequest>());
            context.LosingReasonDbSet.AddRange(testData.GetAll<LosingReason>());
            context.MarketFieldDbSet.AddRange(testData.GetAll<MarketField>());
            context.PaymentActualDbSet.AddRange(testData.GetAll<PaymentActual>());
            context.PaymentConditionPointDbSet.AddRange(testData.GetAll<PaymentConditionPoint>());
            context.PaymentPlannedDbSet.AddRange(testData.GetAll<PaymentPlanned>());
            context.PenaltyDbSet.AddRange(testData.GetAll<Penalty>());
            context.PriceCalculationDbSet.AddRange(testData.GetAll<PriceCalculation>());
            context.PriceCalculationItemDbSet.AddRange(testData.GetAll<PriceCalculationItem>());
            context.ProductCategoryDbSet.AddRange(testData.GetAll<ProductCategory>());
            context.ProductCategoryPriceAndCostDbSet.AddRange(testData.GetAll<ProductCategoryPriceAndCost>());
            context.ProductIncludedDbSet.AddRange(testData.GetAll<ProductIncluded>());
            context.ProductDesignationDbSet.AddRange(testData.GetAll<ProductDesignation>());
            context.ProductTypeDbSet.AddRange(testData.GetAll<ProductType>());
            context.ProductTypeDesignationDbSet.AddRange(testData.GetAll<ProductTypeDesignation>());
            context.ProjectTypeDbSet.AddRange(testData.GetAll<ProjectType>());
            context.StandartMarginalIncomeDbSet.AddRange(testData.GetAll<StandartMarginalIncome>());
            context.StandartProductionTermDbSet.AddRange(testData.GetAll<StandartProductionTerm>());
            context.StructureCostDbSet.AddRange(testData.GetAll<StructureCost>());
            context.SupervisionDbSet.AddRange(testData.GetAll<Supervision>());
            context.TechnicalRequrementsDbSet.AddRange(testData.GetAll<TechnicalRequrements>());
            context.TechnicalRequrementsFileDbSet.AddRange(testData.GetAll<TechnicalRequrementsFile>());
            context.TechnicalRequrementsTaskDbSet.AddRange(testData.GetAll<TechnicalRequrementsTask>());
            context.GlobalPropertiesDbSet.AddRange(testData.GetAll<GlobalProperties>());
            context.AddressDbSet.AddRange(testData.GetAll<Address>());
            context.CountryDbSet.AddRange(testData.GetAll<Country>());
            context.DistrictDbSet.AddRange(testData.GetAll<District>());
            context.LocalityDbSet.AddRange(testData.GetAll<Locality>());
            context.LocalityTypeDbSet.AddRange(testData.GetAll<LocalityType>());
            context.RegionDbSet.AddRange(testData.GetAll<Region>());
            context.SumDbSet.AddRange(testData.GetAll<Sum>());
            context.CurrencyExchangeRateDbSet.AddRange(testData.GetAll<CurrencyExchangeRate>());
            context.NoteDbSet.AddRange(testData.GetAll<Note>());
            context.OfferUnitDbSet.AddRange(testData.GetAll<OfferUnit>());
            context.PaymentConditionSetDbSet.AddRange(testData.GetAll<PaymentConditionSet>());
            context.ProductBlockDbSet.AddRange(testData.GetAll<ProductBlock>());
            context.ProductDependentDbSet.AddRange(testData.GetAll<ProductDependent>());
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
            context.ParameterRelationDbSet.AddRange(testData.GetAll<ParameterRelation>());
            context.SalesUnitDbSet.AddRange(testData.GetAll<SalesUnit>());
            context.DocumentDbSet.AddRange(testData.GetAll<Document>());
            context.SumOnDateDbSet.AddRange(testData.GetAll<SumOnDate>());
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
