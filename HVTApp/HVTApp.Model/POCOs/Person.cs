﻿using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Person : BaseEntity
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public Gender Gender { get; set; }
        public Employee CurrentEmployee { get; set; } //сотрудник какой компании в настоящее время
        public List<Employee> Employees { get; set; }
    }

    public enum Gender
    {
        Man, Wooman
    }

}