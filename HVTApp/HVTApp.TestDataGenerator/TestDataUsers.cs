﻿using System;
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

        public User UserIvanov;
        public User UserPetrov;

        public User UserKosolapov;
        public User UserEmelyanov;
        public User UserRybin;

        public Employee EmployeeIvanov;
        public Employee EmployeePetrov;
        public Employee EmployeeSidorov;

        public Employee EmployeeKosolapov;
        public Employee EmployeeEmelyanov;
        public Employee EmployeeRybin;

        public UserRole UserRoleDataBaseFiller;
        public UserRole UserRoleAdmin;
        public UserRole UserRoleSalesManager;
        public UserRole UserRoleEconomist;
        public UserRole UserRolePricer;
        public UserRole UserRoleDirector;
        public UserRole UserRolePlanMaker;

        private void GeneratePersons()
        {
            PersonIvanov.Clone(new Person { Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", IsMan = true });
            PersonPetrov.Clone(new Person { Surname = "Петров", Name = "Иван", Patronymic = "Иванович", IsMan = true });
            PersonSidorov.Clone(new Person { Surname = "Сидоров", Name = "Иван", Patronymic = "Иванович", IsMan = true });

            PersonKosolapov.Clone(new Person { Surname = "Косолапов", Name = "Александр", Patronymic = "Геннадьевич", IsMan = true });
            PersonEmelyanov.Clone(new Person { Surname = "Емельянов", Name = "Тимофей", Patronymic = "Викторович", IsMan = true });
            PersonRybin.Clone(new Person { Surname = "Рыбин", Name = "Андрей", Patronymic = "Юрьевич", IsMan = true });
        }

        private void GenerateEmployeesPositions()
        {
            EmployeesPositionDirector.Clone(new EmployeesPosition { Name = "Директор" });
            EmployeesPositionManager.Clone(new EmployeesPosition { Name = "Менеджер" });
        }

        private void GenerateEmployees()
        {
            EmployeeIvanov.Clone(new Employee { Person = PersonIvanov, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36" });
            EmployeePetrov.Clone(new Employee { Person = PersonPetrov, Position = EmployeesPositionDirector, Company = CompanyFsk, Email = "pii@mail.ru", PhoneNumber = "326-36-37" });
            EmployeeSidorov.Clone(new Employee { Person = PersonSidorov, Position = EmployeesPositionDirector, Company = CompanyEnel, Email = "sii@mail.ru", PhoneNumber = "326-36-38" });

            EmployeeKosolapov.Clone(new Employee { Person = PersonKosolapov, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36" });
            EmployeeEmelyanov.Clone(new Employee { Person = PersonEmelyanov, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36" });
            EmployeeRybin.Clone(new Employee { Person = PersonRybin, Position = EmployeesPositionManager, Company = CompanyUetm, Email = "iii@mail.ru", PhoneNumber = "326-36-36" });
        }

        private void GenerateUserRoles()
        {
            UserRoleDataBaseFiller.Clone(new UserRole { Role = Role.DataBaseFiller, Name = "DataBaseFiller" });
            UserRoleAdmin.Clone(new UserRole { Role = Role.Admin, Name = "Администратор" });
            UserRoleSalesManager.Clone(new UserRole { Role = Role.SalesManager, Name = "Менеджер" });
            UserRoleEconomist.Clone(new UserRole { Role = Role.Economist, Name = "Экономист" });
            UserRolePricer.Clone(new UserRole { Role = Role.Pricer, Name = "Расчетчик" });
            UserRoleDirector.Clone(new UserRole { Role = Role.Director, Name = "Директор" });
            UserRolePlanMaker.Clone(new UserRole { Role = Role.PlanMaker, Name = "Плановик" });
        }

        private void GenerateUsers()
        {
            UserIvanov.Clone(new User { Login = "1", Password = StringToGuid.GetHashString("1"), Employee = EmployeeIvanov, PersonalNumber = "1", Roles = new List<UserRole> { UserRoleAdmin, UserRoleDataBaseFiller, UserRoleSalesManager, UserRolePlanMaker, UserRoleDirector, UserRoleEconomist, UserRolePricer } });
            UserPetrov.Clone(new User { Login = "2", Password = StringToGuid.GetHashString("2"), Employee = EmployeePetrov, PersonalNumber = "2", Roles = new List<UserRole> { UserRoleDataBaseFiller } });

            UserKosolapov.Clone(new User { Login = "kosolapov", Password = StringToGuid.GetHashString("1"), Employee = EmployeeKosolapov, PersonalNumber = "7412", Roles = new List<UserRole> { UserRoleAdmin, UserRoleDataBaseFiller, UserRoleSalesManager, UserRolePlanMaker, UserRoleDirector, UserRoleEconomist, UserRolePricer } });
            UserEmelyanov.Clone(new User { Login = "emelyanov", Password = StringToGuid.GetHashString("1"), Employee = EmployeeEmelyanov, PersonalNumber = "74??", Roles = new List<UserRole> { UserRoleSalesManager } });
            UserRybin.Clone(new User { Login = "rybin", Password = StringToGuid.GetHashString("1"), Employee = EmployeeRybin, PersonalNumber = "74??", Roles = new List<UserRole> { UserRoleSalesManager } });
        }

    }
}