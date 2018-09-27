using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.TestDataGenerator
{
    public partial class TestData
    {
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

        public IEnumerable<TData> GetAll<TData>()
        {
            var fields = GetType().GetFields().Where(x => x.FieldType == typeof(TData)).ToList();
            foreach (var field in fields)
            {
                yield return (TData)field.GetValue(this);
            }
        }
        
    }
    public partial class TestData
    {

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

        public ProjectType ProjectTypeReconstruction;

        public Project ProjectSubstation;
        public Project ProjectStation;

        public FacilityType FacilityTypeStation;
        public FacilityType FacilityTypeSubStation;

        public Facility FacilityStation;
        public Facility FacilitySubstation;

        public Measure MeasureKv;

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


        private void GenerateCompanyForms()
        {
            CompanyFormAo.Clone(new CompanyForm { FullName = "Акционерное общество", ShortName = "АО" });
            CompanyFormPao.Clone(new CompanyForm { FullName = "Публичное акционерное общество", ShortName = "ПАО" });
            CompanyFormOao.Clone(new CompanyForm { FullName = "Открытое акционерное общество", ShortName = "ОАО" });
            CompanyFormZao.Clone(new CompanyForm { FullName = "Закрытое акционерное общество", ShortName = "ЗАО" });
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
            LocalityVillage.Clone(new LocalityType { FullName = "Деревня", ShortName = "д." });
        }

        private void GenerateLocalities()
        {
            LocalityMoscow.Clone(new Locality { LocalityType = LocalityTypeCity, Name = "Москва", Region = RegionMoskovskayaOblast, IsCountryCapital = true, IsDistrictCapital = true, IsRegionCapital = true, DistanceToEkb = 2000 });
            LocalityEkaterinburg.Clone(new Locality { LocalityType = LocalityTypeCity, Name = "Екатеринбург", Region = RegionSverdlovskayaOblast, IsDistrictCapital = true, IsRegionCapital = true });
        }

        private void GenerateRegions()
        {
            RegionMoskovskayaOblast.Clone(new Region { Name = "Московская область", District = DistrictCentr });
            RegionSverdlovskayaOblast.Clone(new Region { Name = "Свердловская область", District = DistrictUral });
        }

        private void GenerateDistricts()
        {
            DistrictCentr.Clone(new District { Country = CountryRussia, Name = "Центральный федеральный округ" });
            DistrictUral.Clone(new District { Country = CountryRussia, Name = "Уральский федеральный округ" });
        }

        private void GenerateCountries()
        {
            CountryRussia.Clone(new Country { Name = "Россия" });
            CountryUsa.Clone(new Country { Name = "США" });
        }

        private void GenerateAddresses()
        {
            AddressUetm.Clone(new Address { Description = "ул.Фронтовых бригад, д.22", Locality = LocalityEkaterinburg });
            AddressStation.Clone(new Address { Description = "ул.Станционная, 5", Locality = LocalityEkaterinburg });
            AddressSubstation.Clone(new Address { Description = "ул.ПодСтанционная, 25", Locality = LocalityMoscow });
        }

        private void GenerateBankDetails()
        {
            BankDetailsOfUetm.Clone(new BankDetails { BankName = "Объебанк", BankIdentificationCode = "1111", CorrespondentAccount = "213564", CheckingAccount = "444554" });
        }

        private void GenerateCompanies()
        {
            CompanyUetm.Clone(new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Inn = "23658", Form = CompanyFormAo, AddressLegal = AddressUetm, BankDetailsList = new List<BankDetails> { BankDetailsOfUetm }, ActivityFilds = new List<ActivityField> { ActivityFieldProducerOfHvt } });
            CompanyRosseti.Clone(new Company { FullName = "Россети", ShortName = "Россети", Inn = "23659", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission } });
            CompanyFsk.Clone(new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Inn = "26658", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti });
            CompanyMrsk.Clone(new Company { FullName = "Межрегиональные распределительные сети", Inn = "23358", ShortName = "МРСК", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti });
            CompanyEnel.Clone(new Company { FullName = "Энел", ShortName = "Энел", Inn = "25658", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityGeneration } });
        }


        private void GenerateProjectTypess()
        {
            ProjectTypeReconstruction.Clone(new ProjectType { Name = "Реконструкция" });
        }

        private void GenerateProjects()
        {
            ProjectSubstation.Clone(new Project { Name = "Реконструкция ПС Первая", Manager = UserIvanov, ProjectType = ProjectTypeReconstruction, Notes = new List<Note> { Note1, Note2 } });
            ProjectStation.Clone(new Project { Name = "Строительство Свердловской ТЭЦ", Manager = UserIvanov, ProjectType = ProjectTypeReconstruction, Notes = new List<Note> { Note3, Note4 } });
        }

        private void GenerateFacilityTypes()
        {
            FacilityTypeStation.Clone(new FacilityType { FullName = "Теплоэлектроцентраль", ShortName = "ТЭЦ" });
            FacilityTypeSubStation.Clone(new FacilityType { FullName = "Понизительная станция", ShortName = "ПС" });
        }

        private void GenerateFacilities()
        {
            FacilitySubstation.Clone(new Facility { Name = "Первая", Type = FacilityTypeSubStation, OwnerCompany = CompanyMrsk, Address = AddressSubstation });
            FacilityStation.Clone(new Facility { Name = "Свердловская", Type = FacilityTypeStation, OwnerCompany = CompanyEnel, Address = AddressStation });
        }

        private void GenerateMeasures()
        {
            MeasureKv.Clone(new Measure { FullName = "Киловольт", ShortName = "кВ" });
        }


        private void GeneratePaymentDocuments()
        {
            PaymentDocument1.Clone(new PaymentDocument { Number = "№1", Payments = new List<PaymentActual> { PaymentActual1, PaymentActual2, PaymentActual3, PaymentActual4, PaymentActual5 } });
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

        private void GenerateSalesUnits()
        {
            SalesUnitVeb1101Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductVeb110, Order = OrderVeb110, SerialNumber = "1", ProductionTerm = 90, AssembleTerm = 7, Cost = 3000000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50, Address = AddressSubstation, Facility = FacilityStation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 2 } }, PaymentsActual = new List<PaymentActual> { PaymentActual1 }, PaymentsPlanned = new List<PaymentPlanned> { PaymentPlanned1, PaymentPlanned2, PaymentPlanned3, PaymentPlanned4, PaymentPlanned5 } });
            SalesUnitVeb1102Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductVeb110, Order = OrderVeb110, SerialNumber = "2", ProductionTerm = 90, AssembleTerm = 7, Cost = 3000000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50, Address = AddressSubstation, Facility = FacilityStation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 3 } }, PaymentsActual = new List<PaymentActual> { PaymentActual2 } });

            SalesUnitZng1101Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductZng110, Order = OrderZng110, SerialNumber = "5", ProductionTerm = 90, AssembleTerm = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50, Address = AddressSubstation, Facility = FacilityStation, PaymentsActual = new List<PaymentActual> { PaymentActual3 } });
            SalesUnitZng1102Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductZng110, Order = OrderZng110, SerialNumber = "6", ProductionTerm = 90, AssembleTerm = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50, Address = AddressSubstation, Facility = FacilityStation, PaymentsActual = new List<PaymentActual> { PaymentActual4 } });
            SalesUnitZng1103Full.Clone(new SalesUnit { Project = ProjectStation, Product = ProductZng110, Order = OrderZng110, SerialNumber = "7", ProductionTerm = 90, AssembleTerm = 7, Cost = 450000, Specification = SpecificationMrsk1, PaymentConditionSet = PaymentConditionSet50Na50, Address = AddressSubstation, Facility = FacilityStation, PaymentsActual = new List<PaymentActual> { PaymentActual5 } });

            SalesUnitVeb1101.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductVeb110, Cost = 3000000, Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation });
            SalesUnitVeb1102.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductVeb110, Cost = 3000000, Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation });

            SalesUnitZng1101.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000, Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation });
            SalesUnitZng1102.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000, Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation });
            SalesUnitZng1103.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000, Producer = CompanyUetm, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation });
            SalesUnitZng1104.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000, Facility = FacilitySubstation, PaymentConditionSet = PaymentConditionSet50Na50 });
            SalesUnitZng1105.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000, Facility = FacilitySubstation, PaymentConditionSet = PaymentConditionSet50Na50 });
            SalesUnitZng1106.Clone(new SalesUnit { Project = ProjectSubstation, Product = ProductZng110, Cost = 500000, Facility = FacilitySubstation, PaymentConditionSet = PaymentConditionSet50Na50 });

        }

        private void GenerateOfferUnits()
        {
            OfferUnitVeb1101.Clone(new OfferUnit { Offer = OfferMrsk, Product = ProductVeb110, Cost = 3100000, ProductionTerm = 120, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilityStation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 3 } } });
            OfferUnitVeb1102.Clone(new OfferUnit { Offer = OfferMrsk, Product = ProductVeb110, Cost = 3100000, ProductionTerm = 120, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilityStation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 3 } } });

            OfferUnitZng1101.Clone(new OfferUnit { Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 180, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 3 } } });
            OfferUnitZng1102.Clone(new OfferUnit { Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 180, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 2 } } });
            OfferUnitZng1103.Clone(new OfferUnit { Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 180, PaymentConditionSet = PaymentConditionSet50Na50, Facility = FacilitySubstation, ProductsIncluded = new List<ProductIncluded> { new ProductIncluded { Product = ProductZip1, Amount = 2 } } });

            OfferUnitZng1104.Clone(new OfferUnit { Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 150, PaymentConditionSet = PaymentConditionSet30Na70, Facility = FacilitySubstation });
            OfferUnitZng1105.Clone(new OfferUnit { Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 150, PaymentConditionSet = PaymentConditionSet30Na70, Facility = FacilitySubstation });
            OfferUnitZng1106.Clone(new OfferUnit { Offer = OfferMrsk, Product = ProductZng110, Cost = 550000, ProductionTerm = 150, PaymentConditionSet = PaymentConditionSet30Na70, Facility = FacilitySubstation });
        }

        private void GenerateOffers()
        {
            OfferMrsk.Clone(new Offer { Number = new DocumentNumber(), Vat = 18, Project = ProjectSubstation, ValidityDate = DateTime.Today.AddDays(60), Author = EmployeeIvanov, SenderEmployee = EmployeeIvanov, RecipientEmployee = EmployeeSidorov, CopyToRecipients = new List<Employee> { EmployeePetrov }, RegistrationDetailsOfRecipient = new DocumentsRegistrationDetails { Number = "12f455", Date = DateTime.Today.AddDays(-3) } });
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
            OrderVeb110.Clone(new Order { Number = "8012-17", DateOpen = DateTime.Today });
            OrderZng110.Clone(new Order { Number = "8111-15", DateOpen = DateTime.Today.AddDays(-50) });
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

            PaymentConditionSet50Na50.Clone(new PaymentConditionSet { PaymentConditions = new List<PaymentCondition> { PaymentConditionAvans50, PaymentConditionDoplata50 } });

            PaymentConditionAvans30.Clone(new PaymentCondition { Part = 0.3, DaysToPoint = 2, PaymentConditionPoint = PaymentConditionPoint.ProductionStart });
            PaymentConditionPostoplata70.Clone(new PaymentCondition { Part = 0.7, DaysToPoint = 1, PaymentConditionPoint = PaymentConditionPoint.Delivery });

            PaymentConditionSet30Na70.Clone(new PaymentConditionSet { PaymentConditions = new List<PaymentCondition> { PaymentConditionAvans30, PaymentConditionPostoplata70 } });
        }

        private void GenerateCommonOption()
        {
            CommonOption.Clone(new CommonOption { OurCompany = CompanyUetm, StandartPaymentsConditionSet = PaymentConditionSet50Na50, NewProductParameter = ParameterNewProduct, NewProductParameterGroup = ParameterGroupNewProductDesignation });
        }

        private void GenerateNotes()
        {
            Note1.Clone(new Note { Date = DateTime.Today.AddDays(-10), Text = "Заметка 1" });
            Note2.Clone(new Note { Date = DateTime.Today.AddDays(-20), Text = "Заметка 2" });
            Note3.Clone(new Note { Date = DateTime.Today.AddDays(-30), Text = "Заметка 3" });
            Note4.Clone(new Note { Date = DateTime.Today.AddDays(-40), Text = "Заметка 4" });
        }

    }
}