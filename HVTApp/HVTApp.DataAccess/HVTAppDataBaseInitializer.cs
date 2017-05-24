using System;
using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Model.POCOs;
using HVTApp.Services.StringToGuidService;

namespace HVTApp.DataAccess
{
    public class HVTAppDataBaseInitializer : DropCreateDatabaseIfModelChanges<HVTAppContext>
    {

        protected override void Seed(HVTAppContext context)
        {

            #region CompanyForm
            CompanyForm formAo = new CompanyForm {FullName = "Акционерное общество", ShortName = "АО"};
            CompanyForm formPao = new CompanyForm {FullName = "Публичное акционерное общество", ShortName = "ПАО"};
            CompanyForm formOao = new CompanyForm {FullName = "Открытое акционерное общество", ShortName = "ОАО"};
            CompanyForm formZao = new CompanyForm {FullName = "Закрытое акционерное общество", ShortName = "ЗАО"};
            #endregion

            #region ActivityField
            ActivityField producerOfHvt = new ActivityField { FieldOfActivity = FieldOfActivity.ProducerOfHighVoltageEquipment, Name = "Производитель ВВА"};
            ActivityField builder = new ActivityField { FieldOfActivity = FieldOfActivity.Builder, Name = "Подрядчик"};
            ActivityField electricityTransmission = new ActivityField { FieldOfActivity = FieldOfActivity.ElectricityTransmission, Name = "Передача электроэнергии" };
            ActivityField electricityGenerator = new ActivityField { FieldOfActivity = FieldOfActivity.ElectricityGeneration, Name = "Генерация электроэнергии" };
            #endregion

            Country country = new Country {Name = "Россия"};
            District districtCfo = new District { Country = country, Name = "Центральный федеральный округ" };
            District districtUrFo = new District { Country = country, Name = "Уральский федеральный округ" };
            Region region = new Region {District = districtUrFo, Name = "Свердловская область"};
            LocalityType localityType = new LocalityType {FullName = "Город", ShortName = "г."};
            Locality ekb = new Locality {Region = region, LocalityType = localityType, Name = "Екатеринбург"};
            Address address = new Address {Description = "ул.Фронтовых бригад, д.22", Locality = ekb};
            BankDetails bankDetails = new BankDetails {BankIdentificationCode = "1111"};
            Company uetm = new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Form = formAo, Address = address, BankDetails = bankDetails, ActivityFilds = new List<ActivityField> { producerOfHvt } };
            Company rosseti = new Company { FullName = "Россети", ShortName = "Россети", Form = formPao, ActivityFilds = new List<ActivityField> { electricityTransmission } };
            Company fsk = new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Form = formPao, ActivityFilds = new List<ActivityField> { electricityTransmission }, ParentCompany = rosseti };
            Company mrsk = new Company { FullName = "Межрегиональные распределительные сети", ShortName = "МРСК", Form = formPao, ActivityFilds = new List<ActivityField> { electricityTransmission }, ParentCompany = rosseti };
            EmployeesPosition employeesPosition = new EmployeesPosition {Name = "Директор"};
            Person person = new Person { Surname = "Иванов", Name = "Иван" };
            Employee employee = new Employee { Person = person, Position = employeesPosition, Company = uetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36" };
            UserRole userRole = new UserRole { Role = Role.DataBaseFiller };
            User user = new User {Login = "1", Password = StringToGuidService.GetHashString("1"), Employee = employee, PersonalNumber = "333", Roles=new List<UserRole> {userRole} };

            Project project = new Project {Name = "TestProject", Manager = user, EstimatedDate = DateTime.Today.AddDays(120)};

            FacilityType facilityType = new FacilityType { FullName = "Понизительная станция", ShortName = "ПС"};
            Facility facility = new Facility {Name = "Тестовая", Type = facilityType, OwnerCompany = mrsk};

            ParameterGroup groupEqType = new ParameterGroup { Name = "Тип оборудования" };
            Parameter paramBreaker = new Parameter { Group = groupEqType, Value = "Выключатель" };
            Parameter paramTransformator = new Parameter { Group = groupEqType, Value = "Трансформатор" };

            RequiredParameters set1 = new RequiredParameters { Parameters = new List<Parameter> { paramBreaker } };

            ParameterGroup groupBreakerType = new ParameterGroup { Name = "Тип выключателя" };
            Parameter paramBreakerDt = new Parameter { Group = groupBreakerType, Value = "Баковый", RequiredParents = new List<RequiredParameters> { set1 } };
            Parameter paramBreakerLt = new Parameter { Group = groupBreakerType, Value = "Колонковый", RequiredParents = new List<RequiredParameters> { set1 } };

            RequiredParameters set2 = new RequiredParameters { Parameters = new List<Parameter> { paramTransformator } };

            ParameterGroup groupTransformatorType = new ParameterGroup { Name = "Тип трансформатора" };
            Parameter paramTransformatorI = new Parameter { Group = groupTransformatorType, Value = "Тока", RequiredParents = new List<RequiredParameters> { set2 } };
            Parameter paramTransformatorV = new Parameter { Group = groupTransformatorType, Value = "Напряжения", RequiredParents = new List<RequiredParameters> { set2 } };

            RequiredParameters setBreakerDt = new RequiredParameters { Parameters = new List<Parameter> { paramBreaker, paramBreakerDt } };
            RequiredParameters setBreakerLt = new RequiredParameters { Parameters = new List<Parameter> { paramBreaker, paramBreakerLt } };

            RequiredParameters setTransformatorV = new RequiredParameters { Parameters = new List<Parameter> { paramTransformator, paramTransformatorV } };


            PhysicalQuantity voltage = new PhysicalQuantity {Name = "Напряжение"};
            Measure kV = new Measure {PhysicalQuantity = voltage, FullName = "килоВольт", ShortName = "кВ"};
            ParameterGroup groupV = new ParameterGroup {Name = "Номинальное напряжение", Measure = kV};
            Parameter paramV35kV = new Parameter { Group = groupV, Value = "35", RequiredParents = new List<RequiredParameters> { setBreakerDt, setBreakerLt } };
            Parameter paramV110kV = new Parameter { Group = groupV, Value = "110", RequiredParents = new List<RequiredParameters> { setBreakerDt, setBreakerLt, setTransformatorV } };
            Parameter paramV220kV = new Parameter { Group = groupV, Value = "220", RequiredParents = new List<RequiredParameters> { setBreakerDt, setBreakerLt, setTransformatorV } };
            Parameter paramV500kV = new Parameter { Group = groupV, Value = "500", RequiredParents = new List<RequiredParameters> { setBreakerLt } };

            Product ZNG110 = new Product { Designation = "ЗНГ-110", Parameters = new List<Parameter> { paramTransformator, paramTransformatorV, paramV110kV }, Prices = new List<SumOnDate> { new SumOnDate { Sum = 75, Date = DateTime.Today } } };
            Product Vgb35 = new Product { Designation = "ВГБ-35", Parameters = new List<Parameter> { paramBreaker, paramBreakerDt, paramV35kV }, Prices = new List<SumOnDate> { new SumOnDate { Sum = 50, Date = DateTime.Today } } };
            Product Veb110 = new Product { Designation = "ВЭБ-110", Parameters = new List<Parameter> { paramBreaker, paramBreakerDt, paramV110kV }, Prices = new List<SumOnDate> { new SumOnDate { Sum = 100, Date = DateTime.Today } } };
            SalesUnit salesUnit = new SalesUnit
            {
                ProductionUnit = new ProductionUnit { Product = Veb110, OrderPosition = 1, SerialNumber = "1234" },
                ShipmentUnit = new ShipmentUnit { ShipmentCost = 100 },
                CostSingle = new SumAndVat { Sum = 1000, Vat = 18},
                Facility = facility,
                Project = project
            };


            context.ActivityFilds.AddRange(new[] {producerOfHvt, builder, electricityTransmission, electricityGenerator});
            context.CompanyForms.AddRange(new[] { formAo, formPao, formOao, formZao });
            context.Companies.AddRange(new[] {uetm, rosseti, fsk, mrsk});
            context.Employees.Add(employee);
            context.Users.Add(user);
            context.Products.AddRange(new [] {Veb110, Vgb35, ZNG110});
            project.SalesUnits.AddRange(new[] { salesUnit, salesUnit, salesUnit});
            context.Projects.Add(project);
            context.Parameters.AddRange(new[] { paramBreaker, paramTransformator, paramBreakerDt, paramBreakerLt, paramTransformatorI, paramTransformatorV, paramV35kV, paramV110kV, paramV220kV, paramV500kV });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}