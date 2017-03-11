using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model;
using HVTApp.Services.StringToGuidService;

namespace HVTApp.DataAccess
{
    public class HVTAppDataBaseInitializer : DropCreateDatabaseIfModelChanges<HVTAppContext>
    {
        private static readonly CompanyForm FormAo = new CompanyForm { FullName = "����������� ��������", ShortName = "��" };
        private static readonly CompanyForm FormPao = new CompanyForm {FullName = "��������� ����������� ��������", ShortName = "���"};
        private static readonly CompanyForm FormOao = new CompanyForm {FullName = "�������� ����������� ��������", ShortName = "���"};
        private static readonly CompanyForm FormZao = new CompanyForm {FullName = "�������� ����������� ��������", ShortName = "���"};

        private static readonly Country Country = new Country {Name = "������"};
        private static readonly District District = new District {Country = Country, Name = "��������� ����������� �����"};
        private static readonly DistrictsRegion DistrictsRegion = new DistrictsRegion {District = District, Name = "������������ �������"};
        private static readonly LocalityType LocalityType = new LocalityType {FullName = "�����", ShortName = "�."};
        private static readonly Locality Locality = new Locality {DistrictsRegion = DistrictsRegion, LocalityType = LocalityType, Name = "������������"};
        private static readonly Address Address = new Address {Description = "��.��������� ������, �.22", Locality = Locality};
        private static readonly ActivityFild ActivityFild = new ActivityFild { FieldOfActivity = FieldOfActivity.ProducerOfHighVoltageEquipment };
        private static readonly BankDetails BankDetails = new BankDetails() {BankIdentificationCode = "1111"};
        private static readonly Company Company = new Company { FullName = "�����������������", ShortName = "����", Form = FormAo, Address = Address, BankDetails = BankDetails, ActivityFilds = new List<ActivityFild> { ActivityFild } };
        private static readonly EmployeesPosition EmployeesPosition = new EmployeesPosition {Name = "��������"};
        private static readonly Employee Employee = new Employee {Surname = "������",Name = "����", Patronymic = "��������", Position = EmployeesPosition, Company = Company, Email = "iii@mail.ru", PhoneNumber = "326-36-36"};
        private static readonly User User = new User {Login = "1",Password = StringToGuidService.GetHashString("1"),Employee = Employee, PersonalNumber = "333"};
        private static readonly UserRole UserRole = new UserRole { User = User, Role = Role.DataBaseFiller };

        protected override void Seed(HVTAppContext context)
        {
            context.CompanyForms.AddRange(new List<CompanyForm> { FormAo, FormPao, FormOao, FormZao });
            context.Companies.Add(Company);
            context.Employees.Add(Employee);
            context.Users.Add(User);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}