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
            var fields = GetType().GetFields().Where(x => x.FieldType == typeof(TData)).ToList();
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
        public LocalityType LocalityVillage;

        public Locality LocalityMoscow;
        public Locality LocalityEkaterinburg;
                
        public Region RegionSverdlovskayaOblast;
        public Region RegionMoskovskayaOblast;
                
        public District DistrictCentr;
        public District DistrictUral;

        public Country CountryRussia;
        public Country CountryUsa;

        public Address AddressUetm;
        public Address AddressStation;
        public Address AddressSubstation;

        public EmployeesPosition EmployeesPositionDirector;
        public EmployeesPosition EmployeesPositionManager;

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
        public User UserPetrov;

        public Project ProjectSubstation;
        public Project ProjectStation;

        public FacilityType FacilityTypeStation;
        public FacilityType FacilityTypeSubStation;
                
        public Facility FacilityStation;
        public Facility FacilitySubstation;
                
        public Measure MeasureKv;

        public ParameterGroup ParameterGroupZip;
        public ParameterGroup ParameterGroupEqType;
        public ParameterGroup ParameterGroupBreakerType;
        public ParameterGroup ParameterGroupTransformatorType;
        public ParameterGroup ParameterGroupTransformatorCurrentType;
        public ParameterGroup ParameterGroupVoltage;
        public ParameterGroup ParameterGroupDrivesVoltage;

        public Parameter ParameterDependentEquipment;
        public Parameter ParameterZip1;
        public Parameter ParameterBreaker;
        public Parameter ParameterTransformator;
        public Parameter ParameterBrakersDrive;
        public Parameter ParameterKtpb;
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
        public Parameter ParameterTransformatorTrg110;
        public Parameter ParameterTransformatorTvg110;

        public ProductRelation RequiredChildProductRelationDrive;
        public ProductRelation RequiredChildProductRelationBreakerBlock;
        public ProductRelation RequiredChildProductRelationTvg110;

        public ProductBlock ProductBlockVgb35;
        public ProductBlock ProductBlockVeb110;
        public ProductBlock ProductBlockZng110;
        public ProductBlock ProductBlockBreakersDrive;
        public ProductBlock ProductBlockZip1;

        public Product ProductVgb35;
        public Product ProductVeb110;
        public Product ProductZng110;
        public Product ProductBreakersDrive;
        public Product ProductZip1;
               
        public SalesUnit SalesUnitVeb1101Full;
        public SalesUnit SalesUnitVeb1102Full;
                                         
        public SalesUnit SalesUnitZng1101Full;
        public SalesUnit SalesUnitZng1102Full;
        public SalesUnit SalesUnitZng1103Full;

        public SalesUnit SalesUnitVeb1101;
        public SalesUnit SalesUnitVeb1102;
        public SalesUnit SalesUnitZng1101;
        public SalesUnit SalesUnitZng1102;
        public SalesUnit SalesUnitZng1103;
        public SalesUnit SalesUnitZng1104;
        public SalesUnit SalesUnitZng1105;
        public SalesUnit SalesUnitZng1106;

        public OfferUnit OfferUnitVeb1101;
        public OfferUnit OfferUnitVeb1102;
        public OfferUnit OfferUnitZng1101;
        public OfferUnit OfferUnitZng1102;
        public OfferUnit OfferUnitZng1103;
        public OfferUnit OfferUnitZng1104;
        public OfferUnit OfferUnitZng1105;
        public OfferUnit OfferUnitZng1106;
                                
        public Offer OfferMrsk;
                               
        public Order OrderVeb110;
        public Order OrderZng110;

        public Contract ContractMrsk;
        public Contract ContractFsk;

        public Specification SpecificationMrsk1;
        public Specification SpecificationFsk;

        public TenderType TenderTypeProject;
        public TenderType TenderTypeWork;
        public TenderType TenderTypeSuply;

        public Tender TenderMrsk;
        public Tender Tender2;

        public PaymentCondition PaymentConditionAvans50;
        public PaymentCondition PaymentConditionDoplata50;

        public PaymentCondition PaymentConditionAvans30;
        public PaymentCondition PaymentConditionPostoplata70;

        public PaymentConditionSet PaymentConditionSet50Na50;
        public PaymentConditionSet PaymentConditionSet30Na70;

        public TestData()
        {
            var fields = GetType().GetFields();
            foreach (var fieldInfo in fields)
            {
                fieldInfo.SetValue(this, Activator.CreateInstance(fieldInfo.FieldType));
            }

            var methods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name.Contains("Generate"));
            foreach (var methodInfo in methods)
            {
                methodInfo.Invoke(this, null);
            }
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
            ActivityFieldProducerOfHvt.Clone(new ActivityField {ActivityFieldEnum = ActivityFieldEnum.ProducerOfHighVoltageEquipment, Name = "Производитель ВВА"});
            ActivityFieldBuilder.Clone(new ActivityField {ActivityFieldEnum = ActivityFieldEnum.Builder, Name = "Подрядчик"});
            ActivityFieldElectricityTransmission.Clone(new ActivityField {ActivityFieldEnum = ActivityFieldEnum.ElectricityTransmission, Name = "Передача электроэнергии"});
            ActivityFieldElectricityGeneration.Clone(new ActivityField {ActivityFieldEnum = ActivityFieldEnum.ElectricityGeneration, Name = "Генерация электроэнергии"});
        }

        private void GenerateLocalityTypes()
        {
            LocalityTypeCity.Clone(new LocalityType { FullName = "Город", ShortName = "г." });
            LocalityVillage.Clone(new LocalityType { FullName = "Деревня", ShortName = "д." });
        }

        private void GenerateLocalities()
        {
            LocalityMoscow.Clone(new Locality {LocalityType = LocalityTypeCity, Name = "Москва", Region = RegionMoskovskayaOblast, IsCountryCapital = true, IsDistrictCapital = true, IsRegionCapital = true});
            LocalityEkaterinburg.Clone(new Locality {LocalityType = LocalityTypeCity, Name = "Екатеринбург", Region = RegionSverdlovskayaOblast, IsDistrictCapital = true, IsRegionCapital = true});
        }

        private void GenerateRegions()
        {
            RegionMoskovskayaOblast.Clone(new Region {Name = "Московская область", District = DistrictCentr});
            RegionSverdlovskayaOblast.Clone(new Region {Name = "Свердловская область", District = DistrictUral});
        }

        private void GenerateDistricts()
        {
            DistrictCentr.Clone(new District {Country = CountryRussia, Name = "Центральный федеральный округ"});
            DistrictUral.Clone(new District {Country = CountryRussia, Name = "Уральский федеральный округ"});
        }

        private void GenerateCountries()
        {
            CountryRussia.Clone(new Country { Name = "Россия" });
            CountryUsa.Clone(new Country { Name = "США" });
        }

        private void GenerateAddresses()
        {
            AddressUetm.Clone(new Address {Description = "ул.Фронтовых бригад, д.22", Locality = LocalityEkaterinburg});
            AddressStation.Clone(new Address {Description = "ул.Станционная, 5", Locality = LocalityEkaterinburg});
            AddressSubstation.Clone(new Address {Description = "ул.ПодСтанционная, 25", Locality = LocalityMoscow});
        }

        private void GenerateBankDetails()
        {
            BankDetailsOfUetm.Clone(new BankDetails {BankName = "Объебанк", BankIdentificationCode = "1111", CorrespondentAccount = "213564", CheckingAccount = "444554"});
        }

        private void GenerateCompanies()
        {
            CompanyUetm.Clone(new Company {FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Inn = "23658", Form = CompanyFormAo, AddressLegal = AddressUetm, BankDetailsList= new List<BankDetails> {BankDetailsOfUetm}, ActivityFilds= new List<ActivityField> {ActivityFieldProducerOfHvt}});
            CompanyRosseti.Clone(new Company {FullName = "Россети", ShortName = "Россети", Inn = "23659", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> {ActivityFieldElectricityTransmission}});
            CompanyFsk.Clone(new Company {FullName = "Федеральная сетевая компания", ShortName = "ФСК", Inn = "26658", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> {ActivityFieldElectricityTransmission}, ParentCompany = CompanyRosseti});
            CompanyMrsk.Clone(new Company {FullName = "Межрегиональные распределительные сети", Inn = "23358", ShortName = "МРСК", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> {ActivityFieldElectricityTransmission}, ParentCompany = CompanyRosseti});
            CompanyEnel.Clone(new Company {FullName = "Энел", ShortName = "Энел", Inn = "25658", Form = CompanyFormPao, ActivityFilds= new List<ActivityField> {ActivityFieldElectricityGeneration}});
        }

        private void GeneratePersons()
        {
            PersonIvanov.Clone(new Person {Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", IsMan = true });
            PersonPetrov.Clone(new Person {Surname = "Петров", Name = "Иван", Patronymic = "Иванович", IsMan = true });
            PersonSidorov.Clone(new Person {Surname = "Сидоров", Name = "Иван", Patronymic = "Иванович", IsMan = true });
        }

        private void GenerateEmployeesPositions()
        {
            EmployeesPositionDirector.Clone(new EmployeesPosition { Name = "Директор" });
            EmployeesPositionManager.Clone(new EmployeesPosition { Name = "Менеджер" });
        }

        private void GenerateEmployees()
        {
            EmployeeIvanov.Clone(new Employee {Person = PersonIvanov, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36"});
            EmployeePetrov.Clone(new Employee {Person = PersonPetrov, Position = EmployeesPositionDirector, Company = CompanyFsk, Email = "pii@mail.ru", PhoneNumber = "326-36-37"});
            EmployeeSidorov.Clone(new Employee {Person = PersonSidorov, Position = EmployeesPositionDirector, Company = CompanyEnel, Email = "sii@mail.ru", PhoneNumber = "326-36-38"});
        }

        private void GenerateUserRoles()
        {
            UserRoleDataBaseFiller.Clone(new UserRole {Role = Role.DataBaseFiller, Name = "DataBaseFiller"});
            UserRoleAdmin.Clone(new UserRole {Role = Role.Admin, Name = "Admin"});
            UserRoleSalesManager.Clone(new UserRole {Role = Role.SalesManager, Name = "SalesManager"});
        }

        private void GenerateUsers()
        {
            UserIvanov.Clone(new User { Login = "1", Password = StringToGuidService.GetHashString("1"), Employee = EmployeeIvanov, PersonalNumber = "1", Roles = new List<UserRole> { UserRoleAdmin, UserRoleDataBaseFiller, UserRoleSalesManager } });
            UserPetrov.Clone(new User { Login = "2", Password = StringToGuidService.GetHashString("2"), Employee = EmployeePetrov, PersonalNumber = "2", Roles = new List<UserRole> { UserRoleDataBaseFiller } });
        }

        private void GenerateProjects()
        {
            ProjectSubstation.Clone(new Project {Name = "Реконструкция ПС Первая", Manager = UserIvanov});
            ProjectStation.Clone(new Project {Name = "Строительство Свердловской ТЭЦ", Manager = UserIvanov});
        }

        private void GenerateFacilityTypes()
        {
            FacilityTypeStation.Clone(new FacilityType {FullName = "Теплоэлектроцентраль", ShortName = "ТЭЦ"});
            FacilityTypeSubStation.Clone(new FacilityType {FullName = "Понизительная станция", ShortName = "ПС"});
        }

        private void GenerateFacilities()
        {
            FacilitySubstation.Clone(new Facility {Name = "Первая", Type = FacilityTypeSubStation, OwnerCompany = CompanyMrsk, Address = AddressSubstation});
            FacilityStation.Clone(new Facility {Name = "Свердловская", Type = FacilityTypeStation, OwnerCompany = CompanyEnel, Address = AddressStation});
        }

        private void GenerateMeasures()
        {
            MeasureKv.Clone(new Measure {FullName = "Киловольт", ShortName = "кВ"});
        }

        private void GenerateParameterGroups()
        {
            ParameterGroupEqType.Clone(new ParameterGroup {Name = "Тип оборудования"});
            ParameterGroupZip.Clone(new ParameterGroup {Name = "Тип ЗИП"});
            ParameterGroupBreakerType.Clone(new ParameterGroup {Name = "Тип выключателя"});
            ParameterGroupTransformatorType.Clone(new ParameterGroup {Name = "Тип трансформатора"});
            ParameterGroupTransformatorCurrentType.Clone(new ParameterGroup {Name = "Тип трансформатора тока"});
            ParameterGroupVoltage.Clone(new ParameterGroup {Name = "Номинальное напряжение", Measure = MeasureKv});
            ParameterGroupDrivesVoltage.Clone(new ParameterGroup {Name = "Номинальное напряжение двигателя завода пружин", Measure = MeasureKv});
        }

        private void GenerateParameters()
        {
            ParameterBreaker.Clone(new Parameter {ParameterGroup = ParameterGroupEqType, Value = "Выключатель"});
            ParameterTransformator.Clone(new Parameter {ParameterGroup = ParameterGroupEqType, Value = "Трансформатор"});
            ParameterBrakersDrive.Clone(new Parameter {ParameterGroup = ParameterGroupEqType, Value = "Привод"});
            ParameterKtpb.Clone(new Parameter {ParameterGroup = ParameterGroupEqType, Value = "КТПБ"});
            ParameterDependentEquipment.Clone(new Parameter {ParameterGroup = ParameterGroupEqType, Value = "Дополнительная комплектация"});

            ParameterZip1.Clone(new Parameter {ParameterGroup = ParameterGroupZip, Value = "ЗИП №1"});
            ParameterBreakerDeadTank.Clone(new Parameter {ParameterGroup = ParameterGroupBreakerType, Value = "Баковый"});
            ParameterBreakerLiveTank.Clone(new Parameter {ParameterGroup = ParameterGroupBreakerType, Value = "Колонковый"});
            ParameterTransformatorCurrent.Clone(new Parameter {ParameterGroup = ParameterGroupTransformatorType, Value = "Тока"});
            ParameterTransformatorVoltage.Clone(new Parameter {ParameterGroup = ParameterGroupTransformatorType, Value = "Напряжения"});
            ParameterVoltage35kV.Clone(new Parameter {ParameterGroup = ParameterGroupVoltage, Value = "35 кВ"});
            ParameterVoltage110kV.Clone(new Parameter {ParameterGroup = ParameterGroupVoltage, Value = "110 кВ"});
            ParameterVoltage220kV.Clone(new Parameter {ParameterGroup = ParameterGroupVoltage, Value = "220 кВ"});
            ParameterVoltage500kV.Clone(new Parameter {ParameterGroup = ParameterGroupVoltage, Value = "500 кВ"});
            ParameterVoltage110V.Clone(new Parameter {ParameterGroup = ParameterGroupDrivesVoltage, Value = "110 В"});
            ParameterVoltage220V.Clone(new Parameter {ParameterGroup = ParameterGroupDrivesVoltage, Value = "220 В"});
            ParameterTransformatorTrg110.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorCurrentType, Value = "Отдельностоящий" });
            ParameterTransformatorTvg110.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorCurrentType, Value = "Встроенный" });

            ParameterZip1.AddRequiredPreviousParameters(new[] {ParameterDependentEquipment});
            ParameterBreakerDeadTank.AddRequiredPreviousParameters(new[] {ParameterBreaker});
            ParameterBreakerLiveTank.AddRequiredPreviousParameters(new[] {ParameterBreaker});

            ParameterTransformatorCurrent.AddRequiredPreviousParameters(new[] {ParameterTransformator});
            ParameterTransformatorVoltage.AddRequiredPreviousParameters(new[] {ParameterTransformator});

            ParameterVoltage35kV.AddRequiredPreviousParameters(new[] {ParameterBreaker})
                                .AddRequiredPreviousParameters(new[] {ParameterTransformatorCurrent});
            ParameterVoltage110kV.AddRequiredPreviousParameters(new[] {ParameterBreaker})
                                 .AddRequiredPreviousParameters(new[] {ParameterTransformator}); 
            ParameterVoltage220kV.AddRequiredPreviousParameters(new[] {ParameterBreaker})
                                 .AddRequiredPreviousParameters(new[] {ParameterTransformator}); 
            ParameterVoltage500kV.AddRequiredPreviousParameters(new[] {ParameterBreaker, ParameterBreakerLiveTank});

            ParameterVoltage110V.AddRequiredPreviousParameters(new[] {ParameterBrakersDrive});

            ParameterVoltage220V.AddRequiredPreviousParameters(new[] {ParameterBrakersDrive});

            ParameterTransformatorTrg110.AddRequiredPreviousParameters(new[] { ParameterTransformatorCurrent });
            ParameterTransformatorTvg110.AddRequiredPreviousParameters(new[] { ParameterTransformatorCurrent });
        }

        private void GenerateRequiredDependentEquipmentsParameters()
        {
            RequiredChildProductRelationDrive.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> {ParameterBreaker},
                ChildProductParameters= new List<Parameter> {ParameterBrakersDrive}, ChildProductsAmount = 1, IsUnique = false
            });

            RequiredChildProductRelationBreakerBlock.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> {ParameterKtpb},
                ChildProductParameters= new List<Parameter> {ParameterBreaker}, ChildProductsAmount = 2, IsUnique = true
            });

            RequiredChildProductRelationTvg110.Clone(new ProductRelation
            {
                ParentProductParameters = new List<Parameter> {ParameterBreakerDeadTank},
                ChildProductParameters= new List<Parameter> {ParameterTransformatorTvg110}, ChildProductsAmount = 3, IsUnique = false
            });
        }



        private void GenerateProductBlocs()
        {
            ProductBlockVgb35.Clone(new ProductBlock
            {
                Name = "Выключатель баковый ВГБ-35",
                Parameters = new List<Parameter> {ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage35kV},
                Prices = new List<SumOnDate> {new SumOnDate {Sum = new Sum {Value = 450000}, Date = DateTime.Today}},
                StructureCostNumber = "StructureCostNumberVGB35",
            });

            ProductBlockVeb110.Clone(new ProductBlock
            {
                Name = "Выключатель баковый ВЭБ-110",
                Parameters = new List<Parameter> {ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage110kV},
                Prices = new List<SumOnDate> {new SumOnDate {Sum = new Sum { Value = 2000000 }, Date = DateTime.Today}},
                StructureCostNumber = "StructureCostNumber3",
            });

            ProductBlockZng110.Clone(new ProductBlock
            {
                Name = "Трансформатор напряжения ЗНГ-110",
                Parameters = new List<Parameter> {ParameterTransformator, ParameterTransformatorVoltage, ParameterVoltage110kV},
                Prices = new List<SumOnDate> {new SumOnDate {Sum = new Sum { Value = 250000 }, Date = DateTime.Today}},
                StructureCostNumber = "StructureCostNumber1"
            });

            ProductBlockBreakersDrive.Clone(new ProductBlock
            {
                Name = "Привод выключателя",
                Parameters = new List<Parameter> { ParameterBrakersDrive, ParameterVoltage220V },
                Prices = new List<SumOnDate> { new SumOnDate { Sum = new Sum { Value = 200000 }, Date = DateTime.Today } },
                StructureCostNumber = "StructureCostNumber4"
            });

            ProductBlockZip1.Clone(new ProductBlock
            {
                Name = "Групповой комплект ЗИП №1",
                Parameters = new List<Parameter> { ParameterDependentEquipment, ParameterZip1 },
                Prices = new List<SumOnDate> { new SumOnDate { Sum = new Sum { Value = 5000 }, Date = DateTime.Today } },
                StructureCostNumber = "StructureCostNumberZip1"
            });
        }

        private void GenerateProducts()
        {
            ProductVgb35.Clone(new Product
            {
                Designation = "Выключатель баковый ВГБ-35",
                ProductBlock = ProductBlockVgb35,
                DependentProducts = new List<Product> {ProductBreakersDrive}
            });

            ProductVeb110.Clone(new Product
            {
                Designation = "Выключатель баковый ВЭБ-110",
                ProductBlock = ProductBlockVeb110,
                DependentProducts = new List<Product> {ProductBreakersDrive}
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

            ProductZip1.Clone(new Product
            {
                Designation = "ЗиП №1",
                ProductBlock = ProductBlockZip1
            });
        }

        private void GenerateSalesUnits()
        {
            SalesUnitVeb1101Full.Clone(new SalesUnit {Project = ProjectStation, Product = ProductVeb110, Order = OrderVeb110, SerialNumber = "1", ProductionTerm = 90, AssembleTerm = 7, Cost = 3000000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50, Address = AddressSubstation, CostOfShipment = 100, DeliveryDate = DateTime.Today.AddDays(180), Facility = FacilitySubstation, DependentProducts = new List<ProductDependent> {new ProductDependent {Product = ProductZip1, Amount = 2 } } });
            SalesUnitVeb1102Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductVeb110, Order = OrderVeb110, SerialNumber = "2", ProductionTerm = 90, AssembleTerm = 7, Cost = 3000000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50, Address = AddressSubstation, CostOfShipment = 100, DeliveryDate = DateTime.Today.AddDays(180), Facility = FacilitySubstation, DependentProducts = new List<ProductDependent> { new ProductDependent { Product = ProductZip1, Amount = 3 }}}); 
            
            SalesUnitZng1101Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductZng110, Order = OrderZng110, SerialNumber = "5", ProductionTerm = 90, AssembleTerm = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50,  Address = AddressSubstation, CostOfShipment = 110, DeliveryDate = DateTime.Today.AddDays(180), Facility = FacilitySubstation}); 
            SalesUnitZng1102Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductZng110, Order = OrderZng110, SerialNumber = "6", ProductionTerm = 90, AssembleTerm = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50,  Address = AddressSubstation, CostOfShipment = 110, DeliveryDate = DateTime.Today.AddDays(180), Facility = FacilitySubstation}); 
            SalesUnitZng1103Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductZng110, Order = OrderZng110, SerialNumber = "7", ProductionTerm = 90, AssembleTerm = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50,  Address = AddressSubstation, CostOfShipment = 110, DeliveryDate = DateTime.Today.AddDays(180), Facility = FacilitySubstation});

            SalesUnitVeb1101.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductVeb110, Cost = 3000000, Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, DeliveryDate = DateTime.Today.AddDays(200)});
            SalesUnitVeb1102.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductVeb110, Cost = 3000000, Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, DeliveryDate = DateTime.Today.AddDays(200)});

            SalesUnitZng1101.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000 , Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilityStation, DeliveryDate = DateTime.Today.AddDays(200)});
            SalesUnitZng1102.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000 , Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilityStation, DeliveryDate = DateTime.Today.AddDays(200)});
            SalesUnitZng1103.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000 , Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilityStation, DeliveryDate = DateTime.Today.AddDays(200)});
            SalesUnitZng1104.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000 , Facility = FacilitySubstation, DeliveryDate = DateTime.Today.AddDays(200), PaymentConditionSet = PaymentConditionSet50Na50});
            SalesUnitZng1105.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000 , Facility = FacilityStation, DeliveryDate = DateTime.Today.AddDays(200), PaymentConditionSet = PaymentConditionSet50Na50});
            SalesUnitZng1106.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000,  Facility = FacilityStation, DeliveryDate = DateTime.Today.AddDays(200), PaymentConditionSet = PaymentConditionSet50Na50 });

        }

        private void GenerateOfferUnits()
        {
            OfferUnitVeb1101.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductVeb110, Cost = 3100000, ProductionTerm = 120, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilityStation});
            OfferUnitVeb1102.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductVeb110, Cost = 3100000, ProductionTerm = 120, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilityStation});
                                                                                    
            OfferUnitZng1101.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 180, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, DependentProducts = new List<ProductDependent> { new ProductDependent { Product = ProductZip1, Amount = 3 } } });
            OfferUnitZng1102.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 180, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, DependentProducts = new List<ProductDependent> { new ProductDependent { Product = ProductZip1, Amount = 2 } } });
            OfferUnitZng1103.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 180, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, DependentProducts = new List<ProductDependent> { new ProductDependent { Product = ProductZip1, Amount = 2 } } });
                                          
            OfferUnitZng1104.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 150, PaymentConditionSet = PaymentConditionSet30Na70, Facility = FacilitySubstation});
            OfferUnitZng1105.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 150, PaymentConditionSet = PaymentConditionSet30Na70, Facility = FacilitySubstation});
            OfferUnitZng1106.Clone(new OfferUnit { Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 150, PaymentConditionSet = PaymentConditionSet30Na70, Facility = FacilitySubstation});
        }

        private void GenerateOffers()
        {
            OfferMrsk.Clone(new Offer {Vat = 0.18, Project = ProjectSubstation, ValidityDate = DateTime.Today.AddDays(60), Author = EmployeeIvanov, SenderEmployee = EmployeeIvanov, RecipientEmployee = EmployeeSidorov, CopyToRecipients = new List<Employee> {EmployeePetrov}, RegistrationDetailsOfSender = new DocumentsRegistrationDetails {RegistrationNumber = "7412-17-0212", RegistrationDate = DateTime.Today}, RegistrationDetailsOfRecipient = new DocumentsRegistrationDetails {RegistrationNumber = "12f455", RegistrationDate = DateTime.Today.AddDays(-3)}});
        }

        private void GenerateTenderTypes()
        {
            TenderTypeProject.Clone(new TenderType { Name = "Проект", Type = TenderTypeEnum.ToProject });
            TenderTypeWork.Clone(new TenderType { Name = "Работы", Type = TenderTypeEnum.ToWork });
            TenderTypeSuply.Clone(new TenderType { Name = "Поставка", Type = TenderTypeEnum.ToSupply });
        }

        private void GenerateTenders()
        {
            TenderMrsk.Clone(new Tender { Project = ProjectSubstation, Types = new List<TenderType> { TenderTypeProject }, Winner = CompanyUetm, Participants = new List<Company> { CompanyUetm, CompanyEnel }, DateOpen = DateTime.Today, DateClose = DateTime.Today.AddDays(7) });
            Tender2.Clone(new Tender { Project = ProjectStation, Types = new List<TenderType> { TenderTypeProject, TenderTypeSuply, TenderTypeWork }, Winner = CompanyUetm, Participants = new List<Company> { CompanyUetm, CompanyEnel }, DateOpen = DateTime.Today, DateClose = DateTime.Today.AddDays(7) });
        }

        private void GenerateOrders()
        {
            OrderVeb110.Clone(new Order {Number = "8012-17", OpenOrderDate = DateTime.Today});
            OrderZng110.Clone(new Order {Number = "8111-15", OpenOrderDate = DateTime.Today.AddDays(-50)});
        }

        private void GenerateContracts()
        {
            ContractMrsk.Clone(new Contract { Contragent = CompanyMrsk, Date = DateTime.Today, Number = "0401-17-0001" });
            ContractFsk.Clone(new Contract { Contragent = CompanyFsk, Date = DateTime.Today, Number = "0401-17-0002" });
        }

        private void GenerateSpecifications()
        {
            SpecificationMrsk1.Clone(new Specification { Contract = ContractMrsk, Date = ContractMrsk.Date, Number = "1", Vat = 0.18 });
            SpecificationFsk.Clone(new Specification { Contract = ContractFsk, Date = ContractMrsk.Date, Number = "1", Vat = 0.18 });
        }

        private void GeneratePaymentConditions()
        {
            PaymentConditionAvans50.Clone(new PaymentCondition { Part = 0.5, DaysToPoint = -10, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            PaymentConditionDoplata50.Clone(new PaymentCondition { Part = 0.5, DaysToPoint = -14, PaymentConditionPoint = PaymentConditionPoint.ProductionEnd });

            PaymentConditionSet50Na50.Clone(new PaymentConditionSet {PaymentConditions = new List<PaymentCondition> {PaymentConditionAvans50, PaymentConditionDoplata50}});

            PaymentConditionAvans30.Clone(new PaymentCondition { Part = 0.3, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            PaymentConditionPostoplata70.Clone(new PaymentCondition { Part = 0.7, DaysToPoint = 1, PaymentConditionPoint = PaymentConditionPoint.Delivery });

            PaymentConditionSet30Na70.Clone(new PaymentConditionSet {PaymentConditions = new List<PaymentCondition> {PaymentConditionAvans30, PaymentConditionPostoplata70}});
        }

        private void GenerateCommonOption()
        {
            CommonOption.Clone(new CommonOption {OurCompanyId = CompanyUetm.Id, StandartPaymentsConditionSetId = PaymentConditionSet50Na50.Id});
        }

    }
}