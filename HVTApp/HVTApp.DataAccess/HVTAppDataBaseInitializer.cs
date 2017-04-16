using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model;
using HVTApp.Services.StringToGuidService;

namespace HVTApp.DataAccess
{
    public class HVTAppDataBaseInitializer : DropCreateDatabaseIfModelChanges<HVTAppContext>
    {
        private static readonly CompanyForm FormAo = new CompanyForm { FullName = "Акционерное общество", ShortName = "АО" };
        private static readonly CompanyForm FormPao = new CompanyForm {FullName = "Публичное акционерное общество", ShortName = "ПАО"};
        private static readonly CompanyForm FormOao = new CompanyForm {FullName = "Открытое акционерное общество", ShortName = "ОАО"};
        private static readonly CompanyForm FormZao = new CompanyForm {FullName = "Закрытое акционерное общество", ShortName = "ЗАО"};

        private static readonly Country Country = new Country {Name = "Россия"};
        private static readonly District DistrictCFO = new District { Country = Country, Name = "Центральный федеральный округ" };
        private static readonly District DistrictUrFO = new District { Country = Country, Name = "Уральский федеральный округ" };
        private static readonly Region Region = new Region {District = DistrictUrFO, Name = "Свердловская область"};
        private static readonly LocalityType LocalityType = new LocalityType {FullName = "Город", ShortName = "г."};
        private static readonly Locality Ekb = new Locality {Region = Region, LocalityType = LocalityType, Name = "Екатеринбург"};
        private static readonly Address Address = new Address {Description = "ул.Фронтовых бригад, д.22", Locality = Ekb};
        private static readonly ActivityField ProducerOfHvt = new ActivityField { FieldOfActivity = FieldOfActivity.ProducerOfHighVoltageEquipment, Name = "Производитель ВВА"};
        private static readonly ActivityField Builder = new ActivityField { FieldOfActivity = FieldOfActivity.Builder, Name = "Подрядчик"};
        private static readonly ActivityField ElectricityTransmission = new ActivityField { FieldOfActivity = FieldOfActivity.ElectricityTransmission, Name = "Передача электроэнергии" };
        private static readonly ActivityField ElectricityGenerator = new ActivityField { FieldOfActivity = FieldOfActivity.ElectricityGeneration, Name = "Генерация электроэнергии" };
        private static readonly BankDetails BankDetails = new BankDetails {BankIdentificationCode = "1111"};
        private static readonly Company Uetm = new Company { FullName = "Уралэлектротяжмаш", ShortName = "УЭТМ", Form = FormAo, Address = Address, BankDetails = BankDetails, ActivityFilds = new List<ActivityField> { ProducerOfHvt } };
        private static readonly Company Rosseti = new Company { FullName = "Россети", ShortName = "Россети", Form = FormPao, ActivityFilds = new List<ActivityField> { ElectricityTransmission } };
        private static readonly Company Fsk = new Company { FullName = "Федеральная сетевая компания", ShortName = "ФСК", Form = FormPao, ActivityFilds = new List<ActivityField> { ElectricityTransmission }, ParentCompany = Rosseti };
        private static readonly Company Mrsk = new Company { FullName = "Межрегиональные распределительные сети", ShortName = "МРСК", Form = FormPao, ActivityFilds = new List<ActivityField> { ElectricityTransmission }, ParentCompany = Rosseti };
        private static readonly EmployeesPosition EmployeesPosition = new EmployeesPosition {Name = "Директор"};
        private static readonly Person Person = new Person { Surname = "Иванов", Name = "Иван" };
        private static readonly Employee Employee = new Employee { Person = Person, Position = EmployeesPosition, Company = Uetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36" };
        private static readonly User User = new User {Login = "1",Password = StringToGuidService.GetHashString("1"),Employee = Employee, PersonalNumber = "333"};
        private static readonly UserRole UserRole = new UserRole { User = User, Role = Role.DataBaseFiller };

        protected override void Seed(HVTAppContext context)
        {
            context.ActivityFilds.AddRange(new[] {ProducerOfHvt, Builder, ElectricityTransmission, ElectricityGenerator});
            context.CompanyForms.AddRange(new [] { FormAo, FormPao, FormOao, FormZao });
            var companies = new List<Company> {Uetm, Rosseti, Fsk, Mrsk};
            companies.ForEach(x => x.ChildCompanies.AddRange(companies.Where(c => Equals(c.ParentCompany, x))));
            context.Companies.AddRange(companies);
            context.Employees.Add(Employee);
            context.Users.Add(User);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}