using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoFixture.AutoEF;
using HVTApp.Model.POCOs;
using HVTApp.Services.StringToGuidService;
using Ploeh.AutoFixture;

namespace HVTApp.DataAccess
{
    public class HVTAppDataBaseInitializer : DropCreateDatabaseIfModelChanges<HVTAppContext>
    {
        protected override void Seed(HVTAppContext context)
        {
            var fixture = new Fixture();
            fixture.Customize(new EntityCustomization(new DbContextEntityTypesProvider(typeof(HVTAppContext))));

            //отключаем поведение - бросать ошибку при обнаружении циклической связи
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            //подключаем поведение - останавливаться на стандартной глубине рекурсии
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));


            var companyForm = fixture.Create<CompanyForm>();

            context.CompanyForms.Add(companyForm);

            base.Seed(context);
        }

        //protected override void Seed(HVTAppContext context)
        //{

        //    #region CompanyForm
        //    CompanyForm formAo = new CompanyForm {FullName = "Акционерное общество", ShortName = "АО"};
        //    CompanyForm formPao = new CompanyForm {FullName = "Публичное акционерное общество", ShortName = "ПАО"};
        //    CompanyForm formOao = new CompanyForm {FullName = "Открытое акционерное общество", ShortName = "ОАО"};
        //    CompanyForm formZao = new CompanyForm {FullName = "Закрытое акционерное общество", ShortName = "ЗАО"};
        //    #endregion

        //    #region ActivityField
        //    ActivityField producerOfHvt = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ProducerOfHighVoltageEquipment, Name = "Производитель ВВА"};
        //    ActivityField builder = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.Builder, Name = "Подрядчик"};
        //    ActivityField electricityTransmission = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityTransmission, Name = "Передача электроэнергии" };
        //    ActivityField electricityGeneration = new ActivityField { ActivityFieldEnum = ActivityFieldEnum.ElectricityGeneration, Name = "Генерация электроэнергии" };
        //    #endregion

        //    Country country = new Country {Name = "Россия"};
        //    District districtCfo = new District { Country = country, Name = "Центральный федеральный округ" };
        //    District districtUrFo = new District { Country = country, Name = "Уральский федеральный округ" };
        //    Region region = new Region {District = districtUrFo, Name = "Свердловская область"};
        //    LocalityType localityType = new LocalityType {FullName = "Город", ShortName = "г."};
        //    Locality ekb = new Locality {Region = region, LocalityType = localityType, Name = "Екатеринбург"};
        //    Address address = new Address {Description = "ул.Фронтовых бригад, д.22", Locality = ekb};
        //    BankDetails bankDetails = new BankDetails {BankIdentificationCode = "1111"};
        //    Company uetm = new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Form = formAo, AddressLegal = address, BankDetailsList = new List<BankDetails> {bankDetails}, ActivityFilds = new List<ActivityField> { producerOfHvt } };
        //    Company rosseti = new Company { FullName = "Россети", ShortName = "Россети", Form = formPao, ActivityFilds = new List<ActivityField> { electricityTransmission } };
        //    Company fsk = new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Form = formPao, ActivityFilds = new List<ActivityField> { electricityTransmission }, ParentCompany = rosseti };
        //    Company mrsk = new Company { FullName = "Межрегиональные распределительные сети", ShortName = "МРСК", Form = formPao, ActivityFilds = new List<ActivityField> { electricityTransmission }, ParentCompany = rosseti };
        //    Company enel = new Company { FullName = "Энел", ShortName = "Энел", Form = formPao, ActivityFilds = new List<ActivityField> { electricityGeneration }, ParentCompany = rosseti };
        //    EmployeesPosition employeesPosition = new EmployeesPosition {Name = "Директор"};
        //    Person person = new Person { Surname = "Иванов", Name = "Иван" };
        //    Employee employee = new Employee { Person = person, Position = employeesPosition, Company = uetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36" };
        //    UserRole userRole = new UserRole { Role = Role.DataBaseFiller };
        //    User user = new User {Login = "1", Password = StringToGuidService.GetHashString("1"), Employee = employee, PersonalNumber = "333", Roles=new List<UserRole> {userRole} };

        //    Project project1 = new Project { Name = "Реконструкция ПС Первая", Manager = user };
        //    Project project2 = new Project { Name = "Строительство Свердловской ТЭЦ", Manager = user };

        //    FacilityType facilityTypeTec = new FacilityType { FullName = "Теплоэлектроцентраль", ShortName = "ТЭЦ" };
        //    FacilityType facilityTypePc = new FacilityType { FullName = "Понизительная станция", ShortName = "ПС" };
        //    Facility substationPervaya = new Facility { Name = "Первая", Type = facilityTypePc, OwnerCompany = mrsk };
        //    Facility stationSverdlovskaya = new Facility { Name = "Свердловская", Type = facilityTypeTec, OwnerCompany = enel };


        //    ParameterGroup groupEqType = new ParameterGroup { Name = "Тип оборудования" };
        //    Parameter paramBreaker = new Parameter { Group = groupEqType, Value = "Выключатель" };
        //    Parameter paramTransformator = new Parameter { Group = groupEqType, Value = "Трансформатор" };
        //    Parameter paramPrivod = new Parameter { Group = groupEqType, Value = "Привод" };
        //    Parameter paramBreakerBlock = new Parameter { Group = groupEqType, Value = "Блок выключателя" };

        //    RequiredPreviousParameters set1 = new RequiredPreviousParameters { RequiredParameters = new List<Parameter> { paramBreaker } };

        //    ParameterGroup groupBreakerType = new ParameterGroup { Name = "Тип выключателя" };
        //    Parameter paramBreakerDt = new Parameter { Group = groupBreakerType, Value = "Баковый", RequiredPreviousParameters = new List<RequiredPreviousParameters> { set1 } };
        //    Parameter paramBreakerLt = new Parameter { Group = groupBreakerType, Value = "Колонковый", RequiredPreviousParameters = new List<RequiredPreviousParameters> { set1 } };

        //    RequiredPreviousParameters set2 = new RequiredPreviousParameters { RequiredParameters = new List<Parameter> { paramTransformator } };

        //    ParameterGroup groupTransformatorType = new ParameterGroup { Name = "Тип трансформатора" };
        //    Parameter paramTransformatorI = new Parameter { Group = groupTransformatorType, Value = "Тока", RequiredPreviousParameters = new List<RequiredPreviousParameters> { set2 } };
        //    Parameter paramTransformatorV = new Parameter { Group = groupTransformatorType, Value = "Напряжения", RequiredPreviousParameters = new List<RequiredPreviousParameters> { set2 } };

        //    RequiredPreviousParameters setBreakerDt = new RequiredPreviousParameters { RequiredParameters = new List<Parameter> { paramBreaker, paramBreakerDt } };
        //    RequiredPreviousParameters setBreakerLt = new RequiredPreviousParameters { RequiredParameters = new List<Parameter> { paramBreaker, paramBreakerLt } };

        //    RequiredPreviousParameters setTransformatorV = new RequiredPreviousParameters { RequiredParameters = new List<Parameter> { paramTransformator, paramTransformatorV } };

        //    RequiredChildProductParameters requiredChildProductParametersPrivod = new RequiredChildProductParameters { MainProductParameters = new List<Parameter> { paramBreaker }, ChildProductParameters = new List<Parameter> { paramPrivod }, Count = 1 };
        //    RequiredChildProductParameters requiredChildProductParametersBreakerBlock = new RequiredChildProductParameters { MainProductParameters = new List<Parameter> { paramBreakerBlock }, ChildProductParameters = new List<Parameter> { paramBreaker }, Count = 2 };


        //    Measure kV = new Measure {FullName = "Киловольт", ShortName = "кВ"};
        //    ParameterGroup groupV = new ParameterGroup {Name = "Номинальное напряжение", Measure = kV};
        //    Parameter paramV35kV = new Parameter { Group = groupV, Value = "35", RequiredPreviousParameters = new List<RequiredPreviousParameters> { setBreakerDt, setBreakerLt } };
        //    Parameter paramV110kV = new Parameter { Group = groupV, Value = "110", RequiredPreviousParameters = new List<RequiredPreviousParameters> { setBreakerDt, setBreakerLt, setTransformatorV } };
        //    Parameter paramV220kV = new Parameter { Group = groupV, Value = "220", RequiredPreviousParameters = new List<RequiredPreviousParameters> { setBreakerDt, setBreakerLt, setTransformatorV } };
        //    Parameter paramV500kV = new Parameter { Group = groupV, Value = "500", RequiredPreviousParameters = new List<RequiredPreviousParameters> { setBreakerLt } };

        //    ProductItem zng110ProductItem = new ProductItem { Designation = "ЗНГ-110", Parameters = new List<Parameter> { paramTransformator, paramTransformatorV, paramV110kV }, Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost {Sum = 75}, Date = DateTime.Today } } };
        //    ProductItem vgb35ProductItem = new ProductItem { Designation = "ВГБ-35", Parameters = new List<Parameter> { paramBreaker, paramBreakerDt, paramV35kV }, Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 50 }, Date = DateTime.Today } } };
        //    ProductItem veb110ProductItem = new ProductItem { Designation = "ВЭБ-110", Parameters = new List<Parameter> { paramBreaker, paramBreakerDt, paramV110kV }, Prices = new List<CostOnDate> { new CostOnDate { Cost = new Cost { Sum = 100 }, Date = DateTime.Today } } };


        //    Product veb110Product = new Product {ProductItem = veb110ProductItem, Designation = "Выключатель баковый ВЭБ-110"};
        //    Product zng110Product = new Product {ProductItem = zng110ProductItem, Designation = "Трансформатор напряжения ЗНГ-110" };

        //    Contract contract = new Contract { Contragent = mrsk, Date = DateTime.Today, Number = "0401-17-0001"};
        //    Specification specification = new Specification { Contract = contract, Date = contract.Date, Number = "1" };

        //    //ProductComplexUnit productComplexUnitVeb110 = new ProductComplexUnit
        //    //{
        //    //    Facility = substationPervaya, Project = project1,

        //    //    Product = veb110Product, OrderPosition = 1, SerialNumber = "3651",
        //    //    ShipmentUnit = new ShipmentUnit { Sum = new Sum {Sum = 100}, Address = address},
        //    //    Specification = specification, Sum = new Sum { Sum = 1000} 
        //    //};

        //    //ProductComplexUnit productComplexUnitVeb1102 = new ProductComplexUnit
        //    //{
        //    //    Facility = stationSverdlovskaya, Project = project2,

        //    //    Product = veb110Product, OrderPosition = 1, SerialNumber = "3652",
        //    //    ShipmentUnit = new ShipmentUnit { Sum = new Sum { Sum = 100}, Address = address },
        //    //    Sum = new Sum { Sum = 1000 } 
        //    //};

        //    //ProductComplexUnit productComplexUnitZng1101 = new ProductComplexUnit
        //    //{
        //    //    Facility = substationPervaya, Project = project1, 

        //    //    Product = zng110Product, OrderPosition = 1, SerialNumber = "325",
        //    //    ShipmentUnit = new ShipmentUnit { Sum = new Sum { Sum = 150}, Address = address },
        //    //    Specification = specification, Sum = new Sum { Sum = 500 }
        //    //};

        //    //ProductComplexUnit productComplexUnitZng1102 = new ProductComplexUnit
        //    //{
        //    //    Facility = substationPervaya, Project = project1,

        //    //    Product = zng110Product, OrderPosition = 1, SerialNumber = "326",
        //    //    ShipmentUnit = new ShipmentUnit { Sum = new Sum { Sum = 150 }, Address = address },
        //    //    Specification = specification, Sum = new Sum { Sum = 500 }
        //    //};



        //    TenderType tenderType = new TenderType {Name = "Проектно-изыскательные работы", Type = TenderTypeEnum.ToProject};
        //    //Tender tender = new Tender { Type = tenderType, Project = project1, Sum = new Sum { Sum = 545 }, DateOpen = DateTime.Today,
        //    //    DateClose = DateTime.Today.AddDays(7),
        //    //    TenderUnits = new List<TenderUnit>(project1.ProductionUnits.Select(x => new TenderUnit {ProductComplexUnit = x}))};








        //    context.ActivityFilds.AddRange(new[] {producerOfHvt, builder, electricityTransmission, electricityGeneration});
        //    context.CompanyForms.AddRange(new[] { formAo, formPao, formOao, formZao });
        //    context.Companies.AddRange(new[] {uetm, rosseti, fsk, mrsk});
        //    context.Employees.Add(employee);
        //    context.Users.Add(user);
        //    context.RequiredProductsChildses.AddRange(new[] { requiredChildProductParametersPrivod, requiredChildProductParametersBreakerBlock });
        //    //context.Products.AddRange(new [] {veb110, vgb35, zng110});
        //    //project1.ProductionUnits.AddRange(new[] { productComplexUnitVeb110, productComplexUnitZng1101 });
        //    context.Facilities.AddRange(new[] {substationPervaya, stationSverdlovskaya});
        //    context.Projects.AddRange(new[] { project1, project2 });
        //    context.Parameters.AddRange(new[] { paramBreaker, paramTransformator, paramBreakerDt, paramBreakerLt, paramTransformatorI, paramTransformatorV, paramV35kV, paramV110kV, paramV220kV, paramV500kV });
        //    context.Specifications.Add(specification);
        //    //context.Tenders.Add(tender);
        //    //context.Units.AddRange(new[] {productComplexUnitVeb110, productComplexUnitVeb1102, productComplexUnitZng1101, productComplexUnitZng1102});

        //    context.SaveChanges();
        //    base.Seed(context);
        //}
    }
}