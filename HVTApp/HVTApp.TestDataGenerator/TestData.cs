using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Services.StringToGuidService;

namespace HVTApp.TestDataGenerator
{
    public class TestData
    {
        public IEnumerable<TData> GetAll<TData>()
        {
            var fields = this.GetType().GetFields().Where(x => x.FieldType == typeof(TData)).ToList();
            foreach (var field in fields)
            {
                yield return (TData) field.GetValue(this);
            }
        }

        public CommonOption CommonOption;

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
                
        public Project ProjectSubstation;
        public Project ProjectStation;

        public FacilityType FacilityTypeStation;
        public FacilityType FacilityTypeSubStation;
                
        public Facility FacilityStation;
        public Facility FacilitySubstation;
                
        public Measure MeasureKv;
                
        public ParameterGroup ParameterGroupEqType;
        public ParameterGroup ParameterGroupBreakerType;
        public ParameterGroup ParameterGroupTransformatorType;
        public ParameterGroup ParameterGroupVoltage;
        public ParameterGroup ParameterGroupDrivesVoltage;

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
        public Parameter ParameterVoltage110V;
        public Parameter ParameterVoltage220V;


        public ProductRelation RequiredChildProductRelationDrive;
        public ProductRelation RequiredChildProductRelationBreakerBlock;

        public ProductBlock ProductBlockVgb35;
        public ProductBlock ProductBlockVeb110;
        public ProductBlock ProductBlockZng110;
        public ProductBlock ProductBlockBreakersDrive;

        public Product ProductVgb35;
        public Product ProductVeb110;
        public Product ProductZng110;
        public Product ProductBreakersDrive;

        public SalesUnit ProjectSalesUnitVeb1101;
        public SalesUnit ProjectSalesUnitVeb1102;
        public SalesUnit ProjectSalesUnitZng1101;
        public SalesUnit ProjectSalesUnitZng1102;
        public SalesUnit ProjectSalesUnitZng1103;
        public SalesUnit ProjectSalesUnitZng1104;
        public SalesUnit ProjectSalesUnitZng1105;
        public SalesUnit ProjectSalesUnitZng1106;

        public SalesUnit OfferSalesUnitVeb1101;
        public SalesUnit OfferSalesUnitVeb1102;
        public SalesUnit OfferSalesUnitZng1101;
        public SalesUnit OfferSalesUnitZng1102;
        public SalesUnit OfferSalesUnitZng1103;
        public SalesUnit OfferSalesUnitZng1104;
        public SalesUnit OfferSalesUnitZng1105;
        public SalesUnit OfferSalesUnitZng1106;
               
        public SalesUnit SalesUnitVeb1101;
        public SalesUnit SalesUnitVeb1102;

        public SalesUnit SalesUnitZng1101;
        public SalesUnit SalesUnitZng1102;
        public SalesUnit SalesUnitZng1103;
                                
        public Offer OfferMrsk;
                               
        public Order OrderVeb110;
        public Order OrderZng110;
                
        public Contract ContractMrsk;
                
        public Specification SpecificationMrsk1;
                
        public TenderType TenderTypeProject;
                
        public Tender TenderMrsk;
                              
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

            var methods = this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name.Contains("Generate"));
            foreach (var methodInfo in methods)
            {
                methodInfo.Invoke(this, null);
            }
        }

        private void GenerateCommonOption()
        {
            CommonOption.Clone(new CommonOption { OurCompanyId = CompanyUetm.Id, StandartPaymentsConditions = StandartPaymentConditions});
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
            LocalityMoscow.Clone(new Locality { LocalityType = LocalityTypeCity, Name = "Москва", RegionId = RegionMoskovskayaOblast.Id, IsCountryCapital = true, IsDistrictsCapital = true, IsRegionCapital = true });
            LocalityEkaterinburg.Clone(new Locality { LocalityType = LocalityTypeCity, Name = "Екатеринбург", RegionId = RegionSverdlovskayaOblast.Id, IsDistrictsCapital = true, IsRegionCapital = true });
        }

        private void GenerateRegions()
        {
            RegionMoskovskayaOblast.Clone(new Region { Name = "Московская область", Localities= new List<Locality> { LocalityMoscow }, DistrictId = DistrictCentr.Id });
            RegionSverdlovskayaOblast.Clone(new Region { Name = "Свердловская область", Localities= new List<Locality> { LocalityEkaterinburg }, DistrictId = DistrictUral.Id });
        }

        private void GenerateDistricts()
        {
            DistrictCentr.Clone(new District { CountryId = CountryRussia.Id, Name = "Центральный федеральный округ", Regions= new List<Region>() {RegionMoskovskayaOblast} });
            DistrictUral.Clone(new District { CountryId = CountryRussia.Id, Name = "Уральский федеральный округ", Regions= new List<Region>() {RegionSverdlovskayaOblast} });
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
            CompanyUetm.Clone(new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Inn = "23658", Form = CompanyFormAo, AddressLegal = AddressOfUetm, BankDetailsList= new List<BankDetails> { BankDetailsOfUetm }, ActivityFilds= new List<ActivityField> { ActivityFieldProducerOfHvt } });
            CompanyRosseti.Clone(new Company { FullName = "Россети", ShortName = "Россети", Inn = "23659", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> { ActivityFieldElectricityTransmission } });
            CompanyFsk.Clone(new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Inn = "26658", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti });
            CompanyMrsk.Clone(new Company { FullName = "Межрегиональные распределительные сети", Inn = "23358", ShortName = "МРСК", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti });
            CompanyEnel.Clone(new Company { FullName = "Энел", ShortName = "Энел", Inn = "25658", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> { ActivityFieldElectricityGeneration } });
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
            EmployeeIvanov.Clone(new Employee { PersonId = PersonIvanov.Id, Position = EmployeesPositionDirector, Company = CompanyUetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36", IsActual = true });
            EmployeePetrov.Clone(new Employee { PersonId = PersonPetrov.Id, Position = EmployeesPositionDirector, Company = CompanyFsk, Email = "iii@mail.ru", PhoneNumber = "326-36-36", IsActual = true });
            EmployeeSidorov.Clone(new Employee { PersonId = PersonSidorov.Id, Position = EmployeesPositionDirector, Company = CompanyEnel, Email = "iii@mail.ru", PhoneNumber = "326-36-36", IsActual = true });
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
            ProjectSubstation.Clone(new Project {Name = "Реконструкция ПС Первая", Manager = UserIvanov});
            ProjectSubstation.SalesUnits = new List<SalesUnit>(new[]
            {
                ProjectSalesUnitVeb1101,
                ProjectSalesUnitVeb1102,
                ProjectSalesUnitZng1101,
                ProjectSalesUnitZng1102,
                ProjectSalesUnitZng1103
            });

            ProjectStation.Clone(new Project {Name = "Строительство Свердловской ТЭЦ", Manager = UserIvanov});
            ProjectStation.SalesUnits = new List<SalesUnit>(new[]
            {
                ProjectSalesUnitZng1104,
                ProjectSalesUnitZng1105,
                ProjectSalesUnitZng1106
            });

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
            ParameterGroupBreakerType.Clone(new ParameterGroup { Name = "Тип выключателя" });
            ParameterGroupTransformatorType.Clone(new ParameterGroup { Name = "Тип трансформатора" });
            ParameterGroupVoltage.Clone(new ParameterGroup { Name = "Номинальное напряжение", Measure = MeasureKv });
            ParameterGroupDrivesVoltage.Clone(new ParameterGroup { Name = "Номинальное напряжение двигателя завода пружин", Measure = MeasureKv });
        }

        private void GenerateParameters()
        {
            ParameterBreaker.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Выключатель" });
            ParameterTransformator.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Трансформатор" });
            ParameterBrakersDrive.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Привод" });
            ParameterBreakerBlock.Clone(new Parameter { ParameterGroup = ParameterGroupEqType, Value = "Блок выключателя" });

            ParameterBreakerDeadTank.Clone(new Parameter { ParameterGroup = ParameterGroupBreakerType, Value = "Баковый" });
            ParameterBreakerLiveTank.Clone(new Parameter { ParameterGroup = ParameterGroupBreakerType, Value = "Колонковый" });
            ParameterTransformatorCurrent.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorType, Value = "Тока" });
            ParameterTransformatorVoltage.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorType, Value = "Напряжения" });
            ParameterVoltage35kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "35" });
            ParameterVoltage110kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "110" });
            ParameterVoltage220kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "220" });
            ParameterVoltage500kV.Clone(new Parameter { ParameterGroup = ParameterGroupVoltage, Value = "500" });
            ParameterVoltage110V.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesVoltage, Value = "110 В" });
            ParameterVoltage220V.Clone(new Parameter { ParameterGroup = ParameterGroupDrivesVoltage, Value = "220 В" });


            ParameterBreakerDeadTank.AddRequiredPreviousParameters(new[] {ParameterBreaker});
            ParameterBreakerLiveTank.AddRequiredPreviousParameters(new[] {ParameterBreaker});

            ParameterTransformatorCurrent.AddRequiredPreviousParameters(new[] {ParameterTransformator});
            ParameterTransformatorVoltage.AddRequiredPreviousParameters(new[] {ParameterTransformator});

            ParameterVoltage35kV.AddRequiredPreviousParameters(new[] {ParameterBreaker})
                                .AddRequiredPreviousParameters(new[] {ParameterTransformator, ParameterTransformatorCurrent});
            ParameterVoltage110kV.AddRequiredPreviousParameters(new[] { ParameterBreaker })
                                 .AddRequiredPreviousParameters(new[] { ParameterTransformator }); 
            ParameterVoltage220kV.AddRequiredPreviousParameters(new[] { ParameterBreaker })
                                 .AddRequiredPreviousParameters(new[] { ParameterTransformator }); 
            ParameterVoltage500kV.AddRequiredPreviousParameters(new[] { ParameterBreaker, ParameterBreakerLiveTank });

            ParameterVoltage110V.AddRequiredPreviousParameters(new[] { ParameterBrakersDrive });

            ParameterVoltage220V.AddRequiredPreviousParameters(new[] { ParameterBrakersDrive });
        }

        private void GenerateRequiredDependentEquipmentsParameters()
        {
            RequiredChildProductRelationDrive.Clone(new ProductRelation { ParentProductParameters = new List<Parameter> { ParameterBreaker },
                ChildProductParameters= new List<Parameter> { ParameterBrakersDrive }, Count = 1 });
            RequiredChildProductRelationBreakerBlock.Clone(new ProductRelation { ParentProductParameters = new List<Parameter> { ParameterBreakerBlock },
                ChildProductParameters= new List<Parameter> { ParameterBreaker }, Count = 2 });
        }



        private void GenerateProductBlocs()
        {
            ProductBlockVgb35.Clone(new ProductBlock
            {
                Name = "Block Выключатель баковый ВГБ-35",
                Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage35kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = 50, Date = DateTime.Today } },
                StructureCostNumber = "StructureCostNumberVGB35",
            });

            ProductBlockVeb110.Clone(new ProductBlock
            {
                Name = "Block Выключатель баковый ВЭБ-110",
                Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage110kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = 2000000, Date = DateTime.Today } },
                StructureCostNumber = "StructureCostNumber3",
            });

            ProductBlockZng110.Clone(new ProductBlock
            {
                Name = "Block Трансформатор напряжения ЗНГ-110",
                Parameters = new List<Parameter> { ParameterTransformator, ParameterTransformatorVoltage, ParameterVoltage110kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = 250000, Date = DateTime.Today } },
                StructureCostNumber = "StructureCostNumber1"
            });

            ProductBlockBreakersDrive.Clone(new ProductBlock
            {
                Name = "Block Привод выключателя",
                Parameters = new List<Parameter> { ParameterBrakersDrive, ParameterVoltage220V },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = 200000, Date = DateTime.Today } },
                StructureCostNumber = "StructureCostNumber4"
            });
        }

        private void GenerateProducts()
        {
            ProductVgb35.Clone(new Product
            {
                Designation = "Выключатель баковый ВГБ-35",
                ProductBlock = ProductBlockVgb35,
                DependentProducts = new List<Product> { ProductBreakersDrive }
            });

            ProductVeb110.Clone(new Product
            {
                Designation = "Выключатель баковый ВЭБ-110",
                ProductBlock = ProductBlockVeb110,
                DependentProducts = new List<Product> { ProductBreakersDrive }
            });

            ProductZng110.Clone(new Product
            {
                Designation = "Трансформатор напряжения ЗНГ-110",
                ProductBlock = ProductBlockZng110
            });

            ProductBreakersDrive.Clone(new Product
            {
                Designation = "Привод выключателя",
                ProductBlock = ProductBlockBreakersDrive
            });
        }

        private void GenerateSalesUnits()
        {
            SalesUnitVeb1101.Clone(new SalesUnit { Product = ProductVeb110, Order = OrderVeb110, SerialNumber = "1", PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7, Cost = 3000000, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, Address = AddressOfSubstation, CostOfShipment = 100, DeliveryDate = DateTime.Today.AddDays(180), Facility = FacilitySubstation });
            SalesUnitVeb1102.Clone(new SalesUnit { Product = ProductVeb110, Order = OrderVeb110, SerialNumber = "2", PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7, Cost = 3000000, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions, Address = AddressOfSubstation, CostOfShipment = 100, DeliveryDate = DateTime.Today.AddDays(180), Facility = FacilitySubstation }); 

            SalesUnitZng1101.Clone(new SalesUnit { Product = ProductZng110, Order = OrderZng110, SerialNumber = "5", PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions,  Address = AddressOfSubstation, CostOfShipment = 110, DeliveryDate = DateTime.Today.AddDays(180), Facility = FacilitySubstation }); 
            SalesUnitZng1102.Clone(new SalesUnit { Product = ProductZng110, Order = OrderZng110, SerialNumber = "6", PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions,  Address = AddressOfSubstation, CostOfShipment = 110, DeliveryDate = DateTime.Today.AddDays(180), Facility = FacilitySubstation }); 
            SalesUnitZng1103.Clone(new SalesUnit { Product = ProductZng110, Order = OrderZng110, SerialNumber = "7", PlannedTermFromStartToEndProduction = 90, PlannedTermFromPickToEndProduction = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentsConditions = StandartPaymentConditions,  Address = AddressOfSubstation, CostOfShipment = 110, DeliveryDate = DateTime.Today.AddDays(180), Facility = FacilitySubstation });

            ProjectSalesUnitVeb1101.Clone(new SalesUnit { Product = ProductVeb110, Cost = 3000000, Producer = CompanyUetm, PaymentsConditions = StandartPaymentConditions, Facility = FacilitySubstation, DeliveryDate = DateTime.Today.AddDays(200) }); ;
            ProjectSalesUnitVeb1102.Clone(new SalesUnit { Product = ProductVeb110, Cost = 3000000, Producer = CompanyUetm, PaymentsConditions = StandartPaymentConditions, Facility = FacilitySubstation, DeliveryDate = DateTime.Today.AddDays(200) });;

            ProjectSalesUnitZng1101.Clone(new SalesUnit { Product = ProductZng110, Cost = 500000 , Producer = CompanyUetm, PaymentsConditions = StandartPaymentConditions, Facility = FacilityStation, DeliveryDate = DateTime.Today.AddDays(200) });
            ProjectSalesUnitZng1102.Clone(new SalesUnit { Product = ProductZng110, Cost = 500000 , Producer = CompanyUetm, PaymentsConditions = StandartPaymentConditions, Facility = FacilityStation, DeliveryDate = DateTime.Today.AddDays(200) });
            ProjectSalesUnitZng1103.Clone(new SalesUnit { Product = ProductZng110, Cost = 500000 , Producer = CompanyUetm, PaymentsConditions = StandartPaymentConditions, Facility = FacilityStation, DeliveryDate = DateTime.Today.AddDays(200) });
            ProjectSalesUnitZng1104.Clone(new SalesUnit { Product = ProductZng110, Cost = 500000 , Facility = FacilitySubstation, DeliveryDate = DateTime.Today.AddDays(200), PaymentsConditions = StandartPaymentConditions });
            ProjectSalesUnitZng1105.Clone(new SalesUnit { Product = ProductZng110, Cost = 500000 , Facility = FacilityStation, DeliveryDate = DateTime.Today.AddDays(200), PaymentsConditions = StandartPaymentConditions });
            ProjectSalesUnitZng1106.Clone(new SalesUnit { Product = ProductZng110, Cost = 500000 , Facility = FacilityStation, DeliveryDate = DateTime.Today.AddDays(200), PaymentsConditions = StandartPaymentConditions });

            OfferSalesUnitVeb1101.Clone(new SalesUnit { Product = ProductVeb110, Cost = 3100000, PlannedTermFromStartToEndProduction = 120, PaymentsConditions = StandartPaymentConditions, Facility = FacilityStation });
            OfferSalesUnitVeb1102.Clone(new SalesUnit { Product = ProductVeb110, Cost = 3100000, PlannedTermFromStartToEndProduction = 120, PaymentsConditions = StandartPaymentConditions, Facility = FacilityStation });
                                                                                                 
            OfferSalesUnitZng1101.Clone(new SalesUnit { Product = ProductZng110, Cost = 5500000, PlannedTermFromStartToEndProduction = 150, PaymentsConditions = StandartPaymentConditions, Facility = FacilitySubstation });
            OfferSalesUnitZng1102.Clone(new SalesUnit { Product = ProductZng110, Cost = 5500000, PlannedTermFromStartToEndProduction = 150, PaymentsConditions = StandartPaymentConditions, Facility = FacilitySubstation });
            OfferSalesUnitZng1103.Clone(new SalesUnit { Product = ProductZng110, Cost = 5500000, PlannedTermFromStartToEndProduction = 150, PaymentsConditions = StandartPaymentConditions, Facility = FacilitySubstation });
            OfferSalesUnitZng1104.Clone(new SalesUnit { Product = ProductZng110, Cost = 5500000, PlannedTermFromStartToEndProduction = 150, PaymentsConditions = StandartPaymentConditions, Facility = FacilitySubstation});
            OfferSalesUnitZng1105.Clone(new SalesUnit { Product = ProductZng110, Cost = 5500000, PlannedTermFromStartToEndProduction = 150, PaymentsConditions = StandartPaymentConditions, Facility = FacilitySubstation});
            OfferSalesUnitZng1106.Clone(new SalesUnit { Product = ProductZng110, Cost = 5500000, PlannedTermFromStartToEndProduction = 150, PaymentsConditions = StandartPaymentConditions, Facility = FacilitySubstation });
        }

        private void GenerateOffers()
        {
            OfferMrsk.Clone(new Offer { Vat = 0.18, Project = ProjectSubstation, ValidityDate = DateTime.Today.AddDays(60), Author = EmployeeIvanov, SenderEmployee = EmployeeIvanov, RecipientEmployee = EmployeeSidorov, CopyToRecipients = new List<Employee> {EmployeePetrov}, RegistrationDetailsOfSender = new DocumentsRegistrationDetails { RegistrationNumber = "7412-17-0212", RegistrationDate = DateTime.Today }, RegistrationDetailsOfRecipient = new DocumentsRegistrationDetails { RegistrationNumber = "12f455", RegistrationDate = DateTime.Today.AddDays(-3) }});
            OfferMrsk.SalesUnits = new List<SalesUnit>(new []
            {
               OfferSalesUnitVeb1101,
               OfferSalesUnitVeb1102,
               OfferSalesUnitZng1101,
               OfferSalesUnitZng1102,
               OfferSalesUnitZng1103,
               OfferSalesUnitZng1104,
               OfferSalesUnitZng1105,
               OfferSalesUnitZng1106
            });
        }

        private void GenerateTenderTypes()
        {
            TenderTypeProject.Clone(new TenderType { Name = "Проектно-изыскательные работы", Type = TenderTypeEnum.ToProject });
        }

        private void GenerateTenders()
        {
            TenderMrsk.Clone(new Tender { ProjectId = ProjectSubstation.Id, Types = new List<TenderType> {TenderTypeProject} , Winner = CompanyUetm, Participants = new List<Company> { CompanyUetm, CompanyEnel }, DateOpen = DateTime.Today, DateClose = DateTime.Today.AddDays(7) });
        }

        private void GenerateOrders()
        {
            OrderVeb110.Clone(new Order { Number = "8012-17", OpenOrderDate = DateTime.Today });
            OrderZng110.Clone(new Order { Number = "8011-15", OpenOrderDate = DateTime.Today.AddDays(-50) });
        }

        private void GenerateContracts()
        {
            ContractMrsk.Clone(new Contract { Contragent = CompanyMrsk, Date = DateTime.Today, Number = "0401-17-0001" });
        }

        private void GenerateSpecifications()
        {
            SpecificationMrsk1.Clone(new Specification { ContractId = ContractMrsk.Id, Date = ContractMrsk.Date, Number = "1", Vat = 0.18});
        }

        private void GeneratePaymentConditions()
        {
            PaymentConditionAvans50.Clone(new PaymentCondition { Part = 0.5, DaysToPoint = -10, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            PaymentConditionDoplata50.Clone(new PaymentCondition { Part = 0.5, DaysToPoint = -14, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });

            StandartPaymentConditions.AddRange(new[] { PaymentConditionAvans50, PaymentConditionDoplata50 });
        }
    }
}