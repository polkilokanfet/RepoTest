using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;

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
        public UserRole UserRoleEconomist;
        public UserRole UserRolePricer;
        public UserRole UserRoleDirector;
        public UserRole UserRolePlanMaker;


        public User UserIvanov;
        public User UserPetrov;

        public ProjectType ProjectTypeReconstruction;

        public Project ProjectSubstation;
        public Project ProjectStation;

        public FacilityType FacilityTypeStation;
        public FacilityType FacilityTypeSubStation;
                
        public Facility FacilityStation;
        public Facility FacilitySubstation;
                
        public Measure MeasureKv;

        public ParameterGroup ParameterGroupProductType;
        public ParameterGroup ParameterGroupZip;
        public ParameterGroup ParameterGroupEqType;
        public ParameterGroup ParameterGroupBreakerType;
        public ParameterGroup ParameterGroupTransformatorType;
        public ParameterGroup ParameterGroupTransformatorCurrentType;
        public ParameterGroup ParameterGroupVoltage;
        public ParameterGroup ParameterGroupDrivesVoltage;
        public ParameterGroup ParameterGroupIsolation;
        public ParameterGroup ParameterGroupAccuracy;
        public ParameterGroup ParameterGroupCurrent;
        public ParameterGroup ParameterGroupNewProductDesignation;

        public Parameter ParameterNewProduct;
        public Parameter ParameterMainEquipment;
        public Parameter ParameterDependentEquipment;
        public Parameter ParameterService;
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
        public Parameter ParameterTransformatorBuiltOut;
        public Parameter ParameterTransformatorBuiltIn;
        public Parameter ParameterDpu2;
        public Parameter ParameterDpu3;
        public Parameter ParameterDpu4;
        public Parameter ParameterAccuracy05P;
        public Parameter ParameterAccuracy10P;
        public Parameter ParameterCurrent2500;
        public Parameter ParameterCurrent3150;
        public Parameter ParameterCurrent4000;

        public ProductType ProductTypeDeadTankBreaker;
        public ProductType ProductTypeLiveTankBreaker;
        public ProductType ProductTypeCurrentTransformer;
        public ProductType ProductTypeVoltageTransformer;

        public ProductTypeDesignation ProductTypeDesignationDeadTankBreaker;
        public ProductTypeDesignation ProductTypeDesignationLiveTankBreaker;
        public ProductTypeDesignation ProductTypeDesignationCurrentTransformer;
        public ProductTypeDesignation ProductTypeDesignationVoltageTransformer;

        public ProductDesignation ProductDesignationVgb35;
        public ProductDesignation ProductDesignationVeb110;
        public ProductDesignation ProductDesignationZng110;
        public ProductDesignation ProductDesignationVeb220;
        public ProductDesignation ProductDesignationZng220;
        public ProductDesignation ProductDesignationTvg110;
        public ProductDesignation ProductDesignationTvg220;
        public ProductDesignation ProductDesignationTrg110;
        public ProductDesignation ProductDesignationTrg220;
        public ProductDesignation ProductDesignationDrive;

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

        public ProductBlockIsService ProductBlockIsService;
               
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

        public PaymentDocument PaymentDocument1;

        public PaymentActual PaymentActual1;
        public PaymentActual PaymentActual2;
        public PaymentActual PaymentActual3;
        public PaymentActual PaymentActual4;
        public PaymentActual PaymentActual5;

        public PaymentPlanned PaymentPlanned1;
        public PaymentPlanned PaymentPlanned2;
        public PaymentPlanned PaymentPlanned3;
        public PaymentPlanned PaymentPlanned4;
        public PaymentPlanned PaymentPlanned5;

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

        public Note Note1;
        public Note Note2;
        public Note Note3;
        public Note Note4;

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
            LocalityMoscow.Clone(new Locality {LocalityType = LocalityTypeCity, Name = "Москва", Region = RegionMoskovskayaOblast, IsCountryCapital = true, IsDistrictCapital = true, IsRegionCapital = true, DistanceToEkb = 2000});
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
            UserRoleAdmin.Clone(new UserRole {Role = Role.Admin, Name = "Администратор"});
            UserRoleSalesManager.Clone(new UserRole {Role = Role.SalesManager, Name = "Менеджер"});
            UserRoleEconomist.Clone(new UserRole { Role = Role.Economist, Name = "Экономист" });
            UserRolePricer.Clone(new UserRole { Role = Role.Pricer, Name = "Расчетчик" });
            UserRoleDirector.Clone(new UserRole { Role = Role.Director, Name = "Директор" });
            UserRolePlanMaker.Clone(new UserRole { Role = Role.PlanMaker, Name = "Плановик" });
        }

        private void GenerateUsers()
        {
            UserIvanov.Clone(new User { Login = "1", Password = StringToGuid.GetHashString("1"), Employee = EmployeeIvanov, PersonalNumber = "1", Roles = new List<UserRole> { UserRoleAdmin, UserRoleDataBaseFiller, UserRoleSalesManager, UserRolePlanMaker, UserRoleDirector, UserRoleEconomist, UserRolePricer } });
            UserPetrov.Clone(new User { Login = "2", Password = StringToGuid.GetHashString("2"), Employee = EmployeePetrov, PersonalNumber = "2", Roles = new List<UserRole> { UserRoleDataBaseFiller } });
        }

        private void GenerateProjectTypess()
        {
            ProjectTypeReconstruction.Clone(new ProjectType { Name = "Реконструкция" });
        }

        private void GenerateProjects()
        {
            ProjectSubstation.Clone(new Project { Name = "Реконструкция ПС Первая", Manager = UserIvanov, ProjectType = ProjectTypeReconstruction, Notes = new List<Note> {Note1, Note2} });
            ProjectStation.Clone(new Project { Name = "Строительство Свердловской ТЭЦ", Manager = UserIvanov, ProjectType = ProjectTypeReconstruction, Notes = new List<Note> { Note3, Note4 } });
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
            ParameterGroupProductType.Clone(new ParameterGroup { Name = "Тип продукта" });
            ParameterGroupEqType.Clone(new ParameterGroup { Name = "Тип оборудования" });
            ParameterGroupZip.Clone(new ParameterGroup {Name = "Тип ЗИП"});
            ParameterGroupBreakerType.Clone(new ParameterGroup {Name = "Тип выключателя"});
            ParameterGroupTransformatorType.Clone(new ParameterGroup {Name = "Тип трансформатора"});
            ParameterGroupTransformatorCurrentType.Clone(new ParameterGroup {Name = "Тип трансформатора тока"});
            ParameterGroupVoltage.Clone(new ParameterGroup {Name = "Номинальное напряжение", Measure = MeasureKv});
            ParameterGroupDrivesVoltage.Clone(new ParameterGroup { Name = "Номинальное напряжение двигателя завода пружин", Measure = MeasureKv });
            ParameterGroupIsolation.Clone(new ParameterGroup { Name = "Длина пути утечки" });
            ParameterGroupAccuracy.Clone(new ParameterGroup { Name = "Класс точности" });
            ParameterGroupCurrent.Clone(new ParameterGroup { Name = "Номинальный ток" });
            ParameterGroupNewProductDesignation.Clone(new ParameterGroup { Name = "Обозначение" });
        }

        private void GenerateParameters()
        {
            ParameterNewProduct.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Оборудование новое" });
            ParameterMainEquipment.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Оборудование главное" });
            ParameterDependentEquipment.Clone(new Parameter {ParameterGroup = ParameterGroupProductType, Value = "Оборудование дополнительное"});
            ParameterService.Clone(new Parameter { ParameterGroup = ParameterGroupProductType, Value = "Услуга" });

            ParameterBreaker.Clone(new Parameter {ParameterGroup = ParameterGroupEqType, Value = "Выключатель"});
            ParameterTransformator.Clone(new Parameter {ParameterGroup = ParameterGroupEqType, Value = "Трансформатор"});
            ParameterBrakersDrive.Clone(new Parameter {ParameterGroup = ParameterGroupEqType, Value = "Привод"});
            ParameterKtpb.Clone(new Parameter {ParameterGroup = ParameterGroupEqType, Value = "КТПБ"});

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
            ParameterTransformatorBuiltOut.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorCurrentType, Value = "Отдельностоящий" });
            ParameterTransformatorBuiltIn.Clone(new Parameter { ParameterGroup = ParameterGroupTransformatorCurrentType, Value = "Встроенный" });
            ParameterDpu2.Clone(new Parameter { ParameterGroup = ParameterGroupIsolation, Value = "II*" });
            ParameterDpu3.Clone(new Parameter { ParameterGroup = ParameterGroupIsolation, Value = "III" });
            ParameterDpu4.Clone(new Parameter { ParameterGroup = ParameterGroupIsolation, Value = "IV" });
            ParameterAccuracy05P.Clone(new Parameter { ParameterGroup = ParameterGroupAccuracy, Value = "5P" });
            ParameterAccuracy10P.Clone(new Parameter { ParameterGroup = ParameterGroupAccuracy, Value = "10P" });
            ParameterCurrent2500.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "2500 А" });
            ParameterCurrent3150.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "3150 А" });
            ParameterCurrent4000.Clone(new Parameter { ParameterGroup = ParameterGroupCurrent, Value = "4000 А" });




            ParameterBreaker.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterTransformator.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterBrakersDrive.AddRequiredPreviousParameters(ParameterMainEquipment);
            ParameterKtpb.AddRequiredPreviousParameters(ParameterMainEquipment);

            ParameterBreakerDeadTank.AddRequiredPreviousParameters(ParameterBreaker);
            ParameterBreakerLiveTank.AddRequiredPreviousParameters(ParameterBreaker);
            ParameterZip1.AddRequiredPreviousParameters(ParameterDependentEquipment);

            ParameterTransformatorCurrent.AddRequiredPreviousParameters(ParameterTransformator);
            ParameterTransformatorVoltage.AddRequiredPreviousParameters(ParameterTransformator);

            ParameterAccuracy05P.AddRequiredPreviousParameters(ParameterTransformatorBuiltIn);
            ParameterAccuracy10P.AddRequiredPreviousParameters(ParameterTransformatorBuiltIn);

            ParameterCurrent2500.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage110kV)
                                .AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage220kV);
            ParameterCurrent3150.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage110kV)
                                .AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage220kV);
            ParameterCurrent4000.AddRequiredPreviousParameters(ParameterBreaker, ParameterVoltage500kV);

            ParameterVoltage35kV.AddRequiredPreviousParameters(ParameterBreaker)
                                .AddRequiredPreviousParameters(ParameterTransformatorCurrent);
            ParameterVoltage110kV.AddRequiredPreviousParameters(ParameterBreaker)
                                 .AddRequiredPreviousParameters(ParameterTransformator); 
            ParameterVoltage220kV.AddRequiredPreviousParameters(ParameterBreaker)
                                 .AddRequiredPreviousParameters(ParameterTransformator); 
            ParameterVoltage500kV.AddRequiredPreviousParameters(ParameterBreaker, ParameterBreakerLiveTank)
                                 .AddRequiredPreviousParameters(ParameterTransformator, ParameterTransformatorBuiltOut);

            ParameterVoltage110V.AddRequiredPreviousParameters(ParameterBrakersDrive);

            ParameterVoltage220V.AddRequiredPreviousParameters(ParameterBrakersDrive);

            ParameterTransformatorBuiltOut.AddRequiredPreviousParameters(ParameterTransformatorCurrent);
            ParameterTransformatorBuiltIn.AddRequiredPreviousParameters(ParameterTransformatorCurrent);

            ParameterDpu2.AddRequiredPreviousParameters(ParameterBreaker)
                         .AddRequiredPreviousParameters(ParameterTransformator, ParameterTransformatorBuiltOut)
                         .AddRequiredPreviousParameters(ParameterTransformator, ParameterTransformatorVoltage);
            ParameterDpu3.AddRequiredPreviousParameters(ParameterBreaker)
                         .AddRequiredPreviousParameters(ParameterTransformator, ParameterTransformatorBuiltOut)
                         .AddRequiredPreviousParameters(ParameterTransformator, ParameterTransformatorVoltage);
            ParameterDpu4.AddRequiredPreviousParameters(ParameterBreaker)
                         .AddRequiredPreviousParameters(ParameterTransformator, ParameterTransformatorBuiltOut)
                         .AddRequiredPreviousParameters(ParameterTransformator, ParameterTransformatorVoltage);
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
                ParentProductParameters = new List<Parameter> {ParameterBreakerDeadTank, ParameterVoltage110kV},
                ChildProductParameters= new List<Parameter> {ParameterTransformatorBuiltIn, ParameterVoltage110kV }, ChildProductsAmount = 3, IsUnique = false
            });
        }

        private void GenerateProductTypes()
        {
            ProductTypeDeadTankBreaker.Clone(new ProductType {Name = "Выключатель баковый"});
            ProductTypeLiveTankBreaker.Clone(new ProductType { Name = "Выключатель колонковый" });
            ProductTypeCurrentTransformer.Clone(new ProductType { Name = "Трансформатор тока" });
            ProductTypeVoltageTransformer.Clone(new ProductType { Name = "Трансформатор напряжения" });
        }

        private void GenerateProductTypeDesignations()
        {
            ProductTypeDesignationDeadTankBreaker.Clone(new ProductTypeDesignation {ProductType = ProductTypeDeadTankBreaker, Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank } });
            ProductTypeDesignationLiveTankBreaker.Clone(new ProductTypeDesignation { ProductType = ProductTypeLiveTankBreaker, Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerLiveTank } });
            ProductTypeDesignationCurrentTransformer.Clone(new ProductTypeDesignation { ProductType = ProductTypeCurrentTransformer, Parameters = new List<Parameter> { ParameterTransformator, ParameterTransformatorCurrent } });
            ProductTypeDesignationVoltageTransformer.Clone(new ProductTypeDesignation { ProductType = ProductTypeVoltageTransformer, Parameters = new List<Parameter> { ParameterTransformator, ParameterTransformatorVoltage } });
        }

        private void GenerateProductDesignations()
        {
            ProductDesignationVgb35.Clone(new ProductDesignation { Designation = "ВГБ-УЭТМ-35", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage35kV } });
            ProductDesignationVeb110.Clone(new ProductDesignation { Designation = "ВЭБ-УЭТМ-110", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage110kV } });
            ProductDesignationVeb220.Clone(new ProductDesignation { Designation = "ВЭБ-УЭТМ-220", Parameters = new List<Parameter> { ParameterBreakerDeadTank, ParameterVoltage220kV } }); 
            ProductDesignationZng110.Clone(new ProductDesignation { Designation = "ЗНГ-УЭТМ-110", Parameters = new List<Parameter> { ParameterTransformatorVoltage, ParameterVoltage110kV } });
            ProductDesignationZng220.Clone(new ProductDesignation { Designation = "ЗНГ-УЭТМ-220", Parameters = new List<Parameter> { ParameterTransformatorVoltage, ParameterVoltage220kV } });
            ProductDesignationTvg110.Clone(new ProductDesignation { Designation = "ТВГ-УЭТМ-110", Parameters = new List<Parameter> { ParameterTransformatorCurrent, ParameterTransformatorBuiltIn, ParameterVoltage110kV } });
            ProductDesignationTvg220.Clone(new ProductDesignation { Designation = "ТВГ-УЭТМ-220", Parameters = new List<Parameter> { ParameterTransformatorCurrent, ParameterTransformatorBuiltIn, ParameterVoltage220kV } });
            ProductDesignationTrg110.Clone(new ProductDesignation { Designation = "ТРГ-УЭТМ-110", Parameters = new List<Parameter> { ParameterTransformatorCurrent, ParameterTransformatorBuiltOut, ParameterVoltage110kV } });
            ProductDesignationTrg220.Clone(new ProductDesignation { Designation = "ТРГ-УЭТМ-220", Parameters = new List<Parameter> { ParameterTransformatorCurrent, ParameterTransformatorBuiltOut, ParameterVoltage220kV } });
            ProductDesignationDrive.Clone(new ProductDesignation { Designation = "Привод", Parameters = new List<Parameter> { ParameterBrakersDrive } });
        }

        private void GenerateProductBlocs()
        {
            ProductBlockVgb35.Clone(new ProductBlock
            {
                //DesignationSpecial = "Блок Выключатель баковый ВГБ-35",
                Parameters = new List<Parameter> { ParameterMainEquipment, ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage35kV},
                Prices = new List<SumOnDate> {new SumOnDate {Sum = 450000, Date = DateTime.Today}},
                StructureCostNumber = "123",
            });

            ProductBlockVeb110.Clone(new ProductBlock
            {
                //DesignationSpecial = "Блок Выключатель баковый ВЭБ-110",
                Parameters = new List<Parameter> {ParameterMainEquipment, ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage110kV},
                Prices = new List<SumOnDate> {new SumOnDate {Sum = 2000000, Date = DateTime.Today}},
                StructureCostNumber = "321",
            });

            ProductBlockZng110.Clone(new ProductBlock
            {
                //DesignationSpecial = "Блок Трансформатор напряжения ЗНГ-110",
                Parameters = new List<Parameter> { ParameterMainEquipment, ParameterTransformator, ParameterTransformatorVoltage, ParameterVoltage110kV},
                Prices = new List<SumOnDate> {new SumOnDate {Sum = 250000, Date = DateTime.Today.AddDays(-95)}},
                StructureCostNumber = "456"
            });

            ProductBlockBreakersDrive.Clone(new ProductBlock
            {
                DesignationSpecial = "ППрК",
                Parameters = new List<Parameter> { ParameterMainEquipment, ParameterBrakersDrive, ParameterVoltage220V },
                Prices = new List<SumOnDate> { new SumOnDate { Sum = 200000, Date = DateTime.Today } },
                StructureCostNumber = "654"
            });

            ProductBlockZip1.Clone(new ProductBlock
            {
                DesignationSpecial = "Блок Групповой комплект ЗИП №1",
                Parameters = new List<Parameter> { ParameterDependentEquipment, ParameterZip1 },
                Prices = new List<SumOnDate> { new SumOnDate { Sum = 2500, Date = DateTime.Today } },
                StructureCostNumber = "789"
            });
        }

        private void GenerateProducts()
        {
            ProductVgb35.Clone(new Product
            {
                //DesignationSpecial = "ВГБ-35",
                ProductBlock = ProductBlockVgb35,
                DependentProducts = new List<ProductDependent> { new ProductDependent {Product = ProductBreakersDrive}  }
            });

            ProductVeb110.Clone(new Product
            {
                //DesignationSpecial = "ВЭБ-110",
                ProductBlock = ProductBlockVeb110,
                DependentProducts = new List<ProductDependent> { new ProductDependent { Product = ProductBreakersDrive } }
            });

            ProductZng110.Clone(new Product
            {
                //DesignationSpecial = "Трансформатор напряжения ЗНГ-110",
                ProductBlock = ProductBlockZng110
            });

            ProductBreakersDrive.Clone(new Product
            {
                //DesignationSpecial = "Привод выключателя",
                ProductBlock = ProductBlockBreakersDrive
            });

            ProductZip1.Clone(new Product
            {
                //DesignationSpecial = "ЗиП №1",
                ProductBlock = ProductBlockZip1
            });
        }

        private void GeneratePaymentDocuments()
        {
            PaymentDocument1.Clone(new PaymentDocument {Number = "№1", Payments = new List<PaymentActual> { PaymentActual1, PaymentActual2, PaymentActual3, PaymentActual4, PaymentActual5 } });
        }

        private void GeneratePaymentsActual()
        {
            PaymentActual1.Clone(new PaymentActual { Sum = 350000, Date = DateTime.Today });
            PaymentActual2.Clone(new PaymentActual { Sum = 360000, Date = DateTime.Today });
            PaymentActual3.Clone(new PaymentActual { Sum = 370000, Date = DateTime.Today });
            PaymentActual4.Clone(new PaymentActual { Sum = 380000, Date = DateTime.Today });
            PaymentActual5.Clone(new PaymentActual { Sum = 390000, Date = DateTime.Today });
        }

        private void GeneratePaymentsPlanned()
        {
            PaymentPlanned1.Clone(new PaymentPlanned { Part = 0.1, Date = DateTime.Today.AddDays(-5), Condition = PaymentConditionDoplata50 });
            PaymentPlanned2.Clone(new PaymentPlanned { Part = 0.2, Date = DateTime.Today.AddDays(20), Condition = PaymentConditionDoplata50 });
            PaymentPlanned3.Clone(new PaymentPlanned { Part = 0.3, Date = DateTime.Today.AddDays(30), Condition = PaymentConditionDoplata50 });
            PaymentPlanned4.Clone(new PaymentPlanned { Part = 0.4, Date = DateTime.Today.AddDays(40), Condition = PaymentConditionDoplata50 });
            PaymentPlanned5.Clone(new PaymentPlanned { Part = 0.5, Date = DateTime.Today.AddDays(50), Condition = PaymentConditionDoplata50 });
        }

        private void GenerateProductBlockIsService()
        {
            ProductBlockIsService.Clone(new ProductBlockIsService {Parameters = new List<Parameter>() {ParameterService} });
        }

        private void GenerateSalesUnits()
        {
            SalesUnitVeb1101Full.Clone(new SalesUnit {Project = ProjectStation, Product = ProductVeb110, Order = OrderVeb110, SerialNumber = "1", ProductionTerm = 90, AssembleTerm = 7, Cost = 3000000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50, Address = AddressSubstation, Facility = FacilityStation, ProductsIncluded = new List<ProductIncluded> {new ProductIncluded { Product = ProductZip1, Amount = 2 } }, PaymentsActual = new List<PaymentActual> { PaymentActual1 }, PaymentsPlanned = new List<PaymentPlanned> {PaymentPlanned1, PaymentPlanned2, PaymentPlanned3, PaymentPlanned4, PaymentPlanned5} });
            SalesUnitVeb1102Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductVeb110, Order = OrderVeb110, SerialNumber = "2", ProductionTerm = 90, AssembleTerm = 7, Cost = 3000000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50, Address = AddressSubstation, Facility = FacilityStation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 3 }}, PaymentsActual = new List<PaymentActual> { PaymentActual2 } }); 
                                                                                                                                                                                                                                                                                                                            
            SalesUnitZng1101Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductZng110, Order = OrderZng110, SerialNumber = "5", ProductionTerm = 90, AssembleTerm = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50,  Address = AddressSubstation, Facility = FacilityStation, PaymentsActual = new List<PaymentActual> { PaymentActual3 } }); 
            SalesUnitZng1102Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductZng110, Order = OrderZng110, SerialNumber = "6", ProductionTerm = 90, AssembleTerm = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50,  Address = AddressSubstation, Facility = FacilityStation, PaymentsActual = new List<PaymentActual> { PaymentActual4 } }); 
            SalesUnitZng1103Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductZng110, Order = OrderZng110, SerialNumber = "7", ProductionTerm = 90, AssembleTerm = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50,  Address = AddressSubstation, Facility = FacilityStation, PaymentsActual = new List<PaymentActual> { PaymentActual5 } });

            SalesUnitVeb1101.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductVeb110, Cost = 3000000, Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation });
            SalesUnitVeb1102.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductVeb110, Cost = 3000000, Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation });
                                                                                                                                                                                                                                
            SalesUnitZng1101.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000 , Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation });
            SalesUnitZng1102.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000 , Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation });
            SalesUnitZng1103.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000 , Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation });
            SalesUnitZng1104.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000 , Facility = FacilitySubstation, PaymentConditionSet = PaymentConditionSet50Na50});
            SalesUnitZng1105.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000 , Facility = FacilitySubstation, PaymentConditionSet = PaymentConditionSet50Na50});
            SalesUnitZng1106.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000,  Facility = FacilitySubstation, PaymentConditionSet = PaymentConditionSet50Na50 });

        }

        private void GenerateOfferUnits()
        {
            OfferUnitVeb1101.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductVeb110, Cost = 3100000, ProductionTerm = 120, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilityStation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 3 } } });
            OfferUnitVeb1102.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductVeb110, Cost = 3100000, ProductionTerm = 120, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilityStation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 3 } } });

            OfferUnitZng1101.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 180, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 3 } } });
            OfferUnitZng1102.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 180, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 2 } } });
            OfferUnitZng1103.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 180, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 2 } } });
                                          
            OfferUnitZng1104.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 150, PaymentConditionSet = PaymentConditionSet30Na70, Facility = FacilitySubstation});
            OfferUnitZng1105.Clone(new OfferUnit {Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 150, PaymentConditionSet = PaymentConditionSet30Na70, Facility = FacilitySubstation});
            OfferUnitZng1106.Clone(new OfferUnit { Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 150, PaymentConditionSet = PaymentConditionSet30Na70, Facility = FacilitySubstation});
        }

        private void GenerateOffers()
        {
            OfferMrsk.Clone(new Offer {Number = new DocumentNumber(), Vat = 18, Project = ProjectSubstation, ValidityDate = DateTime.Today.AddDays(60), Author = EmployeeIvanov, SenderEmployee = EmployeeIvanov, RecipientEmployee = EmployeeSidorov, CopyToRecipients = new List<Employee> {EmployeePetrov}, RegistrationDetailsOfRecipient = new DocumentsRegistrationDetails {Number = "12f455", Date = DateTime.Today.AddDays(-3)}});
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
            OrderVeb110.Clone(new Order {Number = "8012-17", DateOpen = DateTime.Today});
            OrderZng110.Clone(new Order {Number = "8111-15", DateOpen = DateTime.Today.AddDays(-50)});
        }

        private void GenerateContracts()
        {
            ContractMrsk.Clone(new Contract { Contragent = CompanyMrsk, Date = DateTime.Today, Number = "0401-17-0001" });
            ContractFsk.Clone(new Contract { Contragent = CompanyFsk, Date = DateTime.Today, Number = "0401-17-0002" });
        }

        private void GenerateSpecifications()
        {
            SpecificationMrsk1.Clone(new Specification { Contract = ContractMrsk, Date = ContractMrsk.Date, Number = "1", Vat = 18 });
            SpecificationFsk.Clone(new Specification { Contract = ContractFsk, Date = ContractFsk.Date, Number = "1", Vat = 18 });
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
            CommonOption.Clone(new CommonOption {OurCompany = CompanyUetm, StandartPaymentsConditionSet = PaymentConditionSet50Na50, NewProductParameter = ParameterNewProduct, NewProductParameterGroup = ParameterGroupNewProductDesignation });
        }

        private void GenerateNotes()
        {
            Note1.Clone(new Note {Date = DateTime.Today.AddDays(-10), Text = "Заметка 1"});
            Note2.Clone(new Note {Date = DateTime.Today.AddDays(-20), Text = "Заметка 2"});
            Note3.Clone(new Note {Date = DateTime.Today.AddDays(-30), Text = "Заметка 3"});
            Note4.Clone(new Note {Date = DateTime.Today.AddDays(-40), Text = "Заметка 4"});
        }

    }
}