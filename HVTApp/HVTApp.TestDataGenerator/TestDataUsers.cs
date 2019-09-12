using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.TestDataGenerator
{
    public partial class TestData
    {
        public EmployeesPosition EmployeesPositionDirector;
        public EmployeesPosition EmployeesPositionManager;

        public Person PersonIvanov;
        public Person PersonPetrov;
        public Person PersonSidorov;

        public Person PersonKosolapov;
        public Person PersonEmelyanov;
        public Person PersonRybin;
        public Person PersonGazizov;
        public Person PersonTekin;
        public Person PersonDeev;

        public User UserIvanov;
        public User UserPetrov;

        public User UserKosolapov;
        public User UserEmelyanov;
        public User UserRybin;
        public User UserGazizov;
        public User UserTekin;
        public User UserDeev;

        public Employee EmployeeIvanov;
        public Employee EmployeePetrov;
        public Employee EmployeeSidorov;

        public Employee EmployeeKosolapov;
        public Employee EmployeeEmelyanov;
        public Employee EmployeeRybin;
        public Employee EmployeeGazizov;
        public Employee EmployeeTekin;
        public Employee EmployeeDeev;

        public UserRole UserRoleDataBaseFiller;
        public UserRole UserRoleAdmin;
        public UserRole UserRoleSalesManager;
        public UserRole UserRoleEconomist;
        public UserRole UserRolePricer;
        public UserRole UserRoleDirector;
        public UserRole UserRolePlanMaker;
        public UserRole UserRoleConstructor;

        private void GenerateUserRoles()
        {
            UserRoleDataBaseFiller.Clone(new UserRole { Role = Role.DataBaseFiller, Name = "DataBaseFiller" });
            UserRoleAdmin.Clone(new UserRole { Role = Role.Admin, Name = "Администратор" });
            UserRoleSalesManager.Clone(new UserRole { Role = Role.SalesManager, Name = "Менеджер" });
            UserRoleEconomist.Clone(new UserRole { Role = Role.Economist, Name = "Экономист" });
            UserRolePricer.Clone(new UserRole { Role = Role.Pricer, Name = "Расчетчик" });
            UserRoleDirector.Clone(new UserRole { Role = Role.Director, Name = "Директор" });
            UserRolePlanMaker.Clone(new UserRole { Role = Role.PlanMaker, Name = "Плановик" });
            UserRoleConstructor.Clone(new UserRole { Role = Role.Constrictor, Name = "Конструктор" });
        }

        private void GenerateEmployeesPositions()
        {
            EmployeesPositionDirector.Clone(new EmployeesPosition { Name = "Директор" });
            EmployeesPositionManager.Clone(new EmployeesPosition { Name = "Менеджер" });
        }

        private void GeneratePersons()
        {
            PersonIvanov.Clone(new Person { Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", IsMan = true });
            PersonPetrov.Clone(new Person { Surname = "Петров", Name = "Петр", Patronymic = "Петрович", IsMan = true });
            PersonSidorov.Clone(new Person { Surname = "Сидоров", Name = "Сидор", Patronymic = "Сидорович", IsMan = true });

            PersonKosolapov.Clone(new Person { Surname = "Косолапов", Name = "Александр", Patronymic = "Геннадьевич", IsMan = true });
            PersonEmelyanov.Clone(new Person { Surname = "Емельянов", Name = "Тимофей", Patronymic = "Викторович", IsMan = true });
            PersonRybin.Clone(new Person { Surname = "Рыбин", Name = "Андрей", Patronymic = "Юрьевич", IsMan = true });
            PersonGazizov.Clone(new Person { Surname = "Газизов", Name = "Евгений", Patronymic = "Рафаильевич", IsMan = true });
            PersonTekin.Clone(new Person { Surname = "Текин", Name = "Вадим", Patronymic = "Владимирович", IsMan = true });
            PersonDeev.Clone(new Person { Surname = "Деев", Name = "Дмитрий", Patronymic = "Геннадьевич", IsMan = true });
        }

        private void GenerateEmployees()
        {
            EmployeeIvanov.Clone(new Employee { Person = PersonIvanov, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "iii@uetm.ru", PhoneNumber = "326-36-36" });
            EmployeePetrov.Clone(new Employee { Person = PersonPetrov, Position = EmployeesPositionDirector, Company = CompanyFsk, Email = "pii@uetm.ru", PhoneNumber = "326-36-37" });
            EmployeeSidorov.Clone(new Employee { Person = PersonSidorov, Position = EmployeesPositionDirector, Company = CompanyEnel, Email = "sii@uetm.ru", PhoneNumber = "326-36-38" });

            EmployeeKosolapov.Clone(new Employee { Person = PersonKosolapov, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "kos@uetm.ru", PhoneNumber = "326-36-36" });
            EmployeeEmelyanov.Clone(new Employee { Person = PersonEmelyanov, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "em@uetm.ru", PhoneNumber = "326-36-36" });
            EmployeeRybin.Clone(new Employee { Person = PersonRybin, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "rbn@uetm.ru", PhoneNumber = "326-36-36" });
            EmployeeGazizov.Clone(new Employee { Person = PersonGazizov, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "gaz@uetm.ru", PhoneNumber = "326-36-36" });
            EmployeeTekin.Clone(new Employee { Person = PersonTekin, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "tkn@uetm.ru", PhoneNumber = "326-36-36" });
            EmployeeDeev.Clone(new Employee { Person = PersonDeev, Position = EmployeesPositionDirector, Company = CompanyUetm, Email = "deev@uetm.ru", PhoneNumber = "326-36-36" });
        }

        private void GenerateUsers()
        {
            var pas1 = StringToGuid.GetHashString("1");
            var pas2 = StringToGuid.GetHashString("2");

            UserIvanov.Clone(new User { Login = "1", Password = pas1, Employee = EmployeeIvanov, PersonalNumber = "1", Roles = new List<UserRole> { UserRoleAdmin, UserRoleDataBaseFiller, UserRoleSalesManager, UserRolePlanMaker, UserRoleDirector, UserRoleEconomist, UserRolePricer } });
            UserPetrov.Clone(new User { Login = "2", Password = pas2, Employee = EmployeePetrov, PersonalNumber = "2", Roles = new List<UserRole> { UserRoleDataBaseFiller } });

            UserKosolapov.Clone(new User { Login = "kosolapov", Password = pas1, Employee = EmployeeKosolapov, PersonalNumber = "7412", Roles = new List<UserRole> { UserRoleAdmin, UserRoleDataBaseFiller, UserRoleSalesManager, UserRolePlanMaker, UserRoleDirector, UserRoleEconomist, UserRolePricer } });
            UserEmelyanov.Clone(new User { Login = "emelyanov", Password = pas1, Employee = EmployeeEmelyanov, PersonalNumber = "7406", Roles = new List<UserRole> { UserRoleSalesManager } });
            UserRybin.Clone(new User { Login = "rybin", Password = pas1, Employee = EmployeeRybin, PersonalNumber = "7403", Roles = new List<UserRole> { UserRoleSalesManager } });
            UserGazizov.Clone(new User { Login = "gazizov", Password = pas1, Employee = EmployeeRybin, PersonalNumber = "7410", Roles = new List<UserRole> { UserRoleSalesManager } });
            UserTekin.Clone(new User { Login = "tekin", Password = pas1, Employee = EmployeeRybin, PersonalNumber = "7478", Roles = new List<UserRole> { UserRoleSalesManager } });
            UserDeev.Clone(new User { Login = "deev", Password = pas1, Employee = EmployeeDeev, PersonalNumber = "74??", Roles = new List<UserRole> { UserRoleDirector } });
        }

    }
}