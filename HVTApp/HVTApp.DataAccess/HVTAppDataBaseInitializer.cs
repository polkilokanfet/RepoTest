using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model;
using HVTApp.Services.StringToGuidService;

namespace HVTApp.DataAccess
{
    public class HVTAppDataBaseInitializer : DropCreateDatabaseIfModelChanges<HVTAppContext>
    {
        #region CompanyForm
        private static readonly CompanyForm FormAo = new CompanyForm { FullName = "Акционерное общество", ShortName = "АО" };
        private static readonly CompanyForm FormPao = new CompanyForm {FullName = "Публичное акционерное общество", ShortName = "ПАО"};
        private static readonly CompanyForm FormOao = new CompanyForm {FullName = "Открытое акционерное общество", ShortName = "ОАО"};
        private static readonly CompanyForm FormZao = new CompanyForm {FullName = "Закрытое акционерное общество", ShortName = "ЗАО"};
        #endregion

        #region ActivityField
        private static readonly ActivityField ProducerOfHvt = new ActivityField { FieldOfActivity = FieldOfActivity.ProducerOfHighVoltageEquipment, Name = "Производитель ВВА"};
        private static readonly ActivityField Builder = new ActivityField { FieldOfActivity = FieldOfActivity.Builder, Name = "Подрядчик"};
        private static readonly ActivityField ElectricityTransmission = new ActivityField { FieldOfActivity = FieldOfActivity.ElectricityTransmission, Name = "Передача электроэнергии" };
        private static readonly ActivityField ElectricityGenerator = new ActivityField { FieldOfActivity = FieldOfActivity.ElectricityGeneration, Name = "Генерация электроэнергии" };
        #endregion

        private static readonly Country Country = new Country {Name = "Россия"};
        private static readonly District DistrictCFO = new District { Country = Country, Name = "Центральный федеральный округ" };
        private static readonly District DistrictUrFO = new District { Country = Country, Name = "Уральский федеральный округ" };
        private static readonly Region Region = new Region {District = DistrictUrFO, Name = "Свердловская область"};
        private static readonly LocalityType LocalityType = new LocalityType {FullName = "Город", ShortName = "г."};
        private static readonly Locality Ekb = new Locality {Region = Region, LocalityType = LocalityType, Name = "Екатеринбург"};
        private static readonly Address Address = new Address {Description = "ул.Фронтовых бригад, д.22", Locality = Ekb};
        private static readonly BankDetails BankDetails = new BankDetails {BankIdentificationCode = "1111"};
        private static readonly Company Uetm = new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Form = FormAo, Address = Address, BankDetails = BankDetails, ActivityFilds = new List<ActivityField> { ProducerOfHvt } };
        private static readonly Company Rosseti = new Company { FullName = "Россети", ShortName = "Россети", Form = FormPao, ActivityFilds = new List<ActivityField> { ElectricityTransmission } };
        private static readonly Company Fsk = new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Form = FormPao, ActivityFilds = new List<ActivityField> { ElectricityTransmission }, ParentCompany = Rosseti };
        private static readonly Company Mrsk = new Company { FullName = "Межрегиональные распределительные сети", ShortName = "МРСК", Form = FormPao, ActivityFilds = new List<ActivityField> { ElectricityTransmission }, ParentCompany = Rosseti };
        private static readonly EmployeesPosition EmployeesPosition = new EmployeesPosition {Name = "Директор"};
        private static readonly Person Person = new Person { Surname = "Иванов", Name = "Иван" };
        private static readonly Employee Employee = new Employee { Person = Person, Position = EmployeesPosition, Company = Uetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36" };
        private static readonly UserRole UserRole = new UserRole { Role = Role.DataBaseFiller };
        private static readonly User User = new User {Login = "1", Password = StringToGuidService.GetHashString("1"), Employee = Employee, PersonalNumber = "333", Roles=new List<UserRole> {UserRole} };

        private static readonly Project Project = new Project {Name = "TestProject", Manager = User, EstimatedDate = DateTime.Today.AddDays(120)};

        private static readonly FacilityType FacilityType = new FacilityType { FullName = "Понизительная станция", ShortName = "ПС"};
        private static readonly Facility Facility = new Facility {Name = "Тестовая", Type = FacilityType, OwnerCompany = Mrsk};

        private static readonly ParameterGroup GroupEqType = new ParameterGroup { Name = "Тип оборудования" };
        private static readonly Parameter ParamBreaker = new Parameter { Group = GroupEqType, Value = "Выключатель" };
        private static readonly Parameter ParamTransformator = new Parameter { Group = GroupEqType, Value = "Трансформатор" };

        private static readonly RequiredParentParameters Set1 = new RequiredParentParameters { Parameters = new List<Parameter> { ParamBreaker } };

        private static readonly ParameterGroup GroupBreakerType = new ParameterGroup { Name = "Тип выключателя" };
        private static readonly Parameter ParamBreakerDT = new Parameter { Group = GroupBreakerType, Value = "Баковый", RequiredParentParametersList = new List<RequiredParentParameters> {Set1} };
        private static readonly Parameter ParamBreakerLT = new Parameter { Group = GroupBreakerType, Value = "Колонковый" };

        private static readonly Product Product = new Product {Parameters = new List<Parameter> {ParamBreaker, ParamBreakerDT}, Prices = new List<SumOnDate> {new SumOnDate {Sum = 100, Date = DateTime.Today} } };
        private static readonly SalesUnit SalesUnit = new SalesUnit
        {
            ProductionUnit = new ProductionUnit {Product = Product, OrderPosition = 1, SerialNumber = "1234"},
            ShipmentUnit = new ShipmentUnit {ShipmentCost = 100},
            CostSingle = new SumAndVat { Sum = 1000, Vat = 18},
            Facility = Facility, Project = Project
        };

        protected override void Seed(HVTAppContext context)
        {
            context.ActivityFilds.AddRange(new[] {ProducerOfHvt, Builder, ElectricityTransmission, ElectricityGenerator});
            context.CompanyForms.AddRange(new[] { FormAo, FormPao, FormOao, FormZao });
            context.Companies.AddRange(new[] {Uetm, Rosseti, Fsk, Mrsk});
            context.Employees.Add(Employee);
            context.Users.Add(User);
            Project.SalesUnits.AddRange(new[] { SalesUnit, SalesUnit, SalesUnit});
            context.Projects.Add(Project);
            context.Parameters.AddRange(new[] {ParamBreaker, ParamBreakerDT, ParamBreakerLT, ParamTransformator});

            context.SaveChanges();
            base.Seed(context);
        }
    }
}