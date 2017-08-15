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

        public ProjectUnit ProjectUnitVeb1101;
        public ProjectUnit ProjectUnitVeb1102;
        public ProjectUnit ProjectUnitZng1101;
        public ProjectUnit ProjectUnitZng1102;
        public ProjectUnit ProjectUnitZng1103;

        public ProductionUnit ProductionUnitVeb1101;
        public ProductionUnit ProductionUnitVeb1102;
        public ProductionUnit ProductionUnitZng1101;
        public ProductionUnit ProductionUnitZng1102;
        public ProductionUnit ProductionUnitZng1103;

        public SalesUnit SalesUnitVeb1101;
        public SalesUnit SalesUnitVeb1102;
        public SalesUnit SalesUnitZng1101;
        public SalesUnit SalesUnitZng1102;
        public SalesUnit SalesUnitZng1103;

        public ShipmentUnit ShipmentUnitVeb1101;
        public ShipmentUnit ShipmentUnitVeb1102;
        public ShipmentUnit ShipmentUnitZng1101;
        public ShipmentUnit ShipmentUnitZng1102;
        public ShipmentUnit ShipmentUnitZng1103;

        public OfferUnit OfferUnitVeb1101;
        public OfferUnit OfferUnitVeb1102;
        public OfferUnit OfferUnitZng1101;
        public OfferUnit OfferUnitZng1102;
        public OfferUnit OfferUnitZng1103;

        public Offer OfferMrsk;


        public Document DocumentOfferMrsk;

        public Order OrderVeb110;
        public Order OrderZng110;

        public Contract ContractMrsk;

        public Specification SpecificationMrsk1;

        public TenderType TenderTypeProject;

        public Tender TenderMrsk;

        public TenderUnit TenderUnitVeb1101;
        public TenderUnit TenderUnitVeb1102;
        public TenderUnit TenderUnitZng1101;
        public TenderUnit TenderUnitZng1102;
        public TenderUnit TenderUnitZng1103;

        public PaymentCondition PaymentConditionAvans50;
        public PaymentCondition PaymentConditionDoplata50;

        public List<PaymentCondition> StandartPaymentConditions;

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
            GenerateParts();
            GenerateProduct();
            GenerateContracts();
            GenerateSpecifications();
            GenerateTenderTypes();
            GenerateProductionUnit();
            GenerateSalesUnits();
            GenerateShippmentUnits();
            GenerateOffers();
            GenerateOfferUnits();
            GenerateOrders();
            GenerateTenders();
            GenerateTanderUnits();
            GenerateProjectUnits();
            GeneratePaymentConditions();
            GenerateDocuments();
        }

        private void GenerateCompanyForms()
        {
            CompanyFormAo = new CompanyForm {FullName = "Акционерное общество", ShortName = "АО"};
            CompanyFormPao = new CompanyForm {FullName = "Публичное акционерное общество", ShortName = "ПАО"};
            CompanyFormOao = new CompanyForm {FullName = "Открытое акционерное общество", ShortName = "ОАО"};
            CompanyFormZao = new CompanyForm {FullName = "Закрытое акционерное общество", ShortName = "ЗАО"};
        }

        private void GenerateActivityFields()
        {
            ActivityFieldProducerOfHvt = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ProducerOfHighVoltageEquipment, Name = "Производитель ВВА" };
            ActivityFieldBuilder = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.Builder, Name = "Подрядчик" };
            ActivityFieldElectricityTransmission = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityTransmission, Name = "Передача электроэнергии" };
            ActivityFieldElectricityGeneration = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityGeneration, Name = "Генерация электроэнергии" };
        }

        private void GenerateLocalityTypes()
        {
            LocalityTypeCity = new LocalityType { FullName = "Город", ShortName = "г." };
        }

        private void GenerateLocalities()
        {
            LocalityMoscow = new Locality { LocalityType = LocalityTypeCity, Name = "Москва", Region = RegionMoskovskayaOblast, IsCountryCapital = true, IsDistrictsCapital = true, IsRegionCapital = true };
            LocalityEkaterinburg = new Locality { LocalityType = LocalityTypeCity, Name = "Екатеринбург", Region = RegionSverdlovskayaOblast, IsDistrictsCapital = true, IsRegionCapital = true };
        }

        private void GenerateRegions()
        {
            RegionMoskovskayaOblast = new Region { Name = "Московская область", Localities = new List<Locality> { LocalityMoscow }, District = DistrictCentr};
            RegionSverdlovskayaOblast = new Region { Name = "Свердловская", Localities = new List<Locality> { LocalityEkaterinburg }, District = DistrictUral};
        }

        private void GenerateDistricts()
        {
            DistrictCentr = new District { Country = CountryRussia, Name = "Центральный федеральный округ", Regions = new List<Region>() {RegionMoskovskayaOblast} };
            DistrictUral = new District { Country = CountryRussia, Name = "Уральский федеральный округ", Regions = new List<Region>() {RegionSverdlovskayaOblast} };
        }

        private void GenerateCountries()
        {
            CountryRussia = new Country { Name = "Россия", Districts = new List<District> {DistrictCentr, DistrictUral} };
        }

        private void GenerateAddresses()
        {
            AddressOfUetm = new Address { Description = "ул.Фронтовых бригад, д.22", Locality = LocalityEkaterinburg };
            AddressOfStation = new Address {Description = "ул.Станционная, 5", Locality = LocalityEkaterinburg };
            AddressOfSubstation = new Address {Description = "ул.ПодСтанционная, 25", Locality = LocalityMoscow };
        }

        private void GenerateBankDetails()
        {
            BankDetailsOfUetm = new BankDetails { BankName = "Объебанк", BankIdentificationCode = "1111", CorrespondentAccount = "213564", CheckingAccount = "444554" };
        }

        private void GenerateCompanies()
        {
            CompanyUetm = new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Form = CompanyFormAo, AddressLegal = AddressOfUetm, BankDetailsList = new List<BankDetails> { BankDetailsOfUetm }, ActivityFilds = new List<ActivityField> { ActivityFieldProducerOfHvt } };
            CompanyRosseti = new Company { FullName = "Россети", ShortName = "Россети", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission } };
            CompanyFsk = new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti };
            CompanyMrsk = new Company { FullName = "Межрегиональные распределительные сети", ShortName = "МРСК", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti };
            CompanyEnel = new Company { FullName = "Энел", ShortName = "Энел", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityGeneration } };
        }

        private void GeneratePersons()
        {
            PersonIvanov = new Person { Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", IsMan = true, Employees = new List<Employee>() { } };
        }

        private void GenerateEmployeesPositions()
        {
            EmployeesPositionDirector = new EmployeesPosition { Name = "Директор" };
        }

        private void GenerateEmployees()
        {
            EmployeeIvanov = new Employee { Person = PersonIvanov, Position = EmployeesPositionDirector, Company = CompanyUetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36", IsActual = true};
        }

        private void GenerateUserRoles()
        {
            UserRoleDataBaseFiller = new UserRole { Role = Role.DataBaseFiller, Name = "DataBaseFiller" };
            UserRoleAdmin = new UserRole { Role = Role.Admin, Name = "Admin" };
            UserRoleSalesManager = new UserRole { Role = Role.SalesManager, Name = "SalesManager" };
        }

        private void GenerateUsers()
        {
            UserIvanov = new User { Login = "1", Password = StringToGuidService.GetHashString("1"), Employee = EmployeeIvanov, PersonalNumber = "333", Roles = new List<UserRole> { UserRoleAdmin, UserRoleDataBaseFiller, UserRoleSalesManager } };
        }

        private void GenerateProjects()
        {
            Project1 = new Project { Name = "Реконструкция ПС Первая", Manager = UserIvanov };
            Project2 = new Project { Name = "Строительство Свердловской ТЭЦ", Manager = UserIvanov };
        }

        private void GenerateFacilityTypes()
        {
            FacilityTypeStation = new FacilityType { FullName = "Теплоэлектроцентраль", ShortName = "ТЭЦ" };
            FacilityTypeSubStation = new FacilityType { FullName = "Понизительная станция", ShortName = "ПС" };
        }

        private void GenerateFacilities()
        {
            FacilitySubstation = new Facility { Name = "Первая", Type = FacilityTypeSubStation, OwnerCompany = CompanyMrsk, Address = AddressOfSubstation};
            FacilityStation = new Facility { Name = "Свердловская", Type = FacilityTypeStation, OwnerCompany = CompanyEnel, Address = AddressOfStation };
        }

        private void GenerateMeasures()
        {
            MeasureKv = new Measure { FullName = "Киловольт", ShortName = "кВ" };
        }

        private void GenerateParameterGroups()
        {
            ParameterGroupEqType = new ParameterGroup { Name = "Тип оборудования" };
            ParameterGroupBreakerType = new ParameterGroup { Name = "Тип выключателя" };
            ParameterGroupTransformatorType = new ParameterGroup { Name = "Тип трансформатора" };
            ParameterGroupVoltage = new ParameterGroup { Name = "Номинальное напряжение", Measure = MeasureKv };
        }

        private void GenerateParameters()
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

        private void GenerateRequiredDependentEquipmentsParameters()
        {
            RequiredChildProductParametersDrive = new RequiredDependentEquipmentsParameters { MainProductParameters = new List<Parameter> { ParameterBreaker },
                ChildProductParameters = new List<Parameter> { ParameterBrakersDrive }, Count = 1 };
            RequiredChildProductParametersBreakerBlock = new RequiredDependentEquipmentsParameters { MainProductParameters = new List<Parameter> { ParameterBreakerBlock },
                ChildProductParameters = new List<Parameter> { ParameterBreaker }, Count = 2 };
        }

        private void GenerateParts()
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

        private void GenerateProduct()
        {
            ProductVeb110 = new Product { Designation = "Выключатель баковый ВЭБ-110", Part = PartVeb110, DependentProducts  = new List<Product> {ProductBreakersDrive} };
            ProductZng110 = new Product { Designation = "Трансформатор напряжения ЗНГ-110", Part = PartZng110 };
            ProductBreakersDrive = new Product { Designation = "Привод выключателя", Part = PartBreakesDrive };
        }

        private void GenerateProductionUnit()
        {
            ProductionUnitVeb1101 = new ProductionUnit { Product = ProductVeb110, Order = OrderVeb110, OrderPosition = 1, SerialNumber = "1", SalesUnit = SalesUnitVeb1101, PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7 };
            ProductionUnitVeb1102 = new ProductionUnit { Product = ProductVeb110, Order = OrderVeb110, OrderPosition = 2, SerialNumber = "2", SalesUnit = SalesUnitVeb1102, PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7 };
            ProductionUnitZng1101 = new ProductionUnit { Product = ProductZng110, Order = OrderZng110, OrderPosition = 1, SerialNumber = "5", SalesUnit = SalesUnitZng1101, PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7 };
            ProductionUnitZng1102 = new ProductionUnit { Product = ProductZng110, Order = OrderZng110, OrderPosition = 2, SerialNumber = "6", SalesUnit = SalesUnitZng1102, PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7 };
            ProductionUnitZng1103 = new ProductionUnit { Product = ProductZng110, Order = OrderZng110, OrderPosition = 3, SerialNumber = "7", SalesUnit = SalesUnitZng1103, PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7 };
        }

        private void GenerateSalesUnits()
        {
            SalesUnitVeb1101 = new SalesUnit { ProductionUnit = ProductionUnitVeb1101, OfferUnit = OfferUnitVeb1101, Cost = 6, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, ShipmentUnit = ShipmentUnitVeb1101 };
            SalesUnitVeb1102 = new SalesUnit { ProductionUnit = ProductionUnitVeb1102, OfferUnit = OfferUnitVeb1102, Cost = 6, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, ShipmentUnit = ShipmentUnitVeb1102 }; 
            SalesUnitZng1101 = new SalesUnit { ProductionUnit = ProductionUnitZng1101, OfferUnit = OfferUnitZng1101, Cost = 5, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, ShipmentUnit = ShipmentUnitZng1101 }; 
            SalesUnitZng1102 = new SalesUnit { ProductionUnit = ProductionUnitZng1102, OfferUnit = OfferUnitZng1102, Cost = 5, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, ShipmentUnit = ShipmentUnitZng1102 }; 
            SalesUnitZng1103 = new SalesUnit { ProductionUnit = ProductionUnitZng1103, OfferUnit = OfferUnitZng1103, Cost = 5, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, ShipmentUnit = ShipmentUnitZng1103 }; 
        }

        private void GenerateShippmentUnits()
        {
            ShipmentUnitVeb1101 = new ShipmentUnit { Address = AddressOfSubstation, SalesUnit = SalesUnitVeb1101, Cost = 1, DeliveryDate = DateTime.Today.AddDays(180)};
            ShipmentUnitVeb1102 = new ShipmentUnit { Address = AddressOfSubstation, SalesUnit = SalesUnitVeb1102, Cost = 1, DeliveryDate = DateTime.Today.AddDays(180) }; 
            ShipmentUnitZng1101 = new ShipmentUnit { Address = AddressOfSubstation, SalesUnit = SalesUnitZng1101, Cost = 1, DeliveryDate = DateTime.Today.AddDays(180) }; 
            ShipmentUnitZng1102 = new ShipmentUnit { Address = AddressOfSubstation, SalesUnit = SalesUnitZng1102, Cost = 1, DeliveryDate = DateTime.Today.AddDays(180) }; 
            ShipmentUnitZng1103 = new ShipmentUnit { Address = AddressOfSubstation, SalesUnit = SalesUnitZng1103, Cost = 1, DeliveryDate = DateTime.Today.AddDays(180) }; 
        }

        private void GenerateOfferUnits()
        {
            OfferUnitVeb1101 = new OfferUnit { Product = ProductVeb110, SalesUnit = SalesUnitVeb1101, Cost = 7, ProjectUnit = ProjectUnitVeb1101, Offer = OfferMrsk, TenderUnit = TenderUnitVeb1101, ProductionTerm = 120 };
            OfferUnitVeb1102 = new OfferUnit { Product = ProductVeb110, SalesUnit = SalesUnitVeb1102, Cost = 7, ProjectUnit = ProjectUnitVeb1102, Offer = OfferMrsk, TenderUnit = TenderUnitVeb1102, ProductionTerm = 120 };
            OfferUnitZng1101 = new OfferUnit { Product = ProductZng110, SalesUnit = SalesUnitZng1101, Cost = 3, ProjectUnit = ProjectUnitZng1101, Offer = OfferMrsk, TenderUnit = TenderUnitZng1101, ProductionTerm = 150 };
            OfferUnitZng1102 = new OfferUnit { Product = ProductZng110, SalesUnit = SalesUnitZng1102, Cost = 3, ProjectUnit = ProjectUnitZng1102, Offer = OfferMrsk, TenderUnit = TenderUnitZng1102, ProductionTerm = 150 };
            OfferUnitZng1103 = new OfferUnit { Product = ProductZng110, SalesUnit = SalesUnitZng1103, Cost = 3, ProjectUnit = ProjectUnitZng1103, Offer = OfferMrsk, TenderUnit = TenderUnitZng1103, ProductionTerm = 150 };
        }

        private void GenerateOffers()
        {
            OfferMrsk = new Offer {Project = Project1, Vat = 0.18, Document = DocumentOfferMrsk, OfferUnits = new List<OfferUnit> {OfferUnitVeb1101, OfferUnitVeb1102, OfferUnitZng1101, OfferUnitZng1102, OfferUnitZng1103}, ValidityDate = DateTime.Today.AddDays(60), Tender = TenderMrsk };
        }

        private void GenerateDocuments()
        {
            DocumentOfferMrsk = new Document { Author = EmployeeIvanov, SenderEmployee = EmployeeIvanov, RecipientEmployee = EmployeeIvanov, RegistrationDetailsOfSender = new DocumentsRegistrationDetails { RegistrationNumber = "7412-17-0212", RegistrationDate = DateTime.Today }, RegistrationDetailsOfRecipient = new DocumentsRegistrationDetails { RegistrationNumber = "12f455", RegistrationDate = DateTime.Today.AddDays(-3) } };
        }

        private void GenerateTenderTypes()
        {
            TenderTypeProject = new TenderType { Name = "Проектно-изыскательные работы", Type = TenderTypeEnum.ToProject };
        }

        private void GenerateTenders()
        {
            TenderMrsk = new Tender {Project = Project1, Sum = 9, Type = TenderTypeProject, Winner = CompanyUetm, Participants = new List<Company> {CompanyUetm, CompanyEnel}, Offers = new List<Offer> {OfferMrsk}, DateOpen = DateTime.Today, DateClose = DateTime.Today.AddDays(7), TenderUnits = new List<TenderUnit> {TenderUnitVeb1101, TenderUnitVeb1102, TenderUnitZng1101, TenderUnitZng1102, TenderUnitZng1103} };
        }

        private void GenerateTanderUnits()
        {
            TenderUnitVeb1101 = new TenderUnit { Product = ProductVeb110, Tender = TenderMrsk, Cost = 2, OfferUnits = new List<OfferUnit> { OfferUnitVeb1101 }, ProjectUnit = ProjectUnitVeb1101, ProducerWinner = CompanyUetm, DeliveryDate = DateTime.Today.AddDays(150), PaymentsConditions = StandartPaymentConditions };
            TenderUnitVeb1102 = new TenderUnit { Product = ProductVeb110, Tender = TenderMrsk, Cost = 2, OfferUnits = new List<OfferUnit> { OfferUnitVeb1102 }, ProjectUnit = ProjectUnitVeb1102, ProducerWinner = CompanyUetm, DeliveryDate = DateTime.Today.AddDays(150), PaymentsConditions = StandartPaymentConditions }; 
            TenderUnitZng1101 = new TenderUnit { Product = ProductZng110, Tender = TenderMrsk, Cost = 1, OfferUnits = new List<OfferUnit> { OfferUnitZng1101 }, ProjectUnit = ProjectUnitZng1101, ProducerWinner = CompanyUetm, DeliveryDate = DateTime.Today.AddDays(120), PaymentsConditions = StandartPaymentConditions }; 
            TenderUnitZng1102 = new TenderUnit { Product = ProductZng110, Tender = TenderMrsk, Cost = 1, OfferUnits = new List<OfferUnit> { OfferUnitZng1102 }, ProjectUnit = ProjectUnitZng1102, ProducerWinner = CompanyUetm, DeliveryDate = DateTime.Today.AddDays(120), PaymentsConditions = StandartPaymentConditions }; 
            TenderUnitZng1103 = new TenderUnit { Product = ProductZng110, Tender = TenderMrsk, Cost = 1, OfferUnits = new List<OfferUnit> { OfferUnitZng1103 }, ProjectUnit = ProjectUnitZng1103, ProducerWinner = CompanyUetm, DeliveryDate = DateTime.Today.AddDays(120), PaymentsConditions = StandartPaymentConditions }; 
        }

        private void GenerateOrders()
        {
            OrderVeb110 = new Order { Number = "8012-17", OpenOrderDate = DateTime.Today, ProductionUnits = new List<ProductionUnit> { ProductionUnitVeb1101, ProductionUnitVeb1102 } };
            OrderVeb110 = new Order { Number = "8011-15", OpenOrderDate = DateTime.Today.AddDays(-50), ProductionUnits = new List<ProductionUnit> { ProductionUnitZng1101, ProductionUnitZng1102, ProductionUnitZng1103 } };
        }

        private void GenerateProjectUnits()
        {
            ProjectUnitVeb1101 = new ProjectUnit { Product = ProductVeb110, Cost = 5, Project = Project1, Facility = FacilitySubstation };
            ProjectUnitVeb1102 = new ProjectUnit { Product = ProductVeb110, Cost = 5, Project = Project1, Facility = FacilitySubstation };
            ProjectUnitZng1101 = new ProjectUnit { Product = ProductZng110, Cost = 7, Project = Project2, Facility = FacilityStation };
            ProjectUnitZng1102 = new ProjectUnit { Product = ProductZng110, Cost = 7, Project = Project2, Facility = FacilityStation };
            ProjectUnitZng1103 = new ProjectUnit { Product = ProductZng110, Cost = 7, Project = Project2, Facility = FacilityStation };
        }

        private void GenerateContracts()
        {
            ContractMrsk = new Contract { Contragent = CompanyMrsk, Date = DateTime.Today, Number = "0401-17-0001" };
        }

        private void GenerateSpecifications()
        {
            SpecificationMrsk1 = new Specification { Contract = ContractMrsk, Date = ContractMrsk.Date, Number = "1", Vat = 0.18 };
        }

        private void GeneratePaymentConditions()
        {
            PaymentConditionAvans50 = new PaymentCondition { Part = 0.5, DaysToPoint = -10, PaymentConditionPoint = PaymentConditionPoint.ProductionStart };
            PaymentConditionDoplata50 = new PaymentCondition { Part = 0.5, DaysToPoint = -14, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd };

            StandartPaymentConditions = new List<PaymentCondition>() {PaymentConditionAvans50, PaymentConditionDoplata50};
        }
    }
}