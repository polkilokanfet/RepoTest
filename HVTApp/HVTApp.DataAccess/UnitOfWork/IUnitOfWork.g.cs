using System;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public partial interface IUnitOfWork
    {
        IAdditionalSalesUnitsRepository AdditionalSalesUnitsRepository { get; }
        IAddressRepository AddressRepository { get; }
        ILocalityRepository LocalityRepository { get; }
        ILocalityTypeRepository LocalityTypeRepository { get; }
        IRegionRepository RegionRepository { get; }
        IDistrictRepository DistrictRepository { get; }
        ICountryRepository CountryRepository { get; }
        IBankDetailsRepository BankDetailsRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        ICompanyFormRepository CompanyFormRepository { get; }
        IDocumentsRegistrationDetailsRepository DocumentsRegistrationDetailsRepository { get; }
        IEmployeesPositionRepository EmployeesPositionRepository { get; }
        IFacilityTypeRepository FacilityTypeRepository { get; }
        IActivityFieldRepository ActivityFieldRepository { get; }
        IContractRepository ContractRepository { get; }
        IMeasureRepository MeasureRepository { get; }
        IParameterRepository ParameterRepository { get; }
        IParameterGroupRepository ParameterGroupRepository { get; }
        IPartRepository PartRepository { get; }
        IProductsRelationRepository ProductsRelationRepository { get; }
        IStandartPaymentConditionsRepository StandartPaymentConditionsRepository { get; }
        IPersonRepository PersonRepository { get; }
        IPaymentPlannedRepository PaymentPlannedRepository { get; }
        IPaymentActualRepository PaymentActualRepository { get; }
        IRequiredPreviousParametersRepository RequiredPreviousParametersRepository { get; }
        IProjectUnitRepository ProjectUnitRepository { get; }
        ITenderUnitRepository TenderUnitRepository { get; }
        IShipmentUnitRepository ShipmentUnitRepository { get; }
        IProductionUnitRepository ProductionUnitRepository { get; }
        ISalesUnitRepository SalesUnitRepository { get; }
        ITestFriendAddressRepository TestFriendAddressRepository { get; }
        ITestFriendRepository TestFriendRepository { get; }
        ITestFriendEmailRepository TestFriendEmailRepository { get; }
        ITestFriendGroupRepository TestFriendGroupRepository { get; }
        IDocumentRepository DocumentRepository { get; }
        ITestEntityRepository TestEntityRepository { get; }
        ITestHusbandRepository TestHusbandRepository { get; }
        ITestWifeRepository TestWifeRepository { get; }
        ITestChildRepository TestChildRepository { get; }
        ICostOnDateRepository CostOnDateRepository { get; }
        ICostRepository CostRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        IExchangeCurrencyRateRepository ExchangeCurrencyRateRepository { get; }
        IProductRepository ProductRepository { get; }
        IOfferRepository OfferRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IOrderRepository OrderRepository { get; }
        IPaymentConditionRepository PaymentConditionRepository { get; }
        IPaymentDocumentRepository PaymentDocumentRepository { get; }
        IFacilityRepository FacilityRepository { get; }
        IProjectRepository ProjectRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        ISpecificationRepository SpecificationRepository { get; }
        ITenderRepository TenderRepository { get; }
        ITenderTypeRepository TenderTypeRepository { get; }
        IUserRepository UserRepository { get; }
        IOfferUnitRepository OfferUnitRepository { get; }
    }
}
