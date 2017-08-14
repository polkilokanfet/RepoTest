using System;
using System.Collections.Generic;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Services.StringToGuidService;

namespace HVTApp.TestDataGenerator
{
    public class TestData
    {
        public BankDetails BankDetailsOfUetm;

        public CompanyForm CompanyFormAo;
        public CompanyForm CompanyFormPao;
        public CompanyForm CompanyFormOao;
        public CompanyForm CompanyFormZao;

        public Company CompanyUetm;
        public Company CompanyRosseti;
        public Company CompanyFsk;
        public Company CompanyMrsk;
        public Company CompanyEnel;

        public ActivityField ActivityFieldProducerOfHvt;
        public ActivityField ActivityFieldBuilder;
        public ActivityField ActivityFieldElectricityTransmission;
        public ActivityField ActivityFieldElectricityGeneration;

        public LocalityType LocalityTypeCity;

        public Locality LocalityMoscow;
        public Locality LocalityEkaterinburg;

        public Region RegionSverdlovskayaOblast;
        public Region RegionMoskovskayaOblast;

        public District DistrictCentr;
        public District DistrictUral;

        public Country CountryRussia;

        public Address AddressOfUetm;
        public Address AddressOfStation;
        public Address AddressOfSubstation;

        public Person PersonIvanov;

        public EmployeesPosition EmployeesPositionDirector;

        public Employee EmployeeIvanov;

        public UserRole UserRoleDataBaseFiller;
        public UserRole UserRoleAdmin;
        public UserRole UserRoleSalesManager;

        public User UserIvanov;

        public Project Project1;
        public Project Project2;

        public FacilityType FacilityTypeStation;
        public FacilityType FacilityTypeSubStation;

        public Facility FacilityStation;
        public Facility FacilitySubstation;

        public Measure MeasureKv;

        public ParameterGroup ParameterGroupEqType;
        public ParameterGroup ParameterGroupBreakerType;
        public ParameterGroup ParameterGroupTransformatorType;
        public ParameterGroup ParameterGroupVoltage;
        public ParameterGroup ParameterGroupBrakersDrive;

        public Parameter ParameterBreaker;
        public Parameter ParameterTransformator;
        public Parameter ParameterBrakersDrive;
        public Parameter ParameterBreakerBlock;
        public Parameter ParameterBreakerDeadTank;
        public Parameter ParameterBreakerLiveTank;
        public Parameter ParameterTransformatorCurrent;
        public Parameter ParameterTransformatorVoltage;
        public Parameter ParameterVoltage35kV;
        public Parameter ParameterVoltage110kV;
        public Parameter ParameterVoltage220kV;
        public Parameter ParameterVoltage500kV;

        public RequiredDependentEquipmentsParameters RequiredChildProductParametersDrive;
        public RequiredDependentEquipmentsParameters RequiredChildProductParametersBreakerBlock;

        public Part PartZng110;
        public Part PartVgb35;
        public Part PartVeb110;
        public Part PartBreakesDrive;

        public Product ProductVeb110;
        public Product ProductZng110;
        public Product ProductBreakersDrive;

        public Contract ContractMrsk;

        public Specification SpecificationMrsk1;

        public TenderType TenderTypeProject;

        public TestData()
        {
            ReGenerateAll();
        }

        public void ReGenerateAll()
        {
            GenerateCompanyForms();
            GenerateActivityFields();
            GenerateLocalityTypes();
            GenerateLocalities();
            GenerateRegions();
            GenerateDistricts();
            GenerateCountries();
            GenerateAddresses();
            GenerateBankDetails();
            GenerateCompanies();
            GeneratePersons();
            GenerateEmployeesPositions();
            GenerateEmployees();
            GenerateUserRoles();
            GenerateUsers();
            GenerateProjects();
            GenerateFacilityTypes();
            GenerateFacilities();
            GenerateMeasures();
            GenerateParameterGroups();
            GenerateParameters();
            GenerateRequiredDependentEquipmentsParameters();
            GenerateProducts();
            GenerateEquipments();
            GenerateContracts();
            GenerateSpecifications();
            GenerateTenderTypes();
        }

    public void GenerateCompanyForms()
        {
            CompanyFormAo = new CompanyForm {FullName = "Акционерное общество", ShortName = "АО"};
            CompanyFormPao = new CompanyForm {FullName = "Публичное акционерное общество", ShortName = "ПАО"};
            CompanyFormOao = new CompanyForm {FullName = "Открытое акционерное общество", ShortName = "ОАО"};
            CompanyFormZao = new CompanyForm {FullName = "Закрытое акционерное общество", ShortName = "ЗАО"};
        }

        public void GenerateActivityFields()
        {
            ActivityFieldProducerOfHvt = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ProducerOfHighVoltageEquipment, Name = "Производитель ВВА" };
            ActivityFieldBuilder = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.Builder, Name = "Подрядчик" };
            ActivityFieldElectricityTransmission = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityTransmission, Name = "Передача электроэнергии" };
            ActivityFieldElectricityGeneration = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityGeneration, Name = "Генерация электроэнергии" };
        }

        public void GenerateLocalityTypes()
        {
            LocalityTypeCity = new LocalityType { FullName = "Город", ShortName = "г." };
        }

        public void GenerateLocalities()
        {
            LocalityMoscow = new Locality { LocalityType = LocalityTypeCity, Name = "Москва", Region = RegionMoskovskayaOblast, IsCountryCapital = true, IsDistrictsCapital = true, IsRegionCapital = true };
            LocalityEkaterinburg = new Locality { LocalityType = LocalityTypeCity, Name = "Екатеринбург", Region = RegionSverdlovskayaOblast, IsDistrictsCapital = true, IsRegionCapital = true };
        }

        public void GenerateRegions()
        {
            RegionMoskovskayaOblast = new Region { Name = "Московская область", Localities = new List<Locality> { LocalityMoscow }, District = DistrictCentr};
            RegionSverdlovskayaOblast = new Region { Name = "Свердловская", Localities = new List<Locality> { LocalityEkaterinburg }, District = DistrictUral};
        }

        public void GenerateDistricts()
        {
            DistrictCentr = new District { Country = CountryRussia, Name = "Центральный федеральный округ", Regions = new List<Region>() {RegionMoskovskayaOblast} };
            DistrictUral = new District { Country = CountryRussia, Name = "Уральский федеральный округ", Regions = new List<Region>() {RegionSverdlovskayaOblast} };
        }

        public void GenerateCountries()
        {
            CountryRussia = new Country { Name = "Россия", Districts = new List<District> {DistrictCentr, DistrictUral} };
        }

        public void GenerateAddresses()
        {
            AddressOfUetm = new Address { Description = "ул.Фронтовых бригад, д.22", Locality = LocalityEkaterinburg };
            AddressOfStation = new Address {Description = "ул.Станционная, 5", Locality = LocalityEkaterinburg };
            AddressOfSubstation = new Address {Description = "ул.ПодСтанционная, 25", Locality = LocalityMoscow };
        }

        public void GenerateBankDetails()
        {
            BankDetailsOfUetm = new BankDetails { BankName = "Объебанк", BankIdentificationCode = "1111", CorrespondentAccount = "213564", CheckingAccount = "444554" };
        }

        public void GenerateCompanies()
        {
            CompanyUetm = new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Form = CompanyFormAo, AddressLegal = AddressOfUetm, BankDetailsList = new List<BankDetails> { BankDetailsOfUetm }, ActivityFilds = new List<ActivityField> { ActivityFieldProducerOfHvt } };
            CompanyRosseti = new Company { FullName = "Россети", ShortName = "Россети", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission } };
            CompanyFsk = new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti };
            CompanyMrsk = new Company { FullName = "Межрегиональные распределительные сети", ShortName = "МРСК", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti };
            CompanyEnel = new Company { FullName = "Энел", ShortName = "Энел", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityGeneration } };
        }

        public void GeneratePersons()
        {
            PersonIvanov = new Person { Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", IsMan = true, Employees = new List<Employee>() { } };
        }

        public void GenerateEmployeesPositions()
        {
            EmployeesPositionDirector = new EmployeesPosition { Name = "Директор" };
        }

        public void GenerateEmployees()
        {
            EmployeeIvanov = new Employee { Person = PersonIvanov, Position = EmployeesPositionDirector, Company = CompanyUetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36", IsActual = true};
        }

        public void GenerateUserRoles()
        {
            UserRoleDataBaseFiller = new UserRole { Role = Role.DataBaseFiller, Name = "DataBaseFiller" };
            UserRoleAdmin = new UserRole { Role = Role.Admin, Name = "Admin" };
            UserRoleSalesManager = new UserRole { Role = Role.SalesManager, Name = "SalesManager" };
        }

        public void GenerateUsers()
        {
            UserIvanov = new User { Login = "1", Password = StringToGuidService.GetHashString("1"), Employee = EmployeeIvanov, PersonalNumber = "333", Roles = new List<UserRole> { UserRoleAdmin, UserRoleDataBaseFiller, UserRoleSalesManager } };
        }

        public void GenerateProjects()
        {
            Project1 = new Project { Name = "Реконструкция ПС Первая", Manager = UserIvanov };
            Project2 = new Project { Name = "Строительство Свердловской ТЭЦ", Manager = UserIvanov };
        }

        public void GenerateFacilityTypes()
        {
            FacilityTypeStation = new FacilityType { FullName = "Теплоэлектроцентраль", ShortName = "ТЭЦ" };
            FacilityTypeSubStation = new FacilityType { FullName = "Понизительная станция", ShortName = "ПС" };
        }

        public void GenerateFacilities()
        {
            FacilitySubstation = new Facility { Name = "Первая", Type = FacilityTypeSubStation, OwnerCompany = CompanyMrsk, Address = AddressOfSubstation};
            FacilityStation = new Facility { Name = "Свердловская", Type = FacilityTypeStation, OwnerCompany = CompanyEnel, Address = AddressOfStation };
        }

        public void GenerateMeasures()
        {
            MeasureKv = new Measure { FullName = "Киловольт", ShortName = "кВ" };
        }

        public void GenerateParameterGroups()
        {
            ParameterGroupEqType = new ParameterGroup { Name = "Тип оборудования" };
            ParameterGroupBreakerType = new ParameterGroup { Name = "Тип выключателя" };
            ParameterGroupTransformatorType = new ParameterGroup { Name = "Тип трансформатора" };
            ParameterGroupVoltage = new ParameterGroup { Name = "Номинальное напряжение", Measure = MeasureKv };
        }

        public void GenerateParameters()
        {
            ParameterBreaker = new Parameter { Group = ParameterGroupEqType, Value = "Выключатель" };
            ParameterTransformator = new Parameter { Group = ParameterGroupEqType, Value = "Трансформатор" };
            ParameterBrakersDrive = new Parameter { Group = ParameterGroupEqType, Value = "Привод" };
            ParameterBreakerBlock = new Parameter { Group = ParameterGroupEqType, Value = "Блок выключателя" };

            ParameterBreakerDeadTank = new Parameter { Group = ParameterGroupBreakerType, Value = "Баковый" }.AddRequiredPreviousParameters(new []{ParameterBreaker});
            ParameterBreakerLiveTank = new Parameter { Group = ParameterGroupBreakerType, Value = "Колонковый" }.AddRequiredPreviousParameters(new[] { ParameterBreaker });

            ParameterTransformatorCurrent = new Parameter { Group = ParameterGroupTransformatorType, Value = "Тока" }.AddRequiredPreviousParameters(new[] { ParameterTransformator });
            ParameterTransformatorVoltage = new Parameter { Group = ParameterGroupTransformatorType, Value = "Напряжения" }.AddRequiredPreviousParameters(new[] { ParameterTransformator });

            ParameterVoltage35kV = new Parameter { Group = ParameterGroupVoltage, Value = "35" }.AddRequiredPreviousParameters(new []{ParameterBreaker})
                .AddRequiredPreviousParameters(new []{ParameterTransformator, ParameterTransformatorCurrent});
            ParameterVoltage110kV = new Parameter { Group = ParameterGroupVoltage, Value = "110" }.AddRequiredPreviousParameters(new[] { ParameterBreaker })
                .AddRequiredPreviousParameters(new[] { ParameterTransformator }); 
            ParameterVoltage220kV = new Parameter { Group = ParameterGroupVoltage, Value = "220" }.AddRequiredPreviousParameters(new[] { ParameterBreaker })
                .AddRequiredPreviousParameters(new[] { ParameterTransformator }); 
            ParameterVoltage500kV = new Parameter { Group = ParameterGroupVoltage, Value = "500" }.AddRequiredPreviousParameters(new[] { ParameterBreaker, ParameterBreakerLiveTank });

        }

        public void GenerateRequiredDependentEquipmentsParameters()
        {
            RequiredChildProductParametersDrive = new RequiredDependentEquipmentsParameters { MainProductParameters = new List<Parameter> { ParameterBreaker },
                ChildProductParameters = new List<Parameter> { ParameterBrakersDrive }, Count = 1 };
            RequiredChildProductParametersBreakerBlock = new RequiredDependentEquipmentsParameters { MainProductParameters = new List<Parameter> { ParameterBreakerBlock },
                ChildProductParameters = new List<Parameter> { ParameterBreaker }, Count = 2 };
        }

        public void GenerateProducts()
        {
            PartZng110 = new Part { Designation = "ЗНГ-110", Parameters = new List<Parameter> { ParameterTransformator, ParameterTransformatorVoltage, ParameterVoltage110kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 75 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber1"};
            PartVgb35 = new Part { Designation = "ВГБ-35", Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage35kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 50 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber2" };
            PartVeb110 = new Part { Designation = "ВЭБ-110", Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage110kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 100 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber3" };
            PartBreakesDrive = new Part { Designation = "Привод выключателя", Parameters = new List<Parameter> { ParameterBreaker },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 100 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber4" };
        }

        public void GenerateEquipments()
        {
            ProductVeb110 = new Product { Designation = "Выключатель баковый ВЭБ-110", Part = PartVeb110, DependentProducts  = new List<Product> {ProductBreakersDrive} };
            ProductZng110 = new Product { Designation = "Трансформатор напряжения ЗНГ-110", Part = PartZng110 };
            ProductBreakersDrive = new Product { Designation = "Привод выключателя", Part = PartBreakesDrive };
        }

        public void GenerateContracts()
        {
            ContractMrsk = new Contract { Contragent = CompanyMrsk, Date = DateTime.Today, Number = "0401-17-0001" };
        }

        public void GenerateSpecifications()
        {
            SpecificationMrsk1 = new Specification { Contract = ContractMrsk, Date = ContractMrsk.Date, Number = "1", Vat = 0.18 };
        }

        public void GenerateTenderTypes()
        {
            TenderTypeProject = new TenderType { Name = "Проектно-изыскательные работы", Type = TenderTypeEnum.ToProject };
        }
    }
}