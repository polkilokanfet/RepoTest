using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.TestDataGenerator
{
    public partial class TestData
    {
        public Measure MeasureKv;

        public CompanyForm CompanyFormAo;
        public CompanyForm CompanyFormPao;
        public CompanyForm CompanyFormOao;
        public CompanyForm CompanyFormZao;
        public CompanyForm CompanyFormOoo;

        public Company CompanyUetm;
        public Company CompanyRosseti;
        public Company CompanyFsk;
        public Company CompanyMrsk;
        public Company CompanyEnel;
        public Company CompanyPmk;

        public GlobalProperties GlobalProperties;

        public BankDetails BankDetailsOfUetm;

        public ActivityField ActivityFieldProducerOfHvt;
        public ActivityField ActivityFieldBuilder;
        public ActivityField ActivityFieldElectricityTransmission;
        public ActivityField ActivityFieldElectricityGeneration;
        public ActivityField ActivityFieldElectricityDistribution;
        public ActivityField ActivityFieldIndustrialEnterprise;
        public ActivityField ActivityFieldRailWay;
        public ActivityField ActivityFieldFuel;
        public ActivityField ActivityFieldSupplier;

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

        public CountryUnion CountryUnionSng;

        public Address AddressUetm;
        public Address AddressStation;
        public Address AddressSubstation;

        public ProjectType ProjectTypeReconstruction;
        public ProjectType ProjectNewBuilder;

        public FacilityType FacilityTypeStation;
        public FacilityType FacilityTypeSubStation;

        public PaymentCondition PaymentConditionAvans50;
        public PaymentCondition PaymentConditionDoplata50;

        public PaymentCondition PaymentConditionAvans30;
        public PaymentCondition PaymentConditionPostoplata70;

        public PaymentConditionSet PaymentConditionSet50Na50;
        public PaymentConditionSet PaymentConditionSet30Na70;

        public TenderType TenderTypeProject;
        public TenderType TenderTypeWork;
        public TenderType TenderTypeSuply;

        private void GenerateTenderTypes()
        {
            TenderTypeProject.Clone(new TenderType { Name = "Проект", Type = TenderTypeEnum.ToProject });
            TenderTypeWork.Clone(new TenderType { Name = "Работы", Type = TenderTypeEnum.ToWork });
            TenderTypeSuply.Clone(new TenderType { Name = "Поставка", Type = TenderTypeEnum.ToSupply });
        }

        private void GenerateCompanyForms()
        {
            CompanyFormAo.Clone(new CompanyForm { FullName = "Акционерное общество", ShortName = "АО" });
            CompanyFormPao.Clone(new CompanyForm { FullName = "Публичное акционерное общество", ShortName = "ПАО" });
            CompanyFormOao.Clone(new CompanyForm { FullName = "Открытое акционерное общество", ShortName = "ОАО" });
            CompanyFormZao.Clone(new CompanyForm { FullName = "Закрытое акционерное общество", ShortName = "ЗАО" });
            CompanyFormOoo.Clone(new CompanyForm { FullName = "Общество с ограниченной ответственностью", ShortName = "ООО" });
        }

        private void GenerateActivityFields()
        {
            ActivityFieldElectricityGeneration.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityGeneration, Name = "Генерация электроэнергии" });
            ActivityFieldProducerOfHvt.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ProducerOfHighVoltageEquipment, Name = "Производитель ВВА" });
            ActivityFieldElectricityTransmission.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityTransmission, Name = "Передача электроэнергии" });
            ActivityFieldBuilder.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.Builder, Name = "Подрядчик" });
            ActivityFieldElectricityDistribution.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityDistribution, Name = "Распределение электроэнергии" });
            ActivityFieldIndustrialEnterprise.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.IndustrialEnterprise, Name = "Промышленное предприятие" });
            ActivityFieldRailWay.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.RailWay, Name = "Железная дорога" });
            ActivityFieldFuel.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.Fuel, Name = "Топливно-энергетический сектор" });
            ActivityFieldSupplier.Clone(new ActivityField { ActivityFieldEnum = ActivityFieldEnum.Supplier, Name = "Снабжающая организация" });
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

        private void GenerateCountryUnions()
        {
            CountryUnionSng.Clone(new CountryUnion {Name = "СНГ", Countries = new List<Country> {CountryRussia} });
        }

        private void GenerateAddresses()
        {
            AddressUetm.Clone(new Address { Description = "ул.Фронтовых бригад, д.22", Locality = LocalityEkaterinburg });
            AddressStation.Clone(new Address { Description = "ул.Станционная, д.5", Locality = LocalityEkaterinburg });
            AddressSubstation.Clone(new Address { Description = "ул.ПодСтанционная, д.25", Locality = LocalityMoscow });
        }

        private void GenerateBankDetails()
        {
            BankDetailsOfUetm.Clone(new BankDetails { BankName = "Объебанк", BankIdentificationCode = "1111", CorrespondentAccount = "213564", CheckingAccount = "444554" });
        }

        private void GenerateCompanies()
        {
            CompanyUetm.Clone(new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Inn = "6673197337", Form = CompanyFormAo, AddressLegal = AddressUetm, BankDetailsList = new List<BankDetails> { BankDetailsOfUetm }, ActivityFilds = new List<ActivityField> { ActivityFieldProducerOfHvt } });
            CompanyRosseti.Clone(new Company { FullName = "Россети", ShortName = "Россети", Inn = "23659", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission } });
            CompanyFsk.Clone(new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Inn = "26658", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti });
            CompanyMrsk.Clone(new Company { FullName = "Межрегиональные распределительные сети", Inn = "23358", ShortName = "МРСК", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityTransmission }, ParentCompany = CompanyRosseti });
            CompanyEnel.Clone(new Company { FullName = "Энел", ShortName = "Энел", Inn = "25658", Form = CompanyFormPao, ActivityFilds = new List<ActivityField> { ActivityFieldElectricityGeneration } });
            CompanyPmk.Clone(new Company { FullName = "ПМК Холдинг", ShortName = "ПМК Холдинг", Inn = "12348", Form = CompanyFormOoo, ActivityFilds = new List<ActivityField> { ActivityFieldSupplier } });
        }

        private void GenerateProjectTypess()
        {
            ProjectTypeReconstruction.Clone(new ProjectType { Name = "Реконструкция" });
            ProjectNewBuilder.Clone(new ProjectType { Name = "Новое строительство" });
        }

        private void GenerateFacilityTypes()
        {
            FacilityTypeStation.Clone(new FacilityType { FullName = "Теплоэлектроцентраль", ShortName = "ТЭЦ" });
            FacilityTypeSubStation.Clone(new FacilityType { FullName = "Подстанция", ShortName = "ПС" });
        }

        private void GenerateMeasures()
        {
            MeasureKv.Clone(new Measure { FullName = "Киловольт", ShortName = "кВ" });
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

        private void GenerateGlobalProperties()
        {
            GlobalProperties.Clone(new GlobalProperties
            {
                OurCompany = CompanyUetm,
                StandartPaymentsConditionSet = PaymentConditionSet50Na50,
                NewProductParameter = ParameterNewProduct,
                NewProductParameterGroup = ParameterGroupNewProductDesignation,
                VoltageGroup = ParameterGroupVoltage,
                ServiceParameter = ParameterService,
                SupervisionParameter = ParameterSupervision,
                SenderOfferEmployee = EmployeeDeev,
                HvtProducersActivityField = ActivityFieldProducerOfHvt,
                PaymentConditionSet = PaymentConditionSet50Na50
            });
        }

    }
}