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
            CompanyFormAo = new CompanyForm {FullName = "����������� ��������", ShortName = "��"};
            CompanyFormPao = new CompanyForm {FullName = "��������� ����������� ��������", ShortName = "���"};
            CompanyFormOao = new CompanyForm {FullName = "�������� ����������� ��������", ShortName = "���"};
            CompanyFormZao = new CompanyForm {FullName = "�������� ����������� ��������", ShortName = "���"};
        }

        public void GenerateActivityFields()
        {
            ActivityFieldProducerOfHvt = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ProducerOfHighVoltageEquipment, Name = "������������� ���" };
            ActivityFieldBuilder = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.Builder, Name = "���������" };
            ActivityFieldElectricityTransmission = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityTransmission, Name = "�������� ��������������" };
            ActivityFieldElectricityGeneration = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityGeneration, Name = "��������� ��������������" };
        }

        public void GenerateLocalityTypes()
        {
            LocalityTypeCity = new LocalityType { FullName = "�����", ShortName = "�." };
        }

        public void GenerateLocalities()
        {
            LocalityMoscow = new Locality { LocalityType = LocalityTypeCity, Name = "������", Region = RegionMoskovskayaOblast, IsCountryCapital = true, IsDistrictsCapital = true, IsRegionCapital = true };
            LocalityEkaterinburg = new Locality { LocalityType = LocalityTypeCity, Name = "������������", Region = RegionSverdlovskayaOblast, IsDistrictsCapital = true, IsRegionCapital = true };
        }

        public void GenerateRegions()
        {
            RegionMoskovskayaOblast = new Region { Name = "���������� �������", Localities = new List<Locality> { LocalityMoscow }, District = DistrictCentr};
            RegionSverdlovskayaOblast = new Region { Name = "������������", Localities = new List<Locality> { LocalityEkaterinburg }, District = DistrictUral};
        }

        public void GenerateDistricts()
        {
            DistrictCentr = new District { Country = CountryRussia, Name = "����������� ����������� �����", Regions = new List<Region>() {RegionMoskovskayaOblast} };
            DistrictUral = new District { Country = CountryRussia, Name = "��������� ����������� �����", Regions = new List<Region>() {RegionSverdlovskayaOblast} };
        }

        public void GenerateCountries()
        {
            CountryRussia = new Country { Name = "������", Districts = new List<District> {DistrictCentr, DistrictUral} };
        }

        public void GenerateAddresses()
        {
            AddressOfUetm = new Address { Description = "��.��������� ������, �.22", Locality = LocalityEkaterinburg };
            AddressOfStation = new Address {Description = "��.�����������, 5", Locality = LocalityEkaterinburg };
            AddressOfSubstation = new Address {Description = "��.��������������, 25", Locality = LocalityMoscow };
        }

        public void GenerateBankDetails()
        {
            BankDetailsOfUetm = new BankDetails { BankName = "��������", BankIdentificationCode = "1111", CorrespondentAccount = "213564", CheckingAccount = "444554" };
        }

        public void GenerateCompanies()
        {
            CompanyUetm = new Company { FullName = "�����������������", ShortName = "����", Form = CompanyFormAo, AddressLegal = AddressOfUetm, BankDetailsList = new List<BankDetails> { BankDetailsOfUetm }, ActivityFilds = new List<ActivityField> { ActivityFieldProducerOfHvt } };
            CompanyRosseti = new Company { FullName = "�������", ShortName = "�������", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission } };
            CompanyFsk = new Company { FullName = "����������� ������� ��������", ShortName = "���", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti };
            CompanyMrsk = new Company { FullName = "��������������� ����������������� ����", ShortName = "����", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti };
            CompanyEnel = new Company { FullName = "����", ShortName = "����", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityGeneration } };
        }

        public void GeneratePersons()
        {
            PersonIvanov = new Person { Surname = "������", Name = "����", Patronymic = "��������", IsMan = true, Employees = new List<Employee>() { } };
        }

        public void GenerateEmployeesPositions()
        {
            EmployeesPositionDirector = new EmployeesPosition { Name = "��������" };
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
            Project1 = new Project { Name = "������������� �� ������", Manager = UserIvanov };
            Project2 = new Project { Name = "������������� ������������ ���", Manager = UserIvanov };
        }

        public void GenerateFacilityTypes()
        {
            FacilityTypeStation = new FacilityType { FullName = "��������������������", ShortName = "���" };
            FacilityTypeSubStation = new FacilityType { FullName = "������������� �������", ShortName = "��" };
        }

        public void GenerateFacilities()
        {
            FacilitySubstation = new Facility { Name = "������", Type = FacilityTypeSubStation, OwnerCompany = CompanyMrsk, Address = AddressOfSubstation};
            FacilityStation = new Facility { Name = "������������", Type = FacilityTypeStation, OwnerCompany = CompanyEnel, Address = AddressOfStation };
        }

        public void GenerateMeasures()
        {
            MeasureKv = new Measure { FullName = "���������", ShortName = "��" };
        }

        public void GenerateParameterGroups()
        {
            ParameterGroupEqType = new ParameterGroup { Name = "��� ������������" };
            ParameterGroupBreakerType = new ParameterGroup { Name = "��� �����������" };
            ParameterGroupTransformatorType = new ParameterGroup { Name = "��� ��������������" };
            ParameterGroupVoltage = new ParameterGroup { Name = "����������� ����������", Measure = MeasureKv };
        }

        public void GenerateParameters()
        {
            ParameterBreaker = new Parameter { Group = ParameterGroupEqType, Value = "�����������" };
            ParameterTransformator = new Parameter { Group = ParameterGroupEqType, Value = "�������������" };
            ParameterBrakersDrive = new Parameter { Group = ParameterGroupEqType, Value = "������" };
            ParameterBreakerBlock = new Parameter { Group = ParameterGroupEqType, Value = "���� �����������" };

            ParameterBreakerDeadTank = new Parameter { Group = ParameterGroupBreakerType, Value = "�������" }.AddRequiredPreviousParameters(new []{ParameterBreaker});
            ParameterBreakerLiveTank = new Parameter { Group = ParameterGroupBreakerType, Value = "����������" }.AddRequiredPreviousParameters(new[] { ParameterBreaker });

            ParameterTransformatorCurrent = new Parameter { Group = ParameterGroupTransformatorType, Value = "����" }.AddRequiredPreviousParameters(new[] { ParameterTransformator });
            ParameterTransformatorVoltage = new Parameter { Group = ParameterGroupTransformatorType, Value = "����������" }.AddRequiredPreviousParameters(new[] { ParameterTransformator });

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
            PartZng110 = new Part { Designation = "���-110", Parameters = new List<Parameter> { ParameterTransformator, ParameterTransformatorVoltage, ParameterVoltage110kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 75 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber1"};
            PartVgb35 = new Part { Designation = "���-35", Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage35kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 50 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber2" };
            PartVeb110 = new Part { Designation = "���-110", Parameters = new List<Parameter> { ParameterBreaker, ParameterBreakerDeadTank, ParameterVoltage110kV },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 100 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber3" };
            PartBreakesDrive = new Part { Designation = "������ �����������", Parameters = new List<Parameter> { ParameterBreaker },
                Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 100 }, Date = DateTime.Today } }, StructureCostNumber = "StructureCostNumber4" };
        }

        public void GenerateEquipments()
        {
            ProductVeb110 = new Product { Designation = "����������� ������� ���-110", Part = PartVeb110, DependentProducts  = new List<Product> {ProductBreakersDrive} };
            ProductZng110 = new Product { Designation = "������������� ���������� ���-110", Part = PartZng110 };
            ProductBreakersDrive = new Product { Designation = "������ �����������", Part = PartBreakesDrive };
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
            TenderTypeProject = new TenderType { Name = "��������-������������� ������", Type = TenderTypeEnum.ToProject };
        }
    }
}