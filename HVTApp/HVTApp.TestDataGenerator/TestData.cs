using System;
using System.Collections.Generic;
using System.Reflection;
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
                
        public EmployeesPosition EmployeesPositionDirector;
                
        public Person PersonIvanov;
        public Person PersonPetrov;
        public Person PersonSidorov;
                
        public Employee EmployeeIvanov;
        public Employee EmployeePetrov;
        public Employee EmployeeSidorov;
                
        public UserRole UserRoleDataBaseFiller;
        public UserRole UserRoleAdmin;
        public UserRole UserRoleSalesManager;
                
        public User UserIvanov;
                
        public Project Project1;
        public Project Project2;
        //public Project Project3;

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
                
        public RequiredDependentProductsParameters RequiredChildProductParametersDrive;
        public RequiredDependentProductsParameters RequiredChildProductParametersBreakerBlock;
                
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
        public ProjectUnit ProjectUnitZng1104;
        public ProjectUnit ProjectUnitZng1105;
        public ProjectUnit ProjectUnitZng1106;

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
            var fields = this.GetType().GetFields();
            foreach (var fieldInfo in fields)
            {
                fieldInfo.SetValue(this, Activator.CreateInstance(fieldInfo.FieldType));
            }

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
            GenerateProducts();
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
            CompanyFormAo.Clone(new CompanyForm {FullName = "Акционерное общество", ShortName = "АО"});
            CompanyFormPao.Clone(new CompanyForm {FullName = "Публичное акционерное общество", ShortName = "ПАО"});
            CompanyFormOao.Clone(new CompanyForm {FullName = "Открытое акционерное общество", ShortName = "ОАО"});
            CompanyFormZao.Clone(new CompanyForm {FullName = "Закрытое акционерное общество", ShortName = "ЗАО"});
        }

        private void GenerateActivityFields()
        {
            ActivityFieldProducerOfHvt.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ProducerOfHighVoltageEquipment, Name = "Производитель ВВА" });
            ActivityFieldBuilder.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.Builder, Name = "Подрядчик" });
            ActivityFieldElectricityTransmission.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityTransmission, Name = "Передача электроэнергии" });
            ActivityFieldElectricityGeneration.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityGeneration, Name = "Генерация электроэнергии" });
        }

        private void GenerateLocalityTypes()
        {
            LocalityTypeCity.Clone(new LocalityType { FullName = "Город", ShortName = "г." });
        }

        private void GenerateLocalities()
        {
            LocalityMoscow.Clone(new Locality { LocalityType = LocalityTypeCity, Name = "Москва", Region = RegionMoskovskayaOblast, IsCountryCapital = true, IsDistrictsCapital = true, IsRegionCapital = true });
            LocalityEkaterinburg.Clone(new Locality { LocalityType = LocalityTypeCity, Name = "Екатеринбург", Region = RegionSverdlovskayaOblast, IsDistrictsCapital = true, IsRegionCapital = true });
        }

        private void GenerateRegions()
        {
            RegionMoskovskayaOblast.Clone(new Region { Name = "Московская область", Localities= new List<Locality> { LocalityMoscow }, District = DistrictCentr });
            RegionSverdlovskayaOblast.Clone(new Region { Name = "Свердловская область", Localities= new List<Locality> { LocalityEkaterinburg }, District = DistrictUral });
        }

        private void GenerateDistricts()
        {
            DistrictCentr.Clone(new District { Country = CountryRussia, Name = "Центральный федеральный округ", Regions= new List<Region>() {RegionMoskovskayaOblast} });
            DistrictUral.Clone(new District { Country = CountryRussia, Name = "Уральский федеральный округ", Regions= new List<Region>() {RegionSverdlovskayaOblast} });
        }

        private void GenerateCountries()
        {
            CountryRussia.Clone(new Country { Name = "Россия", Districts= new List<District> {DistrictCentr, DistrictUral} });
        }

        private void GenerateAddresses()
        {
            AddressOfUetm.Clone(new Address { Description = "ул.Фронтовых бригад, д.22", Locality = LocalityEkaterinburg });
            AddressOfStation.Clone(new Address {Description = "ул.Станционная, 5", Locality = LocalityEkaterinburg });
            AddressOfSubstation.Clone(new Address {Description = "ул.ПодСтанционная, 25", Locality = LocalityMoscow });
        }

        private void GenerateBankDetails()
        {
            BankDetailsOfUetm.Clone(new BankDetails { BankName = "Объебанк", BankIdentificationCode = "1111", CorrespondentAccount = "213564", CheckingAccount = "444554" });
        }

        private void GenerateCompanies()
        {
            CompanyUetm.Clone(new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Form = CompanyFormAo, AddressLegal = AddressOfUetm, BankDetailsList= new List<BankDetails> { BankDetailsOfUetm }, ActivityFilds= new List<ActivityField> { ActivityFieldProducerOfHvt } });
            CompanyRosseti.Clone(new Company { FullName = "Россети", ShortName = "Россети", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> { ActivityFieldElectricityTransmission } });
            CompanyFsk.Clone(new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti });
            CompanyMrsk.Clone(new Company { FullName = "Межрегиональные распределительные сети", ShortName = "МРСК", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti });
            CompanyEnel.Clone(new Company { FullName = "Энел", ShortName = "Энел", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> { ActivityFieldElectricityGeneration } });
        }

        private void GeneratePersons()
        {
            PersonIvanov.Clone(new Person { Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", IsMan = true, Employees= new List<Employee> { EmployeeIvanov } });
            PersonPetrov.Clone(new Person { Surname = "Петров", Name = "Иван", Patronymic = "Иванович", IsMan = true, Employees= new List<Employee> { EmployeePetrov } });
            PersonSidorov.Clone(new Person { Surname = "Сидоров", Name = "Иван", Patronymic = "Иванович", IsMan = true, Employees= new List<Employee> { EmployeeSidorov } });
        }

        private void GenerateEmployeesPositions()
        {
            EmployeesPositionDirector.Clone(new EmployeesPosition { Name = "Директор" });
        }

        private void GenerateEmployees()
        {
            EmployeeIvanov.Clone(new Employee { Person = PersonIvanov, Position = EmployeesPositionDirector, Company = CompanyUetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36", IsActual = true });
            EmployeePetrov.Clone(new Employee { Person = PersonPetrov, Position = EmployeesPositionDirector, Company = CompanyFsk, Email = "iii@mail.ru", PhoneNumber = "326-36-36", IsActual = true });
            EmployeeSidorov.Clone(new Employee { Person = PersonSidorov, Position = EmployeesPositionDirector, Company = CompanyEnel, Email = "iii@mail.ru", PhoneNumber = "326-36-36", IsActual = true });
        }

        private void GenerateUserRoles()
        {
            UserRoleDataBaseFiller.Clone(new UserRole { Role = Role.DataBaseFiller, Name = "DataBaseFiller" });
            UserRoleAdmin.Clone(new UserRole { Role = Role.Admin, Name = "Admin" });
            UserRoleSalesManager.Clone(new UserRole { Role = Role.SalesManager, Name = "SalesManager" });
        }

        private void GenerateUsers()
        {
            UserIvanov.Clone(new User { Login = "1", Password = StringToGuidService.GetHashString("1"), Employee = EmployeeIvanov, PersonalNumber = "333", Roles= new List<UserRole> { UserRoleAdmin, UserRoleDataBaseFiller, UserRoleSalesManager } });
        }

        private void GenerateProjects()
        {
            Project1.Clone(new Project { Name = "Реконструкция ПС Первая", Manager = UserIvanov, Offers= new List<Offer> {OfferMrsk}, ProjectUnits = new List<ProjectUnit> {ProjectUnitVeb1101, ProjectUnitVeb1102, ProjectUnitZng1101, ProjectUnitZng1102, ProjectUnitZng1103}, Tenders = new List<Tender> {TenderMrsk} });
            Project2.Clone(new Project { Name = "Строительство Свердловской ТЭЦ", Manager = UserIvanov, ProjectUnits = new List<ProjectUnit> { ProjectUnitZng1104, ProjectUnitZng1105, ProjectUnitZng1106 } });
        }

        private void GenerateFacilityTypes()
        {
            FacilityTypeStation.Clone(new FacilityType { FullName = "Теплоэлектроцентраль", ShortName = "ТЭЦ" });
            FacilityTypeSubStation.Clone(new FacilityType { FullName = "Понизительная станция", ShortName = "ПС" });
        }

        private void GenerateFacilities()
        {
            FacilitySubstation.Clone(new Facility { Name = "Первая", Type = FacilityTypeSubStation, OwnerCompany = CompanyMrsk, Address = AddressOfSubstation});
            FacilityStation.Clone(new Facility { Name = "Свердловская", Type = FacilityTypeStation, OwnerCompany = CompanyEnel, Address = AddressOfStation });
        }

        private void GenerateMeasures()
        {
            MeasureKv.Clone(new Measure { FullName = "Киловольт", ShortName = "кВ" });
        }

        private void GenerateParameterGroups()
        {
            ParameterGroupEqType.Clone(new ParameterGroup { Name = "Тип оборудования" });
            ParameterGroupEqType.Parameters.AddRange(new []{ParameterBreaker, ParameterTransformator, ParameterBrakersDrive, ParameterBreakerBlock});

            ParameterGroupBreakerType.Clone(new ParameterGroup { Name = "Тип выключателя" });
            ParameterGroupBreakerType.Parameters.AddRange(new []{ParameterBreakerDeadTank, ParameterBreakerLiveTank} );

            ParameterGroupTransformatorType.Clone(new ParameterGroup { Name = "Тип трансформатора" });
            ParameterGroupTransformatorType.Parameters.AddRange(new []{ParameterTransformatorCurrent, ParameterTransformatorVoltage});

            ParameterGroupVoltage.Clone(new ParameterGroup { Name = "Номинальное напряжение", Measure = MeasureKv });
            ParameterGroupVoltage.Parameters.AddRange(new []{ParameterVoltage35kV, ParameterVoltage110kV, ParameterVoltage220kV, ParameterVoltage500kV});
        }

        private void GenerateParameters()
        {
            ParameterBreaker.Clone(new Parameter { Group = ParameterGroupEqType, Value = "Выключатель" });
            ParameterTransformator.Clone(new Parameter { Group = ParameterGroupEqType, Value = "Трансформатор" });
            ParameterBrakersDrive.Clone(new Parameter { Group = ParameterGroupEqType, Value = "Привод" });
            ParameterBreakerBlock.Clone(new Parameter { Group = ParameterGroupEqType, Value = "Блок выключателя" });

            ParameterBreakerDeadTank.Clone(new Parameter { Group = ParameterGroupBreakerType, Value = "Баковый" });
            ParameterBreakerDeadTank.AddRequiredPreviousParameters(new []{ParameterBreaker});
            ParameterBreakerLiveTank.Clone(new Parameter { Group = ParameterGroupBreakerType, Value = "Колонковый" });
            ParameterBreakerLiveTank.AddRequiredPreviousParameters(new[] { ParameterBreaker });

            ParameterTransformatorCurrent.Clone(new Parameter { Group = ParameterGroupTransformatorType, Value = "Тока" });
            ParameterTransformatorCurrent.AddRequiredPreviousParameters(new[] { ParameterTransformator });
            ParameterTransformatorVoltage.Clone(new Parameter { Group = ParameterGroupTransformatorType, Value = "Напряжения" });
            ParameterTransformatorVoltage.AddRequiredPreviousParameters(new[] { ParameterTransformator });

            ParameterVoltage35kV.Clone(new Parameter { Group = ParameterGroupVoltage, Value = "35" });
            ParameterVoltage35kV.AddRequiredPreviousParameters(new []{ParameterBreaker})
                                .AddRequiredPreviousParameters(new []{ParameterTransformator, ParameterTransformatorCurrent});
            ParameterVoltage110kV.Clone(new Parameter { Group = ParameterGroupVoltage, Value = "110" });
            ParameterVoltage110kV.AddRequiredPreviousParameters(new[] { ParameterBreaker })
                                 .AddRequiredPreviousParameters(new[] { ParameterTransformator }); 
            ParameterVoltage220kV.Clone(new Parameter { Group = ParameterGroupVoltage, Value = "220" });
            ParameterVoltage220kV.AddRequiredPreviousParameters(new[] { ParameterBreaker })
                                 .AddRequiredPreviousParameters(new[] { ParameterTransformator }); 
            ParameterVoltage500kV.Clone(new Parameter { Group = ParameterGroupVoltage, Value = "500" });
            ParameterVoltage500kV.AddRequiredPreviousParameters(new[] { ParameterBreaker, ParameterBreakerLiveTank });

        }

        private void GenerateRequiredDependentEquipmentsParameters()
        {
            RequiredChildProductParametersDrive.Clone(new RequiredDependentProductsParameters { MainProductParameters= new List<Parameter> { ParameterBreaker },
                ChildProductParameters= new List<Parameter> { ParameterBrakersDrive }, Count = 1 });
            RequiredChildProductParametersBreakerBlock.Clone(new RequiredDependentProductsParameters { MainProductParameters= new List<Parameter> { ParameterBreakerBlock },
                ChildProductParameters= new List<Parameter> { ParameterBreaker }, Count = 2 });
        }

        private void GenerateParts()
        {
            PartZng110.Clone(new Part { Designation = "ЗНГ-110", Parameters= new List<Parameter> { ParameterTransformator, ParameterTransformatorVoltage, ParameterVoltage110kV },
                Prices= new List<CostOnDate> { new CostOnDate { Cost=75, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber1"});
            PartVgb35.Clone(new Part { Designation = "ВГБ-35", Parameters= new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage35kV },
                Prices= new List<CostOnDate> { new CostOnDate { Cost=50, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber2" });
            PartVeb110.Clone(new Part { Designation = "ВЭБ-110", Parameters= new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage110kV },
                Prices= new List<CostOnDate> { new CostOnDate { Cost=100, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber3" });
            PartBreakesDrive.Clone(new Part { Designation = "Привод выключателя", Parameters= new List<Parameter> { ParameterBreaker },
                Prices= new List<CostOnDate> { new CostOnDate { Cost=100, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber4" });
        }

        private void GenerateProducts()
        {
            ProductVeb110.Clone(new Product { Designation = "Выключатель баковый ВЭБ-110", Part = PartVeb110, DependentProducts = new List<Product> {ProductBreakersDrive} });
            ProductZng110.Clone(new Product { Designation = "Трансформатор напряжения ЗНГ-110", Part = PartZng110 });
            ProductBreakersDrive.Clone(new Product { Designation = "Привод выключателя", Part = PartBreakesDrive });
        }

        private void GenerateProductionUnit()
        {
            ProductionUnitVeb1101.Clone(new ProductionUnit { Product = ProductVeb110, Order = OrderVeb110, OrderPosition = 1, SerialNumber = "1", SalesUnit = SalesUnitVeb1101, PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7 });
            ProductionUnitVeb1102.Clone(new ProductionUnit { Product = ProductVeb110, Order = OrderVeb110, OrderPosition = 2, SerialNumber = "2", SalesUnit = SalesUnitVeb1102, PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7 });

            ProductionUnitZng1101.Clone(new ProductionUnit { Product = ProductZng110, Order = OrderZng110, OrderPosition = 1, SerialNumber = "5", SalesUnit = SalesUnitZng1101, PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7 });
            ProductionUnitZng1102.Clone(new ProductionUnit { Product = ProductZng110, Order = OrderZng110, OrderPosition = 2, SerialNumber = "6", SalesUnit = SalesUnitZng1102, PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7 });
            ProductionUnitZng1103.Clone(new ProductionUnit { Product = ProductZng110, Order = OrderZng110, OrderPosition = 3, SerialNumber = "7", SalesUnit = SalesUnitZng1103, PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7 });
        }

        private void GenerateSalesUnits()
        {
            SalesUnitVeb1101.Clone(new SalesUnit { ProductionUnit = ProductionUnitVeb1101, OfferUnit = OfferUnitVeb1101, Cost = 6, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, ShipmentUnit = ShipmentUnitVeb1101 });
            SalesUnitVeb1102.Clone(new SalesUnit { ProductionUnit = ProductionUnitVeb1102, OfferUnit = OfferUnitVeb1102, Cost = 6, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, ShipmentUnit = ShipmentUnitVeb1102 }); 

            SalesUnitZng1101.Clone(new SalesUnit { ProductionUnit = ProductionUnitZng1101, OfferUnit = OfferUnitZng1101, Cost = 5, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, ShipmentUnit = ShipmentUnitZng1101 }); 
            SalesUnitZng1102.Clone(new SalesUnit { ProductionUnit = ProductionUnitZng1102, OfferUnit = OfferUnitZng1102, Cost = 5, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, ShipmentUnit = ShipmentUnitZng1102 }); 
            SalesUnitZng1103.Clone(new SalesUnit { ProductionUnit = ProductionUnitZng1103, OfferUnit = OfferUnitZng1103, Cost = 5, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, ShipmentUnit = ShipmentUnitZng1103 }); 
        }

        private void GenerateShippmentUnits()
        {
            ShipmentUnitVeb1101.Clone(new ShipmentUnit { SalesUnit = SalesUnitVeb1101, Address = AddressOfSubstation, Cost = 1, DeliveryDate = DateTime.Today.AddDays(180) });
            ShipmentUnitVeb1102.Clone(new ShipmentUnit { SalesUnit = SalesUnitVeb1102, Address = AddressOfSubstation, Cost = 1, DeliveryDate = DateTime.Today.AddDays(180) }); 

            ShipmentUnitZng1101.Clone(new ShipmentUnit { SalesUnit = SalesUnitZng1101, Address = AddressOfSubstation, Cost = 1, DeliveryDate = DateTime.Today.AddDays(180) }); 
            ShipmentUnitZng1102.Clone(new ShipmentUnit { SalesUnit = SalesUnitZng1102, Address = AddressOfSubstation, Cost = 1, DeliveryDate = DateTime.Today.AddDays(180) }); 
            ShipmentUnitZng1103.Clone(new ShipmentUnit { SalesUnit = SalesUnitZng1103, Address = AddressOfSubstation, Cost = 1, DeliveryDate = DateTime.Today.AddDays(180) }); 
        }

        private void GenerateOfferUnits()
        {
            OfferUnitVeb1101.Clone(new OfferUnit { Product = ProductVeb110, Cost = 7, Offer = OfferMrsk, ProductionTerm = 120, PaymentsConditions = StandartPaymentConditions });
            OfferUnitVeb1102.Clone(new OfferUnit { Product = ProductVeb110, Cost = 7, Offer = OfferMrsk, ProductionTerm = 120, PaymentsConditions = StandartPaymentConditions });

            OfferUnitZng1101.Clone(new OfferUnit { Product = ProductZng110, Cost = 3, Offer = OfferMrsk, ProductionTerm = 150, PaymentsConditions = StandartPaymentConditions });
            OfferUnitZng1102.Clone(new OfferUnit { Product = ProductZng110, Cost = 3, Offer = OfferMrsk, ProductionTerm = 150, PaymentsConditions = StandartPaymentConditions });
            OfferUnitZng1103.Clone(new OfferUnit { Product = ProductZng110, Cost = 3, Offer = OfferMrsk, ProductionTerm = 150, PaymentsConditions = StandartPaymentConditions });
        }

        private void GenerateOffers()
        {
            OfferMrsk.Clone(new Offer { Vat = 0.18, Document = DocumentOfferMrsk, OfferUnits= new List<OfferUnit> {OfferUnitVeb1101, OfferUnitVeb1102, OfferUnitZng1101, OfferUnitZng1102, OfferUnitZng1103}, ValidityDate = DateTime.Today.AddDays(60) });
        }

        private void GenerateDocuments()
        {
            DocumentOfferMrsk.Clone(new Document { Author = EmployeeIvanov, SenderEmployee = EmployeeIvanov, RecipientEmployee = EmployeeSidorov, CopyToRecipients = new List<Employee> {EmployeePetrov}, RegistrationDetailsOfSender = new DocumentsRegistrationDetails { RegistrationNumber = "7412-17-0212", RegistrationDate = DateTime.Today }, RegistrationDetailsOfRecipient = new DocumentsRegistrationDetails { RegistrationNumber = "12f455", RegistrationDate = DateTime.Today.AddDays(-3) } });
        }

        private void GenerateTenderTypes()
        {
            TenderTypeProject.Clone(new TenderType { Name = "Проектно-изыскательные работы", Type = TenderTypeEnum.ToProject });
        }

        private void GenerateTenders()
        {
            TenderMrsk.Clone(new Tender { Project = Project1, Sum = 9, Type = TenderTypeProject, Winner = CompanyUetm, Participants = new List<Company> { CompanyUetm, CompanyEnel }, Offers = new List<Offer> { OfferMrsk }, DateOpen = DateTime.Today, DateClose = DateTime.Today.AddDays(7), TenderUnits = new List<TenderUnit> { TenderUnitVeb1101, TenderUnitVeb1102, TenderUnitZng1101, TenderUnitZng1102, TenderUnitZng1103 } });
        }

        private void GenerateTanderUnits()
        {
            TenderUnitVeb1101.Clone(new TenderUnit { Product = ProductVeb110, Tender = TenderMrsk, Cost = 2, ProjectUnit = ProjectUnitVeb1101, ProducerWinner = CompanyUetm, DeliveryDate = DateTime.Today.AddDays(150), PaymentsConditions = StandartPaymentConditions });
            TenderUnitVeb1102.Clone(new TenderUnit { Product = ProductVeb110, Tender = TenderMrsk, Cost = 2, ProjectUnit = ProjectUnitVeb1102, ProducerWinner = CompanyUetm, DeliveryDate = DateTime.Today.AddDays(150), PaymentsConditions = StandartPaymentConditions }); 

            TenderUnitZng1101.Clone(new TenderUnit { Product = ProductZng110, Tender = TenderMrsk, Cost = 1, ProjectUnit = ProjectUnitZng1101, ProducerWinner = CompanyUetm, DeliveryDate = DateTime.Today.AddDays(120), PaymentsConditions = StandartPaymentConditions }); 
            TenderUnitZng1102.Clone(new TenderUnit { Product = ProductZng110, Tender = TenderMrsk, Cost = 1, ProjectUnit = ProjectUnitZng1102, ProducerWinner = CompanyUetm, DeliveryDate = DateTime.Today.AddDays(120), PaymentsConditions = StandartPaymentConditions }); 
            TenderUnitZng1103.Clone(new TenderUnit { Product = ProductZng110, Tender = TenderMrsk, Cost = 1, ProjectUnit = ProjectUnitZng1103, ProducerWinner = CompanyUetm, DeliveryDate = DateTime.Today.AddDays(120), PaymentsConditions = StandartPaymentConditions }); 
        }

        private void GenerateOrders()
        {
            OrderVeb110.Clone(new Order { Number = "8012-17", OpenOrderDate = DateTime.Today, ProductionUnits= new List<ProductionUnit> { ProductionUnitVeb1101, ProductionUnitVeb1102 } });
            OrderZng110.Clone(new Order { Number = "8011-15", OpenOrderDate = DateTime.Today.AddDays(-50), ProductionUnits= new List<ProductionUnit> { ProductionUnitZng1101, ProductionUnitZng1102, ProductionUnitZng1103 } });
        }

        private void GenerateProjectUnits()
        {
            ProjectUnitVeb1101.Clone(new ProjectUnit { Product = ProductVeb110, Cost = 5, Project = Project1, Facility = FacilitySubstation, RequiredDeliveryDate = DateTime.Today.AddDays(200)});
            ProjectUnitVeb1102.Clone(new ProjectUnit { Product = ProductVeb110, Cost = 5, Project = Project1, Facility = FacilitySubstation, RequiredDeliveryDate = DateTime.Today.AddDays(200) });

            ProjectUnitZng1101.Clone(new ProjectUnit { Product = ProductZng110, Cost = 7, Project = Project1, Facility = FacilityStation, RequiredDeliveryDate = DateTime.Today.AddDays(200) });
            ProjectUnitZng1102.Clone(new ProjectUnit { Product = ProductZng110, Cost = 7, Project = Project1, Facility = FacilityStation, RequiredDeliveryDate = DateTime.Today.AddDays(200) });
            ProjectUnitZng1103.Clone(new ProjectUnit { Product = ProductZng110, Cost = 7, Project = Project1, Facility = FacilityStation, RequiredDeliveryDate = DateTime.Today.AddDays(200) });

            ProjectUnitZng1104.Clone(new ProjectUnit { Product = ProductZng110, Cost = 7, Project = Project2, Facility = FacilitySubstation, RequiredDeliveryDate = DateTime.Today.AddDays(200) });
            ProjectUnitZng1105.Clone(new ProjectUnit { Product = ProductZng110, Cost = 7, Project = Project2, Facility = FacilityStation, RequiredDeliveryDate = DateTime.Today.AddDays(200) });
            ProjectUnitZng1106.Clone(new ProjectUnit { Product = ProductZng110, Cost = 7, Project = Project2, Facility = FacilityStation, RequiredDeliveryDate = DateTime.Today.AddDays(200) });
        }

        private void GenerateContracts()
        {
            ContractMrsk.Clone(new Contract { Contragent = CompanyMrsk, Date = DateTime.Today, Number = "0401-17-0001" });
        }

        private void GenerateSpecifications()
        {
            SpecificationMrsk1.Clone(new Specification { Contract = ContractMrsk, Date = ContractMrsk.Date, Number = "1", Vat = 0.18, SalesUnits = new List<SalesUnit> {SalesUnitVeb1101, SalesUnitVeb1102, SalesUnitZng1101, SalesUnitZng1102, SalesUnitZng1103} });
        }

        private void GeneratePaymentConditions()
        {
            PaymentConditionAvans50.Clone(new PaymentCondition { Part = 0.5, DaysToPoint = -10, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            PaymentConditionDoplata50.Clone(new PaymentCondition { Part = 0.5, DaysToPoint = -14, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });

            StandartPaymentConditions.AddRange(new[] { PaymentConditionAvans50, PaymentConditionDoplata50 });
        }
    }
}