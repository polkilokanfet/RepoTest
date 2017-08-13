using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using AutoFixture.AutoEF;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Services.StringToGuidService;
using Ploeh.AutoFixture;

namespace HVTApp.DataAccess
{
    public static class TestDataGenerator
    {
        public static BankDetails BankDetailsOfUetm;

        public static CompanyForm CompanyFormAo;
        public static CompanyForm CompanyFormPao;
        public static CompanyForm CompanyFormOao;
        public static CompanyForm CompanyFormZao;

        public static Company CompanyUetm;
        public static Company CompanyRosseti;
        public static Company CompanyFsk;
        public static Company CompanyMrsk;
        public static Company CompanyEnel;

        public static ActivityField ActivityFieldProducerOfHvt;
        public static ActivityField ActivityFieldBuilder;
        public static ActivityField ActivityFieldElectricityTransmission;
        public static ActivityField ActivityFieldElectricityGeneration;

        public static LocalityType LocalityTypeCity;

        public static Locality LocalityMoscow;
        public static Locality LocalityEkaterinburg;

        public static Region RegionSverdlovskayaOblast;
        public static Region RegionMoskovskayaOblast;

        public static District DistrictCentr;
        public static District DistrictUral;

        public static Country CountryRussia;

        public static Address AddressOfUetm;
        public static Address AddressOfStation;
        public static Address AddressOfSubstation;

        public static Person PersonIvanov;

        public static EmployeesPosition EmployeesPositionDirector;

        public static Employee EmployeeIvanov;

        public static UserRole UserRoleDataBaseFiller;
        public static UserRole UserRoleAdmin;
        public static UserRole UserRoleSalesManager;

        public static User UserIvanov;

        public static Project Project1;
        public static Project Project2;

        public static FacilityType FacilityTypeStation;
        public static FacilityType FacilityTypeSubStation;

        public static Facility FacilityStation;
        public static Facility FacilitySubstation;

        public static Measure MeasureKv;

        public static ParameterGroup ParameterGroupEqType;
        public static ParameterGroup ParameterGroupBreakerType;
        public static ParameterGroup ParameterGroupTransformatorType;
        public static ParameterGroup ParameterGroupVoltage;
        public static ParameterGroup ParameterGroupBrakersDrive;

        public static Parameter ParameterBreaker;
        public static Parameter ParameterTransformator;
        public static Parameter ParameterBrakersDrive;
        public static Parameter ParameterBreakerBlock;
        public static Parameter ParameterBreakerDeadTank;
        public static Parameter ParameterBreakerLiveTank;
        public static Parameter ParameterTransformatorCurrent;
        public static Parameter ParameterTransformatorVoltage;
        public static Parameter ParameterVoltage35kV;
        public static Parameter ParameterVoltage110kV;
        public static Parameter ParameterVoltage220kV;
        public static Parameter ParameterVoltage500kV;

        public static RequiredDependentEquipmentsParameters RequiredChildProductParametersDrive;
        public static RequiredDependentEquipmentsParameters RequiredChildProductParametersBreakerBlock;

        public static Product ProductZng110;
        public static Product ProductVgb35;
        public static Product ProductVeb110;
        public static Product ProductBreakesDrive;

        public static Equipment EquipmentVeb110;
        public static Equipment EquipmentZng110;
        public static Equipment EquipmentBreakersDrive;

        public static Contract ContractMrsk;

        public static Specification SpecificationMrsk1;

        public static TenderType TenderTypeProject;

        static TestDataGenerator()
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

        public static void GenerateCompanyForms()
        {
            CompanyFormAo = new CompanyForm {FullName = "Акционерное общество", ShortName = "АО"};
            CompanyFormPao = new CompanyForm {FullName = "Публичное акционерное общество", ShortName = "ПАО"};
            CompanyFormOao = new CompanyForm {FullName = "Открытое акционерное общество", ShortName = "ОАО"};
            CompanyFormZao = new CompanyForm {FullName = "Закрытое акционерное общество", ShortName = "ЗАО"};
        }

        public static void GenerateActivityFields()
        {
            ActivityFieldProducerOfHvt = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ProducerOfHighVoltageEquipment, Name = "Производитель ВВА" };
            ActivityFieldBuilder = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.Builder, Name = "Подрядчик" };
            ActivityFieldElectricityTransmission = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityTransmission, Name = "Передача электроэнергии" };
            ActivityFieldElectricityGeneration = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityGeneration, Name = "Генерация электроэнергии" };
        }

        public static void GenerateLocalityTypes()
        {
            LocalityTypeCity = new LocalityType { FullName = "Город", ShortName = "г." };
        }

        public static void GenerateLocalities()
        {
            LocalityMoscow = new Locality { LocalityType = LocalityTypeCity, Name = "Москва", Region = RegionMoskovskayaOblast, IsCountryCapital = true, IsDistrictsCapital = true, IsRegionCapital = true };
            LocalityEkaterinburg = new Locality { LocalityType = LocalityTypeCity, Name = "Екатеринбург", Region = RegionSverdlovskayaOblast, IsDistrictsCapital = true, IsRegionCapital = true };
        }

        public static void GenerateRegions()
        {
            RegionMoskovskayaOblast = new Region { Name = "Московская область", Localities = new List<Locality> { LocalityMoscow }, District = DistrictCentr};
            RegionSverdlovskayaOblast = new Region { Name = "Свердловская", Localities = new List<Locality> { LocalityEkaterinburg }, District = DistrictUral};
        }

        public static void GenerateDistricts()
        {
            DistrictCentr = new District { Country = CountryRussia, Name = "Центральный федеральный округ", Regions = new List<Region>() {RegionMoskovskayaOblast} };
            DistrictUral = new District { Country = CountryRussia, Name = "Уральский федеральный округ", Regions = new List<Region>() {RegionSverdlovskayaOblast} };
        }

        public static void GenerateCountries()
        {
            CountryRussia = new Country { Name = "Россия", Districts = new List<District> {DistrictCentr, DistrictUral} };
        }

        public static void GenerateAddresses()
        {
            AddressOfUetm = new Address { Description = "ул.Фронтовых бригад, д.22", Locality = LocalityEkaterinburg };
            AddressOfStation = new Address {Description = "ул.Станционная, 5", Locality = LocalityEkaterinburg };
            AddressOfSubstation = new Address {Description = "ул.ПодСтанционная, 25", Locality = LocalityMoscow };
        }

        public static void GenerateBankDetails()
        {
            BankDetailsOfUetm = new BankDetails { BankName = "Объебанк", BankIdentificationCode = "1111", CorrespondentAccount = "213564", CheckingAccount = "444554" };
        }

        public static void GenerateCompanies()
        {
            CompanyUetm = new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Form = CompanyFormAo, AddressLegal = AddressOfUetm, BankDetailsList = new List<BankDetails> { BankDetailsOfUetm }, ActivityFilds = new List<ActivityField> { ActivityFieldProducerOfHvt } };
            CompanyRosseti = new Company { FullName = "Россети", ShortName = "Россети", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission } };
            CompanyFsk = new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti };
            CompanyMrsk = new Company { FullName = "Межрегиональные распределительные сети", ShortName = "МРСК", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti };
            CompanyEnel = new Company { FullName = "Энел", ShortName = "Энел", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityGeneration } };
        }

        public static void GeneratePersons()
        {
            PersonIvanov = new Person { Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", IsMan = true, Employees = new List<Employee>() { } };
        }

        public static void GenerateEmployeesPositions()
        {
            EmployeesPositionDirector = new EmployeesPosition { Name = "Директор" };
        }

        public static void GenerateEmployees()
        {
            EmployeeIvanov = new Employee { Person = PersonIvanov, Position = EmployeesPositionDirector, Company = CompanyUetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36", IsActual = true};
        }

        public static void GenerateUserRoles()
        {
            UserRoleDataBaseFiller = new UserRole { Role = Role.DataBaseFiller, Name = "DataBaseFiller" };
            UserRoleAdmin = new UserRole { Role = Role.Admin, Name = "Admin" };
            UserRoleSalesManager = new UserRole { Role = Role.SalesManager, Name = "SalesManager" };
        }

        public static void GenerateUsers()
        {
            UserIvanov = new User { Login = "1", Password = StringToGuidService.GetHashString("1"), Employee = EmployeeIvanov, PersonalNumber = "333", Roles = new List<UserRole> { UserRoleAdmin, UserRoleDataBaseFiller, UserRoleSalesManager } };
        }

        public static void GenerateProjects()
        {
            Project1 = new Project { Name = "Реконструкция ПС Первая", Manager = UserIvanov };
            Project2 = new Project { Name = "Строительство Свердловской ТЭЦ", Manager = UserIvanov };
        }

        public static void GenerateFacilityTypes()
        {
            FacilityTypeStation = new FacilityType { FullName = "Теплоэлектроцентраль", ShortName = "ТЭЦ" };
            FacilityTypeSubStation = new FacilityType { FullName = "Понизительная станция", ShortName = "ПС" };
        }

        public static void GenerateFacilities()
        {
            FacilitySubstation = new Facility { Name = "Первая", Type = FacilityTypeSubStation, OwnerCompany = CompanyMrsk, Address = AddressOfSubstation};
            FacilityStation = new Facility { Name = "Свердловская", Type = FacilityTypeStation, OwnerCompany = CompanyEnel, Address = AddressOfStation };
        }

        public static void GenerateMeasures()
        {
            MeasureKv = new Measure { FullName = "Киловольт", ShortName = "кВ" };
        }

        public static void GenerateParameterGroups()
        {
            ParameterGroupEqType = new ParameterGroup { Name = "Тип оборудования" };
            ParameterGroupBreakerType = new ParameterGroup { Name = "Тип выключателя" };
            ParameterGroupTransformatorType = new ParameterGroup { Name = "Тип трансформатора" };
            ParameterGroupVoltage = new ParameterGroup { Name = "Номинальное напряжение", Measure = MeasureKv };
        }

        public static void GenerateParameters()
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

        public static void GenerateRequiredDependentEquipmentsParameters()
        {
            RequiredChildProductParametersDrive = new RequiredDependentEquipmentsParameters { MainProductParameters = new List<Parameter> { ParameterBreaker },
                                                                                               ChildProductParameters = new List<Parameter> { ParameterBrakersDrive }, Count = 1 };
            RequiredChildProductParametersBreakerBlock = new RequiredDependentEquipmentsParameters { MainProductParameters = new List<Parameter> { ParameterBreakerBlock },
                                                                                                     ChildProductParameters = new List<Parameter> { ParameterBreaker }, Count = 2 };
        }

        public static void GenerateProducts()
        {
            ProductZng110 = new Product { Designation = "ЗНГ-110", Parameters = new List<Parameter> { ParameterTransformator, ParameterTransformatorVoltage, ParameterVoltage110kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 75 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber1"};
            ProductVgb35 = new Product { Designation = "ВГБ-35", Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage35kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 50 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber2" };
            ProductVeb110 = new Product { Designation = "ВЭБ-110", Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage110kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 100 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber3" };
            ProductBreakesDrive = new Product { Designation = "Привод выключателя", Parameters = new List<Parameter> { ParameterBreaker },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 100 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber4" };
        }

        public static void GenerateEquipments()
        {
            EquipmentVeb110 = new Equipment { Designation = "Выключатель баковый ВЭБ-110", Product = ProductVeb110, DependentEquipments  = new List<Equipment> {EquipmentBreakersDrive} };
            EquipmentZng110 = new Equipment { Designation = "Трансформатор напряжения ЗНГ-110", Product = ProductZng110 };
            EquipmentBreakersDrive = new Equipment { Designation = "Привод выключателя", Product = ProductBreakesDrive };
        }

        public static void GenerateContracts()
        {
            ContractMrsk = new Contract { Contragent = CompanyMrsk, Date = DateTime.Today, Number = "0401-17-0001" };
        }

        public static void GenerateSpecifications()
        {
            SpecificationMrsk1 = new Specification { Contract = ContractMrsk, Date = ContractMrsk.Date, Number = "1", Vat = 0.18 };
        }

        public static void GenerateTenderTypes()
        {
            TenderTypeProject = new TenderType { Name = "Проектно-изыскательные работы", Type = TenderTypeEnum.ToProject };
        }
    }
    public class HvtAppDataBaseInitializer : DropCreateDatabaseIfModelChanges<HVTAppContext>
    {
        protected override void Seed(HVTAppContext context)
        {
            context.Countries.Add(TestDataGenerator.CountryRussia);
            context.ActivityFilds.AddRange(new[] { TestDataGenerator.ActivityFieldProducerOfHvt, TestDataGenerator.ActivityFieldBuilder, TestDataGenerator.ActivityFieldElectricityTransmission, TestDataGenerator.ActivityFieldElectricityGeneration });
            context.CompanyForms.AddRange(new[] { TestDataGenerator.CompanyFormAo, TestDataGenerator.CompanyFormOao, TestDataGenerator.CompanyFormPao, TestDataGenerator.CompanyFormZao });
            context.Companies.AddRange(new[] { TestDataGenerator.CompanyEnel, TestDataGenerator.CompanyFsk, TestDataGenerator.CompanyMrsk, TestDataGenerator.CompanyRosseti, TestDataGenerator.CompanyUetm });
            context.Employees.Add(TestDataGenerator.EmployeeIvanov);
            context.Users.Add(TestDataGenerator.UserIvanov);
            context.RequiredDependentEquipmentsParameterses.AddRange(new[] { TestDataGenerator.RequiredChildProductParametersBreakerBlock, TestDataGenerator.RequiredChildProductParametersDrive });
            //context.Equipments.AddRange(new [] {veb110, vgb35, zng110});
            context.Facilities.AddRange(new[] { TestDataGenerator.FacilityStation, TestDataGenerator.FacilitySubstation });
            context.Projects.AddRange(new[] { TestDataGenerator.Project1, TestDataGenerator.Project2 });
            context.Parameters.AddRange(new[] { TestDataGenerator.ParameterBreaker, TestDataGenerator.ParameterTransformator, TestDataGenerator.ParameterBreakerDeadTank,
                TestDataGenerator.ParameterBreakerLiveTank, TestDataGenerator.ParameterTransformatorCurrent, TestDataGenerator.ParameterTransformatorVoltage,
                TestDataGenerator.ParameterVoltage35kV, TestDataGenerator.ParameterVoltage110kV, TestDataGenerator.ParameterVoltage220kV, TestDataGenerator.ParameterVoltage500kV });
            context.Specifications.Add(TestDataGenerator.SpecificationMrsk1);
            //context.Tenders.Add(tender);
            //context.Units.AddRange(new[] {productComplexUnitVeb110, productComplexUnitVeb1102, productComplexUnitZng1101, productComplexUnitZng1102});

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

        private List<ParameterGroup> _groups;
        private Parameter _breaker, _transformator, _drive, _drivesReducer;
        private Parameter _v110, _v220, _v500;
        private Parameter _c2500, _c3150, _c0001, _c0005;
        private ParameterGroup _current, _voltage, _eqType;

        private RequiredDependentEquipmentsParameters _requiredDependentEquipmentsParametersTransformatorsToBreker, _requiredDependentEquipmentsParametersDriveToBreker, _requiredDependentEquipmentsParametersReducerToDrive;
        private List<RequiredDependentEquipmentsParameters> _requiredDependentEquipmentsParametersList;

        public void Init()
        {
            _breaker = new Parameter() { Value = "выключатель" };
            _transformator = new Parameter() { Value = "трансформатор" };
            _drive = new Parameter() { Value = "привод выключателя" };
            _drivesReducer = new Parameter() { Value = "редуктор" };
            _eqType = new ParameterGroup() {Name = "Тип оборудования"}.AddParameters(new[] { _breaker, _transformator, _drive, _drivesReducer });

            _v110 = new Parameter() { Value = "110кВ" }.AddRequiredPreviousParameters(new[] { _breaker });
            _v220 = new Parameter() { Value = "220кВ" }.AddRequiredPreviousParameters(new[] { _breaker });
            _v500 = new Parameter() { Value = "500кВ" }.AddRequiredPreviousParameters(new[] { _breaker });
            _voltage = new ParameterGroup() {Name = "Voltage"}.AddParameters(new[] { _v110, _v220, _v500 });

            _c2500 = new Parameter() { Value = "2500А" }.AddRequiredPreviousParameters(new[] { _breaker, _v110 })
                                                        .AddRequiredPreviousParameters(new[] { _breaker, _v220 });
            _c3150 = new Parameter() { Value = "3150А" }.AddRequiredPreviousParameters(new[] { _breaker, _v110 })
                                                        .AddRequiredPreviousParameters(new[] { _breaker, _v220 })
                                                        .AddRequiredPreviousParameters(new[] { _breaker, _v500 });
            _c0001 = new Parameter() { Value = "1А" }.AddRequiredPreviousParameters(new[] { _transformator });
            _c0005 = new Parameter() { Value = "5А" }.AddRequiredPreviousParameters(new[] { _transformator });
            _current = new ParameterGroup() {Name = "Current"}.AddParameters(new[] { _c2500, _c3150, _c0001, _c0005 });

            _groups = new List<ParameterGroup> { _eqType, _voltage, _current };

            _requiredDependentEquipmentsParametersTransformatorsToBreker = new RequiredDependentEquipmentsParameters
            {
                MainProductParameters = new List<Parameter> { _breaker, _v110 },
                ChildProductParameters = new List<Parameter> { _transformator },
                Count = 6
            };
            _requiredDependentEquipmentsParametersDriveToBreker = new RequiredDependentEquipmentsParameters
            {
                MainProductParameters = new List<Parameter> { _breaker },
                ChildProductParameters = new List<Parameter> { _drive },
                Count = 1
            };
            _requiredDependentEquipmentsParametersReducerToDrive = new RequiredDependentEquipmentsParameters
            {
                MainProductParameters = new List<Parameter> { _drive },
                ChildProductParameters = new List<Parameter> { _drivesReducer },
                Count = 3
            };
            _requiredDependentEquipmentsParametersList = new List<RequiredDependentEquipmentsParameters>
            {
                _requiredDependentEquipmentsParametersTransformatorsToBreker,
                _requiredDependentEquipmentsParametersDriveToBreker,
                _requiredDependentEquipmentsParametersReducerToDrive
            };

        }

    }
}